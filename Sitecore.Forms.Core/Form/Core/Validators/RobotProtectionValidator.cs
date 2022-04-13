// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Validators.RobotProtectionValidator
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Submit;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Validators
{
  public class RobotProtectionValidator : CustomValidator, IAttackProtection
  {
    private readonly IAnalyticsTracker analyticsTracker;
    private string thresholdDescription;

    public RobotProtectionValidator()
      : this(DependenciesManager.AnalyticsTracker)
    {
    }

    public RobotProtectionValidator(IAnalyticsTracker analyticsTracker)
    {
      Assert.IsNotNull((object) analyticsTracker, nameof (analyticsTracker));
      this.analyticsTracker = analyticsTracker;
      this.RobotDetection = (ProtectionSchema) string.Empty;
    }

    [TypeConverter(typeof (ProtectionSchemaAdapter))]
    public ProtectionSchema RobotDetection { get; set; }

    public virtual ProtectionType Type => ProtectionType.Robot;

    public override bool Enabled
    {
      get => true;
      set
      {
      }
    }

    public string ThresholdDescription
    {
      get => this.thresholdDescription ?? string.Empty;
      set => this.thresholdDescription = value;
    }

    public override bool Visible
    {
      get => true;
      set
      {
      }
    }

    protected override bool EvaluateIsValid()
    {
      string str = string.Empty;
      string controlToValidate = this.ControlToValidate;
      if (controlToValidate.Length > 0)
        str = this.GetControlValidationValue(controlToValidate);
      return this.OnServerValidate(str);
    }

    protected virtual string GetRedirectPage() => this.RobotDetection.RedirectToPage;

    protected virtual string GetRedirectPlaceholder() => this.RobotDetection.Placeholder;

    protected override bool OnServerValidate(string value)
    {
      if (this.RobotDetection.Enabled && this.analyticsTracker.IsRobot)
      {
        Control control = this.FindControl(this.ControlToValidate);
        if (control != null)
        {
          BaseUserControl parent = WebUtil.GetParent<BaseUserControl>(control);
          if (parent != null)
          {
            if (!parent.Visible)
            {
              parent.Form.FailedSubmit += new EventHandler<EventArgs>(new FormThresholdExceededTracker(this.RobotDetection, this.ThresholdDescription).TrackFormThresholdExceeded);
              return this.RedirectPage(parent);
            }
            parent.Attributes["AttackProtection"] = "1";
          }
        }
      }
      return true;
    }

    protected virtual bool RedirectPage(BaseUserControl captcha)
    {
      captcha.Visible = true;
      captcha.Attributes["AttackProtection"] = "1";
      return false;
    }
  }
}
