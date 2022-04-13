// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.Fields.CaptchaField
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using MSCaptcha;
using Sitecore.Data;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Forms.Mvc.Attributes;
using Sitecore.Forms.Mvc.Controllers.ModelBinders.FieldBinders;
using Sitecore.Forms.Mvc.Extensions;
using Sitecore.Forms.Mvc.Validators;
using Sitecore.WFFM.Abstractions.Dependencies;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sitecore.Forms.Mvc.ViewModels.Fields
{
  public class CaptchaField : SingleLineTextField, IValidatableObject
  {
    public CaptchaField() => this.InitializeCaptcha();

    public bool UseSession { get; set; }

    public int CaptchaMaxTimeout { get; set; }

    public string Passkey => this.Captcha == null ? (string) null : this.Captcha.Text;

    [PropertyBinder(typeof (CaptchaFieldBinder))]
    public string PostedCaptchaUniqueId { get; set; }

    public override string ResultParameters => Sitecore.StringExtensions.StringExtensions.FormatWith(Sitecore.WFFM.Abstractions.Constants.Core.Constants.SecureToken, new object[1]
    {
      (object) this.Value
    });

    [DataType(DataType.Text)]
    [CaptchaResponseValidator]
    public override string Value { get; set; }

    [TypeConverter(typeof (ProtectionSchemaAdapter))]
    public ProtectionSchema RobotDetection { get; set; }

    [DefaultValue("")]
    public string ThresholdDescription { get; set; }

    public CaptchaImage Captcha { get; set; }

    public virtual bool IsRobot => DependenciesManager.AnalyticsTracker.IsRobot;

    public override void Initialize() => this.InitializeCaptcha();

    private void InitializeCaptcha()
    {
      base.Initialize();
      this.UseSession = true;
      this.ThresholdDescription = string.Empty;
      this.Visible = this.RobotDetection == null || !this.RobotDetection.Enabled;
      this.Captcha = new CaptchaImage();
      string s1 = this.Parameters.GetValue("LineNoiseLevel");
      if (s1 != null)
        this.Captcha.LineNoise = (CaptchaImage.lineNoiseLevel) int.Parse(s1);
      string s2 = this.Parameters.GetValue("BackgroundNoiseLevel");
      if (s2 != null)
        this.Captcha.BackgroundNoise = (CaptchaImage.backgroundNoiseLevel) int.Parse(s2);
      string s3 = this.Parameters.GetValue("CaptchaFontWarping");
      if (s3 != null)
        this.Captcha.FontWarp = (CaptchaImage.fontWarpFactor) int.Parse(s3);
      this.PostedCaptchaUniqueId = this.Captcha.UniqueId;
    }

    public IEnumerable<ValidationResult> Validate(
      ValidationContext validationContext)
    {
      if (this.Visible || this.RobotDetection == null || !this.RobotDetection.Enabled)
        return (IEnumerable<ValidationResult>) new ValidationResult[1]
        {
          ValidationResult.Success
        };
      ID id = new ID(this.FormId);
      if (this.RobotDetection != null && this.RobotDetection.Session.Enabled)
        this.RobotDetection.AddSubmitToSession(id);
      if (this.RobotDetection != null && this.RobotDetection.Server.Enabled)
        this.RobotDetection.AddSubmitToServer(id);
      int num1 = this.IsRobot ? 1 : 0;
      bool flag1 = false;
      bool flag2 = false;
      if (this.RobotDetection.Session.Enabled)
        flag1 = this.RobotDetection.IsSessionThresholdExceeded(id);
      if (this.RobotDetection.Server.Enabled)
        flag2 = this.RobotDetection.IsServerThresholdExceeded(id);
      int num2 = flag1 ? 1 : 0;
      if ((num1 | num2 | (flag2 ? 1 : 0)) != 0)
      {
        this.Visible = true;
        return (IEnumerable<ValidationResult>) new ValidationResult[1]
        {
          new ValidationResult("You've been treated as a robot. Please enter the captcha to proceed")
        };
      }
      return (IEnumerable<ValidationResult>) new ValidationResult[1]
      {
        ValidationResult.Success
      };
    }
  }
}
