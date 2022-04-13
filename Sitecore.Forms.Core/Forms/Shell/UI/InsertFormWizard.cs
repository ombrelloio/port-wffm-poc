// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.InsertFormWizard
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.Forms.Shell.UI.Controls;
using Sitecore.Globalization;
using Sitecore.Layouts;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Sitecore.Forms.Shell.UI
{
  public class InsertFormWizard : CreateFormWizard
  {
    protected PlaceholderList Placeholders;
    protected WizardDialogBaseXmlControl SelectPlaceholder;
    protected WizardDialogBaseXmlControl FormName;
    protected Radiobutton InsertForm;
    private string currentItemUri;

    public Item GetCurrentItem()
    {
      string queryString1 = Sitecore.Web.WebUtil.GetQueryString("id");
      string queryString2 = Sitecore.Web.WebUtil.GetQueryString("la");
      string queryString3 = Sitecore.Web.WebUtil.GetQueryString("vs");
      string queryString4 = Sitecore.Web.WebUtil.GetQueryString("db");
      Language language = Language.Parse(queryString2);
      Sitecore.Data.Version version = Sitecore.Data.Version.Parse(queryString3);
      string str = queryString4;
      return Database.GetItem(new ItemUri(queryString1, language, version, str));
    }

    protected override bool ActivePageChanging(string page, ref string newpage)
    {
      bool flag = true;
      if (!this.AnalyticsSettings.IsAnalyticsAvailable && newpage == "AnalyticsPage")
        newpage = "ConfirmationPage";
      if (!this.CheckGoalSettings(page, ref newpage))
        return flag;
      if (this.InsertForm.Checked && page == "CreateForm" && newpage == "FormName")
        newpage = "SelectForm";
      if (this.InsertForm.Checked && page == "ConfirmationPage" && newpage == "AnalyticsPage")
        newpage = "SelectPlaceholder";
      if (this.InsertForm.Checked && page == "SelectForm" && newpage == "FormName")
        newpage = "CreateForm";
      if ((page == "CreateForm" || page == "FormName") && newpage == "SelectForm")
      {
        if (((Control) this.EbFormName).Value == string.Empty)
        {
          Context.ClientPage.ClientResponse.Alert(DependenciesManager.ResourceManager.Localize("EMPTY_FORM_NAME"));
          newpage = page == "CreateForm" ? "CreateForm" : "FormName";
          return flag;
        }
        if (this.FormsRoot.Database.GetItem(this.FormsRoot.Paths.ContentPath + "/" + ((Control) this.EbFormName).Value) != null)
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.AppendFormat("'{0}' ", (object) ((Control) this.EbFormName).Value);
          stringBuilder.Append(DependenciesManager.ResourceManager.Localize("IS_NOT_UNIQUE_NAME"));
          Context.ClientPage.ClientResponse.Alert(stringBuilder.ToString());
          newpage = page == "CreateForm" ? "CreateForm" : "FormName";
          return flag;
        }
        if (!Regex.IsMatch(((Control) this.EbFormName).Value, Sitecore.Configuration.Settings.ItemNameValidation, RegexOptions.ECMAScript))
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.AppendFormat("'{0}' ", (object) ((Control) this.EbFormName).Value);
          stringBuilder.Append(DependenciesManager.ResourceManager.Localize("IS_NOT_VALID_NAME"));
          Context.ClientPage.ClientResponse.Alert(stringBuilder.ToString());
          newpage = page == "CreateForm" ? "CreateForm" : "FormName";
          return flag;
        }
        if (this.CreateBlankForm.Checked)
        {
          newpage = !string.IsNullOrEmpty(this.Placeholder) ? "ConfirmationPage" : "SelectPlaceholder";
          if (this.AnalyticsSettings.IsAnalyticsAvailable && newpage == "ConfirmationPage")
            newpage = "AnalyticsPage";
        }
      }
      if (page == "SelectForm" && (newpage == "SelectPlaceholder" || newpage == "ConfirmationPage" || newpage == "AnalyticsPage"))
      {
        string selected = this.multiTree.Selected;
        Item obj = StaticSettings.GlobalFormsRoot.Database.GetItem(selected);
        if (selected == null || obj == null)
        {
          Context.ClientPage.ClientResponse.Alert(DependenciesManager.ResourceManager.Localize("PLEASE_SELECT_FORM"));
          newpage = "SelectForm";
          return flag;
        }
        if (obj.TemplateID!=IDs.FormTemplateID)
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.AppendFormat("'{0}' ", (object) obj.Name);
          stringBuilder.Append(DependenciesManager.ResourceManager.Localize("IS_NOT_FORM"));
          Context.ClientPage.ClientResponse.Alert(stringBuilder.ToString());
          newpage = "SelectForm";
          return flag;
        }
      }
      if (newpage == "SelectPlaceholder" && page == "AnalyticsPage")
        newpage = string.IsNullOrEmpty(this.Placeholder) ? "SelectPlaceholder" : "SelectForm";
      if (newpage == "SelectPlaceholder" && page == "SelectForm" && !this.InsertForm.Checked)
        newpage = string.IsNullOrEmpty(this.Placeholder) ? "SelectPlaceholder" : (!this.AnalyticsSettings.IsAnalyticsAvailable ? "ConfirmationPage" : "AnalyticsPage");
      if (newpage == "SelectPlaceholder" && page == "SelectForm" && this.InsertForm.Checked)
        newpage = string.IsNullOrEmpty(this.Placeholder) ? "SelectPlaceholder" : "ConfirmationPage";
      if (page == "ConfirmationPage" && newpage == "ConfirmationPage" && !this.AnalyticsSettings.IsAnalyticsAvailable)
        newpage = string.IsNullOrEmpty(this.Placeholder) ? "SelectPlaceholder" : "SelectForm";
      if (page == "ConfirmationPage" && (newpage == "SelectPlaceholder" || newpage == "AnalyticsPage"))
      {
        if (newpage != "AnalyticsPage")
          newpage = string.IsNullOrEmpty(this.Placeholder) ? "SelectPlaceholder" : "SelectForm";
        ((Control) this.NextButton).Disabled = false;
        ((Control) this.BackButton).Disabled = false;
        ((HeaderedItemsControl) this.CancelButton).Header = "Cancel";
        ((HeaderedItemsControl) this.NextButton).Header = "Next >";
      }
      if (page == "SelectPlaceholder" && (newpage == "ConfirmationPage" || newpage == "AnalyticsPage"))
      {
        if (string.IsNullOrEmpty(this.ListValue))
        {
          newpage = "SelectPlaceholder";
          Context.ClientPage.ClientResponse.Alert(DependenciesManager.ResourceManager.Localize("SELECT_MUST_SELECT_PLACEHOLDER"));
        }
        if (this.InsertForm.Checked)
          newpage = "ConfirmationPage";
      }
      if (((page == "ConfirmationPage" || page == "AnalyticsPage") && newpage == "SelectForm" || page == "SelectPlaceholder" && newpage == "SelectForm") && this.CreateBlankForm.Checked)
        newpage = "CreateForm";
      if (newpage == "ConfirmationPage")
        this.ChoicesLiteral.Text = this.RenderSetting();
      return flag;
    }

    protected override string GenerateItemSetting()
    {
      string str1 = this.ListValue ?? this.Placeholder;
      string str2 = ((Control) this.EbFormName).Value;
      Item obj = Database.GetItem(ItemUri.Parse(this.currentItemUri));
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<p>");
      Item formsRootItemForItem = SiteUtils.GetFormsRootItemForItem(obj);
      stringBuilder.AppendFormat(DependenciesManager.ResourceManager.Localize("FORM_ADDED_MESSAGE"), (object) obj.Name, (object) str1, (object) formsRootItemForItem.Paths.FullPath, (object) str2);
      stringBuilder.Append("</p>");
      return stringBuilder.ToString();
    }

    protected override void Localize()
    {
      base.Localize();
      (this.SelectPlaceholder)["Header"] = (object) DependenciesManager.ResourceManager.Localize("SELECT_PLACEHOLDER");
      (this.SelectPlaceholder)["Text"] = (object) DependenciesManager.ResourceManager.Localize("FORM_WILL_BE_INSERTED_INTO_PLACEHOLDER");
      this.InsertForm.Header = DependenciesManager.ResourceManager.Localize("INSERT_FORM");
      (this.CreateForm)["Header"] = (object) DependenciesManager.ResourceManager.Localize("INSERT_FORM_HEADER");
      (this.CreateForm)["Text"] = (object) DependenciesManager.ResourceManager.Localize("INSERT_FORM_TEXT");
      (this.FormName)["Header"] = (object) DependenciesManager.ResourceManager.Localize("ENTER_FORM_NAME_HEADER");
      (this.FormName)["Text"] = (object) DependenciesManager.ResourceManager.Localize("ENTER_FORM_NAME_TEXT");
    }

    protected override void OnLoad(EventArgs e)
    {
      if (!Context.ClientPage.IsEvent)
      {
        this.currentItemUri = ((object) this.GetCurrentItem().Uri).ToString();
        this.Localize();
      }
      base.OnLoad(e);
      if (!Context.ClientPage.IsEvent)
      {
        Item currentItem = this.GetCurrentItem();
        ((Control) this.EbFormName).Value = this.GetUniqueName(currentItem.Name);
        this.Layout = ((BaseItem) currentItem)[(ID) FieldIDs.LayoutField];
        this.Placeholders.DeviceID = this.DeviceID;
        this.Placeholders.ShowDeviceTree = string.IsNullOrEmpty(this.Mode);
        this.Placeholders.ItemUri = this.currentItemUri;
        this.Placeholders.AllowedRendering = ((object) StaticSettings.GetRendering(currentItem)).ToString();
      }
      else
        this.currentItemUri = ((BaseForm) this).ServerProperties["forms_current_item"] as string;
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      ((BaseForm) this).ServerProperties["forms_current_item"] = (object) this.currentItemUri;
    }

    protected override void ActivePageChanged(string page, string oldPage)
    {
      base.ActivePageChanged(page, oldPage);
      if (!(page == "ConfirmationPage") || !this.InsertForm.Checked)
        return;
      ((HeaderedItemsControl) this.CancelButton).Header = "Cancel";
      ((HeaderedItemsControl) this.NextButton).Header = "Insert";
    }

    protected override void OnNext(object sender, EventArgs formEventArgs)
    {
      if (((HeaderedItemsControl) this.NextButton).Header == "Create" || ((HeaderedItemsControl) this.NextButton).Header == "Insert")
      {
        this.SaveForm();
        SheerResponse.SetModified(false);
      }
      this.Next();
    }

    protected override void SaveForm()
    {
      string deviceId = this.Placeholders.DeviceID;
      Item form;
      if (!this.InsertForm.Checked)
      {
        base.SaveForm();
        form = Database.GetItem(ItemUri.Parse((string) ((BaseForm) this).ServerProperties[this.newFormUri]));
      }
      else
      {
        string queryString = Sitecore.Web.WebUtil.GetQueryString("la");
        Language contentLanguage = Context.ContentLanguage;
        if (!string.IsNullOrEmpty(queryString))
          Language.TryParse(Sitecore.Web.WebUtil.GetQueryString("la"), out contentLanguage);
        form = this.FormsRoot.Database.GetItem(this.CreateBlankForm.Checked ? string.Empty : this.multiTree.Selected, contentLanguage);
      }
      if (this.Mode != StaticSettings.DesignMode && this.Mode != "edit")
      {
        Item obj = Database.GetItem(ItemUri.Parse(this.currentItemUri));
        LayoutDefinition definition = LayoutDefinition.Parse(LayoutField.GetFieldValue(((BaseItem) obj).Fields[(ID) FieldIDs.LayoutField]));
        RenderingDefinition rendering = new RenderingDefinition();
        string listValue = this.ListValue;
        ID rendering1 = StaticSettings.GetRendering(definition);
        rendering.ItemID = ((object) rendering1).ToString();
        if (rendering.ItemID == ((object) IDs.FormInterpreterID).ToString())
          rendering.Parameters = "FormID=" + (object) form.ID;
        else
          rendering.Datasource = ((object) form.ID).ToString();
        rendering.Placeholder = listValue;
        DeviceDefinition device = definition.GetDevice(deviceId);
        List<RenderingDefinition> renderings = device.GetRenderings(rendering.ItemID);
        if (rendering1!=IDs.FormMvcInterpreterID && ((IEnumerable<RenderingDefinition>) renderings).Any<RenderingDefinition>((Func<RenderingDefinition, bool>) (x =>
        {
          if (x.Parameters != null && x.Parameters.Contains(rendering.Parameters))
            return true;
          return x.Datasource != null && x.Datasource.Contains(((object) form.ID).ToString());
        })))
        {
          Context.ClientPage.ClientResponse.Alert(DependenciesManager.ResourceManager.Localize("FORM_CANT_BE_INSERTED"));
        }
        else
        {
          obj.Editing.BeginEdit();
          device.AddRendering(rendering);
          if (obj.Name != "__Standard Values")
            LayoutField.SetFieldValue(((BaseItem) obj).Fields[(ID) FieldIDs.LayoutField], ((XmlSerializable) definition).ToXml());
          else
            ((BaseItem) obj)[(ID) FieldIDs.LayoutField] = ((XmlSerializable) definition).ToXml();
          obj.Editing.EndEdit();
        }
      }
      else
      {
        LayoutDefinition definition = LayoutDefinition.Parse(LayoutField.GetFieldValue(((BaseItem) Database.GetItem(ItemUri.Parse(this.currentItemUri))).Fields[(ID) FieldIDs.LayoutField]));
        RenderingDefinition rendering = new RenderingDefinition();
        string listValue = this.ListValue;
        ID rendering2 = StaticSettings.GetRendering(definition);
        rendering.ItemID = ((object) rendering2).ToString();
        rendering.Parameters = "FormID=" + (object) form.ID;
        rendering.Datasource = ((object) form.ID).ToString();
        rendering.Placeholder = listValue;
        List<RenderingDefinition> renderings = definition.GetDevice(deviceId).GetRenderings(rendering.ItemID);
        if (rendering2!=IDs.FormMvcInterpreterID && ((IEnumerable<RenderingDefinition>) renderings).Any<RenderingDefinition>((Func<RenderingDefinition, bool>) (x =>
        {
          if (x.Parameters != null && x.Parameters.Contains(rendering.Parameters))
            return true;
          return x.Datasource != null && x.Datasource.Contains(((object) form.ID).ToString());
        })))
          Context.ClientPage.ClientResponse.Alert(DependenciesManager.ResourceManager.Localize("FORM_CANT_BE_INSERTED"));
        else
          SheerResponse.SetDialogValue(((object) form.ID).ToString());
      }
    }

    public bool IsCalledFromPageEditor => Sitecore.Web.WebUtil.GetQueryString("pe", "0") == "1";

    public string DeviceID => Sitecore.Web.WebUtil.GetQueryString("deviceid");

    public string Layout
    {
      get => StringUtil.GetString(((BaseForm) this).ServerProperties["LayoutCurrent"]);
      set
      {
        Assert.ArgumentNotNull((object) value, nameof (value));
        ((BaseForm) this).ServerProperties["LayoutCurrent"] = (object) value;
      }
    }

    public string ListValue => this.Placeholders.SelectedPlaceholder;

    public string Mode => Sitecore.Web.WebUtil.GetQueryString("mode");

    public string Placeholder => Sitecore.Web.WebUtil.GetQueryString("placeholder");

    protected override Item FormsRoot => SiteUtils.GetFormsRootItemForItem(Database.GetItem(ItemUri.Parse(this.currentItemUri)));

    protected override bool RenderConfirmationFormSection => !this.InsertForm.Checked;
  }
}
