// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.SendSmsPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using ComponentArt.Web.UI;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Forms.Shell.UI.Controls;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XamlSharp.Xaml;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class SendSmsPage : EditorBase
  {
    public static readonly string mailKey = "Mail";
    protected ContextMenu MailBodyContextMenu;
    protected Sitecore.Web.UI.HtmlControls.Literal SmsBodyLabel;
    protected GenericControl SmsBodyLink;
    protected Sitecore.Web.UI.HtmlControls.Image SmsBodyImg;
    protected TextBox SmsBody;
    protected DropDownList RecipientNumber;
    protected TextBox RecipientGatewayEdit;
    protected TextBox FromEdit;
    protected ControlledChecklist EditModeList;
    protected ComboBox ModeCombobox;
    protected Border ModeComboboxHolder;
    protected Sitecore.Web.UI.HtmlControls.Literal TelephoneNumberLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal SMSGatewayLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal RecipientLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal FromNumberLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal SendMessageLiteral;

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.RecipientNumber.Items.LoadItemsFromForm(this.CurrentForm).DisableAll().EnableFieldTypes(this.AllowedRecipientFieldTypes);
      this.RecipientNumber.Select(this.GetValueByKey("Recipient"));
      this.EditModeList.AddRange(ConditionalStatementUtil.GetConditionalItems(this.CurrentForm));
      this.EditModeList.SelectRange(this.GetValueByKey("EditMode", "Always"));
      if (this.FromEdit != null)
        this.FromEdit.Text = this.GetValueByKey("FromPhone", string.Empty);
      this.RecipientGatewayEdit.Text = this.GetValueByKey("RecipientGateway", string.Empty);
      this.SmsBody.Text = this.GetValueByKey(SendSmsPage.mailKey, string.Empty);
      this.ModeCombobox.Text = this.EditModeList.SelectedTitle;
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if ((this).Page.IsPostBack)
        return;
      this.FillContextMenu(this.MailBodyContextMenu, this.AllowedSmsBodyTypes, this.SmsBody, this.SmsBodyLink, this.SmsBodyImg, this.SmsBodyLabel);
      this.BuildUpClientDictionary();
    }

    protected override void Localize()
    {
      base.Localize();
      this.Header = DependenciesManager.ResourceManager.Localize("SEND_SMS");
      this.Text = DependenciesManager.ResourceManager.Localize("PLEASE_SELECT_FORM_FIELD_YOU_WANT_USE_FOR_SMS");
      this.TelephoneNumberLiteral.Text = DependenciesManager.ResourceManager.Localize("TELEPHONE_NUMBER");
      this.SMSGatewayLiteral.Text = DependenciesManager.ResourceManager.Localize("SMS_GATEWAY");
      this.RecipientLiteral.Text = DependenciesManager.ResourceManager.Localize("RECIPIENT");
      this.FromNumberLiteral.Text = DependenciesManager.ResourceManager.Localize("FROM_NUMBER");
      this.SendMessageLiteral.Text = DependenciesManager.ResourceManager.Localize("SEND_MESSAGE");
      this.SmsBodyLabel.Text = DependenciesManager.ResourceManager.Localize("MESSAGE_BODY");
      this.SmsBodyImg.Alt = DependenciesManager.ResourceManager.Localize("INSERT_FIELDS");
    }

    protected virtual void BuildUpClientDictionary() => (this).Page.ClientScript.RegisterClientScriptBlock(((object) this).GetType(), "scWfmNeverLabel", Sitecore.StringExtensions.StringExtensions.FormatWith("var sc = new Object();sc.dictionary = [];sc.dictionary['Never'] = \"{0}\";", new object[1]
    {
      (object) DependenciesManager.ResourceManager.Localize("NEVER")
    }), true);

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      Sitecore.Context.ClientPage.RegisterKey("13", string.Empty, string.Empty);
    }

    protected override void OK_Click()
    {
      if (string.IsNullOrEmpty(this.RecipientNumber.GetEnabledSelectedValue()))
        XamlControl.AjaxScriptManager.Alert(DependenciesManager.ResourceManager.Localize("RECIPIENT_NUMBER_EMPTY"));
      else
        base.OK_Click();
    }

    protected override void SaveValues()
    {
      this.SetValue("Recipient", this.RecipientNumber.GetEnabledSelectedValue());
      this.SetValue("EditMode", string.Join("|", this.EditModeList.GetManagedSelectedValues().ToArray<string>()));
      if (this.FromEdit != null)
        this.SetValue("FromPhone", this.FromEdit.Text);
      this.SetValue("RecipientGateway", this.RecipientGatewayEdit.Text);
      this.SetLongValue(SendSmsPage.mailKey, this.SmsBody.Text);
    }

    public void AddValue(string value, string id)
    {
      this.SmartAdd(this.SmsBody, string.Join("", new string[3]
      {
        "[",
        value,
        "]"
      }));
      SheerResponse.SetOuterHtml(id, this.SmsBody);
    }

    private void SmartAdd(TextBox edit, string value)
    {
      if (string.IsNullOrEmpty(edit.Text))
        edit.Text = value;
      else if (edit.Text.EndsWith(" "))
      {
        edit.Text += value;
      }
      else
      {
        TextBox textBox = edit;
        textBox.Text = textBox.Text + " " + value;
      }
    }

    private void FillContextMenu(
      ContextMenu menu,
      string allowedTypes,
      TextBox insertValueTo,
      GenericControl link,
      Sitecore.Web.UI.HtmlControls.Image img,
      Sitecore.Web.UI.HtmlControls.Literal label)
    {
      foreach (IFieldItem field in this.CurrentForm.Fields)
      {
        if (string.IsNullOrEmpty(allowedTypes) || allowedTypes == "*" || allowedTypes.Contains(((object) field.TypeID).ToString()))
          ((menu).Add(field.Name, string.Empty, string.Empty)).Attributes["onclick"] = Sitecore.StringExtensions.StringExtensions.FormatWith("javascript:return scForm.OnContextMenuClick(this, event, '{0}')", new object[1]
          {
            (object) field.Name
          });
      }
      if ((menu).Controls.Count != 0)
        return;
      (link).Attributes.Remove("href");
      (link).Attributes.Remove("onclick");
      (label).Style.Add("margin", "0 22 0 0");
      (img).Style.Add("display", "none");
    }

    public string AllowedSmsBodyTypes => Sitecore.Web.WebUtil.GetQueryString(nameof (AllowedSmsBodyTypes), "*");

    public string AllowedRecipientFieldTypes => Sitecore.Web.WebUtil.GetQueryString("AllowedFieldTypes", "{5A45591E-2FDC-444F-AB2C-AD814788928F}");
  }
}
