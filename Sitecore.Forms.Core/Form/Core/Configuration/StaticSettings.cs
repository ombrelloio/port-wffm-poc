// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Configuration.StaticSettings
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Layouts;
using Sitecore.Mvc.Configuration;
using Sitecore.Mvc.Extensions;
using Sitecore.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Sitecore.Form.Core.Configuration
{
  public class StaticSettings
  {
    public static readonly string AssemblyFieldName = "Assembly";
    public static readonly string ClassFieldName = "Class";
    public static readonly string ThemesPath = "/sitecore modules/shell/Web Forms for Marketers/themes/{0}";
    public static readonly string ColorsPath = StaticSettings.ThemesPath + "colors/";
    public static readonly string CommandsRoot = "{E5EABB1F-40BC-45BB-8D87-3B6C239B521B}";
    public static readonly string ContentPath = "/sitecore/content";
    public static readonly string CustomCssPath = string.Join(string.Empty, new string[2]
    {
      StaticSettings.ThemesPath.Replace(" ", "%20"),
      "Custom.css"
    });
    public static readonly string MvcValidationMessagesPath = "/sitecore/system/Modules/Web Forms for Marketers/Settings/Meta data/Mvc Validation Error Messages";
    public static readonly string DataSourceFieldID = "{0793DCFF-0EE7-48AD-B9D4-EF1DD24EB59B}";
    public static readonly string DesignMode = "design";
    public static readonly string FormFieldID = "{B1C581F7-F3BD-466C-90DD-00BCA5FB1635}";
    public static readonly string GlobalFormsRootID = "{4F42E032-6174-4A79-B3B0-5056494D6B39}";
    public static readonly string GoalsRootID = "{0CB97A9F-CAFB-42A0-8BE1-89AB9AE32BD9}";
    public static readonly string JqueryUiCss = StaticSettings.ColorsPath + "/jquery-ui.custom.";
    public static readonly string LocalFormFolderRoot = "{AD45E5EF-6CBF-49D5-9A10-4A3103369C2B}";
    public static readonly string LocalFormsRootID = "{AD45E5EF-6CBF-49D5-9A10-4A3103369C2B}";
    public static readonly string Mode = nameof (Mode);
    public static readonly string NormalMode = "normal";
    public static readonly string PlaceholderKeyId = "placeholderKey";
    public static readonly string SourceMarker = "source:";
    public static readonly string StarterKitRevision = "/sitecore/content/Meta-Data/Starter Kit Revision";
    public static readonly string StarterKitThemeField = "Theme";
    public static readonly string SubmitActionsMenu = "/sitecore/content/Applications/Modules/Web Forms for Marketers/Form Designer/Menues/Submit Actions";
    public static readonly string PrefixId = "pb_forms_";
    public static readonly int PrefixIdLength = StaticSettings.PrefixId.Length;
    public static readonly string PrefixLocalizeId = "loc_";
    public static readonly int PrefixLocalizeIdLength = StaticSettings.PrefixLocalizeId.Length;
    public static readonly string ScriptPath = "/sitecore/shell/Applications/Modules/Web%20Forms%20for%20Marketers/script/";
    public static readonly string VisitorIdentificationPath = "/layouts/system/visitoridentificationextension.aspx";
    internal static readonly string SessionDataFormPrefix = "data_form";
    private static readonly Database CoreDatabasePrivate;
    private static readonly Database MasterDatabasePrivate = Factory.GetDatabase(Settings.MasterDatabaseName, false);
    private static readonly Database WebDatabasePrivate;

    static StaticSettings()
    {
      StaticSettings.CoreDatabasePrivate = Factory.GetDatabase(Settings.CoreDatabaseName, false);
      StaticSettings.WebDatabasePrivate = Factory.GetDatabase(Settings.WebDatabaseName, false);
    }

    public static Database ContextDatabase
    {
      get
      {
        if (Context.Database == null)
          return StaticSettings.MasterDatabase ?? StaticSettings.WebDatabase;
        return Context.Database.Name == Settings.CoreDatabaseName ? Context.ContentDatabase : Context.Database;
      }
    }

    public static Database CoreDatabase => StaticSettings.CoreDatabasePrivate;

    public static Database WebDatabase => StaticSettings.WebDatabasePrivate;

    public static Item CssRoot => StaticSettings.ContextDatabase.GetItem(IDs.CssClassRoot);

    public static Item TitleTagsRoot => StaticSettings.ContextDatabase.GetItem(IDs.TitleTagsRoot);

    public static Item GlobalFormsRoot => StaticSettings.MasterDatabase.GetItem(StaticSettings.GlobalFormsRootID);

        //public static bool IsStarterKit => StaticSettings.ContextDatabase.SelectSingleItem(StaticSettings.StarterKitRevision) != null;
        public static bool IsStarterKit => StaticSettings.ContextDatabase.GetItem(StaticSettings.StarterKitRevision) != null;

        public static Database MasterDatabase => StaticSettings.MasterDatabasePrivate;

    internal static IEnumerable<WebEditButton> FormsWebEditButtons
    {
      get
      {
        List<WebEditButton> webEditButtonList = new List<WebEditButton>();
        if (StaticSettings.CoreDatabase != null)
        {
          Item obj = StaticSettings.CoreDatabase.GetItem(IDs.WebEditButtonsRootID);
          if (obj != null)
          {
            foreach (Item child in obj.Children)
            {
              WebEditButton webEditButton = new WebEditButton()
              {
                Header = ((BaseItem) child)["Header"],
                Icon = ((BaseItem) child)["Icon"],
                Click = ((BaseItem) child)["Click"],
                Tooltip = ((BaseItem) child)["Tooltip"]
              };
              webEditButtonList.Add(webEditButton);
            }
          }
        }
        return (IEnumerable<WebEditButton>) webEditButtonList;
      }
    }

    public static ID GetRendering(Item currentItem) => StaticSettings.GetRendering(LayoutDefinition.Parse(LayoutField.GetFieldValue(((BaseItem) currentItem).Fields[(ID)Sitecore.FieldIDs.LayoutField])));

    public static Dictionary<string, string> GetDefaultMvcValidationErrors() => ((IEnumerable<Item>) StaticSettings.ContextDatabase.GetItem(StaticSettings.MvcValidationMessagesPath, Language.Current).GetChildren()).ToDictionary<Item, string, string>((Func<Item, string>) (x => ((BaseItem) x)[FieldIDs.MvcValidationErrorKey]), (Func<Item, string>) (y => ((BaseItem) y)[FieldIDs.MvcValidationText]));

    public static ID GetRendering(LayoutDefinition definition)
    {
      string filePath = ((LayoutItem)(StaticSettings.ContextDatabase.GetItem(XDocument.Parse(((XmlSerializable) definition).ToXml()).XPathSelectElement("//d").Attribute((XName) "l").Value))).FilePath;
      return Sitecore.Mvc.Extensions.StringExtensions.IsWhiteSpaceOrNull(filePath) || !MvcSettings.IsViewExtension(System.IO.Path.GetExtension(filePath)) ? IDs.FormInterpreterID : IDs.FormMvcInterpreterID;
    }
  }
}
