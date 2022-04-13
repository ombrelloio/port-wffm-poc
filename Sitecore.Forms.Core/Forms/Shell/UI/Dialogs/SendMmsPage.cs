// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.SendMmsPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using ComponentArt.Web.UI;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Forms.Shell.UI.Controls;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.XamlSharp.Xaml;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class SendMmsPage : XamlIDEHtmlEditorForm
  {
    public static readonly string MailKey = "Mail";
    protected ComboBox ModeCombobox;
    protected Border ModeComboboxHolder;
    protected DropDownList RecipientNumber;
    protected TextBox RecipientGatewayEdit;
    protected TextBox FromEdit;
    protected ControlledChecklist EditModeList;
    protected Sitecore.Web.UI.HtmlControls.Literal TelephoneNumberLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal MMSGatewayLiteral;
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
      this.ModeCombobox.Text = this.EditModeList.SelectedTitle;
    }

    protected override void OnLoad(EventArgs e)
    {
      Sitecore.Web.WebUtil.SetSessionValue("hdl", (object) this.MailValue);
      base.OnLoad(e);
      if ((this).Page.IsPostBack)
        return;
      this.BuildUpClientDictionary();
    }

    protected override void Localize()
    {
      base.Localize();
      this.Header = DependenciesManager.ResourceManager.Localize("SEND_MMS");
      this.Text = DependenciesManager.ResourceManager.Localize("PLEASE_SELECT_FORM_FIELD_YOU_WANT_USE_FOR_MMS");
      this.TelephoneNumberLiteral.Text = DependenciesManager.ResourceManager.Localize("TELEPHONE_NUMBER");
      this.MMSGatewayLiteral.Text = DependenciesManager.ResourceManager.Localize("MMS_GATEWAY");
      this.RecipientLiteral.Text = DependenciesManager.ResourceManager.Localize("RECIPIENT");
      this.FromNumberLiteral.Text = DependenciesManager.ResourceManager.Localize("FROM_NUMBER");
      this.SendMessageLiteral.Text = DependenciesManager.ResourceManager.Localize("SEND_MESSAGE");
    }

    protected override void BuildUpClientDictionary()
    {
      (this).Page.ClientScript.RegisterClientScriptBlock(((object) this).GetType(), "scWfmNeverLabel", Sitecore.StringExtensions.StringExtensions.FormatWith("var sc = new Object();sc.dictionary = [];sc.dictionary['Never'] = \"{0}\";", new object[1]
      {
        (object) DependenciesManager.ResourceManager.Localize("NEVER")
      }), true);
      base.BuildUpClientDictionary();
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
      this.ReadState();
      this.SetLongValue(SendMmsPage.MailKey, this.Body);
    }

    public string MailValue
    {
      get => this.GetValueByKey(SendMmsPage.MailKey);
      set => this.SetValue(SendMmsPage.MailKey, value);
    }

    public string AllowedRecipientFieldTypes => Sitecore.Web.WebUtil.GetQueryString("AllowedFieldTypes", "{5A45591E-2FDC-444F-AB2C-AD814788928F}");
  }
}
