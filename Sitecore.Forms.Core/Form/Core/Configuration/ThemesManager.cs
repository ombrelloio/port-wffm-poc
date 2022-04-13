// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Configuration.ThemesManager
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Utility;
using Sitecore.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Sitecore.Form.Core.Configuration
{
  public class ThemesManager
  {
    public static string BrowserName => HttpContext.Current.Request.Browser.Browser;

    public static string GetColorUrl(Item form, Item contextItem) => ThemesManager.GetColorUrl(form, contextItem, false);

    public static string GetColorUrl(Item form, Item contextItem, bool deviceDependant) => ThemesManager.GetColorUrl(StaticSettings.ColorsPath, deviceDependant ? ThemesManager.BrowserName + "/" : string.Empty, ThemesManager.GetStarterKitTheme(form, contextItem) ?? ThemesManager.GetThemeName(form, FormIDs.FormFolderColorID));

    public static string GetJqueryUiCssUrl(Item form, Item contextItem) => ThemesManager.GetJqueryUiCssUrl(form, contextItem, false);

    public static string GetJqueryUiCssUrl(Item form, Item contextItem, bool deviceDependant) => ThemesManager.GetColorUrl(StaticSettings.JqueryUiCss, deviceDependant ? ThemesManager.BrowserName + "/" : string.Empty, ThemesManager.GetStarterKitTheme(form, contextItem) ?? ThemesManager.GetThemeName(form, FormIDs.FormFolderColorID));

    public static IDictionary<string, string> GetLinkDictionary(Page page)
    {
      string str = "scwfmlinkscriptmanager";
      IDictionary<string, string> dictionary = (IDictionary<string, string>) page.Items[(object) str];
      if (dictionary == null)
      {
        dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
        page.Items[(object) str] = (object) dictionary;
      }
      return dictionary;
    }

    public static string GetThemeUrl(Item form) => ThemesManager.GetThemeUrl(form, false);

    public static string GetThemeUrl(Item form, bool deviceDependant) => ThemesManager.ReplaceSpaces(string.Join(string.Empty, new string[3]
    {
      Sitecore.StringExtensions.StringExtensions.FormatWith(StaticSettings.ThemesPath, new object[1]
      {
        deviceDependant ? (object) (ThemesManager.BrowserName + "/") : (object) string.Empty
      }),
      ThemesManager.GetThemeName(form, FormIDs.FormFolderThemeID),
      ".css?v=17072012"
    }));

    public static void RegisterCssScript(Page page, Item form, Item contextItem)
    {
      if (page == null)
        page = HttpContext.Current.Handler as Page;
      Assert.IsNotNull((object) page, nameof (page));
      Assert.IsNotNull((object) form, "formItem");
      if (page == null)
        return;
      IDictionary<string, string> linkDictionary = ThemesManager.GetLinkDictionary(page);
      bool flag = false;
      foreach (string scriptsTag in ThemesManager.ScriptsTags(form, contextItem))
      {
        if (!linkDictionary.ContainsKey(scriptsTag))
        {
          HtmlLink htmlLink = new HtmlLink();
          htmlLink.Href = scriptsTag;
          htmlLink.Attributes.Add("rel", "stylesheet");
          htmlLink.Attributes.Add("type", "text/css");
          if (page.Header != null)
          {
            if (!flag)
            {
              try
              {
                page.Header.Controls.Add((Control) htmlLink);
                linkDictionary.Add(scriptsTag, scriptsTag);
                continue;
              }
              catch (Exception ex)
              {
                HtmlTextWriter writer = new HtmlTextWriter((TextWriter) new StringWriter(new StringBuilder()));
                htmlLink.RenderControl(writer);
                page.ClientScript.RegisterClientScriptBlock(typeof (Page), writer.InnerWriter.ToString(), writer.InnerWriter.ToString());
                flag = true;
                continue;
              }
            }
          }
          HtmlTextWriter writer1 = new HtmlTextWriter((TextWriter) new StringWriter(new StringBuilder()));
          htmlLink.RenderControl(writer1);
          page.ClientScript.RegisterClientScriptBlock(typeof (Page), writer1.InnerWriter.ToString(), writer1.InnerWriter.ToString());
        }
      }
    }

    public static string[] ScriptsTags(Item form, Item contextItem)
    {
      List<string> scripts = new List<string>()
      {
        ThemesManager.GetThemeUrl(form),
        ThemesManager.GetJqueryUiCssUrl(form, contextItem, false),
        ThemesManager.GetColorUrl(form, contextItem, false),
        Sitecore.StringExtensions.StringExtensions.FormatWith(StaticSettings.CustomCssPath, new object[1]
        {
          (object) string.Empty
        })
      };
      string path = Sitecore.StringExtensions.StringExtensions.FormatWith(StaticSettings.CustomCssPath, new object[1]
      {
        (object) (ThemesManager.BrowserName + "/")
      });
      ThemesManager.AddScript(scripts, path);
      string themeUrl = ThemesManager.GetThemeUrl(form, true);
      ThemesManager.AddScript(scripts, themeUrl);
      string jqueryUiCssUrl = ThemesManager.GetJqueryUiCssUrl(form, contextItem, true);
      ThemesManager.AddScript(scripts, jqueryUiCssUrl);
      string colorUrl = ThemesManager.GetColorUrl(form, contextItem, true);
      ThemesManager.AddScript(scripts, colorUrl);
      return scripts.ToArray();
    }

    private static void AddScript(List<string> scripts, string path)
    {
      if (!FileUtil.Exists(path.IndexOf('?') > -1 ? path.Substring(0, path.IndexOf('?')) : path))
        return;
      scripts.Add(path);
    }

    private static string GetColorUrl(string path, string browser, string color) => ThemesManager.ReplaceSpaces(string.Join(string.Empty, new string[3]
    {
      Sitecore.StringExtensions.StringExtensions.FormatWith(path, new object[1]
      {
        (object) browser
      }),
      color,
      ".css"
    }));

    private static string GetStarterKitTheme(Item form, Item contextItem)
    {
      if (!StaticSettings.IsStarterKit)
        return (string) null;
      Item[] realXpath = Sitecore.Form.Core.Utility.Utils.EvaluateRealXPath(contextItem, "./ancestor-or-self::item[@template='site root']");
      string str = (realXpath.Length == 0 || realXpath[0] == null ? (BaseItem) form.Database.SelectSingleItem(SiteUtils.GetSiteRoot(Context.Site)) : (BaseItem) realXpath[0]).Fields[StaticSettings.StarterKitThemeField].Value;
      return string.IsNullOrEmpty(str) ? "Default" : form.Database.GetItem(ID.Parse(str)).Name;
    }

    public static string GetThemeName(Item form, ID fieldId)
    {
      string str = string.Empty;
      if (Settings.UseThemeFromParent && (form.Parent.TemplateID==IDs.FormFolderTemplateID))
        str = ((object) form.Parent.ID).ToString();
      if (Sitecore.StringExtensions.StringExtensions.IsNullOrEmpty(str))
        str = SiteUtils.GetFormsRootForSite(Context.Site);
      Item obj1 = form;
      if ((form.TemplateID!=IDs.FormFolderTemplateID))
        obj1 = form.Database.GetItem(str);
      Assert.IsNotNull((object) obj1, "folder");
      if (string.IsNullOrEmpty(((BaseItem) obj1).Fields[fieldId].Value))
        return "Default";
      Item obj2 = form.Database.GetItem(((BaseItem) obj1).Fields[fieldId].Value);
      return obj2 == null || string.IsNullOrEmpty(obj2.Name) ? "Default" : obj2.Name;
    }

    private static string ReplaceSpaces(string url)
    {
      Assert.ArgumentNotNull((object) url, nameof (url));
      return url.Replace(" ", "%20");
    }
  }
}
