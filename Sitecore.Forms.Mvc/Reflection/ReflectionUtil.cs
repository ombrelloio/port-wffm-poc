// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Reflection.ReflectionUtil
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Attributes;
using Sitecore.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Sitecore.Forms.Mvc.Reflection
{
  public static class ReflectionUtil
  {
    public static void SetXmlProperties(object obj, Dictionary<string, string> parameters)
    {
      Assert.ArgumentNotNull(obj, nameof (obj));
      if (parameters == null || parameters.Count == 0)
        return;
      foreach (KeyValuePair<string, string> parameter in parameters)
      {
        PropertyInfo propertyInfo = Sitecore.Reflection.ReflectionUtil.GetPropertyInfo(obj, parameter.Key);
        if (!(propertyInfo == (PropertyInfo) null) && !(propertyInfo.GetSetMethod(false) == (MethodInfo) null))
          ReflectionUtil.SetProperty(obj, propertyInfo.Name, (object) parameter.Value);
      }
      foreach (var data in ((IEnumerable<PropertyInfo>) obj.GetType().GetProperties()).Select(property => new
      {
        property = property,
        attr = property.GetCustomAttributes(typeof (ParameterNameAttribute)).SingleOrDefault<Attribute>() as ParameterNameAttribute
      }).Where(_param1 => _param1.attr != null).Select(_param1 => new
      {
        property = _param1.property,
        attr = _param1.attr
      }))
      {
        string lower = data.attr.Name.ToLower();
        if (parameters.ContainsKey(lower))
          ReflectionUtil.SetProperty(obj, data.property.Name, (object) parameters[lower]);
      }
    }

    public static bool SetProperty(object obj, string name, object value)
    {
      Assert.ArgumentNotNull(obj, nameof (obj));
      Assert.ArgumentNotNullOrEmpty(name, nameof (name));
      bool flag = true;
      try
      {
        PropertyInfo propertyInfo = Sitecore.Reflection.ReflectionUtil.GetPropertyInfo(obj, name);
        if (propertyInfo == (PropertyInfo) null)
          return false;
        ReflectionUtil.SetProperty(obj, propertyInfo, value);
      }
      catch (Exception ex)
      {
        flag = false;
      }
      return flag;
    }

    private static void SetProperty(object obj, PropertyInfo property, object value)
    {
      Error.AssertObject(obj, nameof (obj));
      Error.AssertObject((object) property, nameof (property));
      Assert.IsTrue((property.CanWrite || 0U > 0U ? 1 : 0) != 0, "Attempt to write to read-only property: {0}. Declaring type: {1}", new object[2]
      {
        (object) property.Name,
        (object) property.DeclaringType
      });
      if (value == null)
      {
        property.SetValue(obj, (object) null, (object[]) null);
      }
      else
      {
        value = ReflectionUtil.ConvertPropertyValue(property, value);
        property.SetValue(obj, value, (object[]) null);
      }
    }

    private static object ConvertPropertyValue(PropertyInfo property, object value)
    {
      if (property.PropertyType.IsEnum && value is string)
        return Enum.Parse(property.PropertyType, value as string, true);
      object[] customAttributes = property.GetCustomAttributes(typeof (TypeConverterAttribute), true);
      if (customAttributes != null && customAttributes.Length != 0 && customAttributes[0] is TypeConverterAttribute converterAttribute)
      {
        Type type = Type.GetType(converterAttribute.ConverterTypeName, false);
        if (type != (Type) null && Sitecore.Reflection.ReflectionUtil.CreateObject(type) is TypeConverter typeConverter2)
          return typeConverter2.ConvertFrom((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, value);
      }
      TypeConverter converter = TypeDescriptor.GetConverter(property.PropertyType);
      if (converter != null && converter.CanConvertFrom(value.GetType()))
        return converter.ConvertFrom((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, value);
      if (value is IConvertible)
        return System.Convert.ChangeType(value, property.PropertyType, (IFormatProvider) CultureInfo.InvariantCulture);
      if (property.PropertyType == typeof (string))
        return (object) System.Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture);
      MainUtil.Warn("Could not convert property value to correct type in ReflectionUtil.SetProperty. Property type: " + property.PropertyType.FullName + " (name: " + property.Name + "). Value type: " + value.GetType().FullName);
      return (object) null;
    }
  }
}
