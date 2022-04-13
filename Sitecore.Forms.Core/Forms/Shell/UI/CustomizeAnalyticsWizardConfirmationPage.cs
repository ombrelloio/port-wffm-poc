// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.CustomizeAnalyticsWizardConfirmationPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Wrappers.UI;

namespace Sitecore.Forms.Shell.UI
{
  public class CustomizeAnalyticsWizardConfirmationPage : ICustomizeAnalyticsWizardPage
  {
    private readonly ISheerResponseWrapper sheerResponse;
    private readonly IAnalyticsSettings analyticsSettings;
    private readonly ITrackingFactory trackingFactory;

    public CustomizeAnalyticsWizardConfirmationPage(
      ISheerResponseWrapper sheerResponse,
      IAnalyticsSettings analyticsSettings,
      ITrackingFactory trackingFactory)
    {
      Assert.ArgumentNotNull((object) sheerResponse, nameof (sheerResponse));
      Assert.ArgumentNotNull((object) analyticsSettings, nameof (analyticsSettings));
      Assert.ArgumentNotNull((object) trackingFactory, nameof (trackingFactory));
      this.sheerResponse = sheerResponse;
      this.analyticsSettings = analyticsSettings;
      this.trackingFactory = trackingFactory;
    }

    public string Name => "ConfirmationPage";

    public void Close(CustomizeAnalyticsWizardPageSettings setting) => this.sheerResponse.SetDialogValue(this.UpdateTracking(setting));

    private string UpdateTracking(CustomizeAnalyticsWizardPageSettings pageSettings)
    {
      if (!this.analyticsSettings.IsAnalyticsAvailable)
        return string.Empty;
      ITracking fromXml = this.trackingFactory.CreateFromXml(pageSettings.TrackingXml, pageSettings.DatabaseName);
      fromXml.Update(true, pageSettings.TrackingEnableFormDropout);
      fromXml.AddEvent(pageSettings.GoalId);
      return fromXml.ToString();
    }
  }
}
