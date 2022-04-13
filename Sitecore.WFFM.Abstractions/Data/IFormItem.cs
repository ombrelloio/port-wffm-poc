// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Data.IFormItem
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Globalization;
using Sitecore.WFFM.Abstractions.ContentEditor;
using Sitecore.WFFM.Abstractions.Data.Enums;
using System.Web.UI;

namespace Sitecore.WFFM.Abstractions.Data
{
  public interface IFormItem
  {
    IListDefinition ActionsDefinition { get; }

    string CheckActions { get; set; }

    IFieldItem[] Fields { get; }

    string Footer { get; }

    string FooterFieldName { get; }

    string FormName { get; }

    bool HasSections { get; }

    string Introduction { get; }

    string IntroductionFieldName { get; }

    [Required("IsXdbTrackerEnabled", true)]
    bool IsAnalyticsEnabled { get; }

    bool IsDropoutTrackingEnabled { get; }

    bool IsAjaxMvcForm { get; }

    bool IsSaveFormDataToStorage { get; }

    Language Language { get; }

    string LeftColumnStyle { get; }

    string RightColumnStyle { get; }

    string FormAlignment { get; }

    HtmlTextWriterTag TitleTag { get; }

    FormType FormType { get; }

    string CustomCss { get; }

    string Parameters { get; }

    string FormTypeClass { get; }

    string ProfileItem { get; }

    string SaveActions { get; set; }

    string LocalizedSaveActions { get; set; }

    Sitecore.Data.Items.Item[] SectionItems { get; }

    Sitecore.Data.Items.Item[] Sections { get; }

    string SubmitButtonType { get; }

    string SubmitButtonPosition { get; }

    string SubmitButtonSize { get; }

    bool ShowFooter { get; }

    bool ShowIntroduction { get; }

    bool ShowTitle { get; }

    string SubmitName { get; set; }

    string SuccessMessage { get; }

    LinkField SuccessPage { get; }

    ID SuccessPageID { get; }

    bool SuccessRedirect { get; }

    ITracking Tracking { get; }

    ItemUri Uri { get; }

    Version Version { get; }

    Database Database { get; }

    string DisplayName { get; }

    string Icon { get; }

    ID ID { get; }

    Sitecore.Data.Items.Item InnerItem { get; }

    string Name { get; }

    IFieldItem[] this[string section] { get; }

    Sitecore.Data.Items.Item AddFormField(string fieldName, string type, bool isValidate);

    IFieldItem GetField(ID fieldID);

    IFieldItem[] GetFields(Sitecore.Data.Items.Item section);

    Sitecore.Data.Items.Item GetSection(string id);

    void BeginEdit();

    void EndEdit();
  }
}
