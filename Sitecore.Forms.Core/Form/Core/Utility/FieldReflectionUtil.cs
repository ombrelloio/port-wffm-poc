// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.FieldReflectionUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Caching;
using Sitecore.Caching.Generics;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Client.Submit;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.SchemaGenerator;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Forms.Core.Data;
using Sitecore.Reflection;
using Sitecore.WFFM.Abstractions.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI;

namespace Sitecore.Form.Core.Utility
{
  [Serializable]
  public class FieldReflectionUtil
  {
    private static readonly Hashtable fieldInstances = new Hashtable();
    private static readonly Dictionary<string, Adapter> adapterInstances = new Dictionary<string, Adapter>();
    private static readonly Dictionary<string, Adapter> reportAdapterInstances = new Dictionary<string, Adapter>();
    private static readonly Dictionary<string, Pair<IListAdapter, string>> listAdapterInstances = new Dictionary<string, Pair<IListAdapter, string>>();
    private static readonly Cache initilaValuesCache = new Cache("wfm:initialvalues", Settings.InitialValuesCacheSize);

    public static Type GetFieldType(string assemblyName, string className, string userControl)
    {
      string str = assemblyName + className + userControl;
      if (string.IsNullOrEmpty(str))
        return (Type) null;
      if (FieldReflectionUtil.fieldInstances.ContainsKey((object) str))
        return FieldReflectionUtil.fieldInstances[(object) str] as Type;
      Type type1 = FieldReflectionUtil.GetUserControlType(userControl, (Page) null);
      if ((object) type1 == null)
        type1 = ReflectionUtil.GetTypeInfo(assemblyName, className);
      Type type2 = type1;
      if (type2 != (Type) null)
      {
        lock (FieldReflectionUtil.fieldInstances)
        {
          if (FieldReflectionUtil.fieldInstances.ContainsKey((object) str))
            return FieldReflectionUtil.fieldInstances[(object) str] as Type;
          FieldReflectionUtil.fieldInstances.Add((object) str, (object) type2);
        }
      }
      return type2;
    }

    public static Type GetFieldType(IFieldItem item)
    {
      Assert.ArgumentNotNull((object) item, nameof (item));
      return FieldReflectionUtil.GetFieldType(item.AssemblyName, item.ClassName, item.UserControl);
    }

    public static Control GetFieldInstance(FieldTypeItem item)
    {
      Assert.ArgumentNotNull((object) item, nameof (item));
      return FieldReflectionUtil.GetUserControlInstance(item.UserControl, (Page) null) ?? (Control) ReflectionUtil.CreateObject(item.AssemblyName, item.ClassName, new object[0]);
    }

    public static Control GetFieldInstance(IFieldTypeItem item, Page page)
    {
      Assert.ArgumentNotNull((object) item, nameof (item));
      return FieldReflectionUtil.GetUserControlInstance(item.UserControl, page) ?? (Control) ReflectionUtil.CreateObject(item.AssemblyName, item.ClassName, new object[0]);
    }

    public static bool FieldCanListAdapt(IFieldItem field)
    {
      Assert.ArgumentNotNull((object) field, "fieldItem");
      string key = field.AssemblyName + field.ClassName + field.UserControl;
      if (FieldReflectionUtil.listAdapterInstances.ContainsKey(key))
        return FieldReflectionUtil.listAdapterInstances[key] != null;
      Type fieldType = FieldReflectionUtil.GetFieldType(field);
      if (fieldType != (Type) null)
      {
        ListAdapterAttribute customAttribute = (ListAdapterAttribute) DesignAttributesUtil.GetCustomAttribute((MemberInfo) fieldType, typeof (ListAdapterAttribute));
        IListAdapter instance = customAttribute?.GetInstance();
        lock (FieldReflectionUtil.listAdapterInstances)
        {
          if (FieldReflectionUtil.listAdapterInstances.ContainsKey(key))
            return FieldReflectionUtil.listAdapterInstances[key] != null;
          if (instance != null)
          {
            FieldReflectionUtil.listAdapterInstances.Add(key, new Pair<IListAdapter, string>(instance, customAttribute.PropertyName));
            return true;
          }
          FieldReflectionUtil.listAdapterInstances.Add(key, (Pair<IListAdapter, string>) null);
        }
      }
      return false;
    }

