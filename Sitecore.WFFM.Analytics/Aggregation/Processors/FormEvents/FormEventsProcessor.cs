// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Aggregation.Processors.FormEvents.FormEventsProcessor
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Analytics.Aggregation.Pipeline;
using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.XConnect;
using System;
using System.Collections.ObjectModel;

namespace Sitecore.WFFM.Analytics.Aggregation.Processors.FormEvents
{
  public class FormEventsProcessor : InteractionAggregationPipelineProcessor
  {
    protected override void OnProcess(InteractionAggregationPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      EventCollection events = args.Context.Interaction.Events;
      if (events == null)
        return;
      Sitecore.WFFM.Analytics.Aggregation.Processors.FormEvents.FormEvents formEvents = (Sitecore.WFFM.Analytics.Aggregation.Processors.FormEvents.FormEvents) null;
      foreach (Event @event in (Collection<Event>) events)
      {
        if (!(@event.DefinitionId != PageEventIds.FormBegin) || !(@event.DefinitionId != PageEventIds.FormSubmit) || !(@event.DefinitionId != PageEventIds.SubmitSuccessEvent) || !(@event.DefinitionId != PageEventIds.FormDropout) || !(@event.DefinitionId != PageEventIds.FormSaveActionFailure))
        {
          FormEventsKey formEventsKey1 = new FormEventsKey();
          Guid? id = ((Entity) args.Context.Contact).Id;
          formEventsKey1.ContactId = id ?? Guid.Empty;
          id = ((Entity) args.Context.Interaction).Id;
          formEventsKey1.InteractionId = id ?? Guid.Empty;
          formEventsKey1.InteractionStartDate = args.DateTimeStrategy.Translate(args.Context.Interaction.StartDateTime);
          formEventsKey1.PageEventDefinitionId = @event.DefinitionId;
          formEventsKey1.FormId = @event.ItemId;
          FormEventsKey formEventsKey2 = formEventsKey1;
          FormEventsValue formEventsValue = new FormEventsValue()
          {
            Count = 1
          };
          if (formEvents == null)
            formEvents = args.Context.Results.GetFact<Sitecore.WFFM.Analytics.Aggregation.Processors.FormEvents.FormEvents>();
          formEvents.Emit(formEventsKey2, formEventsValue);
        }
      }
    }
  }
}
