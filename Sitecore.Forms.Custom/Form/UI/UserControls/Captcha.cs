// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.UI.UserControls.Captcha
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using MSCaptcha;
using Sitecore.Data.Items;
using Sitecore.Form.Core.Ascx.Controls;
using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.Core.Visual;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Resources.Media;
using Sitecore.Web;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.UI.UserControls
{
  [ValidationProperty("Value")]
  public class Captcha : ValidateUserControl, IHasTitle
  {
    private readonly IResourceManager resourceManager;
    protected Panel captchaCodeBorder;
    protected Panel Panel1;
    protected Sitecore.Form.Web.UI.Controls.Label captchaCodeText;
    protected Panel captchaCodePanel;
    protected Sitecore.Form.Web.UI.Controls.Captcha captchaCode;
    protected Panel captchaTextBorder;
    protected Panel captchaTextPanel;
    protected Sitecore.Form.Web.UI.Controls.Label captchaTextTitle;
    protected Panel captchLimitTextPanel;
    protected Panel captchStrongTextPanel;
    protected TextBox captchaText;
    protected System.Web.UI.WebControls.Label captchaTextHelp;

    public Captcha()
      : this(DependenciesManager.ResourceManager)
    {
    }

    public Captcha(IResourceManager resourceManager)
    {
      this.resourceManager = resourceManager;
      this.CssClass = "scfCaptcha";
      this.RobotDetection = (ProtectionSchema) string.Empty;
      this.AttackProtection = false;
    }

    public override string ID
    {
      get => this.captchaText.ID;
      set
      {
        this.captchaText.ID = value;
        this.captchaTextTitle.AssociatedControlID = value;
        base.ID = value + "border";
      }
    }

    public override ControlResult Result
    {
      get => new ControlResult(this.ControlName, (object) this.captchaText.Text, Sitecore.StringExtensions.StringExtensions.FormatWith(Sitecore.WFFM.Abstractions.Constants.Core.Constants.SecureToken, new object[1]
      {
        (object) this.captchaText.Text
      }));
      set
      {
        this.ControlName = value.FieldName;
        this.captchaText.Text = value.Value.ToString();
      }
    }

    public virtual string Value => this.captchaText.Text;

    protected override Control InnerValidatorContainer => (Control) this.captchStrongTextPanel;

    protected override Control ValidatorContainer => (Control) this.captchLimitTextPanel;

    public string Title
    {
      get => this.captchaTextTitle.Text;
      set => this.captchaTextTitle.Text = value;
    }

    [VisualProperty("Font Warping:", 190)]
    [VisualCategory("Appearance")]
    [VisualFieldType(typeof (ListChoiceField), new object[] {"{7EDF6BCC-1620-4B5F-908A-0752727D68E9}"})]
    public int CaptchaFontWarping
    {
      get => (int) this.captchaCode.CaptchaImage.CaptchaFontWarping;
      set => this.captchaCode.CaptchaImage.CaptchaFontWarping = (CaptchaImage.fontWarpFactor) value;
    }

    [VisualProperty("Background Noise:", 200)]
    [VisualCategory("Appearance")]
    [VisualFieldType(typeof (ListChoiceField), new object[] {"{7EDF6BCC-1620-4B5F-908A-0752727D68E9}"})]
    public int BackgroundNoiseLevel
    {
      get => (int) this.captchaCode.CaptchaImage.CaptchaBackgroundNoise;
      set => this.captchaCode.CaptchaImage.CaptchaBackgroundNoise = (CaptchaImage.backgroundNoiseLevel) value;
    }

    [VisualProperty("Line Noise:", 210)]
    [VisualCategory("Appearance")]
    [VisualFieldType(typeof (ListChoiceField), new object[] {"{7EDF6BCC-1620-4B5F-908A-0752727D68E9}"})]
    public int LineNoiseLevel
    {
      get => (int) this.captchaCode.CaptchaImage.CaptchaLineNoise;
      set => this.captchaCode.CaptchaImage.CaptchaLineNoise = (CaptchaImage.lineNoiseLevel) value;
    }

    [VisualProperty("Help:", 550)]
    [VisualCategory("Appearance")]
    [VisualFieldType(typeof (TextAreaField))]
    [Localize]
    public string CardNumberHelp
    {
      get => this.captchaTextHelp.Text;
      set
      {
        this.captchaTextHelp.Text = value;
        if (!string.IsNullOrEmpty(this.captchaTextHelp.Text))
          this.captchaTextHelp.Style.Remove("display");
        else
          this.captchaTextHelp.Style["display"] = "none";
      }
    }

    [VisualProperty("Attack Protection:", 10)]
    [VisualCategory("Security")]
    [VisualFieldType(typeof (RobotDetectionField))]
    [TypeConverter(typeof (ProtectionSchemaAdapter))]
    public ProtectionSchema RobotDetection { get; set; }

    [DefaultValue("scfCaptcha")]
    [VisualProperty("CSS Class:", 600)]
    [VisualFieldType(typeof (CssClassField))]
    public new string CssClass
    {
      get => this.Attributes["class"];
      set => this.Attributes["class"] = value;
    }

    protected bool AttackProtection
    {
      get => !string.IsNullOrEmpty(Sitecore.Web.WebUtil.GetQueryString(SimpleForm.FormRedirectingHandlerKey)) || this.Attributes[nameof (AttackProtection)] == "1";
      set => this.Attributes[nameof (AttackProtection)] = value ? "1" : "0";
    }

    protected override void OnInit(EventArgs e)
    {
      this.Page.RegisterRequiresControlState((Control) this);
      base.OnInit(e);
      Item obj1 = Sitecore.Context.Database.GetItem("/sitecore/media library/web forms for marketers/icons/refresh");
      if (obj1 != null)
        this.captchaCode.CaptchaRefreshButton.ImageUrl = StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(new MediaItem(obj1)));
      Item obj2 = Sitecore.Context.Database.GetItem("/sitecore/media library/web forms for marketers/icons/loudspeaker");
      if (obj2 != null)
        this.captchaCode.CaptchaPlayButton.ImageUrl = StringUtil.EnsurePrefix('/', MediaManager.GetMediaUrl(new MediaItem(obj2)));
      this.captchaCode.CaptchaRefreshButton.AlternateText = this.resourceManager.Localize("DISPLAY_ANOTHER_TEXT");
      this.captchaCode.CaptchaRefreshButton.ToolTip = this.captchaCode.CaptchaRefreshButton.AlternateText;
      this.captchaCode.CaptchaPlayButton.AlternateText = this.resourceManager.Localize("PLAY_AUDIT_VERSION_OF_TEXT");
      this.captchaCode.CaptchaPlayButton.ToolTip = this.captchaCode.CaptchaPlayButton.AlternateText;
      this.captchaCode.RefreshButtonClick += new ImageClickEventHandler(this.OnCaptchRefreshClick);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      Control control = (Control) null;
      if (this.IsPostBack)
        control = Sitecore.Form.Core.Utility.WebUtil.GetPostBackEventHandler(this.Page);
      if (this.IsPostBack && (control == null || !(control.ID != this.ValidationGroup)))
        return;
      this.AutoShowCaptcha();
    }

    public override bool SetValidatorProperties(BaseValidator validator)
    {
      base.SetValidatorProperties(validator);
      validator.ErrorMessage = Sitecore.StringExtensions.StringExtensions.FormatWith(validator.ErrorMessage, new object[1]
      {
        (object) this.captchaTextTitle.Text
      });
      validator.Text = Sitecore.StringExtensions.StringExtensions.FormatWith(validator.Text, new object[1]
      {
        (object) this.captchaTextTitle.Text
      });
      validator.CssClass += string.Join(string.Empty, new string[2]
      {
        " captchaCodeControlId.",
        this.captchaCode.ID
      });
      return true;
    }

    protected void OnCaptchRefreshClick(object sender, ImageClickEventArgs arg) => this.captchaText.Focus();

    protected override void OnPreRender(EventArgs e)
    {
      this.captchaText.Text = string.Empty;
      base.OnPreRender(e);
    }

    protected override void LoadControlState(object savedState)
    {
      if (savedState == null)
        return;
      this.AttackProtection = (bool) savedState;
      this.AutoShowCaptcha();
    }

    protected override void Render(HtmlTextWriter writer)
    {
      this.Attributes.Remove("AttackProtection");
      base.Render(writer);
    }

    protected override object SaveControlState() => (object) this.AttackProtection;

    private void AutoShowCaptcha()
    {
      if (this.AttackProtection || this.RobotDetection == null || !string.IsNullOrEmpty(this.Value) || !this.RobotDetection.Enabled || this.Form == null)
        return;
      bool flag1 = this.RobotDetection.IsSessionThresholdExceeded(this.Form.FormID);
      bool flag2 = this.RobotDetection.IsServerThresholdExceeded(this.Form.FormID);
      if (flag1 && this.RobotDetection.Session.RedirectEnabled)
        this.Visible = false;
      else if (flag2 && this.RobotDetection.Server.RedirectEnabled)
        this.Visible = false;
      else if (this.RobotDetection.RedirectRobots && DependenciesManager.AnalyticsTracker.IsRobot)
        this.Visible = false;
      else if (flag1 | flag2)
      {
        this.AttackProtection = true;
        this.Visible = true;
      }
      else
        this.Visible = false;
    }
  }
}