    public static List<string> ListAdapt(IFieldItem field)
    {
      Assert.ArgumentNotNull((object) field, "fieldItem");
      string key = field.AssemblyName + field.ClassName + field.UserControl;
      if (FieldReflectionUtil.listAdapterInstances.ContainsKey(key))
      {
        Pair<IListAdapter, string> listAdapterInstance = FieldReflectionUtil.listAdapterInstances[key];
        return listAdapterInstance != null ? listAdapterInstance.Part1.AdaptList(FieldReflectionUtil.GetListValue(field, listAdapterInstance.Part2)) : new List<string>();
      }
      Type fieldType = FieldReflectionUtil.GetFieldType(field);
      if (fieldType != (Type) null)
      {
        ListAdapterAttribute customAttribute = (ListAdapterAttribute) DesignAttributesUtil.GetCustomAttribute((MemberInfo) fieldType, typeof (ListAdapterAttribute));
        IListAdapter instance = customAttribute?.GetInstance();
        lock (FieldReflectionUtil.listAdapterInstances)
        {
          if (FieldReflectionUtil.listAdapterInstances.ContainsKey(key))
          {
            Pair<IListAdapter, string> listAdapterInstance = FieldReflectionUtil.listAdapterInstances[key];
            return listAdapterInstance.Part1.AdaptList(FieldReflectionUtil.GetListValue(field, listAdapterInstance.Part2));
          }
          if (instance != null)
          {
            FieldReflectionUtil.listAdapterInstances.Add(key, new Pair<IListAdapter, string>(instance, customAttribute.PropertyName));
            Pair<IListAdapter, string> listAdapterInstance = FieldReflectionUtil.listAdapterInstances[key];
            return listAdapterInstance.Part1.AdaptList(FieldReflectionUtil.GetListValue(field, listAdapterInstance.Part2));
          }
          FieldReflectionUtil.listAdapterInstances.Add(key, (Pair<IListAdapter, string>) null);
        }
      }
      return new List<string>();
    }

    internal static string GetAdaptedResult(ID fieldID, object value)
    {
      Assert.ArgumentNotNull((object) fieldID, nameof (fieldID));
      Item innerItem = StaticSettings.ContextDatabase.GetItem(fieldID);
      if (innerItem != null)
      {
        FieldItem fieldItem = new FieldItem(innerItem);
        string key = fieldItem.AssemblyName + fieldItem.ClassName;
        if (FieldReflectionUtil.adapterInstances.ContainsKey(key))
        {
          Adapter adapterInstance = FieldReflectionUtil.adapterInstances[key];
          return adapterInstance == null ? value.ToString() : adapterInstance.AdaptResult((IFieldItem) fieldItem, value);
        }
        Type fieldType = FieldReflectionUtil.GetFieldType((IFieldItem) fieldItem);
        if (fieldType != (Type) null)
        {
          AdapterAttribute customAttribute = (AdapterAttribute) DesignAttributesUtil.GetCustomAttribute((MemberInfo) fieldType, typeof (AdapterAttribute));
          Adapter instance = customAttribute?.GetInstance();
          lock (FieldReflectionUtil.adapterInstances)
          {
            if (FieldReflectionUtil.adapterInstances.ContainsKey(key))
            {
              Adapter adapterInstance = FieldReflectionUtil.adapterInstances[key];
              return adapterInstance != null ? adapterInstance.AdaptResult((IFieldItem) fieldItem, value) : value.ToString();
            }
            if (instance != null)
            {
              FieldReflectionUtil.adapterInstances.Add(key, instance);
              return customAttribute.GetInstance().AdaptResult((IFieldItem) fieldItem, value);
            }
            FieldReflectionUtil.adapterInstances.Add(key, (Adapter) null);
          }
        }
      }
      return (value ?? (object) string.Empty).ToString();
    }

