// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.FormSettingsDesigner
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.ContentEditor.Data;
using Sitecore.Form.Core.Utility;
using Sitecore.Forms.Core.Applications.ContentEditor;
using Sitecore.Layouts;
using Sitecore.Shell.Applications.ContentEditor.RichTextEditor;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.ContentEditor;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public class FormSettingsDesigner : Sitecore.Web.UI.XmlControls.XmlControl
  {
    public static readonly string ActiveTabKey = "/Current_User/Forms/Preview/ActiveTab";
    protected Scrollbox PropertySettings;
    protected Memo IntroHtml;
    protected Border HtmlIntroductionValidate;
    protected Inline HtmlIntroductionFix;
    protected Memo FooterHtml;
    protected Border HtmlFooterValidate;
    protected Inline HtmlFooterFix;
    protected Edit TitleEdit;
    protected Sitecore.Web.UI.XmlControls.XmlControl TitleFieldSet;
    protected Sitecore.Web.UI.HtmlControls.Literal TitleLiteral;
    protected Border TitleScope;
    protected Listbox TitleTagListbox;
    protected Sitecore.Web.UI.HtmlControls.Literal TitleTagLiteral;
    protected Border TitleTagScope;
    protected Sitecore.Web.UI.XmlControls.XmlControl IntroFieldSet;
    protected Border IntroScope;
    protected Sitecore.Web.UI.HtmlControls.Literal IntroductionLiteral;
    protected Sitecore.Web.UI.XmlControls.XmlControl FooterFieldSet;
    protected Border FooterScope;
    protected Sitecore.Web.UI.HtmlControls.Literal FooterLiteral;
    protected Sitecore.Web.UI.XmlControls.XmlControl SubmitFieldSet;
    protected Sitecore.Web.UI.HtmlControls.Literal ButtonNameLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal EditLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal SuccessLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal SuccessDescriptionLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal SuccessPageLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal SuccessMesssageLiteral;
    protected Edit SubmitEdit;
    protected Border SubmitCommands;
    protected Sitecore.Web.UI.HtmlControls.Literal SuccessPage;
    protected Sitecore.Web.UI.HtmlControls.Literal SuccessMessage;
    protected Border SuccessPageSection;
    protected Border SuccessMessageSection;
    protected Border SuccessSettings;
    protected Scrollbox Content;

    protected override void OnLoad(EventArgs e)
    {
      // ISSUE: explicit non-virtual call
      base.OnLoad(e);
      if (Sitecore.Context.ClientPage.IsEvent)
        return;
      this.IsModifiedActions = false;
      this.Localize();
      this.SaveActions = ListDefinition.Parse(this.SaveActionsValue);
      this.CheckActions = ListDefinition.Parse(this.CheckActionsValue);
      this.RefreshCommandsControl((string) null);
      this.RefreshSuccessControls();
    }

    private void Localize()
    {
      this.TitleFieldSet["Header"] = (object) DependenciesManager.ResourceManager.Localize("TITLE_CAPTION");
      this.TitleLiteral.Text = DependenciesManager.ResourceManager.Localize("TITLE_CAPTION");
      this.TitleTagLiteral.Text = DependenciesManager.ResourceManager.Localize("TITLE_TAG");
      this.IntroFieldSet["Header"] = (object) DependenciesManager.ResourceManager.Localize("INTRODUCTION");
      this.IntroductionLiteral.Text = DependenciesManager.ResourceManager.Localize("INTRODUCTION");
      this.FooterFieldSet["Header"] = (object) DependenciesManager.ResourceManager.Localize("FOOTER");
      this.FooterLiteral.Text = DependenciesManager.ResourceManager.Localize("FOOTER");
      this.SubmitFieldSet["Header"] = (object) DependenciesManager.ResourceManager.Localize("SUBMIT");
      this.ButtonNameLiteral.Text = DependenciesManager.ResourceManager.Localize("BUTTON_NAME");
      this.EditLiteral.Text = DependenciesManager.ResourceManager.Localize("EDIT");
      this.SuccessLiteral.Text = DependenciesManager.ResourceManager.Localize("SUCCESS");
      this.SuccessDescriptionLiteral.Text = DependenciesManager.ResourceManager.Localize("WHEN_FORM_SUCCESSFULLY_SUBMITTED");
      this.SuccessPageLiteral.Text = DependenciesManager.ResourceManager.Localize("SUCCESS_PAGE");
      this.SuccessMesssageLiteral.Text = DependenciesManager.ResourceManager.Localize("SUCCESS_MESSAGE");
    }

    private void RefreshSuccessControls()
    {
      ((WebControl) this.SuccessPageSection).Attributes["class"] = this.SuccessRedirect ? "scWfmSuccessChoice" : "scWfmSuccessChoiceGrey";
      ((WebControl) this.SuccessMessageSection).Attributes["class"] = !this.SuccessRedirect ? "scWfmSuccessChoice" : "scWfmSuccessChoiceGrey";
    }

    private void RefreshCommandsControl(string formDefinition)
    {
      GroupListField groupListField1 = new GroupListField();
      groupListField1.ReadOnlyMode = false;
      groupListField1.GroupClass = "scFbGroup";
      groupListField1.ListItemClass = "scFbListItem";
      groupListField1.Class = "scFbCommandsList";
      groupListField1.ItemID = this.FormID;
      groupListField1.Description = "CHECK_SHORT_DESC";
      GroupListField groupListField2 = groupListField1;
      Item obj1 = StaticSettings.ContextDatabase.GetItem(FormIDs.CheckActionTemplateFieldID);
      if (obj1 != null)
        groupListField2.Source = ((BaseItem) obj1).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldSource].Value;
      groupListField2.SetValue(this.CheckActions.ToXml());
      groupListField2.OnClick = Sitecore.StringExtensions.StringExtensions.FormatWith("javascript:return scForm.postEvent(this,event,'forms:addaction(mode=check,root={0})')", new object[1]
      {
        (object) FormIDs.CheckActionsRootID
      });
      SubmitCommands.Controls.Add(groupListField2);
      GroupListField groupListField3 = new GroupListField();
      groupListField3.ReadOnlyMode = false;
      groupListField3.GroupClass = "scFbGroup";
      groupListField3.ListItemClass = "scFbListItem";
      groupListField3.Class = "scFbCommandsList";
      groupListField3.ItemID = this.FormID;
      groupListField3.Description = "SAVE_SHORT_DESC";
      GroupListField groupListField4 = groupListField3;
      Item obj2 = StaticSettings.ContextDatabase.GetItem(FormIDs.SaveActionTemplateFieldID);
      if (obj2 != null)
      {
        groupListField4.Source = ((BaseItem) obj2).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldSource].Value;
        groupListField4.TrackingXml = this.TrackingXml;
        groupListField4.Structure = formDefinition;
      }
      groupListField4.SetValue(this.SaveActions.ToXml());
      groupListField4.OnClick = Sitecore.StringExtensions.StringExtensions.FormatWith("javascript:return scForm.postEvent(this,event,'forms:addaction(mode=save,root={0},system={1})')", new object[2]
      {
        (object) FormIDs.SaveActionsRootID,
        (object) FormIDs.SystemActionsRootID
      });
      SubmitCommands.Controls.Add(groupListField4);
    }

    public void UpdateSuccess(string message, string pageID, string page, bool redirect)
    {
      if (this.SuccessRedirect != redirect || this.SubmitPageID != pageID || this.SubmitMessage != message)
        SheerResponse.SetModified(true);
      this.SuccessRedirect = redirect;
      this.SubmitPageID = pageID;
      this.SubmitPage = page;
      this.SubmitMessage = message;
      this.RefreshSuccessControls();
      Sitecore.Context.ClientPage.ClientResponse.SetOuterHtml((SuccessSettings).ID, SuccessSettings);
    }

    public void UpdateCommands(ListDefinition definition, string formDefinition, bool save)
    {
      if (save)
      {
        if (this.SaveActions != definition)
        {
          this.SaveActions = definition;
          this.IsModifiedActions = true;
        }
      }
      else if (this.CheckActions != definition)
      {
        this.CheckActions = definition;
        this.IsModifiedActions = true;
      }
      SubmitCommands.Controls.Clear();
      this.RefreshCommandsControl(formDefinition);
      Sitecore.Context.ClientPage.ClientResponse.SetOuterHtml((SubmitCommands).ID, SubmitCommands);
    }

    internal void RefreshSaveActions() => this.SaveActions = ListDefinition.Parse(this.SaveActionsValue);

    public string FormID { get; set; }

    public string Introduce
    {
      get => IntroHtml.Value;
      set => IntroHtml.Value = value;
    }

    public string PropertyEditor
    {
      set
      {
        PropertySettings.Controls.Clear();
        PropertySettings.Controls.Add(new Sitecore.Web.UI.HtmlControls.Literal(value));
        Sitecore.Context.ClientPage.ClientResponse.SetOuterHtml((PropertySettings).ID, this.PropertySettings);
      }
    }

    public int ActiveTab
    {
      get => Registry.GetInt(FormSettingsDesigner.ActiveTabKey, 1);
      set => Registry.SetInt(FormSettingsDesigner.ActiveTabKey, value);
    }

    public string Footer
    {
      get => FooterHtml.Value;
      set => this.FooterHtml.Value = value;
    }

    public string SubmitMessage
    {
      get => (string) ViewState["successmessage"];
      set
      {
        if (this.SuccessRedirect)
          this.SuccessMessage.Text = Sitecore.StringExtensions.StringExtensions.FormatWith("<span style=\"color:#999999\" margin='0px 0px 0px 10px' padding='0px 0px 0px 10px'>[{0}]</span>", new object[1]
          {
            (object) DependenciesManager.ResourceManager.Localize("NONE")
          });
        else
          this.SuccessMessage.Text = value;
        ViewState["successmessage"] = (object) value;
      }
    }

    public string SubmitPage
    {
      get
      {
        Match match = Regex.Match(this.SuccessPage.Text, ">.*<");
        if (!match.Success)
          return this.SuccessPage.Text;
        return match.Value.Trim('>', '<');
      }
      set
      {
                Sitecore.Web.UI.HtmlControls.Literal successPage = this.SuccessPage;
        string str;
        if (!this.SuccessRedirect)
          str = Sitecore.StringExtensions.StringExtensions.FormatWith("<span style=\"color:#999999\" margin='0px 0px 0px 10px' padding='0px 0px 0px 10px'>[{0}]</span>", new object[1]
          {
            (object) DependenciesManager.ResourceManager.Localize("NONE")
          });
        else
          str = Sitecore.StringExtensions.StringExtensions.FormatWith("<a target='_blank' href='{0}'>{0}</a>", new object[1]
          {
            (object) value
          });
        successPage.Text = str;
      }
    }

    public string SubmitPageID
    {
      get => (string) ViewState["successpageid"];
      set
      {
        if (!this.SuccessRedirect)
          return;
        ViewState["successpageid"] = (object) value;
      }
    }

    public bool SuccessRedirect
    {
      get => (bool) ViewState["successredirect"];
      set => ViewState["successredirect"] = (object) value;
    }

    public bool IsModifiedActions
    {
      get => (bool) ViewState[nameof (IsModifiedActions)];
      set => ViewState[nameof (IsModifiedActions)] = (object) value;
    }

    public string SubmitName
    {
      get => SubmitEdit.Value;
      set => SubmitEdit.Value = value;
    }

    public string TitleName
    {
      get => TitleEdit.Value;
      set => TitleEdit.Value = value;
    }

    public HtmlTextWriterTag SelectedTitleTag
    {
      get
      {
        HtmlTextWriterTag result;
        return Enum.TryParse<HtmlTextWriterTag>(TitleTagListbox.Value, out result) ? result : HtmlTextWriterTag.H1;
      }
      set => TitleTagListbox.Value = value.ToString();
    }

    public string[] TitleTags
    {
      get => ((IEnumerable<Sitecore.Web.UI.HtmlControls.ListItem>) this.TitleTagListbox.Items).Select<Sitecore.Web.UI.HtmlControls.ListItem, string>((Func<Sitecore.Web.UI.HtmlControls.ListItem, string>) (li => li.Name)).ToArray<string>();
      set
      {
        TitleTagListbox.Controls.Clear();
        foreach (string s in value)
        {
          string str = HttpUtility.HtmlEncode(s);
          ControlCollection controls = TitleTagListbox.Controls;
          var listItem = new Sitecore.Web.UI.HtmlControls.ListItem();
          listItem.Header = str;
          listItem.Value = str;
          controls.Add(listItem);
        }
      }
    }

    public string TrackingXml
    {
      get => (string) ViewState["tracking"];
      set => ViewState["tracking"] = (object) value;
    }

    public ListDefinition CheckActions
    {
      get => ViewState["listcheckdefinition"] as ListDefinition;
      private set
      {
        ListDefinition listDefinition = value;
        if (!listDefinition.Groups.Any<IGroupDefinition>())
        {
          GroupDefinition groupDefinition = new GroupDefinition()
          {
            DisplayName = DependenciesManager.ResourceManager.Localize("FORM_VERIFICATION"),
            ID = ((object) FormIDs.CheckActionsRootID).ToString()
          };
          listDefinition.AddGroup((IGroupDefinition) groupDefinition);
        }
        ViewState.Add("listcheckdefinition", (object) listDefinition);
      }
    }

    public ListDefinition SaveActions
    {
      get => ViewState["listdefinition"] as ListDefinition;
      private set
      {
        ListDefinition listDefinition = value;
        if (!listDefinition.Groups.Any<IGroupDefinition>())
        {
          GroupDefinition groupDefinition = new GroupDefinition()
          {
            DisplayName = DependenciesManager.ResourceManager.Localize("SAVE_ACTIONS"),
            ID = ((object) FormIDs.SaveActionsRootID).ToString()
          };
          listDefinition.AddGroup((IGroupDefinition) groupDefinition);
        }
        ViewState.Add("listdefinition", (object) listDefinition);
      }
    }

    public string SaveActionsValue { get; set; }

    public string CheckActionsValue { get; set; }

    public void Validate(string ctrl)
    {
      Memo control1 = FindControl(ctrl) as Memo;
      Border control2 = FindControl(ctrl + "validate") as Border;
      if (control1 == null || control2 == null)
        return;
      this.ValidateControl(control1.Value, control2, true);
    }

    private bool ValidateControl(string text, Border validator, bool updateOnClient)
    {
      bool flag = false;
      Collection<XHtmlValidatorError> collection = Sitecore.Form.Core.Utility.Utils.ValidateText(text);
      string str = string.Empty;
      if (collection.Count > 0)
      {
        flag = true;
        foreach (XHtmlValidatorError xhtmlValidatorError in collection)
          str = str + xhtmlValidatorError.Message + "\n\t";
      }
      ((WebControl) validator).Attributes["class"] = str;
      if (updateOnClient)
        Sitecore.Context.ClientPage.ClientResponse.SetAttribute(validator.ID, "title", str);
      if (str == string.Empty)
        validator.Class = "scFbValidate";
      else
        validator.Class = "scFbNotValide";
      if (updateOnClient)
        Sitecore.Context.ClientPage.ClientResponse.SetAttribute(validator.ID, "class", validator.Class);
      return flag;
    }

    protected virtual void EditHtml(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (args.IsPostBack)
      {
        if (args.Result == null || !(args.Result != "undefined"))
          return;
        this.UpdateHtml(args);
      }
      else
      {
        UrlString urlString = new UrlString("/sitecore/shell/~/xaml/Sitecore.Shell.Applications.ContentEditor.Dialogs.EditHtml.aspx");
        UrlHandle urlHandle = new UrlHandle();
        if (FindControl(args.Parameters["id"]) is Memo control2)
        {
          string empty = control2.Value;
          if (empty == "__#!$No value$!#__")
            empty = string.Empty;
          string str = RuntimeHtml.Convert(empty, Sitecore.Configuration.Settings.HtmlEditor.SupportWebControls);
          urlHandle["html"] = str;
        }
        urlHandle.Add(urlString);
        SheerResponse.ShowModalDialog(((object) urlString).ToString(), "800px", "500px", string.Empty, true);
        args.WaitForPostBack();
      }
    }

    private void UpdateHtml(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      string str1 = args.Result;
      if (str1 == "__#!$No value$!#__")
        str1 = string.Empty;
      string str2 = RuntimeHtml.Convert(str1, Sitecore.Configuration.Settings.HtmlEditor.SupportWebControls);
      SheerResponse.Eval("Sitecore.FormBuilder.setRichText('" + args.Parameters["id"] + "'," + StringUtil.EscapeJavascriptString(str2) + ");");
    }

    protected virtual void EditText(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (args.IsPostBack)
      {
        if (args.Result == null || !(args.Result != "undefined"))
          return;
        this.UpdateHtml(args);
      }
      else
      {
        RichTextEditorUrl richTextEditorUrl = new RichTextEditorUrl();
        richTextEditorUrl.Conversion = (RichTextEditorUrl.HtmlConversion) 2;
        richTextEditorUrl.Disabled = false;
        richTextEditorUrl.FieldID = string.Empty;
        richTextEditorUrl.ID = ID;
        richTextEditorUrl.ItemID = string.Empty;
        richTextEditorUrl.Language = string.Empty;
        richTextEditorUrl.Mode = string.Empty;
        richTextEditorUrl.Source = string.Empty;
        richTextEditorUrl.Url = UIUtil.GetUri("control:RichTextEditor");
        if (FindControl(args.Parameters["id"]) is Memo control2)
          richTextEditorUrl.Value = control2.Value;
        richTextEditorUrl.Version = string.Empty;
        SheerResponse.ShowModalDialog(((object) richTextEditorUrl.GetUrl()).ToString(), "800px", "500px", string.Empty, true);
        args.WaitForPostBack();
      }
    }

    protected virtual void Fix(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (args.IsPostBack)
      {
        if (args.Result == null || !(args.Result != "undefined"))
          return;
        this.UpdateHtml(args);
      }
      else
      {
        UrlString urlString = new UrlString("/sitecore/shell/~/xaml/Sitecore.Shell.Applications.ContentEditor.Dialogs.FixHtml.aspx");
        UrlHandle urlHandle = new UrlHandle();
        if (FindControl(args.Parameters["id"]) is Memo control2)
        {
          string empty = control2.Value;
          if (empty == "__#!$No value$!#__")
            empty = string.Empty;
          string str = RuntimeHtml.Convert(empty, Sitecore.Configuration.Settings.HtmlEditor.SupportWebControls);
          urlHandle["html"] = str;
        }
        urlHandle.Add(urlString);
        SheerResponse.ShowModalDialog(((object) urlString).ToString(), "800px", "500px", string.Empty, true);
        args.WaitForPostBack();
      }
    }

    public Sitecore.Web.UI.HtmlControls.Control ShowEmptyForm()
    {
      PropertySettings.Controls.Clear();
      PropertySettings.Controls.Add(XmlResourceUtil.GetResourceControl("Forms.EmptyFormMessage", new NameValueCollection()
      {
        {
          "Title",
          DependenciesManager.ResourceManager.Localize("EMPTY_FORM_TITLE")
        },
        {
          "Desc",
          string.Empty
        },
        {
          "ID",
          ((object) ShortID.NewId()).ToString()
        }
      }));
      SheerResponse.SetOuterHtml(PropertySettings.ID, PropertySettings);
      return PropertySettings;
    }
  }
}
