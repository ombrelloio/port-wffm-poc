// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.SchemaGenerator.DesignAttributesUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Utility;
using Sitecore.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI;

namespace Sitecore.Form.Core.SchemaGenerator
{
  public class DesignAttributesUtil
  {
    internal const int INSTANCE_TYPE_ATTRIBUTE = 2;
    internal const int INSTANCE_TYPE_ELEMENT_INNER = 8;
    internal const int INSTANCE_TYPE_ELEMENT_INNER_DEFAULT = 64;
    internal const int INSTANCE_TYPE_EVENT = 4;
    internal const int INSTANCE_TYPE_FLATTENED = 16;
    internal const int INSTANCE_TYPE_NONE = 1;
    internal const int INSTANCE_TYPE_NOT_SPECIFIED = 0;
    internal const int INSTANCE_TYPE_TOP_LEVEL_ELEMENT = 32;
    private static Type controlType = typeof (Control);
    private static Type parserAccessorType = typeof (IParserAccessor);
    private static Dictionary<Type, bool> isCollection = new Dictionary<Type, bool>();

    public static Attribute GetCustomAttributeUsingReflection(
      MemberInfo mi,
      Type attributeType)
    {
      Attribute attribute = (Attribute) null;
      Attribute[] customAttributes = Attribute.GetCustomAttributes(mi, attributeType);
      if (customAttributes != null && customAttributes.Length != 0)
        attribute = customAttributes[0];
      return attribute;
    }

    public static Attribute GetCustomAttribute(MemberInfo mi, Type attributeType)
    {
      System.ComponentModel.AttributeCollection attributeCollection;
      if ((object) (mi as PropertyInfo) != null)
      {
        PropertyDescriptor property = TypeDescriptor.GetProperties(mi.DeclaringType)[mi.Name];
        attributeCollection = property == null ? TypeDescriptor.GetAttributes(((PropertyInfo) mi).PropertyType) : property.Attributes;
      }
      else if ((object) (mi as EventInfo) != null)
        attributeCollection = TypeDescriptor.GetEvents(mi.DeclaringType)[mi.Name].Attributes;
      else
        attributeCollection = (object) (mi as Type) != null ? TypeDescriptor.GetAttributes((Type) mi) : throw new ArgumentException("EventInfo is of unknown type");
      return attributeCollection[attributeType];
    }

    public static bool IsPersistChildren(object control) => DesignAttributesUtil.IsPersistChildren(control.GetType());

    public static bool IsDummy(object control) => (DummyAttribute) DesignAttributesUtil.GetCustomAttribute((MemberInfo) control.GetType(), typeof (DummyAttribute)) != null;

    public static bool IsPersistChildren(Type type)
    {
      PersistChildrenAttribute customAttribute = (PersistChildrenAttribute) DesignAttributesUtil.GetCustomAttribute((MemberInfo) type, typeof (PersistChildrenAttribute));
      return customAttribute != null && customAttribute.Persist;
    }

    public static bool IsParseChildren(object control) => DesignAttributesUtil.IsPersistChildren(control.GetType());

    public static bool IsParseChildren(Type type)
    {
      bool flag = false;
      ParseChildrenAttribute attributeUsingReflection = (ParseChildrenAttribute) DesignAttributesUtil.GetCustomAttributeUsingReflection((MemberInfo) type, typeof (ParseChildrenAttribute));
      if (attributeUsingReflection == null)
      {
        if (!DesignAttributesUtil.parserAccessorType.IsAssignableFrom(type))
          flag = true;
      }
      else if (attributeUsingReflection.ChildrenAsProperties)
        flag = true;
      else if (DesignAttributesUtil.controlType != attributeUsingReflection.ChildControlType)
        flag = true;
      return flag;
    }

