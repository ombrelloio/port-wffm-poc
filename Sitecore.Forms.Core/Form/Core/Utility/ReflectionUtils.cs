// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.ReflectionUtils
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Collections;
using Sitecore.Diagnostics;
using Sitecore.Reflection;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;

namespace Sitecore.Form.Core.Utility
{
  public class ReflectionUtils
  {
    public static bool GetNamespaceAndAssemblyFromType(
      Type type,
      out string nspace,
      out string asmName)
    {
      Assert.IsNotNull((object) type, nameof (type));
      Assembly assembly = type.Module.Assembly;
      asmName = assembly.GlobalAssemblyCache ? assembly.FullName : assembly.GetName().Name;
      nspace = type.Namespace ?? string.Empty;
      nspace = nspace.TrimEnd('.');
      if (nspace != null && !string.IsNullOrEmpty(asmName))
        return true;
      nspace = (string) null;
      asmName = (string) null;
      return false;
    }

    public static void SetField(Type type, object obj, string name, object value) => type.InvokeMember(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetField, (Binder) null, obj, new object[1]
    {
      value
    });

    public static void SetProperties(object obj, string parameters, bool ignoreErrors)
    {
      if (obj == null)
        return;
      NameValueCollection nameValues = Utils.GetNameValues(parameters, '=', '&');
      foreach (string key in nameValues.Keys)
      {
        try
        {
          ReflectionUtil.SetProperty(obj, key, (object) nameValues[key]);
        }
        catch
        {
          if (!ignoreErrors)
            throw;
        }
      }
    }

    public static void SetXmlProperties(
      object obj,
      string parameters,
      bool ignoreErrors,
      string[] skip)
    {
      if (obj == null || parameters == null)
        return;
      IEnumerable<Pair<string, string>> pairArray = ParametersUtil.XmlToPairArray(parameters);
      ReflectionUtils.SetXmlProperties(obj, pairArray, ignoreErrors, skip);
    }

    public static void SetXmlProperties(object obj, string parameters, bool ignoreErrors) => ReflectionUtils.SetXmlProperties(obj, parameters, ignoreErrors, (string[]) null);

    public static void SetXmlProperties(
      object obj,
      IEnumerable<Pair<string, string>> parameters,
      bool ignoreErrors,
      string[] skip)
    {
      if (obj == null)
        return;
      foreach (Pair<string, string> parameter in parameters)
      {
        Pair<string, string> value = parameter;
        if (skip == null || ((IEnumerable<string>) skip).FirstOrDefault<string>((Func<string, bool>) (s => s == value.Part1)) == null)
          ReflectionUtils.SetProperty(obj, value.Part1, (object) value.Part2, ignoreErrors);
      }
    }

    public static void SetProperty(object obj, string property, object value, bool ignoreErrors = true)
    {
      try
      {
        object obj1 = value;
        PropertyInfo[] properties = obj.GetType().GetProperties();
        Type type = (Type) null;
        foreach (PropertyInfo propertyInfo in properties)
        {
          if (propertyInfo.Name.Equals(property, StringComparison.OrdinalIgnoreCase))
          {
            type = propertyInfo.PropertyType;
            break;
          }
        }
        if (type == typeof (bool))
        {
          if (obj1.ToString() == "1")
            obj1 = (object) "true";
          else if (obj1.ToString() == "0")
            obj1 = (object) "false";
        }
        ReflectionUtil.SetProperty(obj, property, obj1);
      }
      catch
      {
        if (ignoreErrors)
          return;
        throw;
      }
    }

    public static void SetXmlProperties(
      object obj,
      IEnumerable<Pair<string, string>> parameters,
      bool ignoreErrors)
    {
      ReflectionUtils.SetXmlProperties(obj, parameters, ignoreErrors, (string[]) null);
    }

    public static PropertyInfo[] GetDeclaredPropInfos(Type type, bool usesInheritance)
    {
      BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public;
      if (!usesInheritance)
        bindingAttr |= BindingFlags.DeclaredOnly;
      return type.GetProperties(bindingAttr);
    }

    public static EventInfo[] GetDeclaredEventInfos(Type type, bool usesInheritance)
    {
      BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public;
      if (usesInheritance)
        bindingAttr |= BindingFlags.DeclaredOnly;
      return type.GetEvents(bindingAttr);
    }
  }
}
