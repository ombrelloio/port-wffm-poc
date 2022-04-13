// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Validators.SuspiciousVisitorsValidator
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Submit;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Web.UI;

namespace Sitecore.Form.Core.Validators
{
  public class SuspiciousVisitorsValidator : RobotProtectionValidator
  {
    private string thresholdServerDescription;
    private string thresholdSessionDescription;

    public SuspiciousVisitorsValidator()
    {
    }

    public SuspiciousVisitorsValidator(IAnalyticsTracker analyticsTracker)
      : base(analyticsTracker)
    {
    }

    public string ThresholdServerDescription
    {
      get => this.thresholdServerDescription ?? string.Empty;
      set => this.thresholdServerDescription = value;
    }

    public string ThresholdSessionDescription
    {
      get => this.thresholdSessionDescription ?? string.Empty;
      set => this.thresholdSessionDescription = value;
    }

    public override ProtectionType Type => ProtectionType.Threshold;

    protected override string GetRedirectPage() => this.RobotDetection.Session.RedirectPage;

    protected override string GetRedirectPlaceholder() => this.RobotDetection.Session.Placeholder;

    protected virtual string GetThresholdMessage()
    {
      try
      {
        return Sitecore.StringExtensions.StringExtensions.FormatWith(this.ThresholdSessionDescription, new object[2]
        {
          (object) this.RobotDetection.Session.SubmitsNumber,
          (object) this.RobotDetection.Session.MinutesInterval
        });
      }
      catch (FormatException ex)
      {
        return this.ThresholdSessionDescription;
      }
    }

    protected virtual bool IsRedirectEnabled() => this.RobotDetection.Session.RedirectEnabled;

    protected virtual bool IsThresholdEnabled() => this.RobotDetection.Session.Enabled;

    protected virtual bool IsThresholdExceeded(ID formID) => this.RobotDetection.IsSessionThresholdExceeded(formID, this.RobotDetection.Session.SubmitsNumber - 1U);

    protected override bool OnServerValidate(string value)
    {
      if (this.RobotDetection.Enabled && this.IsThresholdEnabled())
      {
        Control control = this.FindControl(this.ControlToValidate);
        if (control != null)
        {
          BaseUserControl parent = WebUtil.GetParent<BaseUserControl>(control);
          if (!parent.Visible && this.IsThresholdExceeded(parent.Form.FormID))
          {
            parent.Form.FailedSubmit += new EventHandler<EventArgs>(new FormThresholdExceededTracker(this.RobotDetection, this.GetThresholdMessage()).TrackFormThresholdExceeded);
            if (this.IsRedirectEnabled())
              return this.RedirectPage(parent);
            parent.Visible = true;
            parent.Attributes["AttackProtection"] = "1";
            return false;
          }
        }
      }
      return true;
    }
  }
}