    public static IDictionary<string, string> GetPropertyAttributes(
      object control,
      object target)
    {
      Assert.ArgumentNotNull(control, nameof (control));
      List<PropertyAttributeInstance> attributeInstanceList = DesignAttributesUtil.FillElementInstances(control.GetType(), true, 2);
      foreach (PropertyAttributeInstance attributeInstance in attributeInstanceList)
      {
        if (attributeInstance.Browsable)
        {
          try
          {
            attributeInstance.Value = PropertyAttributeInstance.ConvertToString(attributeInstance.TypeConverter, attributeInstance.PropertyInfo.GetValue(control, (object[]) null));
          }
          catch
          {
          }
        }
      }
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      foreach (PropertyAttributeInstance attributeInstance in attributeInstanceList)
      {
        if (attributeInstance.Value != attributeInstance.DefaultValue && (attributeInstance.DefaultValue != null || !string.IsNullOrEmpty(attributeInstance.Value)))
          dictionary.Add(attributeInstance.Name.ToLower(), attributeInstance.Value.Replace("\"", "&quot;"));
      }
      System.Web.UI.AttributeCollection attributesValue = DesignAttributesUtil.GetAttributesValue(control);
      if (attributesValue != null && attributesValue.Count > 0)
      {
        foreach (string key in (IEnumerable) attributesValue.Keys)
        {
          if (!dictionary.ContainsKey(key.ToLower()))
            dictionary.Add(key, attributesValue[key]);
        }
      }
      foreach (EventInfoInstance fillEventInstance in DesignAttributesUtil.FillEventInstances(control.GetType(), true))
      {
        if (!string.IsNullOrEmpty(fillEventInstance.Value) && fillEventInstance.Browseable && target.GetType().GetMethod(fillEventInstance.Name, BindingFlags.Instance | BindingFlags.NonPublic) != (MethodInfo) null)
          dictionary.Add(fillEventInstance.Name, fillEventInstance.Value);
      }
      return dictionary;
    }

    private static System.Web.UI.AttributeCollection GetAttributesValue(
      object control)
    {
      return (System.Web.UI.AttributeCollection) ReflectionUtil.GetProperty(control, "Attributes");
    }

    public static IDictionary<string, object> GetPropertiesInnerElement(object control)
    {
      Assert.ArgumentNotNull(control, nameof (control));
      Type type = control.GetType();
      return DesignAttributesUtil.GetInner(control, (IEnumerable<PropertyAttributeInstance>) DesignAttributesUtil.FillElementInstances(type, true, 64));
    }

    public static IDictionary<string, object> GetPropertiesInnerCollection(object control)
    {
      Assert.ArgumentNotNull(control, nameof (control));
      Type type = control.GetType();
      return DesignAttributesUtil.GetInner(control, (IEnumerable<PropertyAttributeInstance>) DesignAttributesUtil.FillElementInstances(type, true, 8));
    }

    private static IDictionary<string, object> GetInner(
      object control,
      IEnumerable<PropertyAttributeInstance> list)
    {
      IDictionary<string, object> dictionary = (IDictionary<string, object>) new Dictionary<string, object>();
      foreach (PropertyAttributeInstance attributeInstance in list)
      {
        try
        {
          dictionary.Add(attributeInstance.Name, attributeInstance.PropertyInfo.GetValue(control, (object[]) null));
        }
        catch
        {
        }
      }
      return dictionary;
    }

    public static IDictionary<string, string> GetPropertyAttributes(Type type)
    {
      List<PropertyAttributeInstance> attributeInstanceList = DesignAttributesUtil.FillElementInstances(type, true, 2);
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      foreach (PropertyAttributeInstance attributeInstance in attributeInstanceList)
        dictionary.Add(attributeInstance.Name, attributeInstance.DefaultValue);
      return dictionary;
    }

