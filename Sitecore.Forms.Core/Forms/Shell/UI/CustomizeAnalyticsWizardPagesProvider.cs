// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.CustomizeAnalyticsWizardPagesProvider
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Wrappers.UI;
using System.Collections.Generic;

namespace Sitecore.Forms.Shell.UI
{
  public class CustomizeAnalyticsWizardPagesProvider : ICustomizeAnalyticsWizardPagesProvider
  {
    private readonly ISheerResponseWrapper sheerResponse;
    private readonly IAnalyticsSettings analyticsSettings;
    private readonly ITrackingFactory trackingFactory;

    public CustomizeAnalyticsWizardPagesProvider(
      ISheerResponseWrapper sheerResponse,
      IAnalyticsSettings analyticsSettings,
      ITrackingFactory trackingFactory)
    {
      this.sheerResponse = sheerResponse;
      this.analyticsSettings = analyticsSettings;
      this.trackingFactory = trackingFactory;
    }

    public IEnumerable<ICustomizeAnalyticsWizardPage> GetPages() => (IEnumerable<ICustomizeAnalyticsWizardPage>) new ICustomizeAnalyticsWizardPage[2]
    {
      (ICustomizeAnalyticsWizardPage) new CustomizeAnalyticsWizardAnalyticsPage(),
      (ICustomizeAnalyticsWizardPage) new CustomizeAnalyticsWizardConfirmationPage(this.sheerResponse, this.analyticsSettings, this.trackingFactory)
    };
  }
}
