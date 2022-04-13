// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Data.ITracking
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Analytics.Data;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Marketing.Definitions.Campaigns;
using System;
using System.Collections.Generic;

namespace Sitecore.WFFM.Abstractions.Data
{
  public interface ITracking
  {
    IEnumerable<ICampaignActivityDefinition> Campaigns { get; }

    IEnumerable<PageEventData> Goals { get; }

    Dictionary<Guid, PageEventData> AllEvents { get; }

    Item Goal { get; }

    bool Ignore { get; }

    Database Database { get; }

    bool IsDropoutTrackingEnabled { get; }

    void AddEvent(Guid id);

    void RemoveAllEventsAndGoals();

    void SetCampaignEvent(Guid eventID);

    void SetIgnore(bool isIgnore);

    string ToString();

    void Update(bool enableAnalytics, bool enableDropout);

    bool ContainsDropoutEvents();
  }
}
