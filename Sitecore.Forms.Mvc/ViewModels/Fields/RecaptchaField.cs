// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.Fields.RecaptchaField
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Forms.Mvc.Attributes;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.Validators;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace Sitecore.Forms.Mvc.ViewModels.Fields
{
  public class RecaptchaField : SingleLineTextField, IConfiguration, IValidatableObject
  {
    private readonly IAnalyticsTracker analyticsTracker;

    public RecaptchaField()
      : this(DependenciesManager.AnalyticsTracker)
    {
    }

    public RecaptchaField(IAnalyticsTracker analyticsTracker)
    {
      this.analyticsTracker = analyticsTracker;
      this.Theme = "light";
      this.CaptchaType = "image";
    }

    public string Theme { get; set; }

    public string CaptchaType { get; set; }

    public string SiteKey { get; set; }

    public string SecretKey { get; set; }

    [TypeConverter(typeof (ProtectionSchemaAdapter))]
    public virtual ProtectionSchema RobotDetection { get; set; }

    public virtual bool IsRobot => this.analyticsTracker.IsRobot;

    [RequestFormValue("g-recaptcha-response")]
    [RecaptchaResponseValidator(ParameterName = "RecaptchaValidatorError")]
    public override string Value { get; set; }

    public override ControlResult GetResult() => new ControlResult(this.FieldItemId, this.Title, (object) this.Value, (string) null, true);

    public override void Initialize()
    {
      this.SiteKey = this.GetAppSetting("RecaptchaPublicKey") ?? this.GetSitecoreSetting("WFM.RecaptchaSiteKey", (string) null);
      this.SecretKey = this.GetAppSetting("RecaptchaPrivateKey") ?? this.GetSitecoreSetting("WFM.RecaptchaSecretKey", (string) null);
      this.Visible = this.RobotDetection == null || !this.RobotDetection.Enabled;
    }

    public override void SetValueFromQuery(string valueFromQuery)
    {
    }

    public virtual string GetAppSetting(string key)
    {
      Assert.ArgumentNotNullOrEmpty(key, nameof (key));
      return ConfigurationManager.AppSettings[key];
    }

    public virtual string GetSitecoreSetting(string key, string defaultValue)
    {
      Assert.ArgumentNotNullOrEmpty(key, nameof (key));
      return Sitecore.Configuration.Settings.GetSetting(key, defaultValue);
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
