// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.FormDesigner
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.ContentEditor.Data;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.Core.Visual;
using Sitecore.Forms.Core.Data;
using Sitecore.Forms.Shell.UI.Controls;
using Sitecore.Forms.Shell.UI.Dialogs;
using Sitecore.Globalization;
using Sitecore.SecurityModel;
using Sitecore.Shell.Controls.Splitters;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using Sitecore.Web.UI.WebControls.Ribbons;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.ContentEditor;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI
{
  public class FormDesigner : ApplicationForm
  {
    public static readonly string FormBuilderID = nameof (FormBuilderID);
    public static readonly string RibbonPath = "/sitecore/content/Applications/Modules/Web Forms for Marketers/Form Designer/Ribbon";
    public static System.Action saveCallback;
    public static FormDesigner savedDesigner;
    public static readonly string DefautSubmitCommand = "{745D9CF0-B189-4EAD-8D1B-8CAB68B5C972}";
    protected FormBuilder builder;
    protected GridPanel DesktopPanel;
    protected Sitecore.Web.UI.HtmlControls.Literal FieldsLabel;
    protected Sitecore.Web.UI.XmlControls.XmlControl Footer;
    protected RichTextBorder FooterGrid;
    protected VSplitterXmlControl FormsSpliter;
    protected GenericControl FormSubmit;
    protected Border FormTablePanel;
    protected Sitecore.Web.UI.HtmlControls.Literal FormTitle;
    protected Sitecore.Web.UI.XmlControls.XmlControl Intro;
    protected RichTextBorder IntroGrid;
    protected Border RibbonPanel;
    protected FormSettingsDesigner SettingsEditor;
    protected Border TitleBorder;
    private readonly IAnalyticsSettings analyticsSettings;

    public FormDesigner() => this.analyticsSettings = DependenciesManager.Resolve<IAnalyticsSettings>();

    protected override void OnLoad(EventArgs e)
    {
      if (!Context.ClientPage.IsEvent)
      {
        this.Localize();
        this.BuildUpClientDictionary();
        if (string.IsNullOrEmpty(Registry.GetString("/Current_User/VSplitters/FormsSpliter")))
          Registry.SetString("/Current_User/VSplitters/FormsSpliter", "412,");
        this.LoadControls();
        if (!this.builder.IsEmpty)
          return;
        this.SettingsEditor.ShowEmptyForm();
      }
      else
      {
        this.builder = (this.FormTablePanel).FindControl(FormDesigner.FormBuilderID) as FormBuilder;
        this.builder.UriItem = ((object) this.GetCurrentItem().Uri).ToString();
      }
    }

    private void Localize() => this.FormTitle.Text = DependenciesManager.ResourceManager.Localize("TITLE_CAPTION");

    protected virtual void BuildUpClientDictionary()
    {
      ClientScriptManager clientScript = (Context.ClientPage).ClientScript;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("Sitecore.FormBuilder.dictionary['tagDescription'] = '{0}';", (object) DependenciesManager.ResourceManager.Localize("TAG_PROPERTY_DESCRIPTION"));
      stringBuilder.AppendFormat("Sitecore.FormBuilder.dictionary['tagLabel'] = '{0}';", (object) DependenciesManager.ResourceManager.Localize("TAG_LABEL_COLON"));
      stringBuilder.AppendFormat("Sitecore.FormBuilder.dictionary['analyticsLabel']= '{0}';", (object) DependenciesManager.ResourceManager.Localize("ANALYTICS"));
      stringBuilder.AppendFormat("Sitecore.FormBuilder.dictionary['editButton']= '{0}';", (object) DependenciesManager.ResourceManager.Localize("EDIT"));
      stringBuilder.AppendFormat("Sitecore.FormBuilder.dictionary['conditionRulesLiteral']= '{0}';", (object) DependenciesManager.ResourceManager.Localize("RULES"));
      stringBuilder.AppendFormat("Sitecore.FormBuilder.dictionary['noConditions']= '{0}';", (object) DependenciesManager.ResourceManager.Localize("THERE_IS_NO_RULES_FOR_THIS_ELEMENT"));
      Type type = ((object) this).GetType();
      string script = stringBuilder.ToString();
      clientScript.RegisterClientScriptBlock(type, "sc-webform-dict", script, true);
    }

    private void ExportToAscx() => Run.ExportToAscx((BaseForm) this, this.GetCurrentItem().Uri);

    private void AddFirstFieldIfNeeded()
    {
      Item currentItem = this.GetCurrentItem();
      if (currentItem.HasChildren)
        return;
      using (new SecurityDisabler())
      {
        TemplateItem template = currentItem.Database.GetTemplate(Sitecore.Form.Core.Configuration.IDs.FieldTemplateID);
        currentItem.Add("InitialFieldItemName", template);
      }
    }

    private void LoadControls()
    {
      this.AddFirstFieldIfNeeded();
      FormItem formItem = new FormItem(this.GetCurrentItem());
      this.builder = new FormBuilder();
      (this.builder).ID = FormDesigner.FormBuilderID;
      this.builder.UriItem = ((object) formItem.Uri).ToString();
      (this.FormTablePanel).Controls.Add(this.builder);
      this.FormTitle.Text = formItem.FormName;
      if (string.IsNullOrEmpty(this.FormTitle.Text))
        this.FormTitle.Text = DependenciesManager.ResourceManager.Localize("UNTITLED_FORM");
      (this.TitleBorder).Controls.Add(new Sitecore.Web.UI.HtmlControls.Literal("<input ID=\"ShowTitle\" Type=\"hidden\"/>"));
      if (!formItem.ShowTitle)
        ((WebControl) this.TitleBorder).Style.Add("display", "none");
      this.SettingsEditor.TitleName = this.FormTitle.Text;
      this.SettingsEditor.TitleTags = ((IEnumerable<Item>) StaticSettings.TitleTagsRoot.Children).Select<Item, string>((Func<Item, string>) (ch => ch.Name)).ToArray<string>();
      this.SettingsEditor.SelectedTitleTag = formItem.TitleTag;
      (this.Intro).Controls.Add(new Sitecore.Web.UI.HtmlControls.Literal("<input ID=\"ShowIntro\" Type=\"hidden\"/>"));
      this.IntroGrid.Value = formItem.Introduction;
      if (string.IsNullOrEmpty(this.IntroGrid.Value))
        this.IntroGrid.Value = DependenciesManager.ResourceManager.Localize("FORM_INTRO_EMPTY");
      if (!formItem.ShowIntroduction)
        ((WebControl) this.Intro).Style.Add("display", "none");
      this.IntroGrid.FieldName = formItem.IntroductionFieldName;
      this.SettingsEditor.FormID = this.CurrentItemID;
      this.SettingsEditor.Introduce = this.IntroGrid.Value;
      this.SettingsEditor.SaveActionsValue = ActionUtil.OverrideParameters(formItem.SaveActions, formItem.LocalizedSaveActions);
      this.SettingsEditor.CheckActionsValue = formItem.CheckActions;
      this.SettingsEditor.TrackingXml = formItem.Tracking.ToString();
      this.SettingsEditor.SuccessRedirect = formItem.SuccessRedirect;
      if (formItem.SuccessPage.TargetItem != null)
      {
        Language language;
        if (!Language.TryParse(Sitecore.Web.WebUtil.GetQueryString("la"), out language))
          language = Context.Language;
        this.SettingsEditor.SubmitPage = Sitecore.Form.Core.Utility.ItemUtil.GetItemUrl(formItem.SuccessPage.TargetItem, Sitecore.Configuration.Settings.Rendering.SiteResolving, language);
      }
      else
        this.SettingsEditor.SubmitPage = formItem.SuccessPage.Url;
      if (!ID.IsNullOrEmpty(formItem.SuccessPageID))
        this.SettingsEditor.SubmitPageID = ((object) formItem.SuccessPageID).ToString();
      (this.Footer).Controls.Add(new Sitecore.Web.UI.HtmlControls.Literal("<input ID=\"ShowFooter\" Type=\"hidden\"/>"));
      this.FooterGrid.Value = formItem.Footer;
      if (string.IsNullOrEmpty(this.FooterGrid.Value))
        this.FooterGrid.Value = DependenciesManager.ResourceManager.Localize("FORM_FOOTER_EMPTY");
      if (!formItem.ShowFooter)
        ((WebControl) this.Footer).Style.Add("display", "none");
      this.FooterGrid.FieldName = formItem.FooterFieldName;
      this.SettingsEditor.Footer = this.FooterGrid.Value;
      this.SettingsEditor.SubmitMessage = formItem.SuccessMessage;
      string str = string.IsNullOrEmpty(formItem.SubmitName) ? DependenciesManager.ResourceManager.Localize("NO_BUTTON_NAME") : Sitecore.Form.Core.Configuration.Translate.TextByItemLanguage(formItem.SubmitName, formItem.Language.GetDisplayName());
      ((WebControl) this.FormSubmit).Attributes["value"] = str;
      this.SettingsEditor.SubmitName = str;
      this.UpdateRibbon();
    }

    private void UpdateRibbon()
    {
      Item currentItem = this.GetCurrentItem();
      Ribbon ribbon = new Ribbon();
      (ribbon).ID = "FormDesigneRibbon";
      ribbon.CommandContext = new CommandContext(currentItem);
      Item obj = Context.Database.GetItem(FormDesigner.RibbonPath);
      Error.AssertItemFound(obj, FormDesigner.RibbonPath);
      ribbon.CommandContext.Parameters.Add("title", (!string.IsNullOrEmpty(this.SettingsEditor.TitleName)).ToString());
      ribbon.CommandContext.Parameters.Add("intro", (!string.IsNullOrEmpty(this.SettingsEditor.Introduce)).ToString());
      ribbon.CommandContext.Parameters.Add("footer", (!string.IsNullOrEmpty(this.SettingsEditor.Footer)).ToString());
      ribbon.CommandContext.Parameters.Add("id", ((object) currentItem.ID).ToString());
      ribbon.CommandContext.Parameters.Add("la", currentItem.Language.Name);
      ribbon.CommandContext.Parameters.Add("vs", currentItem.Version.Number.ToString());
      ribbon.CommandContext.Parameters.Add("db", currentItem.Database.Name);
      ribbon.CommandContext.RibbonSourceUri = obj.Uri;
      ribbon.ShowContextualTabs = false;
      this.RibbonPanel.InnerHtml = Sitecore.Web.HtmlUtil.RenderControl(ribbon);
    }

    protected virtual void SaveFormStructure() => SheerResponse.Eval("Sitecore.FormBuilder.SaveData();");

    protected virtual void SaveFormStructure(bool refresh, System.Action callback)
    {
      FormDefinition formStucture = this.builder.FormStucture;
      bool flag = false;
      foreach (SectionDefinition section in formStucture.Sections)
      {
        if (section.Name == string.Empty && section.Deleted != "1" && section.IsHasOnlyEmptyField)
        {
          flag = true;
          break;
        }
        foreach (FieldDefinition field in section.Fields)
        {
          if (string.IsNullOrEmpty(field.Name) && field.Deleted != "1")
          {
            flag = true;
            break;
          }
        }
        if (flag)
          break;
      }
      if (flag)
      {
        FormDesigner.saveCallback = callback;
        FormDesigner.savedDesigner = this;
        ClientDialogs.Confirmation(DependenciesManager.ResourceManager.Localize("EMPTY_FIELD_NAME"), new BasePipelineMessage.ExecuteCallback(new FormDesigner.ClientDialogCallback().SaveConfirmation));
      }
      else
      {
        this.Save(refresh);
        if (callback == null)
          return;
        callback();
      }
    }

    private void Save(bool refresh)
    {
      FormItem.UpdateFormItem(this.GetCurrentItem().Database, this.CurrentLanguage, this.builder.FormStucture);
      this.SaveFormsText();
      Context.ClientPage.Modified = false;
      if (!refresh)
        return;
      this.Refresh(string.Empty);
    }

    protected virtual void SaveFormStructureAndClose()
    {
      Context.ClientPage.Modified = false;
      this.SettingsEditor.IsModifiedActions = false;
      this.SaveFormStructure(false, new System.Action(this.CloseFormWebEdit));
    }

    protected virtual void CloseFormWebEdit()
    {
      if (!this.CheckModified(true))
        return;
      object sessionValue = Sitecore.Web.WebUtil.GetSessionValue(StaticSettings.Mode);
      bool flag = sessionValue == null ? string.IsNullOrEmpty(Sitecore.Web.WebUtil.GetQueryString("formId")) : string.Compare(sessionValue.ToString(), StaticSettings.DesignMode, true) == 0;
      int num = Context.PageMode.IsExperienceEditor ? 1 : 0;
      SheerResponse.SetDialogValue(Sitecore.Web.WebUtil.GetQueryString("hdl"));
      if (this.IsWebEditForm || !flag)
      {
        if (!string.IsNullOrEmpty(this.BackUrl))
        {
          SheerResponse.Eval("window.top.location.href='" + MainUtil.DecodeName(this.BackUrl) + "'");
        }
        else
        {
          SheerResponse.Eval("if(window.parent!=null&&window.parent.parent!=null&&window.parent.parent.scManager!= null){window.parent.parent.scManager.closeWindow(window.parent);}else{}");
          SheerResponse.CloseWindow();
        }
      }
      else
        SheerResponse.CloseWindow();
    }

    private void SaveFormsText()
    {
      Item currentItem = this.GetCurrentItem();
      FormItem formItem = new FormItem(currentItem);
      currentItem.Editing.BeginEdit();
      ((BaseItem) currentItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FormTitleID].Value = this.SettingsEditor.TitleName;
      ((BaseItem) currentItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FormTitleTagID].Value = this.SettingsEditor.SelectedTitleTag.ToString();
      ((BaseItem) currentItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.ShowFormTitleID].Value = Context.ClientPage.ClientRequest.Form["ShowTitle"];
      ((BaseItem) currentItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FormIntroductionID].Value = this.SettingsEditor.Introduce;
      ((BaseItem) currentItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.ShowFormIntroID].Value = Context.ClientPage.ClientRequest.Form["ShowIntro"];
      ((BaseItem) currentItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FormFooterID].Value = this.SettingsEditor.Footer;
      ((BaseItem) currentItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.ShowFormFooterID].Value = Context.ClientPage.ClientRequest.Form["ShowFooter"];
      ((BaseItem) currentItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FormSubmitID].Value = this.SettingsEditor.SubmitName == string.Empty ? DependenciesManager.ResourceManager.Localize("NO_BUTTON_NAME") : this.SettingsEditor.SubmitName;
      ((BaseItem) currentItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.SaveActionsID].Value = ActionUtil.GetGlobalSaveActions(((BaseItem) currentItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.SaveActionsID].Value, this.SettingsEditor.SaveActions.ToXml());
      ((BaseItem) currentItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.LocalizedSaveActionsID].Value = ActionUtil.GetLocalizedSaveActions(this.SettingsEditor.SaveActions.ToXml());
      ((BaseItem) currentItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.CheckActionsID].Value = this.SettingsEditor.CheckActions.ToXml();
      ((BaseItem) currentItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.SuccessMessageID].Value = this.SettingsEditor.SubmitMessage;
      ((BaseItem) currentItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.SuccessModeID].Value = this.SettingsEditor.SuccessRedirect ? "{F4D50806-6B89-4F2D-89FE-F77FC0A07D48}" : "{3B8369A0-CC1A-4E9A-A3DB-7B086379C53B}";
      LinkField successPage = formItem.SuccessPage;
      successPage.TargetID = MainUtil.GetID(this.SettingsEditor.SubmitPageID, ID.Null);
      if (successPage.TargetItem != null)
        successPage.Url = successPage.TargetItem.Paths.Path;
      ((BaseItem) currentItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.SuccessPageID].Value = ((XmlField) successPage).Xml.OuterXml;
      currentItem.Editing.EndEdit();
    }

    private void SaveFormAnalyticsText()
    {
      Item currentItem = this.GetCurrentItem();
      currentItem.Editing.BeginEdit();
      if (((BaseItem) currentItem).Fields["__Tracking"] != null)
        ((BaseItem) currentItem).Fields["__Tracking"].Value = this.SettingsEditor.TrackingXml;
      currentItem.Editing.EndEdit();
    }

    protected void Refresh(string url) => this.builder.ReloadForm();

    public void CompareTypes(string id, string newTypeID, string oldTypeID, string propValue)
    {
      string xml = HttpUtility.UrlDecode(propValue);
      Item currentItem = this.GetCurrentItem();
      List<string> stringList = new List<string>(PropertiesFactory.CompareTypes(ParametersUtil.XmlToPairArray(xml), currentItem.Database.GetItem(newTypeID), currentItem.Database.GetItem(oldTypeID), Sitecore.Form.Core.Configuration.FieldIDs.FieldTypeAssemblyID, Sitecore.Form.Core.Configuration.FieldIDs.FieldTypeClassID));
      if (stringList.Count > 0)
        ClientDialogs.Confirmation(string.Format(DependenciesManager.ResourceManager.Localize("CHANGE_TYPE"), (object) "\n\n", (object) string.Join(",\n\t", stringList.ToArray()), (object) "\t"), new BasePipelineMessage.ExecuteCallback(new FormDesigner.ClientDialogCallback(id, oldTypeID, newTypeID).Execute));
      else
        SheerResponse.Eval(FormDesigner.GetUpdateTypeScript("yes", id, oldTypeID, newTypeID));
    }

    private static string GetUpdateTypeScript(
      string res,
      string id,
      string oldTypeID,
      string newTypeID)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("Sitecore.PropertiesBuilder.changeType('");
      stringBuilder.Append(res);
      stringBuilder.Append("','");
      stringBuilder.Append(id);
      stringBuilder.Append("','");
      stringBuilder.Append(newTypeID);
      stringBuilder.Append("','");
      stringBuilder.Append(oldTypeID);
      stringBuilder.Append("')");
      return stringBuilder.ToString();
    }

    private void UpdateSubmit()
    {
      this.SettingsEditor.FormID = this.CurrentItemID;
      this.SettingsEditor.UpdateCommands(this.SettingsEditor.SaveActions, this.builder.FormStucture.ToXml(), true);
    }

    private void LoadPropertyEditor(string typeID, string id)
    {
      Item currentItem = this.GetCurrentItem();
      Item obj = currentItem.Database.GetItem(typeID);
      if (!string.IsNullOrEmpty(typeID))
      {
        try
        {
          string str = PropertiesFactory.RenderPropertiesSection(obj, Sitecore.Form.Core.Configuration.FieldIDs.FieldTypeAssemblyID, Sitecore.Form.Core.Configuration.FieldIDs.FieldTypeClassID);
          Tracking tracking = new Tracking(this.SettingsEditor.TrackingXml, currentItem.Database);
          if (!this.analyticsSettings.IsAnalyticsAvailable || tracking.Ignore || ((BaseItem) obj)["Deny Tag"] == "1")
            str += "<input id='denytag' type='hidden'/>";
          if (string.IsNullOrEmpty(str))
            return;
          this.SettingsEditor.PropertyEditor = str;
        }
        catch
        {
        }
      }
      else
      {
        if (!(id == "Welcome"))
          return;
        this.SettingsEditor.ShowEmptyForm();
      }
    }

    private void WarningEmptyForm()
    {
      this.builder.ShowEmptyForm();
      var control = this.SettingsEditor.ShowEmptyForm();
      Context.ClientPage.ClientResponse.SetOuterHtml((control).ID, control);
    }

    private void AddNewSection(string id, string index) => this.builder.AddToSetNewSection(id, int.Parse(index));

    private void AddNewField()
    {
      this.builder.AddToSetNewField();
      SheerResponse.Eval("Sitecore.FormBuilder.updateStructure(true);");
      SheerResponse.Eval("$j('#f1 input:first').trigger('focus'); $j('.v-splitter').trigger('change')");
    }

    private void AddNewField(string parent, string id, string index) => this.builder.AddToSetNewField(parent, id, int.Parse(index));

    private void UpgradeToSection(string parent, string id) => this.builder.UpgradeToSection(id);

    [Sitecore.Web.UI.Sheer.HandleMessage("forms:validatetext", true)]
    private void ValidateText(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (args.IsPostBack)
        return;
      this.SettingsEditor.Validate(args.Parameters["ctrl"]);
    }

    [Sitecore.Web.UI.Sheer.HandleMessage("item:save", true)]
    private void SaveMessage(ClientPipelineArgs args)
    {
      this.SaveFormStructure(true, (System.Action) null);
      SheerResponse.Eval("Sitecore.FormBuilder.updateStructure(true);");
    }

    [Sitecore.Web.UI.Sheer.HandleMessage("item:selectlanguage", true)]
    private void SelectLanguage(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      Run.SetLanguage((BaseForm) this, this.GetCurrentItem().Uri);
    }

    [Sitecore.Web.UI.Sheer.HandleMessage("item:load", true)]
    private void ChangeLanguage(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (string.IsNullOrEmpty(args.Parameters["language"]) || !this.CheckModified(true))
        return;
      Context.ClientPage.ClientResponse.SetLocation(((object) new UrlString(HttpUtility.UrlDecode(HttpContext.Current.Request.RawUrl.Replace("&amp;", "&")))
      {
        ["la"] = args.Parameters["language"]
      }).ToString());
    }

    private bool CheckModified(bool checkIfActionsModified)
    {
      if (checkIfActionsModified && this.SettingsEditor.IsModifiedActions)
      {
        Context.ClientPage.Modified = true;
        this.SettingsEditor.IsModifiedActions = false;
      }
      return SheerResponse.CheckModified();
    }

    [Sitecore.Web.UI.Sheer.HandleMessage("forms:editsuccess", true)]
    private void EditSuccess(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (!this.CheckModified(false))
        return;
      if (args.IsPostBack)
      {
        if (!args.HasResult)
          return;
        NameValueCollection nameValueCollection = ParametersUtil.XmlToNameValueCollection(args.Result);
        FormItem formItem = new FormItem(this.GetCurrentItem());
        LinkField successPage = formItem.SuccessPage;
        Item obj = formItem.Database.GetItem(nameValueCollection["page"]);
        if (!string.IsNullOrEmpty(nameValueCollection["page"]))
        {
          successPage.TargetID = MainUtil.GetID(nameValueCollection["page"], (ID) null);
          if (obj != null)
          {
            Language language;
            if (!Language.TryParse(Sitecore.Web.WebUtil.GetQueryString("la"), out language))
              language = Context.Language;
            successPage.Url = Sitecore.Form.Core.Utility.ItemUtil.GetItemUrl(obj, Sitecore.Configuration.Settings.Rendering.SiteResolving, language);
          }
        }
        this.SettingsEditor.UpdateSuccess(nameValueCollection["message"], nameValueCollection["page"], successPage.Url, nameValueCollection["choice"] == "1");
      }
      else
      {
        UrlString urlString = new UrlString(UIUtil.GetUri("control:SuccessForm.Editor"));
        UrlHandle urlHandle = new UrlHandle();
        urlHandle["message"] = this.SettingsEditor.SubmitMessage;
        if (!string.IsNullOrEmpty(this.SettingsEditor.SubmitPageID))
          urlHandle["page"] = this.SettingsEditor.SubmitPageID;
        urlHandle["choice"] = this.SettingsEditor.SuccessRedirect ? "1" : "0";
        urlHandle.Add(urlString);
        Context.ClientPage.ClientResponse.ShowModalDialog(((object) urlString).ToString(), true);
        args.WaitForPostBack();
      }
    }

    [Sitecore.Web.UI.Sheer.HandleMessage("forms:addaction", true)]
    private void OpenSetSubmitActions(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (!this.CheckModified(false))
        return;
      if (args.IsPostBack)
      {
        this.SettingsEditor.TrackingXml = UrlHandle.Get(new UrlString(args.Parameters["url"]))["tracking"];
        this.SettingsEditor.FormID = this.CurrentItemID;
        if (!args.HasResult)
          return;
        this.SettingsEditor.UpdateCommands(ListDefinition.Parse(args.Result == "-" ? string.Empty : args.Result), this.builder.FormStucture.ToXml(), args.Parameters["mode"] == "save");
      }
      else
      {
        string name = ((object) ID.NewID).ToString();
        HttpContext.Current.Session.Add(name, args.Parameters["mode"] == "save" ? (object) this.SettingsEditor.SaveActions : (object) this.SettingsEditor.CheckActions);
        UrlString urlString = new UrlString(UIUtil.GetUri("control:SubmitCommands.Editor"));
        urlString.Append("definition", name);
        urlString.Append("db", this.GetCurrentItem().Database.Name);
        urlString.Append("id", this.CurrentItemID);
        urlString.Append("la", this.CurrentLanguage.Name);
        urlString.Append("root", args.Parameters["root"]);
        urlString.Append("system", args.Parameters["system"] ?? string.Empty);
        args.Parameters.Add("params", name);
        new UrlHandle()
        {
          ["title"] = DependenciesManager.ResourceManager.Localize(args.Parameters["mode"] == "save" ? "SELECT_SAVE_TITLE" : "SELECT_CHECK_TITLE"),
          ["desc"] = DependenciesManager.ResourceManager.Localize(args.Parameters["mode"] == "save" ? "SELECT_SAVE_DESC" : "SELECT_CHECK_DESC"),
          ["actions"] = DependenciesManager.ResourceManager.Localize(args.Parameters["mode"] == "save" ? "SAVE_ACTIONS" : "CHECK_ACTIONS"),
          ["addedactions"] = DependenciesManager.ResourceManager.Localize(args.Parameters["mode"] == "save" ? "ADDED_SAVE_ACTIONS" : "ADDED_CHECK_ACTIONS"),
          ["tracking"] = this.SettingsEditor.TrackingXml,
          ["structure"] = this.builder.FormStucture.ToXml()
        }.Add(urlString);
        args.Parameters["url"] = ((object) urlString).ToString();
        Context.ClientPage.ClientResponse.ShowModalDialog(((object) urlString).ToString(), true);
        args.WaitForPostBack();
      }
    }

    [Sitecore.Web.UI.Sheer.HandleMessage("forms:configuregoal", true)]
    protected void ConfigureGoal(ClientPipelineArgs args)
    {
      Item goal = new Tracking(this.SettingsEditor.TrackingXml, Factory.GetDatabase(this.CurrentDatabase)).Goal;
      if (goal != null)
      {
        CommandContext commandContext = new CommandContext(new Item[1]
        {
          goal
        });
        CommandManager.GetCommand("item:personalize").Execute(commandContext);
      }
      else
        SheerResponse.Alert(DependenciesManager.ResourceManager.Localize("CHOOSE_GOAL_AT_FIRST"), Array.Empty<string>());
    }

    [Sitecore.Web.UI.Sheer.HandleMessage("forms:analytics", true)]
    protected void CustomizeAnalytics(ClientPipelineArgs args)
    {
      if (args.IsPostBack)
      {
        if (!args.HasResult)
          return;
        this.SettingsEditor.FormID = this.CurrentItemID;
        this.SettingsEditor.TrackingXml = args.Result;
        this.SettingsEditor.UpdateCommands(this.SettingsEditor.SaveActions, this.builder.FormStucture.ToXml(), true);
        SheerResponse.Eval("Sitecore.PropertiesBuilder.editors = [];");
        SheerResponse.Eval("Sitecore.PropertiesBuilder.setActiveProperties(Sitecore.FormBuilder, null)");
        this.SaveFormAnalyticsText();
      }
      else
      {
        UrlString urlString = new UrlString(UIUtil.GetUri("control:Forms.CustomizeAnalyticsWizard"));
        new UrlHandle()
        {
          ["tracking"] = this.SettingsEditor.TrackingXml
        }.Add(urlString);
        Context.ClientPage.ClientResponse.ShowModalDialog(((object) urlString).ToString(), true);
        args.WaitForPostBack();
      }
    }

    [Sitecore.Web.UI.Sheer.HandleMessage("forms:edititem", true)]
    protected void Edit(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (!this.CheckModified(false))
        return;
      bool save = true;
      ListDefinition definition = this.SettingsEditor.SaveActions;
      if (!definition.Groups.Any<IGroupDefinition>())
        return;
      IListItemDefinition listItem = definition.Groups.First<IGroupDefinition>().GetListItem(args.Parameters["unicid"]);
      if (listItem == null)
      {
        save = false;
        definition = this.SettingsEditor.CheckActions;
        if (definition.Groups.Any<IGroupDefinition>())
          listItem = definition.Groups.First<IGroupDefinition>().GetListItem(args.Parameters["unicid"]);
      }
      if (listItem == null)
        return;
      if (args.IsPostBack)
      {
        UrlHandle urlHandle = UrlHandle.Get(new UrlString(args.Parameters["url"]));
        this.SettingsEditor.FormID = this.CurrentItemID;
        this.SettingsEditor.TrackingXml = urlHandle["tracking"];
        if (!args.HasResult)
          return;
        listItem.Parameters = args.Result == "-" ? string.Empty : ParametersUtil.Expand(args.Result);
        this.SettingsEditor.UpdateCommands(definition, this.builder.FormStucture.ToXml(), save);
        if (!save || !listItem.Parameters.Contains(HttpUtility.HtmlEncode("<localized>")))
          return;
        this.SettingsEditor.SaveActionsValue = ActionUtil.OverrideParameters(ActionUtil.GetGlobalSaveActions(((BaseItem) this.GetCurrentItem()).Fields[Sitecore.Form.Core.Configuration.FieldIDs.SaveActionsID].Value, this.SettingsEditor.SaveActions.ToXml()), ActionUtil.GetLocalizedSaveActions(this.SettingsEditor.SaveActions.ToXml()));
        this.SettingsEditor.RefreshSaveActions();
      }
      else
      {
        string name = ((object) ID.NewID).ToString();
        HttpContext.Current.Session.Add(name, (object) listItem.Parameters);
        ActionItem actionItem = new ActionItem(StaticSettings.ContextDatabase.GetItem(listItem.ItemID));
        UrlString url = !actionItem.Editor.Contains("~/xaml/") ? new UrlString(UIUtil.GetUri(actionItem.Editor)) : new UrlString(actionItem.Editor);
        url.Append("params", name);
        url.Append("id", this.CurrentItemID);
        url.Append("actionid", listItem.ItemID);
        url.Append("la", this.CurrentLanguage.Name);
        url.Append("uniqid", listItem.Unicid);
        url.Append("db", this.CurrentDatabase);
        new UrlHandle()
        {
          ["tracking"] = this.SettingsEditor.TrackingXml,
          ["actiondefinition"] = this.SettingsEditor.SaveActions.ToXml()
        }.Add(url);
        args.Parameters["url"] = ((object) url).ToString();
        string queryString = actionItem.QueryString;
        ModalDialog.Show(url, queryString);
        args.WaitForPostBack();
      }
    }

    [Sitecore.Web.UI.Sheer.HandleMessage("list:edit", true)]
    protected void ListEdit(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (!args.IsPostBack)
      {
        UrlString urlString = new UrlString("/sitecore/shell/~/xaml/Sitecore.Forms.Shell.UI.Dialogs.ListItemsEditor.aspx");
        string name = ((object) ID.NewID).ToString();
        string str = HttpUtility.UrlDecode(args.Parameters["value"]);
        if (str.StartsWith(StaticSettings.SourceMarker))
          str = new QuerySettings("root", str.Substring(StaticSettings.SourceMarker.Length)).ToString();
        HttpContext.Current.Session.Add(name, (object) ParametersUtil.NameValueCollectionToXml(new NameValueCollection()
        {
          ["queries"] = str
        }, true));
        urlString.Append("params", name);
        urlString.Append("id", this.CurrentItemID);
        urlString.Append("db", this.CurrentDatabase);
        urlString.Append("la", this.CurrentLanguage.Name);
        urlString.Append("vs", this.CurrentVersion.Number.ToString());
        urlString.Append("target", args.Parameters["target"]);
        Context.ClientPage.ClientResponse.ShowModalDialog(((object) urlString).ToString(), true);
        args.WaitForPostBack();
      }
      else
      {
        if (!args.HasResult)
          return;
        if (args.Result == "-")
          args.Result = string.Empty;
        NameValueCollection nameValueCollection = ParametersUtil.XmlToNameValueCollection(ParametersUtil.Expand(args.Result, true), true);
        SheerResponse.SetAttribute(args.Parameters["target"], "value", HttpUtility.UrlEncode(nameValueCollection["queries"]));
        SheerResponse.Eval("Sitecore.FormBuilder.executeOnChange($('" + args.Parameters["target"] + "'));");
        if (!(HttpUtility.UrlDecode(args.Parameters["value"]) != nameValueCollection["queries"]))
          return;
        SheerResponse.SetModified(true);
      }
    }

    public override void HandleMessage(Message message)
    {
      Assert.ArgumentNotNull((object) message, nameof (message));
      base.HandleMessage(message);
      string name = message.Name;
      if (name == null)
        return;
      if (name == "forms:save")
      {
        this.SaveFormStructure(true, (System.Action) null);
      }
      else
      {
        if (string.IsNullOrEmpty(message["id"]))
          return;
        ClientPipelineArgs clientPipelineArgs = new ClientPipelineArgs();
        clientPipelineArgs.Parameters.Add("id", message["id"]);
        if (name == "richtext:edit")
          Context.ClientPage.Start((object) this.SettingsEditor, "EditText", clientPipelineArgs);
        else if (name == "richtext:edithtml")
        {
          Context.ClientPage.Start((object) this.SettingsEditor, "EditHtml", clientPipelineArgs);
        }
        else
        {
          if (!(name == "richtext:fix"))
            return;
          Context.ClientPage.Start((object) this.SettingsEditor, "Fix", clientPipelineArgs);
        }
      }
    }

    public Item GetCurrentItem() => Database.GetItem(new ItemUri(this.CurrentItemID, this.CurrentLanguage, this.CurrentVersion, this.CurrentDatabase));

    public bool IsWebEditForm => !string.IsNullOrEmpty(Sitecore.Web.WebUtil.GetQueryString("webform"));

    public Language CurrentLanguage => Language.Parse(Sitecore.Web.WebUtil.GetQueryString("la"));

    public string CurrentDatabase => Sitecore.Web.WebUtil.GetQueryString("db");

    public Sitecore.Data.Version CurrentVersion => Sitecore.Data.Version.Parse(Sitecore.Web.WebUtil.GetQueryString("vs"));

    public string BackUrl => Sitecore.Web.WebUtil.GetQueryString("backurl");

    public string CurrentItemID
    {
      get
      {
        string str = Sitecore.Web.WebUtil.GetQueryString("formid");
        if (string.IsNullOrEmpty(str))
          str = Sitecore.Web.WebUtil.GetQueryString("webform");
        if (string.IsNullOrEmpty(str))
          str = Sitecore.Web.WebUtil.GetQueryString("id");
        if (string.IsNullOrEmpty(str))
          str = Sitecore.Form.Core.Utility.Utils.GetDataSource(Sitecore.Web.WebUtil.GetQueryString());
        return str;
      }
    }

    [Serializable]
    public class ClientDialogCallback
    {
      private string id;
      private string oldTypeID;
      private string newTypeID;

      public ClientDialogCallback()
      {
      }

      public ClientDialogCallback(string id, string oldTypeID, string newTypeID)
      {
        this.id = id;
        this.oldTypeID = oldTypeID;
        this.newTypeID = newTypeID;
      }

      public void Execute(string res) => SheerResponse.Eval(FormDesigner.GetUpdateTypeScript(res, this.id, this.oldTypeID, this.newTypeID));

      public void SaveConfirmation(string result)
      {
        if (!(result == "yes"))
          return;
        FormDesigner.savedDesigner.Save(true);
        if (FormDesigner.saveCallback == null)
          return;
        FormDesigner.saveCallback();
      }

      public delegate void Action();
    }
  }
}