    public static string GetAdaptedValue(string fieldId, string value)
    {
      Assert.ArgumentNotNullOrEmpty(fieldId, "fieldid");
      ID fieldId1;
      return ID.TryParse(fieldId, out fieldId1) ? FieldReflectionUtil.GetAdaptedValue(fieldId1, value) : value;
    }

    public static string GetSitecoreStyleValue(string fieldId, string value)
    {
      Assert.ArgumentNotNullOrEmpty(fieldId, nameof (fieldId));
      ID fieldId1;
      return ID.TryParse(fieldId, out fieldId1) ? FieldReflectionUtil.GetSitecoreStyleValue(fieldId1, value) : value;
    }

    private static string GetSitecoreStyleValue(ID fieldId, string value)
    {
      Item innerItem = StaticSettings.ContextDatabase.GetItem(fieldId);
      return innerItem != null && (innerItem.TemplateID == IDs.FieldTemplateID) ? FieldReflectionUtil.GetSitecoreStyleValue(new FieldItem(innerItem), value) : value;
    }

    public static string GetSitecoreStyleValue(FieldItem field, string value)
    {
      Assert.ArgumentNotNull((object) field, "fieldItem");
      string key = field.AssemblyName + field.ClassName + field.UserControl;
      if (FieldReflectionUtil.adapterInstances.ContainsKey(key))
      {
        Adapter adapterInstance = FieldReflectionUtil.adapterInstances[key];
        return adapterInstance == null ? value : adapterInstance.AdaptToSitecoreStandard((IFieldItem) field, value);
      }
      Type fieldType = FieldReflectionUtil.GetFieldType((IFieldItem) field);
      if (fieldType != (Type) null)
      {
        AdapterAttribute customAttribute = (AdapterAttribute) DesignAttributesUtil.GetCustomAttribute((MemberInfo) fieldType, typeof (AdapterAttribute));
        Adapter instance = customAttribute?.GetInstance();
        lock (FieldReflectionUtil.adapterInstances)
        {
          if (FieldReflectionUtil.adapterInstances.ContainsKey(key))
          {
            Adapter adapterInstance = FieldReflectionUtil.adapterInstances[key];
            return adapterInstance != null ? adapterInstance.AdaptToFriendlyValue((IFieldItem) field, value) : value;
          }
          if (instance != null)
          {
            FieldReflectionUtil.adapterInstances.Add(key, instance);
            return customAttribute.GetInstance().AdaptToSitecoreStandard((IFieldItem) field, value);
          }
          FieldReflectionUtil.adapterInstances.Add(key, (Adapter) null);
        }
      }
      return value;
    }

    public static string GetAdaptedValue(ID fieldId, string value)
    {
      Assert.ArgumentNotNull((object) fieldId, nameof (fieldId));
      Item innerItem = StaticSettings.ContextDatabase.GetItem(fieldId);
      return innerItem != null && (innerItem.TemplateID == IDs.FieldTemplateID) ? FieldReflectionUtil.GetAdaptedValue((IFieldItem) new FieldItem(innerItem), value) : value;
    }