    private static void Create(PropertyInfo pi, ICollection<PropertyAttributeInstance> pail)
    {
      if ((DesignAttributesUtil.GetInstanceType(pi) & 2) == 0 || (!pi.CanWrite || !pi.CanWrite) && !DesignAttributesUtil.IsAttributeCollection(pi.PropertyType))
        return;
      TypeConverter tc = (TypeConverter) null;
      Type attributeType = pi.PropertyType;
      TypeConverterAttribute customAttribute = (TypeConverterAttribute) DesignAttributesUtil.GetCustomAttribute((MemberInfo) pi, typeof (TypeConverterAttribute));
      if (customAttribute != null && customAttribute.ConverterTypeName.Length > 0)
      {
        Type type = Type.GetType(customAttribute.ConverterTypeName);
        try
        {
          tc = (TypeConverter) Activator.CreateInstance(type);
          if (tc.GetStandardValuesSupported())
          {
            if (tc.GetStandardValues() != null)
              attributeType = type;
          }
        }
        catch
        {
        }
      }
      if (attributeType == pi.PropertyType)
        attributeType = typeof (string);
      PropertyAttributeInstance attributeInstance = new PropertyAttributeInstance(pi, attributeType, tc);
      pail.Add(attributeInstance);
    }

    private static void CreateInnerElement(
      PropertyInfo pi,
      ICollection<PropertyAttributeInstance> innerElements)
    {
      if ((DesignAttributesUtil.GetInstanceType(pi) & 64) == 0)
        return;
      PropertyAttributeInstance attributeInstance = new PropertyAttributeInstance(pi, pi.PropertyType, (TypeConverter) null);
      innerElements.Add(attributeInstance);
    }

    private static void CreateInnerCollection(
      PropertyInfo pi,
      ICollection<PropertyAttributeInstance> innerElements)
    {
      if ((DesignAttributesUtil.GetInstanceType(pi) & 8) == 0)
        return;
      PropertyAttributeInstance attributeInstance = new PropertyAttributeInstance(pi, pi.PropertyType, (TypeConverter) null);
      innerElements.Add(attributeInstance);
    }

    private static List<EventInfoInstance> FillEventInstances(
      Type type,
      bool usesInheritance)
    {
      List<EventInfoInstance> eventInfoInstanceList = new List<EventInfoInstance>();
      if (!DesignAttributesUtil.IsCollection(type))
      {
        foreach (EventInfo declaredEventInfo in ReflectionUtils.GetDeclaredEventInfos(type, usesInheritance))
          eventInfoInstanceList.Add(new EventInfoInstance(declaredEventInfo));
      }
      return eventInfoInstanceList;
    }

    private static List<PropertyAttributeInstance> FillElementInstances(
      Type type,
      bool usesInheritance,
      int mode)
    {
      List<PropertyAttributeInstance> attributeInstanceList = new List<PropertyAttributeInstance>();
      if (!DesignAttributesUtil.IsCollection(type))
      {
        foreach (PropertyInfo declaredPropInfo in ReflectionUtils.GetDeclaredPropInfos(type, usesInheritance))
        {
          switch (mode)
          {
            case 2:
              DesignAttributesUtil.Create(declaredPropInfo, (ICollection<PropertyAttributeInstance>) attributeInstanceList);
              break;
            case 8:
              DesignAttributesUtil.CreateInnerCollection(declaredPropInfo, (ICollection<PropertyAttributeInstance>) attributeInstanceList);
              break;
            case 64:
              DesignAttributesUtil.CreateInnerElement(declaredPropInfo, (ICollection<PropertyAttributeInstance>) attributeInstanceList);
              break;
          }
        }
      }
      return attributeInstanceList;
    }

