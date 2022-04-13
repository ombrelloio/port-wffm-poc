// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.SiteUtils
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Collections;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.IO;
using Sitecore.Sites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sitecore.Form.Core.Utility
{
  public class SiteUtils
  {
    public static readonly string DefaultSiteName = "website";
    public static readonly string excludeSites = "|shell|login|testing|admin|modules_shell|modules_website|scheduler|system|publisher|";
    public static readonly string WithoutRoot = "{AD45E5EF-6CBF-49D5-9A10-4A3103369C2B}";
    private static readonly string DatabaseAttribute = "database";
    private static readonly string FormsRootAttribute = "formsRoot";
    private static readonly string RootPathAttribute = "rootPath";
    private static readonly string SiteNameAttribute = "name";
    private static readonly string StartItemAttributte = "startItem";

    public static Site DefaultSite => SiteManager.GetSite(Settings.GetSetting("Preview.DefaultSite", SiteUtils.DefaultSiteName));

    public static string GetFormsRootForSite(SiteContext site)
    {
      if (site != null)
      {
        string property = site.Properties[SiteUtils.FormsRootAttribute];
        if (!string.IsNullOrEmpty(property))
          return property;
      }
      return SiteUtils.DefaultSite != null && !string.IsNullOrEmpty(((SafeDictionary<string, string>) SiteUtils.DefaultSite.Properties)[SiteUtils.FormsRootAttribute]) ? ((SafeDictionary<string, string>) SiteUtils.DefaultSite.Properties)[SiteUtils.FormsRootAttribute] : SiteUtils.WithoutRoot;
    }

    public static Item GetFormsRootItemForItem(Item item)
    {
      Assert.ArgumentNotNull((object) item, nameof (item));
      string rootValueForItem = SiteUtils.GetFormsRootValueForItem(item);
      return item.Database.GetItem(rootValueForItem, item.Language);
    }

    public static string GetFormsRootValueForItem(Item item)
    {
      Assert.ArgumentNotNull((object) item, nameof (item));
      string str1 = string.Empty;
      string str2 = string.Empty;
      foreach (Site site in (Collection<Site>) SiteManager.GetSites())
      {
        string property = ((SafeDictionary<string, string>) site.Properties)[SiteUtils.SiteNameAttribute];
        if (!string.IsNullOrEmpty(property) && !string.IsNullOrEmpty(((SafeDictionary<string, string>) site.Properties)[SiteUtils.FormsRootAttribute]) && !SiteUtils.excludeSites.Contains(string.Format("|{0}|", (object) property)))
        {
          string siteRoot = SiteUtils.GetSiteRoot(property);
          if (item.Paths.FullPath.StartsWith(siteRoot, StringComparison.InvariantCultureIgnoreCase) && str1.Length < siteRoot.Length)
          {
            str1 = siteRoot;
            str2 = ((SafeDictionary<string, string>) site.Properties)[SiteUtils.FormsRootAttribute];
          }
        }
      }
      return string.IsNullOrEmpty(str2) ? ((SafeDictionary<string, string>) SiteUtils.DefaultSite.Properties)[SiteUtils.FormsRootAttribute] ?? SiteUtils.WithoutRoot : str2;
    }

    public static IEnumerable<KeyValuePair<string, string>> GetFormsSites()
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (Site site in (Collection<Site>) SiteManager.GetSites())
      {
        if (!string.IsNullOrEmpty(((SafeDictionary<string, string>) site.Properties)[SiteUtils.FormsRootAttribute]) && !SiteUtils.excludeSites.Contains(string.Format("|{0}|", (object) ((SafeDictionary<string, string>) site.Properties)[SiteUtils.SiteNameAttribute])))
        {
          string property1 = ((SafeDictionary<string, string>) site.Properties)[SiteUtils.DatabaseAttribute];
          if (!string.IsNullOrEmpty(property1))
          {
            Database database = Factory.GetDatabase(property1);
            if (database != null)
            {
              string property2 = ((SafeDictionary<string, string>) site.Properties)[SiteUtils.FormsRootAttribute];
              Item obj = !ID.IsID(property2) ? database.SelectSingleItem(property2) : database.GetItem(property2);
              if (obj != null)
              {
                string key = ((object) obj.ID).ToString();
                if (!dictionary.ContainsKey(key))
                  dictionary.Add(key, ((SafeDictionary<string, string>) site.Properties)[SiteUtils.SiteNameAttribute]);
              }
            }
          }
        }
      }
      return (IEnumerable<KeyValuePair<string, string>>) dictionary;
    }

    public static string GetSiteRoot(string name)
    {
      Site site = SiteManager.GetSite(name);
      if (site == null)
        throw new ArgumentException(nameof (name), Sitecore.StringExtensions.StringExtensions.FormatWith("The '{0}' site does not exist.", new object[1]
        {
          (object) name
        }));
      return FileUtil.MakePath(((SafeDictionary<string, string>) site.Properties)[SiteUtils.RootPathAttribute] ?? string.Empty, ((SafeDictionary<string, string>) site.Properties)[SiteUtils.StartItemAttributte] ?? string.Empty);
    }

    public static string GetSiteRoot(SiteContext site)
    {
      Assert.ArgumentNotNull((object) site, nameof (site));
      return site.StartPath;
    }
  }
}
