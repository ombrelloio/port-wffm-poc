// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Update.Steps.UpdateListItemsLocation
// Assembly: Sitecore.WFFM.Update, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 141BB56E-B1B2-4D04-B15D-AD6BA1098ABC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Update.dll

using Sitecore.Collections;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Sites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Sitecore.WFFM.Update.Steps
{
  public class UpdateListItemsLocation : IPostStepPartial
  {
    private static readonly ID FieldLocalizedParametersID = new ID("{26CDC7C2-5307-4591-A7F9-5C034A05630A}");
    private static readonly ID FieldParametersID = new ID("{358E7AA0-E3E6-4EF6-92DF-EC1301737B50}");
    private static readonly ID FieldTemplateID = new ID("{C9E1BF85-800A-4247-A3A3-C3F5DFBFD6AA}");
    private static readonly string GlobalFormsRootID = "{4F42E032-6174-4A79-B3B0-5056494D6B39}";
    private static readonly ID FieldLinkTypeID = new ID("{35C76A0B-B630-458C-AECC-1D62C50D24FC}");
    private static readonly string ExcludeSites = "|shell|login|testing|admin|modules_shell|modules_website|scheduler|system|publisher|";
    private static readonly string LocalFormsRootID = "{AD45E5EF-6CBF-49D5-9A10-4A3103369C2B}";

    public void Process()
    {
      foreach (Item obj in this.GetItemsToProcess())
      {
        string str1 = ((BaseItem) obj).Fields[UpdateListItemsLocation.FieldParametersID].Value;
        if (!string.IsNullOrEmpty(str1))
        {
          Match match = Regex.Match(str1, "<items>.*?</items>", RegexOptions.IgnoreCase);
          if (!match.Success)
            match = Regex.Match(str1, "<loc_items>.*?</loc_items>", RegexOptions.IgnoreCase);
          if (match.Success)
          {
            string str2 = match.Value;
            string newParameters = str1.Replace(str2, string.Empty);
            Item langFieldItem1 = (Item) null;
            foreach (Language language in obj.Languages)
            {
              Item langFieldItem2 = Factory.GetDatabase("master").GetItem(obj.ID, language);
              if (langFieldItem2.Versions.Count > 0 && !UpdateListItemsLocation.UpdateLangItem(langFieldItem2, str2, str1, ref newParameters))
                langFieldItem1 = langFieldItem2;
            }
            if (langFieldItem1 == null)
            {
              langFieldItem1 = Factory.GetDatabase("master").GetItem(obj.ID, Language.Current);
              UpdateListItemsLocation.UpdateLangItem(langFieldItem1, str2, str1, ref newParameters);
            }
            langFieldItem1.Editing.BeginEdit();
            ((BaseItem) langFieldItem1).Fields[UpdateListItemsLocation.FieldParametersID].Value = newParameters;
            langFieldItem1.Editing.EndEdit();
          }
        }
      }
    }

    private static bool UpdateLangItem(
      Item langFieldItem,
      string itemsValue,
      string parameters,
      ref string newParameters)
    {
      string input = ((BaseItem) langFieldItem).Fields[UpdateListItemsLocation.FieldLocalizedParametersID].Value;
      if (Regex.Match(input, "<items>.*?</items>", RegexOptions.IgnoreCase).Success)
        return true;
      StringBuilder stringBuilder = new StringBuilder(input);
      stringBuilder.Append(itemsValue.Replace("<loc_items>", "<items>").Replace("</loc_items>", "</items>").Replace("<loc_Items>", "<Items>").Replace("</loc_Items>", "</Items>"));
      Match match = Regex.Match(parameters, "<selectedValue>.*?</selectedValue>", RegexOptions.IgnoreCase);
      if (match.Success)
      {
        stringBuilder.Append(match.Value);
        newParameters = newParameters.Replace(match.Value, string.Empty);
      }
      langFieldItem.Editing.BeginEdit();
      ((BaseItem) langFieldItem).Fields[UpdateListItemsLocation.FieldLocalizedParametersID].Value = stringBuilder.ToString();
      langFieldItem.Editing.EndEdit();
      return false;
    }

    private IEnumerable<Item> GetItemsToProcess()
    {
      List<Item> objList = new List<Item>();
      List<string> listTypes = new List<string>()
      {
        "{CDD533E2-918A-4BE3-A12F-83A8580363F7}",
        "{C6D97C39-23B5-4B7E-AFC7-9F41795533C6}",
        "{0FAE4DE2-5C37-45A6-B474-9E3AB95FF452}",
        "{E994EAE0-EDB0-4D89-B545-FEBEF07DD7CD}"
      };
      foreach (string formRoot in UpdateListItemsLocation.GetFormRoots())
      {
        if (!string.IsNullOrEmpty(formRoot))
        {
          Item obj = Factory.GetDatabase("master").GetItem(formRoot);
          if (obj != null)
          {
            Item[] objArray = obj.Axes.SelectItems(string.Format(".//*[@@templateid='{0}']", (object) UpdateListItemsLocation.FieldTemplateID));
            if (objArray != null)
              objList.AddRange((IEnumerable<Item>) ((IEnumerable<Item>) objArray).Where<Item>((Func<Item, bool>) (x => listTypes.Contains(((BaseItem) x)[UpdateListItemsLocation.FieldLinkTypeID]))).ToArray<Item>());
          }
        }
      }
      return (IEnumerable<Item>) objList;
    }

    private static List<string> GetFormRoots()
    {
      List<string> stringList = new List<string>()
      {
        UpdateListItemsLocation.GlobalFormsRootID
      };
      foreach (KeyValuePair<string, string> formsSite in UpdateListItemsLocation.GetFormsSites())
      {
        if (!stringList.Contains(formsSite.Key))
          stringList.Add(formsSite.Key);
      }
      if (!stringList.Contains(UpdateListItemsLocation.LocalFormsRootID))
        stringList.Add(UpdateListItemsLocation.LocalFormsRootID);
      return stringList;
    }

    private static IEnumerable<KeyValuePair<string, string>> GetFormsSites()
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (Site site in (Collection<Site>) SiteManager.GetSites())
      {
        if (!string.IsNullOrEmpty(((SafeDictionary<string, string>) site.Properties)["formsRoot"]) && !UpdateListItemsLocation.ExcludeSites.Contains(string.Format("|{0}|", (object) ((SafeDictionary<string, string>) site.Properties)["name"])))
        {
          string property1 = ((SafeDictionary<string, string>) site.Properties)["database"];
          if (!string.IsNullOrEmpty(property1))
          {
            Database database = Factory.GetDatabase(property1);
            if (database != null)
            {
              string property2 = ((SafeDictionary<string, string>) site.Properties)["formsRoot"];
              Item obj = !ID.IsID(property2) ? database.SelectSingleItem(property2) : database.GetItem(property2);
              if (obj != null)
              {
                string key = ((object) obj.ID).ToString();
                if (!dictionary.ContainsKey(key))
                  dictionary.Add(key, ((SafeDictionary<string, string>) site.Properties)["name"]);
              }
            }
          }
        }
      }
      return (IEnumerable<KeyValuePair<string, string>>) dictionary;
    }
  }
}
