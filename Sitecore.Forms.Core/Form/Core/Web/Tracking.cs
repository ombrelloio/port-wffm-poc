// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Web.Tracking
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Web.UI;

namespace Sitecore.Form.Core.Web
{
  public class Tracking : Page
  {
    private readonly ILogger logger;
    private readonly IAnalyticsTracker analyticsTracker;

    public Tracking()
      : this(DependenciesManager.Logger, DependenciesManager.AnalyticsTracker)
    {
    }

    public Tracking(ILogger logger, IAnalyticsTracker analyticsTracker)
    {
      Assert.IsNotNull((object) logger, nameof (logger));
      Assert.IsNotNull((object) analyticsTracker, nameof (analyticsTracker));
      this.logger = logger;
      this.analyticsTracker = analyticsTracker;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
      if (this.logger.IsNull((object) this.analyticsTracker.CurrentPage, "Tracker.Current.CurrentPage"))
        return;
      this.analyticsTracker.Current.CurrentPage.Cancel();
      string str = this.Page.Server.UrlDecode(this.Page.Request.Form["track"]);
      if (!string.IsNullOrEmpty(str))
      {
        foreach (ClientEvent clientEvent in Json.Instance.DeserializeObject<ClientEvent[]>(str))
          this.analyticsTracker.TriggerEvent(clientEvent);
      }
      this.Response.End();
    }
  }
}
