// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Aggregation.Processors.FormSummary.FormSummaryProcessor
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Analytics.Aggregation.Pipeline;
using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.XConnect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Sitecore.WFFM.Analytics.Aggregation.Processors.FormSummary
{
  public class FormSummaryProcessor : InteractionAggregationPipelineProcessor
  {
    protected override void OnProcess(InteractionAggregationPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      Contact contact = args.Context.Contact;
      Interaction interaction = args.Context.Interaction;
      IEnumerable<Event> source = ((IEnumerable<Event>) args.Context.Interaction.Events).Where<Event>((Func<Event, bool>) (e => e.DefinitionId == PageEventIds.SubmitSuccessEvent));
      if (!source.Any<Event>())
        return;
      Guid? id = ((Entity) contact).Id;
      if (!id.HasValue)
        return;
      id = ((Entity) interaction).Id;
      if (!id.HasValue)
        return;
      foreach (Event @event in source)
      {
        Sitecore.WFFM.Analytics.Aggregation.Processors.FormSummary.FormSummary fact = args.Context.Results.GetFact<Sitecore.WFFM.Analytics.Aggregation.Processors.FormSummary.FormSummary>();
        FormSummaryKey formSummaryKey1 = new FormSummaryKey();
        formSummaryKey1.Id = Guid.NewGuid();
        id = ((Entity) contact).Id;
        formSummaryKey1.ContactId = id.Value;
        formSummaryKey1.FormId = @event.ItemId;
        id = ((Entity) interaction).Id;
        formSummaryKey1.InteractionId = id.Value;
        formSummaryKey1.Created = @event.Timestamp;
        FormSummaryKey formSummaryKey2 = formSummaryKey1;
        FormSummaryValue formSummaryValue1 = new FormSummaryValue()
        {
          Count = 1
        };
        FormSummaryKey formSummaryKey3 = formSummaryKey2;
        FormSummaryValue formSummaryValue2 = formSummaryValue1;
        fact.Emit(formSummaryKey3, formSummaryValue2);
        List<FieldData> fieldDataList;
        using (TextReader textReader = (TextReader) new StringReader(@event.Data))
          fieldDataList = new XmlSerializer(typeof (List<FieldData>)).Deserialize(textReader) as List<FieldData>;
        if (fieldDataList != null)
        {
          Sitecore.WFFM.Analytics.Aggregation.Processors.FormFieldValues.FormFieldValues dimension = args.Context.Results.GetDimension<Sitecore.WFFM.Analytics.Aggregation.Processors.FormFieldValues.FormFieldValues>();
          foreach (FieldData fieldData in fieldDataList)
          {
            foreach (string fieldValue in fieldData.Values == null || fieldData.Values.Count <= 0 ? (!string.IsNullOrEmpty(fieldData.Value) ? Enumerable.Repeat<string>(fieldData.Value, 1) : Enumerable.Repeat<string>(string.Empty, 1)) : (IEnumerable<string>) fieldData.Values)
              dimension.Add(formSummaryKey2.Id, fieldData.FieldId, fieldData.FieldName, fieldValue);
          }
        }
      }
    }
  }
}