    private static int GetInstanceType(PropertyInfo pi)
    {
      int num = 1;
      DesignerSerializationVisibilityAttribute customAttribute1 = (DesignerSerializationVisibilityAttribute) DesignAttributesUtil.GetCustomAttribute((MemberInfo) pi, typeof (DesignerSerializationVisibilityAttribute));
      DesignerSerializationVisibility serializationVisibility = customAttribute1 != null ? customAttribute1.Visibility : DesignerSerializationVisibility.Visible;
      PersistenceModeAttribute customAttribute2 = (PersistenceModeAttribute) DesignAttributesUtil.GetCustomAttribute((MemberInfo) pi, typeof (PersistenceModeAttribute));
      switch (customAttribute2 != null ? (int) customAttribute2.Mode : 0)
      {
        case 0:
          switch (serializationVisibility)
          {
            case DesignerSerializationVisibility.Hidden:
            case DesignerSerializationVisibility.Visible:
              return 2;
            case DesignerSerializationVisibility.Content:
              return 16;
            default:
              return num;
          }
        case 1:
          switch (serializationVisibility)
          {
            case DesignerSerializationVisibility.Hidden:
            case DesignerSerializationVisibility.Visible:
              return 8;
            case DesignerSerializationVisibility.Content:
              return 24;
            default:
              return num;
          }
        case 2:
        case 3:
          switch (serializationVisibility)
          {
            case DesignerSerializationVisibility.Hidden:
            case DesignerSerializationVisibility.Visible:
              return 64;
            case DesignerSerializationVisibility.Content:
              return 80;
            default:
              return num;
          }
        default:
          return num;
      }
    }

    public static int GetInstanceType(MemberInfo mi)
    {
      if (!(mi != (MemberInfo) null))
        return 32;
      return DesignAttributesUtil.IsHidden(mi) ? 1 : DesignAttributesUtil.GetInstanceType((PropertyInfo) mi);
    }

    private static bool IsCollection(Type type)
    {
      bool flag = DesignAttributesUtil.IsAttributeCollection(type);
      if (flag)
      {
        Type[] types = new Type[1]{ typeof (int) };
        flag = type.GetProperty("Item", types) != (PropertyInfo) null;
      }
      return flag;
    }

    private static bool IsAttributeCollection(Type type)
    {
      if (!DesignAttributesUtil.isCollection.ContainsKey(type))
      {
        bool flag = false;
        if (typeof (ICollection).IsAssignableFrom(type))
        {
          try
          {
            flag = type.GetProperty("Item", BindingFlags.Instance | BindingFlags.Public) != (PropertyInfo) null;
          }
          catch (AmbiguousMatchException ex)
          {
            flag = true;
          }
        }
        else if (type == typeof (CssStyleCollection))
          flag = true;
        DesignAttributesUtil.isCollection[type] = flag;
      }
      return DesignAttributesUtil.isCollection[type];
    }

    public static bool IsHidden(MemberInfo mi)
    {
      bool flag = false;
      if ((object) (mi as PropertyInfo) != null)
      {
        try
        {
          flag = (PropertyInfo) null == mi.DeclaringType.GetProperty(mi.Name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
        }
        catch (AmbiguousMatchException ex)
        {
          flag = true;
        }
      }
      else if ((object) (mi as EventInfo) != null)
        flag = (EventInfo) null == mi.DeclaringType.GetEvent(mi.Name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
      if (!flag)
      {
        DesignerSerializationVisibilityAttribute customAttribute1 = (DesignerSerializationVisibilityAttribute) DesignAttributesUtil.GetCustomAttribute(mi, typeof (DesignerSerializationVisibilityAttribute));
        if ((customAttribute1 != null ? (int) customAttribute1.Visibility : 1) == 0)
        {
          BindableAttribute customAttribute2 = (BindableAttribute) DesignAttributesUtil.GetCustomAttribute(mi, typeof (BindableAttribute));
          if (customAttribute2 == null || !customAttribute2.Bindable)
            flag = true;
        }
        if ((object) (mi as PropertyInfo) != null)
        {
          PropertyInfo pi = mi as PropertyInfo;
          if (flag && DesignAttributesUtil.IsStyleProperty(pi))
            flag = false;
        }
      }
      return flag;
    }

    public static bool IsStyleProperty(PropertyInfo pi)
    {
      bool flag = false;
      if (pi.Name == "Style" && pi.DeclaringType.FullName == "System.Web.UI.WebControls.WebControl")
        flag = true;
      return flag;
    }
  }
}
