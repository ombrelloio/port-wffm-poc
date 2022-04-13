// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.ItemUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Forms.Core.Data;
using Sitecore.Globalization;
using Sitecore.Links;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Form.Core.Utility
{
  public class ItemUtil
  {
    public static IEnumerable<Item> GetChildren(string source)
    {
      Assert.ArgumentNotNullOrEmpty(source, nameof (source));
      Item obj = StaticSettings.ContextDatabase.SelectSingleItem(source);
      return obj != null ? (IEnumerable<Item>) obj.Children : (IEnumerable<Item>) new List<Item>();
    }

    public static string GetItemUrl(Item item)
    {
      Assert.ArgumentNotNull((object) item, nameof (item));
      UrlOptions defaultOptions = UrlOptions.DefaultOptions;
      defaultOptions.ShortenUrls = false;
      defaultOptions.SiteResolving = Sitecore.Configuration.Settings.Rendering.SiteResolving;
      return ItemUtil.GetItemUrl(item, defaultOptions);
    }

    public static string GetItemUrl(Item item, bool siteResolving, Language lang = null)
    {
      Assert.ArgumentNotNull((object) item, nameof (item));
      UrlOptions defaultOptions = UrlOptions.DefaultOptions;
      if (lang != (Language) null)
        defaultOptions.Language = lang;
      defaultOptions.ShortenUrls = false;
      defaultOptions.SiteResolving = siteResolving;
      return ItemUtil.GetItemUrl(item, defaultOptions);
    }

    public static string GetItemValue(Item item, string field)
    {
      Assert.ArgumentNotNull((object) item, nameof (item));
      Assert.ArgumentNotNullOrEmpty(field, nameof (field));
      if (field == "__Item Name")
        return item.Name;
      return field == "__ID" ? ((object) item.ID).ToString() : ((BaseItem) item)[field];
    }

    public static string GetItemValue(string itemId, string field)
    {
      Assert.ArgumentNotNullOrEmpty(itemId, "itemID");
      Assert.ArgumentNotNullOrEmpty(field, nameof (field));
      return StaticSettings.ContextDatabase == null || StaticSettings.ContextDatabase.GetItem(itemId) == null ? string.Empty : ItemUtil.GetItemValue(StaticSettings.ContextDatabase.GetItem(itemId), field);
    }

    public static IEnumerable<TemplateFieldItem> GetTemplateFields(
      Item item)
    {
      Assert.ArgumentNotNull((object) item, nameof (item));
      return ItemUtil.GetTemplateFields(item.Template);
    }

    public static bool IsFieldContainsValue(ID itemID, ID fieldIID, string value)
    {
      Assert.ArgumentNotNull((object) itemID, nameof (itemID));
      Assert.ArgumentNotNull((object) fieldIID, "fieldID");
      Assert.ArgumentNotNullOrEmpty(value, nameof (value));
      Item obj = StaticSettings.ContextDatabase.GetItem(itemID);
      return obj != null && ((BaseItem) obj)[fieldIID].Contains(value);
    }

    public static string GetFieldDisplayName(string key)
    {
      Assert.ArgumentNotNullOrEmpty(key, key);
      Item innerItem1 = Context.Database.GetItem(key);
      if (innerItem1 != null && !Sitecore.Form.Core.Configuration.Settings.InsertIdToAnalytics)
        return new FieldItem(innerItem1).FieldDisplayName;
      string[] strArray = key.Split('/');
      if (strArray.Length == 2)
      {
        Item innerItem2 = Context.Database.GetItem(strArray[0] ?? string.Empty);
        if (innerItem2 != null && !string.IsNullOrEmpty(strArray[1]))
        {
          if (Sitecore.Form.Core.Configuration.Settings.InsertIdToAnalytics)
            return "<scparent>" + strArray[0] + "</scparent>" + strArray[1];
          FieldItem fieldItem = new FieldItem(innerItem2);
          return "<scparent>" + fieldItem.FieldDisplayName + "</scparent>" + fieldItem.GetSubFieldTitle(strArray[1]);
        }
      }
      return key.Trim('{', '}');
    }

    public static string GetText(string formId, string fieldId, string text)
    {
      Assert.ArgumentNotNullOrEmpty(formId, nameof (formId));
      Assert.ArgumentNotNull((object) fieldId, nameof (fieldId));
      Item innerItem1 = Context.Database.GetItem(formId);
      Assert.ArgumentNotNull((object) innerItem1, "form");
      string str = fieldId;
      Item innerItem2 = Context.Database.GetItem(fieldId);
      if (innerItem2 != null)
      {
        str = new FieldItem(innerItem2).FieldDisplayName;
      }
      else
      {
        string[] strArray = fieldId.Split('/');
        if (strArray.Length == 2)
        {
          Item innerItem3 = Context.Database.GetItem(strArray[0] ?? string.Empty);
          if (innerItem3 != null && !string.IsNullOrEmpty(strArray[1]))
          {
            FieldItem fieldItem = new FieldItem(innerItem3);
            str = string.Join(string.Empty, new string[3]
            {
              fieldItem.FieldDisplayName,
              " : ",
              fieldItem.GetSubFieldTitle(strArray[1])
            });
          }
        }
      }
      if (Sitecore.DateUtil.IsIsoDate(text))
        text = Sitecore.DateUtil.IsoDateToUtcIsoDate(text);
      return string.Join(string.Empty, new string[6]
      {
        "(",
        innerItem1 != null ? new FormItem(innerItem1).FormName : formId,
        ") ",
        str,
        !string.IsNullOrEmpty(text) ? ": " : string.Empty,
        text
      });
    }

    private static string GetItemUrl(Item item, UrlOptions defaultOptions)
    {
      string str = LinkManager.GetItemUrl(item, defaultOptions);
      if (str.EndsWith("sitecore/shell.aspx"))
        str = str.Replace(".aspx", "/default.aspx");
      return str.Replace("/sitecore/shell", string.Empty);
    }

    private static void AddFields(
      List<TemplateFieldItem> list,
      IEnumerable<TemplateFieldItem> fields)
    {
      foreach (TemplateFieldItem field1 in fields)
      {
        TemplateFieldItem field = field1;
        if (((IEnumerable<TemplateFieldItem>) list).FirstOrDefault<TemplateFieldItem>((Func<TemplateFieldItem, bool>) (i => ((CustomItemBase) i).ID == ((CustomItemBase) field).ID)) == null)
          list.Add(field);
      }
    }

    private static IEnumerable<TemplateFieldItem> GetTemplateFields(
      TemplateItem template)
    {
      List<TemplateFieldItem> list = new List<TemplateFieldItem>();
      if (template != null)
      {
        Array.ForEach<TemplateSectionItem>(template.GetSections(), (Action<TemplateSectionItem>) (s => ItemUtil.AddFields(list, (IEnumerable<TemplateFieldItem>) s.GetFields())));
        foreach (TemplateItem baseTemplate in template.BaseTemplates)
        {
          IEnumerable<TemplateFieldItem> templateFields = ItemUtil.GetTemplateFields(baseTemplate);
          ItemUtil.AddFields(list, templateFields);
        }
        list.Sort((Comparison<TemplateFieldItem>) ((x, y) => ((CustomItemBase) x.Section).Name == ((CustomItemBase) y.Section).Name ? (x.Section.Sortorder * 10000 + x.Sortorder).CompareTo(x.Section.Sortorder * 10000 + y.Sortorder) : ((CustomItemBase) x.Section).Name.CompareTo(((CustomItemBase) y.Section).Name)));
      }
      return (IEnumerable<TemplateFieldItem>) list;
    }
  }
}
