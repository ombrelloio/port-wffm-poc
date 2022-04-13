// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Aggregation.Processors.FormStatisticsByContact.FormStatisticsByContactProcessor
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Analytics.Aggregation.Pipeline;
using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.XConnect;
using System;
using System.Collections.ObjectModel;

namespace Sitecore.WFFM.Analytics.Aggregation.Processors.FormStatisticsByContact
{
  public class FormStatisticsByContactProcessor : InteractionAggregationPipelineProcessor
  {
    protected override void OnProcess(InteractionAggregationPipelineArgs args)
    {
            //TODO: must be reworked
      //Assert.ArgumentNotNull((object) args, nameof (args));
      //Sitecore.XConnect.EventCollection events = args.Context.Interaction.Events;
      //if (events == null)
      //  return;
      //Sitecore.WFFM.Analytics.Aggregation.Processors.FormStatisticsByContact.FormStatisticsByContact statisticsByContact = (Sitecore.WFFM.Analytics.Aggregation.Processors.FormStatisticsByContact.FormStatisticsByContact) null;
      //foreach (Event @event in (Collection<Event>) events)
      //{
      //  if (!(@event.DefinitionId != PageEventIds.FormSubmit) || !(@event.DefinitionId != PageEventIds.SubmitSuccessEvent) || !(@event.DefinitionId != PageEventIds.FormDropout) || !(@event.DefinitionId != PageEventIds.FormSaveActionFailure))
      //  {
      //    FormStatisticsByContactKey statisticsByContactKey = new FormStatisticsByContactKey()
      //    {
      //      ContactId = ((IEntityReference) args.Context.Interaction.Contact).Id ?? Guid.Empty,
      //      FormId = @event.ItemId
      //    };
      //    FormStatisticsByContactValue statisticsByContactValue = new FormStatisticsByContactValue()
      //    {
      //      LastInteractionDate = args.DateTimeStrategy.Translate(args.Context.Interaction.StartDateTime),
      //      FinalResult = 1
      //    };
      //    if (@event.DefinitionId == PageEventIds.FormSubmit)
      //      statisticsByContactValue.Submits = 1;
      //    else if (@event.DefinitionId == PageEventIds.SubmitSuccessEvent)
      //    {
      //      statisticsByContactValue.Success = 1;
      //      statisticsByContactValue.FinalResult = 0;
      //    }
      //    else if (@event.DefinitionId == PageEventIds.FormDropout)
      //      statisticsByContactValue.Dropouts = 1;
      //    else if (@event.DefinitionId == PageEventIds.FormSaveActionFailure)
      //    {
      //      statisticsByContactValue.Failures = 1;
      //      statisticsByContactValue.FinalResult = 2;
      //    }
      //    statisticsByContactValue.Visits = 1;
      //    if (statisticsByContact == null)
      //      statisticsByContact = args.Context.Results.GetFact<Sitecore.WFFM.Analytics.Aggregation.Processors.FormStatisticsByContact.FormStatisticsByContact>();
      //    statisticsByContact.Emit(statisticsByContactKey, statisticsByContactValue);
      //  }
      //}
    }
  }
}
