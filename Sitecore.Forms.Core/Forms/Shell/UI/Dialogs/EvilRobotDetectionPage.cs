// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.EvilRobotDetectionPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using ComponentArt.Web.UI;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Utility;
using Sitecore.Text;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class EvilRobotDetectionPage : SmartDialogPage
  {
    private readonly IResourceManager resourceManager;
    protected TabStripTab BasicOptionTab;
    protected TextBox Body;
    protected Sitecore.Web.UI.HtmlControls.Literal BodyLabel;
    protected Edit CC;
    protected Sitecore.Web.UI.HtmlControls.Literal CCLabel;
    protected Groupbox DetectionThresholdsGroupbax;
    protected TabStripTab EmailingTab;
    protected Sitecore.Web.UI.HtmlControls.Literal EnableAttackProtectionLiteral;
    protected Groupbox FormDisplayPageGroupbox;
    protected Sitecore.Web.UI.HtmlControls.Literal FormWithCAPTCHALiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal IfAnyThresholdExceededLiteral;
    protected Edit MinutesPerServerSlider;
    protected Edit MinutesPerSessionSlider;
    protected GridPanel PageLinkSetingsGrid;
    protected Checkbox PerServerCheckbox;
    protected Sitecore.Web.UI.HtmlControls.Literal PerServerLiteral;
    protected Checkbox PerSessionCheckbox;
    protected Sitecore.Web.UI.HtmlControls.Literal PerSessionLiteral;
    protected GridPanel RedirectGrid;
    protected HtmlInputButton RobotPageLinkButton;
    protected Checkbox RobotRedirectCheckbox;
    protected Sitecore.Web.UI.HtmlControls.Literal RobotRedirectPageCaptionLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal RobotRedirectPageLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal RobotRedirectPlaceholderCaptionLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal RobotRedirectPlaceholderLiteral;
    protected GridPanel RobotRedirectSettings;
    protected Sitecore.Web.UI.HtmlControls.Literal ServerMinutesLiteral;
    protected HtmlInputButton ServerPageLinkButton;
    protected Checkbox ServerRedirectCheckbox;
    protected Sitecore.Web.UI.HtmlControls.Literal ServerRedirectPageCaptionLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal ServerRedirectPageLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal ServerRedirectPlaceholderCaptionLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal ServerRedirectPlaceholderLiteral;
    protected GridPanel ServerRedirectSettings;
    protected Sitecore.Web.UI.HtmlControls.Literal ServerTimesInLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal SessionMinutesLiteral;
    protected HtmlInputButton SessionPageLinkButton;
    protected Checkbox SessionRedirectCheckbox;
    protected Sitecore.Web.UI.HtmlControls.Literal SessionRedirectPageCaptionLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal SessionRedirectPageLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal SessionRedirectPlaceholderCaptionLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal SessionRedirectPlaceholderLiteral;
    protected GridPanel SessionRedirectSettings;
    protected Sitecore.Web.UI.HtmlControls.Literal SessionTimesInLiteral;
    protected Edit Subject;
    protected Sitecore.Web.UI.HtmlControls.Literal SubjectLabel;
    protected Edit SubmitsPerServerSlider;
    protected Edit SubmitsPerSessionSlider;
    protected Border SuspiciousFormSettings;
    protected Border SuspiciousVisitorSetting;
    protected Edit To;
    protected Sitecore.Web.UI.HtmlControls.Literal ToLabel;

    public EvilRobotDetectionPage()
      : this(DependenciesManager.ResourceManager)
    {
    }

    public EvilRobotDetectionPage(IResourceManager resourceManager)
    {
      Assert.IsNotNull((object) resourceManager, "Dependency resourceManager is null");
      this.resourceManager = resourceManager;
    }

    protected override void Localize()
    {
      base.Localize();
      this.Header = this.resourceManager.Localize("ROBOT_ATTACK_PROTECTION");
      this.Text = this.resourceManager.Localize("SET_ATTACK_DETECTION_THRESHOLDS");
      ((NavigationNode) this.BasicOptionTab).Text = this.resourceManager.Localize("ATTACK_PROTECTION");
      ((NavigationNode) this.EmailingTab).Text = this.resourceManager.Localize("WARNING_EMAIL");
      ((HeaderedItemsControl) this.DetectionThresholdsGroupbax).Header = this.resourceManager.Localize("DETECTION_THRESHOLDS");
      this.EnableAttackProtectionLiteral.Text = this.resourceManager.Localize("ENABLE_ATTACK_PROTECTION_BY_SHOWING_CAPTCHA");
      this.PerSessionCheckbox.Header = this.resourceManager.Localize("SUSPICIOUS_VISITOR_DETECTED");
      this.SessionRedirectCheckbox.Header = this.resourceManager.Localize("SUSPICIOUS_VISITOR_DETECTED");
      this.SessionTimesInLiteral.Text = this.resourceManager.Localize("TIMES_IN");
      this.SessionMinutesLiteral.Text = this.resourceManager.Localize("MINUTES");
      this.PerServerCheckbox.Header = this.resourceManager.Localize("SUSPICIOUS_FORM_ACTIVITY_DETECTED");
      this.ServerRedirectCheckbox.Header = this.resourceManager.Localize("SUSPICIOUS_FORM_ACTIVITY_DETECTED");
      this.ServerTimesInLiteral.Text = this.resourceManager.Localize("TIMES_IN");
      this.ServerMinutesLiteral.Text = this.resourceManager.Localize("MINUTES");
      ((HeaderedItemsControl) this.FormDisplayPageGroupbox).Header = this.resourceManager.Localize("FORM_DISPLAY_PAGE");
      this.RobotRedirectCheckbox.Header = this.resourceManager.Localize("VISITOR_IDENTIFIED_AS_ROBOTS");
      this.FormWithCAPTCHALiteral.Text = this.resourceManager.Localize("FORM_WITH_CAPTCHA");
      this.RobotRedirectPageCaptionLiteral.Text = this.resourceManager.Localize("PAGE");
      this.SessionRedirectPageCaptionLiteral.Text = this.resourceManager.Localize("PAGE");
      this.ServerRedirectPageCaptionLiteral.Text = this.resourceManager.Localize("PAGE");
      this.RobotPageLinkButton.Value = this.resourceManager.Localize("CHANGE");
      this.ServerPageLinkButton.Value = this.resourceManager.Localize("CHANGE");
      this.SessionPageLinkButton.Value = this.resourceManager.Localize("CHANGE");
      this.RobotRedirectPlaceholderCaptionLiteral.Text = this.resourceManager.Localize("PLACEHOLDER");
      this.SessionRedirectPlaceholderCaptionLiteral.Text = this.resourceManager.Localize("PLACEHOLDER");
      this.ServerRedirectPlaceholderCaptionLiteral.Text = this.resourceManager.Localize("PLACEHOLDER");
      this.IfAnyThresholdExceededLiteral.Text = this.resourceManager.Localize("IF_ANY_THRESHOLD_EXCEEDED_SEND_EMAIL");
      this.ToLabel.Text = this.resourceManager.Localize("TO");
      this.CCLabel.Text = this.resourceManager.Localize("CC");
      this.SubjectLabel.Text = this.resourceManager.Localize("SUBJECT") + ":";
      this.BodyLabel.Text = this.resourceManager.Localize("MESSAGE_BODY") + ":";
      this.PerSessionLiteral.Text = this.resourceManager.Localize("VISITOR_SUBMIT_FORM_MORE_THAN");
      this.PerServerLiteral.Text = this.resourceManager.Localize("FORM_IS_SUBMITTED_MORE_THAN");
    }

    protected override void OK_Click()
    {
      AjaxScriptManager.Current.SetModified(false);
      base.OK_Click();
    }

    protected void OnBrowsePage(
      ClientPipelineArgs currentArgs,
      Sitecore.Web.UI.HtmlControls.Literal pageHoder,
      Sitecore.Web.UI.HtmlControls.Literal placeholderHolder)
    {
      if (currentArgs.IsPostBack)
      {
        if (!currentArgs.HasResult)
          return;
        string[] strArray = currentArgs.Result.Split('/');
        this.InitRedirectSection(strArray[0], strArray[1], pageHoder, placeholderHolder);
        SheerResponse.SetOuterHtml((pageHoder).ID, pageHoder);
        SheerResponse.SetOuterHtml((placeholderHolder).ID, placeholderHolder);
      }
      else
      {
        UrlString urlString = new UrlString(UIUtil.GetUri("control:Form.SelectPage"));
        urlString.Append("page", (pageHoder).Attributes["page"]);
        urlString.Append("placeholder", placeholderHolder.Text);
        SheerResponse.ShowModalDialog(((object) urlString).ToString(), true);
        currentArgs.WaitForPostBack();
      }
    }

    protected void OnChangeRobotPage() => this.OnBrowsePage(ContinuationManager.Current.CurrentArgs as ClientPipelineArgs, this.RobotRedirectPageLiteral, this.RobotRedirectPlaceholderLiteral);

    protected void OnChangeSuspiciousFormPage() => this.OnBrowsePage(ContinuationManager.Current.CurrentArgs as ClientPipelineArgs, this.ServerRedirectPageLiteral, this.ServerRedirectPlaceholderLiteral);

    protected void OnChangeSuspiciousVisitorPage() => this.OnBrowsePage(ContinuationManager.Current.CurrentArgs as ClientPipelineArgs, this.SessionRedirectPageLiteral, this.SessionRedirectPlaceholderLiteral);

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      Page.EnableEventValidation = false;
      ProtectionSchema protectionSchema = ProtectionSchema.Parse(this.GetValueByKey("RobotDetection"));
      this.InitActivePage(this.RobotRedirectPageLiteral, this.RobotRedirectPlaceholderLiteral, protectionSchema.RedirectToPage);
      this.InitPlaceholders(protectionSchema.Placeholder);
      this.PerSessionCheckbox.Checked = protectionSchema.Session.Enabled;
      if (!this.PerSessionCheckbox.Checked)
      {
        this.SessionRedirectCheckbox.Checked = false;
        (this.SubmitsPerSessionSlider).Disabled = true;
        (this.MinutesPerSessionSlider).Disabled = true;
        (this.SessionRedirectCheckbox).Disabled = true;
        this.SessionPageLinkButton.Disabled = true;
      }
      else
      {
        this.SessionRedirectCheckbox.Checked = protectionSchema.Session.RedirectEnabled;
        (this.SubmitsPerSessionSlider).Value = protectionSchema.Session.SubmitsNumber.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        (this.MinutesPerSessionSlider).Value = protectionSchema.Session.MinutesInterval.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      }
      if (!this.SessionRedirectCheckbox.Checked)
        (this.SessionRedirectSettings).Attributes["disabled"] = "true";
      this.PerServerCheckbox.Checked = protectionSchema.Server.Enabled;
      if (!this.PerServerCheckbox.Checked)
      {
        this.ServerRedirectCheckbox.Checked = false;
        (this.SubmitsPerServerSlider).Disabled = true;
        (this.MinutesPerServerSlider).Disabled = true;
        (this.ServerRedirectCheckbox).Disabled = true;
        this.ServerPageLinkButton.Disabled = true;
      }
      else
      {
        this.ServerRedirectCheckbox.Checked = protectionSchema.Server.RedirectEnabled;
        (this.SubmitsPerServerSlider).Value = protectionSchema.Server.SubmitsNumber.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        (this.MinutesPerServerSlider).Value = protectionSchema.Server.MinutesInterval.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      }
      if (!this.ServerRedirectCheckbox.Checked)
        (this.ServerRedirectSettings).Attributes["disabled"] = "true";
      (this.To).Value = protectionSchema.WarningEmail.To;
      (this.CC).Value = protectionSchema.WarningEmail.CC;
      (this.Subject).Value = protectionSchema.WarningEmail.Subject;
      this.Body.Text = protectionSchema.WarningEmail.Body;
      (this.RobotRedirectSettings).Enabled = this.RobotRedirectCheckbox.Checked;
      (this.ServerRedirectSettings).Enabled = this.ServerRedirectCheckbox.Checked;
      (this.SessionRedirectSettings).Enabled = this.SessionRedirectCheckbox.Checked;
      this.InitRedirectSection(protectionSchema.RedirectToPage, protectionSchema.Placeholder, this.RobotRedirectPageLiteral, this.RobotRedirectPlaceholderLiteral);
      this.InitRedirectSection(protectionSchema.Session.RedirectPage, protectionSchema.Session.Placeholder, this.SessionRedirectPageLiteral, this.SessionRedirectPlaceholderLiteral);
      this.InitRedirectSection(protectionSchema.Server.RedirectPage, protectionSchema.Server.Placeholder, this.ServerRedirectPageLiteral, this.ServerRedirectPlaceholderLiteral);
      this.SetSlidersRange();
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if ((this).Page.IsPostBack)
        return;
      this.Localize();
    }

    protected override void SaveValues()
    {
      this.SetValue("ProtectionSchema", new ProtectionSchema(this.PerSessionCheckbox.Checked ? new ProtectionSettings(uint.Parse((this).Context.Request.Form[(this.MinutesPerSessionSlider).ID]), uint.Parse((this).Context.Request.Form[(this.SubmitsPerSessionSlider).ID])) : ProtectionSettings.SessionNoProtection, this.PerServerCheckbox.Checked ? new ProtectionSettings(uint.Parse((this).Context.Request.Form[(this.MinutesPerServerSlider).ID]), uint.Parse((this).Context.Request.Form[(this.SubmitsPerServerSlider).ID])) : ProtectionSettings.ServerNoProtection)
      {
        Session = {
          RedirectEnabled = this.SessionRedirectCheckbox.Checked,
          RedirectPage = (this.SessionRedirectPageLiteral).Attributes["page"],
          Placeholder = this.SessionRedirectPlaceholderLiteral.Text
        },
        Server = {
          RedirectEnabled = this.ServerRedirectCheckbox.Checked,
          RedirectPage = (this.ServerRedirectPageLiteral).Attributes["page"],
          Placeholder = this.ServerRedirectPlaceholderLiteral.Text
        },
        RedirectToPage = (this.RobotRedirectPageLiteral).Attributes["page"],
        Placeholder = this.RobotRedirectPlaceholderLiteral.Text,
        WarningEmail = {
          To = (this).Page.Request.Form[(this.To).ID],
          CC = (this).Page.Request.Form[(this.CC).ID],
          Subject = (this).Page.Request.Form[(this.Subject).ID],
          Body = (this).Page.Request.Form[this.Body.UniqueID]
        }
      }.ToString());
      base.SaveValues();
    }

    private void InitActivePage(Sitecore.Web.UI.HtmlControls.Literal pageLiteral, Sitecore.Web.UI.HtmlControls.Literal placeholderLiteral, string page)
    {
      if (Sitecore.Context.ContentDatabase != null)
      {
        Item obj = Sitecore.Context.ContentDatabase.GetItem(page);
        if (obj != null)
          pageLiteral.Text = StringUtil.Right(Sitecore.Form.Core.Utility.ItemUtil.GetItemUrl(obj), 20);
      }
      (placeholderLiteral).Attributes[nameof (page)] = PlaceholderManager.GetOnlyPlaceholderNamesUrl((pageLiteral).Value);
      (pageLiteral).Attributes[nameof (page)] = page;
    }

    private void InitPlaceholders(string placeholder) => string.IsNullOrEmpty(placeholder);

    private void InitRedirectSection(
      string page,
      string placeholder,
      Sitecore.Web.UI.HtmlControls.Literal pageHoder,
      Sitecore.Web.UI.HtmlControls.Literal placeholderHolder)
    {
      if (string.IsNullOrEmpty(page))
        return;
      Item obj = StaticSettings.ContextDatabase.GetItem(page);
      if (obj == null)
        return;
      string itemUrl = Sitecore.Form.Core.Utility.ItemUtil.GetItemUrl(obj);
      pageHoder.Text = itemUrl.Length <= 48 ? itemUrl : "..." + StringUtil.Right(Sitecore.Form.Core.Utility.ItemUtil.GetItemUrl(obj), 48);
      (pageHoder).Attributes[nameof (page)] = ((object) obj.ID).ToString();
      placeholderHolder.Text = placeholder;
    }

    private void SetSlidersRange()
    {
      string[] strArray1 = Settings.AttackProtection.SessionThreshold.Split('/', '-');
      if (strArray1.Length == 4)
      {
        int result;
        if (int.TryParse(strArray1[0], out result))
          (this.SubmitsPerSessionSlider).Attributes["MinValue"] = result.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        if (int.TryParse(strArray1[2], out result))
          (this.SubmitsPerSessionSlider).Attributes["MaxValue"] = result.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        if (int.TryParse(strArray1[1], out result))
          (this.MinutesPerSessionSlider).Attributes["MinValue"] = result.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        if (int.TryParse(strArray1[3], out result))
          (this.MinutesPerSessionSlider).Attributes["MaxValue"] = result.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      }
      string[] strArray2 = Settings.AttackProtection.ServerThreshold.Split('/', '-');
      if (strArray2.Length != 4)
        return;
      int result1;
      if (int.TryParse(strArray2[0], out result1))
        (this.SubmitsPerServerSlider).Attributes["MinValue"] = result1.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      if (int.TryParse(strArray2[2], out result1))
        (this.SubmitsPerServerSlider).Attributes["MaxValue"] = result1.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      if (int.TryParse(strArray2[1], out result1))
        (this.MinutesPerServerSlider).Attributes["MinValue"] = result1.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      if (!int.TryParse(strArray2[3], out result1))
        return;
      (this.MinutesPerServerSlider).Attributes["MaxValue"] = result1.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    private string Shrink(string path) => !string.IsNullOrEmpty(path) && path.Length > 20 ? path.Substring(path.Length - 20, 20) : path;
  }
}
