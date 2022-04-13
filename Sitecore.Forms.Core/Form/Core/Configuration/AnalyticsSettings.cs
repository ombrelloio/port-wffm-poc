// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Configuration.AnalyticsSettings
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Linq;
using System.Reflection;

namespace Sitecore.Form.Core.Configuration
{
  public class AnalyticsSettings : IAnalyticsSettings
  {
    private readonly IRequirementsChecker requirementsChecker;

    public AnalyticsSettings(IRequirementsChecker requirementsChecker)
    {
      Assert.ArgumentNotNull((object) requirementsChecker, nameof (requirementsChecker));
      this.requirementsChecker = requirementsChecker;
    }

    [Required("IsXdbTrackerEnabled", true)]
    public bool IsAnalyticsAvailable => this.requirementsChecker.CheckRequirements(typeof (AnalyticsSettings).GetProperty(nameof (IsAnalyticsAvailable)).GetCustomAttributes().ToArray<Attribute>()) && Settings.Roles.IsAnalyticsMaintaining;

    [Required("IsXdbTrackerEnabled", true)]
    public bool IsReportingAvailable => this.requirementsChecker.CheckRequirements(typeof (AnalyticsSettings).GetProperty(nameof (IsReportingAvailable)).GetCustomAttributes().ToArray<Attribute>()) && Settings.Roles.IsAnalyticsReporting;
  }
}