    public static string GetAdaptedValue(IFieldItem fieldItem, string value)
    {
      Assert.ArgumentNotNull((object) fieldItem, nameof (fieldItem));
      string key = fieldItem.AssemblyName + fieldItem.ClassName + fieldItem.UserControl;
      if (FieldReflectionUtil.adapterInstances.ContainsKey(key))
      {
        Adapter adapterInstance = FieldReflectionUtil.adapterInstances[key];
        return adapterInstance == null ? value : adapterInstance.AdaptToFriendlyValue(fieldItem, value);
      }
      Type fieldType = FieldReflectionUtil.GetFieldType(fieldItem);
      if (fieldType != (Type) null)
      {
        AdapterAttribute customAttribute = (AdapterAttribute) DesignAttributesUtil.GetCustomAttribute((MemberInfo) fieldType, typeof (AdapterAttribute));
        Adapter instance = customAttribute?.GetInstance();
        lock (FieldReflectionUtil.adapterInstances)
        {
          if (FieldReflectionUtil.adapterInstances.ContainsKey(key))
          {
            Adapter adapterInstance = FieldReflectionUtil.adapterInstances[key];
            return adapterInstance != null ? adapterInstance.AdaptToFriendlyValue(fieldItem, value) : value;
          }
          if (instance != null)
          {
            FieldReflectionUtil.adapterInstances.Add(key, instance);
            return customAttribute.GetInstance().AdaptToFriendlyValue(fieldItem, value);
          }
          FieldReflectionUtil.adapterInstances.Add(key, (Adapter) null);
        }
      }
      return value;
    }

    public static IEnumerable<string> GetAdaptedListValue(
      FieldItem field,
      string value,
      bool returnTexts)
    {
      Assert.ArgumentNotNull((object) field, "fieldItem");
      string key = field.AssemblyName + field.ClassName + field.UserControl;
      if (FieldReflectionUtil.adapterInstances.ContainsKey(key))
      {
        Adapter adapterInstance = FieldReflectionUtil.adapterInstances[key];
        if (adapterInstance != null)
          return adapterInstance.AdaptToFriendlyListValues((IFieldItem) field, value, returnTexts);
        return (IEnumerable<string>) new List<string>((IEnumerable<string>) new string[1]
        {
          value
        });
      }
      Type fieldType = FieldReflectionUtil.GetFieldType((IFieldItem) field);
      if (fieldType != (Type) null)
      {
        AdapterAttribute customAttribute = (AdapterAttribute) DesignAttributesUtil.GetCustomAttribute((MemberInfo) fieldType, typeof (AdapterAttribute));
        Adapter instance = customAttribute?.GetInstance();
        lock (FieldReflectionUtil.adapterInstances)
        {
          if (FieldReflectionUtil.adapterInstances.ContainsKey(key))
          {
            Adapter adapterInstance = FieldReflectionUtil.adapterInstances[key];
            IEnumerable<string> strings;
            if (adapterInstance == null)
              strings = (IEnumerable<string>) new List<string>((IEnumerable<string>) new string[1]
              {
                value
              });
            else
              strings = adapterInstance.AdaptToFriendlyListValues((IFieldItem) field, value, returnTexts);
            return strings;
          }
          if (instance != null)
          {
            FieldReflectionUtil.adapterInstances.Add(key, instance);
            return customAttribute.GetInstance().AdaptToFriendlyListValues((IFieldItem) field, value, returnTexts);
          }
          FieldReflectionUtil.adapterInstances.Add(key, (Adapter) null);
        }
      }
      return (IEnumerable<string>) new List<string>((IEnumerable<string>) new string[1]
      {
        value
      });
    }

