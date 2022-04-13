// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Aggregation.Processors.Helper
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Analytics.Aggregation.Pipeline;
using Sitecore.Analytics.Model;
using Sitecore.Diagnostics;
using Sitecore.XConnect;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.WFFM.Analytics.Aggregation.Processors
{
  public static class Helper
  {
    public static VisitData GetVisitData(InteractionAggregationPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (args.Context?.Interaction?.Events == null)
        return (VisitData) null;
      List<PageViewEvent> list = ((IEnumerable<PageViewEvent>) ((IEnumerable) args.Context.Interaction.Events).OfType<PageViewEvent>().OrderBy<PageViewEvent, DateTime>((Func<PageViewEvent, DateTime>) (ev => ((Event) ev).Timestamp))).ToList<PageViewEvent>();
      if (!((IEnumerable<PageViewEvent>) list).Any<PageViewEvent>())
        return new VisitData();
      ILookup<Guid, Event> lookup = ((IEnumerable<Event>) args.Context.Interaction.Events).Where<Event>((Func<Event, bool>) (ev => ev.ParentEventId.HasValue)).ToLookup<Event, Guid>((Func<Event, Guid>) (ev => ev.ParentEventId.Value));
      DateTime dateTime = ((IEnumerable<PageViewEvent>) list).FirstOrDefault<PageViewEvent>() != null ? ((Event) ((IEnumerable<PageViewEvent>) list).First<PageViewEvent>()).Timestamp : DateTime.MinValue;
      List<PageData> pageDataList = new List<PageData>();
      foreach (PageViewEvent pageViewEvent in list)
      {
        ItemData itemData = new ItemData()
        {
          Id = ((Event) pageViewEvent).ItemId,
          Language = pageViewEvent.ItemLanguage,
          Version = pageViewEvent.ItemVersion
        };
        PageData pageData = new PageData()
        {
          DateTime = ((Event) pageViewEvent).Timestamp,
          Duration = ((Event) pageViewEvent).Duration.Milliseconds,
          Item = itemData,
          SitecoreDevice = Helper.GetSitecoreDeviceData(pageViewEvent),
          VisitPageIndex = list.IndexOf(pageViewEvent) + 1,
          PageEvents = Helper.GetPageEventData(pageViewEvent, lookup)
        };
        pageDataList.Add(pageData);
      }
      VisitData visitData = new VisitData();
      visitData.Pages = pageDataList;
      visitData.VisitPageCount = list.Count;
      ((InteractionData) visitData).StartDateTime = dateTime;
      return visitData;
    }

    private static Sitecore.Analytics.Model.SitecoreDeviceData GetSitecoreDeviceData(
      PageViewEvent pageViewEvent)
    {
      if (pageViewEvent.SitecoreRenderingDevice == null)
        return null;
      return new Sitecore.Analytics.Model.SitecoreDeviceData()
      {
        Id = pageViewEvent.SitecoreRenderingDevice.Id,
        Name = pageViewEvent.SitecoreRenderingDevice.Name
      };
    }

    private static List<PageEventData> GetPageEventData(
      PageViewEvent pageViewEvent,
      ILookup<Guid, Event> eventMap)
    {
      Assert.ArgumentNotNull((object) pageViewEvent, nameof (pageViewEvent));
      List<PageEventData> pageEventDataList = new List<PageEventData>();
      foreach (Event @event in eventMap[((Event) pageViewEvent).Id])
      {
        PageEventData pageEventData = new PageEventData()
        {
          ItemId = ((Event) pageViewEvent).ItemId,
          PageEventDefinitionId = @event.DefinitionId,
          Data = @event.Data,
          DataKey = @event.DataKey,
          DateTime = @event.Timestamp,
          IsGoal = ((object) @event).GetType() == typeof (Goal),
          Text = @event.Text,
          Value = @event.EngagementValue
        };
        pageEventDataList.Add(pageEventData);
      }
      return pageEventDataList;
    }
  }
}
