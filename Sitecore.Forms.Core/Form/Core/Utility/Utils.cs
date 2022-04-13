// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.Utils
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Collections;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Layouts;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Xml.XPath;

namespace Sitecore.Form.Core.Utility
{
  public class Utils
  {
    public static Guid[] ArrayToGuidArray(ID[] ids)
    {
      List<Guid> guidList = new List<Guid>();
      if (ids != null)
      {
        foreach (ID id in ids)
          guidList.Add(id.ToGuid());
      }
      return guidList.ToArray();
    }

    public static string DictionaryToString(IDictionary<string, object> dic, char divider)
    {
      string str = string.Empty;
      if (dic != null)
      {
        foreach (string key in (IEnumerable<string>) dic.Keys)
          str = str + key + "=" + dic[key] + (object) divider;
      }
      return str.TrimEnd(divider);
    }

    public static Item[] EvaluateRealXPath(Item contextNode, string xpath)
    {
      List<Item> objList = new List<Item>();
      if (!string.IsNullOrEmpty(xpath))
      {
        try
        {
          XPathNodeIterator xpathNodeIterator = ((XPathNavigator) Factory.CreateItemNavigator(contextNode)).Select(xpath);
          int num = 0;
          while (xpathNodeIterator.MoveNext())
          {
            if (num < Sitecore.Configuration.Settings.Query.MaxItems)
            {
              string attribute = xpathNodeIterator.Current.GetAttribute("id", string.Empty);
              if (!string.IsNullOrEmpty(attribute))
              {
                Item obj = contextNode.Database.GetItem(attribute);
                if (obj != null)
                {
                  objList.Add(obj);
                  ++num;
                }
              }
            }
            else
              break;
          }
        }
        catch (Exception ex)
        {
          DependenciesManager.Logger.Warn("XPath Query: " + ex.Message, new object());
        }
      }
      return objList.ToArray();
    }

    public static Dictionary<string, string> GetFormRoots()
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>()
      {
        {
          StaticSettings.GlobalFormsRootID,
          DependenciesManager.ResourceManager.Localize("GLOBAL_FORMS_DESC")
        }
      };
      foreach (KeyValuePair<string, string> formsSite in SiteUtils.GetFormsSites())
      {
        if (!dictionary.ContainsKey(formsSite.Key))
          dictionary.Add(formsSite.Key, formsSite.Value);
      }
      if (!dictionary.ContainsKey(StaticSettings.LocalFormsRootID))
        dictionary.Add(StaticSettings.LocalFormsRootID, DependenciesManager.ResourceManager.Localize("LOCAL_FORMS_DESC"));
      return dictionary;
    }

    public static List<string> GetItemsName(string source)
    {
      if (string.IsNullOrEmpty(source))
        source = ((object) ItemIDs.RootID).ToString();
      List<string> stringList = new List<string>();
      foreach (Item child in ItemUtil.GetChildren(source))
        stringList.Add(child.Name);
      return stringList;
    }

    public static NameValueCollection GetNameValues(
      string list,
      char equil,
      char separator)
    {
      NameValueCollection nameValueCollection = new NameValueCollection();
      string str1 = list ?? string.Empty;
      char[] chArray = new char[1]{ separator };
      foreach (string str2 in str1.Split(chArray))
      {
        char[] separator1 = new char[1]{ equil };
        string[] strArray = str2.Split(separator1, 2);
        if (strArray.Length == 2)
          nameValueCollection.Add(strArray[0], strArray[1]);
      }
      return nameValueCollection;
    }

    public static IList<Item> GetSiblings(Item item, ID templateID, bool returnMe)
    {
      Assert.ArgumentNotNull((object) item, nameof (item));
      Assert.ArgumentNotNull((object) templateID, nameof (templateID));
      ChildList children = item.Parent.Children;
      List<Item> objList = new List<Item>();
      foreach (Item obj in children)
      {
        if (obj.TemplateID == templateID && ((obj.ID != item.ID ? 1 : 0) | (!returnMe ? 0 : (obj.ID == item.ID ? 1 : 0))) != 0)
          objList.Add(obj);
      }
      return (IList<Item>) objList;
    }

    public static void RemoveVersionOrItem(Item item)
    {
      Assert.ArgumentNotNull((object) item, nameof (item));
      if (item.Versions.GetVersions(true).Length > item.Versions.Count)
        item.Versions.RemoveVersion();
      else
        ItemManager.DeleteItem(item);
    }

    public static Item[] SelectItems(Database database, string idsquery)
    {
      List<Item> objList = new List<Item>();
      if (!string.IsNullOrEmpty(idsquery) && database != null)
      {
        string str1 = idsquery;
        char[] chArray = new char[1]{ '|' };
        foreach (string str2 in str1.Split(chArray))
        {
          Item obj = database.GetItem(str2);
          if (obj != null)
            objList.Add(obj);
        }
      }
      return objList.ToArray();
    }

    public static void SetUserCulture()
    {
      if (HttpContext.Current == null || HttpContext.Current.Request == null || HttpContext.Current.Request.UserLanguages == null)
        return;
      if (HttpContext.Current.Request.UserLanguages.Length == 0)
        return;
      try
      {
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Context.Language.CultureInfo.Name);
      }
      catch
      {
      }
    }

    public static string[] ToStringArray(Item[] items)
    {
      List<string> stringList = new List<string>();
      if (items != null)
      {
        foreach (Item obj in items)
        {
          if (obj != null)
            stringList.Add(((object) obj.ID).ToString());
        }
      }
      return stringList.ToArray();
    }

    public static Collection<XHtmlValidatorError> ValidateText(
      string result)
    {
      return new XHtmlValidator(XHtml.MakeDocument(XHtml.Convert(result))).Validate();
    }

    public static string GetDataSource(string url)
    {
      string str = UrlHandle.Get(new UrlString(url), "hdl", false)["fields"];
      string empty = string.Empty;
      if (!string.IsNullOrEmpty(str))
      {
        foreach (string encodedUri in new ListString(str))
        {
          FieldDescriptor fieldDescriptor = FieldDescriptor.Parse(Utils.Decode(encodedUri));
          if ((((object) fieldDescriptor.FieldID).ToString() == StaticSettings.FormFieldID || ((object) fieldDescriptor.FieldID).ToString() == StaticSettings.DataSourceFieldID) && !string.IsNullOrEmpty(fieldDescriptor.Value))
          {
            empty = fieldDescriptor.Value;
            break;
          }
        }
      }
      return empty;
    }

    private static string Decode(string encodedUri)
    {
      Assert.ArgumentNotNull((object) encodedUri, nameof (encodedUri));
      return encodedUri.Replace("-q-", "?").Replace("-a-", "&").Replace("-eq-", "=");
    }
  }
}