    public static IEnumerable<string> ListAdapt(IFieldItem field, string value)
    {
      Assert.ArgumentNotNull((object) field, "fieldItem");
      string key = field.AssemblyName + field.ClassName + field.UserControl;
      if (FieldReflectionUtil.listAdapterInstances.ContainsKey(key))
      {
        Pair<IListAdapter, string> listAdapterInstance = FieldReflectionUtil.listAdapterInstances[key];
        if (listAdapterInstance != null)
          return listAdapterInstance.Part1.AdaptList(value);
        return (IEnumerable<string>) new List<string>((IEnumerable<string>) new string[1]
        {
          value
        });
      }
      Type fieldType = FieldReflectionUtil.GetFieldType(field);
      if (fieldType != (Type) null)
      {
        ListAdapterAttribute customAttribute = (ListAdapterAttribute) DesignAttributesUtil.GetCustomAttribute((MemberInfo) fieldType, typeof (ListAdapterAttribute));
        IListAdapter instance = customAttribute?.GetInstance();
        lock (FieldReflectionUtil.adapterInstances)
        {
          if (FieldReflectionUtil.adapterInstances.ContainsKey(key))
          {
            Pair<IListAdapter, string> listAdapterInstance = FieldReflectionUtil.listAdapterInstances[key];
            IEnumerable<string> strings;
            if (listAdapterInstance == null)
              strings = (IEnumerable<string>) new List<string>((IEnumerable<string>) new string[1]
              {
                value
              });
            else
              strings = listAdapterInstance.Part1.AdaptList(value);
            return strings;
          }
          if (instance != null)
          {
            FieldReflectionUtil.listAdapterInstances.Add(key, new Pair<IListAdapter, string>(instance, customAttribute.PropertyName));
            return instance.AdaptList(value);
          }
          FieldReflectionUtil.listAdapterInstances.Add(key, (Pair<IListAdapter, string>) null);
        }
      }
      return (IEnumerable<string>) new List<string>((IEnumerable<string>) new string[1]
      {
        value
      });
    }

    public static string GetReportAdaptedValue(FieldItem field, string value)
    {
      Assert.ArgumentNotNull((object) field, "fieldItem");
      string key = field.AssemblyName + field.ClassName + field.UserControl;
      if (FieldReflectionUtil.reportAdapterInstances.ContainsKey(key))
      {
        Adapter reportAdapterInstance = FieldReflectionUtil.reportAdapterInstances[key];
        return reportAdapterInstance == null ? value : reportAdapterInstance.AdaptResult((IFieldItem) field, (object) value);
      }
      Type fieldType = FieldReflectionUtil.GetFieldType((IFieldItem) field);
      if (fieldType == (Type) null)
        return value;
      ReportAdapterAttribute customAttribute = (ReportAdapterAttribute) DesignAttributesUtil.GetCustomAttribute((MemberInfo) fieldType, typeof (ReportAdapterAttribute));
      if (customAttribute == null)
        return FieldReflectionUtil.GetAdaptedValue((IFieldItem) field, value);
      Adapter instance = customAttribute.GetInstance();
      lock (FieldReflectionUtil.reportAdapterInstances)
      {
        if (FieldReflectionUtil.reportAdapterInstances.ContainsKey(key))
        {
          Adapter reportAdapterInstance = FieldReflectionUtil.reportAdapterInstances[key];
          return reportAdapterInstance != null ? reportAdapterInstance.AdaptResult((IFieldItem) field, (object) value) : value;
        }
        if (instance != null)
        {
          FieldReflectionUtil.reportAdapterInstances.Add(key, instance);
          return instance.AdaptResult((IFieldItem) field, (object) value);
        }
        FieldReflectionUtil.reportAdapterInstances.Add(key, (Adapter) null);
      }
      return value;
    }

    public static string GetInitialValue(string fieldId)
    {
      Assert.ArgumentNotNullOrEmpty(fieldId, nameof (fieldId));
      ID fieldId1;
      return ID.TryParse(fieldId, out fieldId1) ? FieldReflectionUtil.GetInitialValue(fieldId1) : string.Empty;
    }

    public static string GetInitialValue(ID fieldId)
    {
      Assert.ArgumentNotNull((object) fieldId, nameof (fieldId));
      Item innerItem = StaticSettings.ContextDatabase.GetItem(fieldId);
      return innerItem != null ? FieldReflectionUtil.GetInitialValue(new FieldItem(innerItem)) : string.Empty;
    }

