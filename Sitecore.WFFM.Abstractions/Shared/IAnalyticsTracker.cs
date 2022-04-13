// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Shared.IAnalyticsTracker
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Analytics;
using Sitecore.Analytics.Data;
using Sitecore.Analytics.Model;
using Sitecore.Analytics.Tracking;
using Sitecore.Data;
using Sitecore.Marketing.Definitions.Campaigns;
using Sitecore.Marketing.Definitions.Goals;
using Sitecore.Marketing.Definitions.PageEvents;
using Sitecore.WFFM.Abstractions.Analytics;
using System;

namespace Sitecore.WFFM.Abstractions.Shared
{
  public interface IAnalyticsTracker
  {
    ID SessionId { get; }

    DateTime BasePageTime { get; set; }

    int EventCounter { get; }

    ITracker Current { get; }

    Contact CurrentContact { get; }

    Session CurrentSession { get; }

    ICurrentPageContext CurrentPage { get; }

    CurrentInteraction CurrentInteraction { get; }

    IMarketingDefinitions DefinitionItems { get; }

    bool IsRobot { get; }

    int CurrentTrackerCurrentPageVisitPageIndex { get; }

    void AddTag(string name, string value);

    string GetTag(string name);

    void RegisterFormDropouts();

    void TriggerCampaign(ICampaignActivityDefinition campaign);

    void TriggerFieldEvent(ServerEvent serverEvent, bool onlyNew = true);

    void TriggerEvent(ClientEvent clientEvent);

    Sitecore.Analytics.Model.PageEventData TriggerEvent(ServerEvent serverEvent);

    void TriggerGoal(Guid formId, Guid goalId, string data);

   Sitecore.Analytics.Model.PageEventData TriggerEvent(
      Guid eventId,
      string eventName,
      ID formId,
      string message,
      string action,
      string data = "");

    Sitecore.Analytics.Model.PageEventData TriggerEvent(Sitecore.Analytics.Data.PageEventData pageEventData);

    Guid GetDataKey(string key);

    IPageEventDefinition GetPageEvent(Guid id);

    IGoalDefinition GetPageGoal(Guid id);

    IGoalDefinition GetPageGoal(string name);

    ICampaignActivityDefinition GetCampaign(Guid id);

    IGoalDefinition GetGoal(ID id);

    void Register(string fieldId, string eventId, string formId, string errorMessage);

    void InitializeTracker();
  }
}
