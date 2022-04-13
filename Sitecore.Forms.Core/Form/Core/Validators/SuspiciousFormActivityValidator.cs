// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Validators.SuspiciousFormActivityValidator
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Form.Core.Data;
using Sitecore.WFFM.Abstractions.Shared;
using System;

namespace Sitecore.Form.Core.Validators
{
  public class SuspiciousFormActivityValidator : SuspiciousVisitorsValidator
  {
    public SuspiciousFormActivityValidator()
    {
    }

    public SuspiciousFormActivityValidator(IAnalyticsTracker analyticsTracker)
      : base(analyticsTracker)
    {
    }

    public override ProtectionType Type => ProtectionType.Threshold;

    protected override string GetRedirectPage() => this.RobotDetection.Server.RedirectPage;

    protected override string GetRedirectPlaceholder() => this.RobotDetection.Server.Placeholder;

    protected override string GetThresholdMessage()
    {
      try
      {
        return Sitecore.StringExtensions.StringExtensions.FormatWith(this.ThresholdServerDescription, new object[2]
        {
          (object) this.RobotDetection.Server.SubmitsNumber,
          (object) this.RobotDetection.Server.MinutesInterval
        });
      }
      catch (FormatException ex)
      {
        return this.ThresholdServerDescription;
      }
    }

    protected override bool IsRedirectEnabled() => this.RobotDetection.Server.RedirectEnabled;

    protected override bool IsThresholdEnabled() => this.RobotDetection.Server.Enabled;

    protected override bool IsThresholdExceeded(ID formID) => this.RobotDetection.IsServerThresholdExceeded(formID, this.RobotDetection.Server.SubmitsNumber - 1U);
  }
}