    public static string GetInitialValue(FieldItem field)
    {
      Assert.ArgumentNotNull((object) field, "fieldItem");
      string str = (string) ((Cache<string>) FieldReflectionUtil.initilaValuesCache).GetValue(((object) ((CustomItemBase) field).ID).ToString());
      if (str == null)
      {
        Control fieldInstance = FieldReflectionUtil.GetFieldInstance((IFieldTypeItem) field, (Page) null);
        if (fieldInstance == null)
          return str;
        ReflectionUtils.SetXmlProperties((object) fieldInstance, "<Title>" + field.Title + "</Title>", true);
        ReflectionUtils.SetXmlProperties((object) fieldInstance, field.Parameters, true);
        ReflectionUtils.SetXmlProperties((object) fieldInstance, field.LocalizedParameters, true);
        if (fieldInstance is IResult)
        {
          ReflectionUtil.CallMethod((object) fieldInstance, "OnInit", true, true, new object[1]
          {
            (object) EventArgs.Empty
          });
          ReflectionUtil.CallMethod((object) fieldInstance, "OnLoad", true, true, new object[1]
          {
            (object) EventArgs.Empty
          });
          ReflectionUtil.CallMethod((object) fieldInstance, "OnPreRender", true, true, new object[1]
          {
            (object) EventArgs.Empty
          });
          str = FieldReflectionUtil.GetAdaptedValue((IFieldItem) field, (((IResult) fieldInstance).Result.Value ?? (object) string.Empty).ToString());
        }
        else
          str = string.Empty;
        ((Cache<string>) FieldReflectionUtil.initilaValuesCache).Add(((object) ((CustomItemBase) field).ID).ToString(), (object) str);
      }
      return str;
    }

    internal static void ClearCaches()
    {
      FieldReflectionUtil.fieldInstances.Clear();
      FieldReflectionUtil.adapterInstances.Clear();
      FieldReflectionUtil.listAdapterInstances.Clear();
      ((Cache<string>) FieldReflectionUtil.initilaValuesCache).Clear();
    }

    private static IList GetListValue(IFieldItem field, string property)
    {
      Control fieldInstance = FieldReflectionUtil.GetFieldInstance((IFieldTypeItem) field, (Page) null);
      IEnumerable<Pair<string, string>> pairArray1 = ParametersUtil.XmlToPairArray(field.Parameters);
      if (!FieldReflectionUtil.InitProperties(fieldInstance, property, pairArray1))
      {
        IEnumerable<Pair<string, string>> pairArray2 = ParametersUtil.XmlToPairArray(field.LocalizedParameters);
        FieldReflectionUtil.InitProperties(fieldInstance, property, pairArray2);
      }
      return (IList) ReflectionUtil.GetProperty((object) fieldInstance, property) ?? (IList) new ListItemCollection();
    }

    private static bool InitProperties(
      Control control,
      string property,
      IEnumerable<Pair<string, string>> properties)
    {
      Pair<string, string> pair = properties.FirstOrDefault<Pair<string, string>>((Func<Pair<string, string>, bool>) (p => string.Compare(p.Part1, property, true) == 0));
      if (pair == null)
        return false;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("<{0}>", (object) property);
      stringBuilder.Append(pair.Part2);
      stringBuilder.AppendFormat("</{0}>", (object) property);
      ReflectionUtils.SetXmlProperties((object) control, stringBuilder.ToString(), true);
      return true;
    }

    private static Control GetUserControlInstance(string userControl, Page contextPage)
    {
      if (!string.IsNullOrEmpty(userControl))
      {
        Page page = contextPage ?? WebUtil.GetPage();
        if (page != null)
        {
          Control control = page.LoadControl(userControl);
          if (control != null)
          {
            control.GetType();
            return control;
          }
        }
      }
      return (Control) null;
    }

    private static Type GetUserControlType(string userControl, Page page) => FieldReflectionUtil.GetUserControlInstance(userControl, page)?.GetType();
  }
}
