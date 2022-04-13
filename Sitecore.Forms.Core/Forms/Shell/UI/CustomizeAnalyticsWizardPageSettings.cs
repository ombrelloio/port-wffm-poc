// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.CustomizeAnalyticsWizardPageSettings
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using System;

namespace Sitecore.Forms.Shell.UI
{
  public class CustomizeAnalyticsWizardPageSettings
  {
    public CustomizeAnalyticsWizardPageSettings(
      string trackingXml,
      bool trackingEnableFormDropout,
      Guid goalId,
      string databaseName)
    {
      Assert.ArgumentNotNull((object) trackingXml, nameof (trackingXml));
      Assert.ArgumentNotNull((object) databaseName, nameof (databaseName));
      this.TrackingXml = trackingXml;
      this.GoalId = goalId;
      this.TrackingEnableFormDropout = trackingEnableFormDropout;
      this.DatabaseName = databaseName;
    }

    public string TrackingXml { get; private set; }

    public Guid GoalId { get; private set; }

    public bool TrackingEnableFormDropout { get; private set; }

    public string DatabaseName { get; private set; }
  }
}
