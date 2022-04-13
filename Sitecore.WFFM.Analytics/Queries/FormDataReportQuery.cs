// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Queries.FormDataReportQuery
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Data;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.Xdb.Reporting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Sitecore.WFFM.Analytics.Queries
{
  public class FormDataReportQuery : ItemBasedReportingQuery
  {
    public FormDataReportQuery(
      Guid formId,
      ID queryItemId,
      ReportDataProviderBase reportProvider = null,
      CachingPolicy cachingPolicy = null)
      : base(queryItemId, reportProvider, cachingPolicy)
    {
      this.FormId = formId;
    }

    public Guid FormId { get; set; }

    public IEnumerable<FormData> Data { get; private set; }

    public override void Execute()
    {
      DataTable dataTable = ExecuteQuery(new Dictionary<string, object>()
      {
        {
          "@FormId",
          (object) this.FormId
        }
      });
      Dictionary<Guid, FormData> source = new Dictionary<Guid, FormData>();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        Guid key = row["_id"] is Guid ? (Guid) row["_id"] : new Guid();
        DateTime dateTime = row["Timestamp"] is DateTime ? (DateTime) row["Timestamp"] : DateTime.Now;
        if (!source.ContainsKey(key))
          source.Add(key, new FormData()
          {
            FormID = row["FormID"] is Guid ? (Guid) row["FormID"] : new Guid(),
            InteractionId = row["InteractionId"] is Guid ? (Guid) row["InteractionId"] : new Guid(),
            ContactId = row["ContactId"] is Guid ? (Guid) row["ContactId"] : new Guid(),
            Timestamp = dateTime,
            Id = row["_id"] is Guid ? (Guid) row["_id"] : new Guid()
          });
        FormData formData = source[key];
        List<FieldData> fieldDataList = new List<FieldData>();
        if (formData.Fields != null)
          fieldDataList.AddRange(formData.Fields);
        FieldData fieldData = new FieldData()
        {
          FieldId = row["Fields_FieldId"] is Guid ? (Guid) row["Fields_FieldId"] : new Guid(),
          Value = row["Fields_Value"] is string ? (string) row["Fields_Value"] : string.Empty
        };
        fieldData.Id = fieldData.FieldId;
        fieldData.FormId = formData.Id;
        fieldData.FieldName = row["Fields_FieldName"] is string ? (string) row["Fields_FieldName"] : string.Empty;
        fieldDataList.Add(fieldData);
        formData.Fields = (IEnumerable<FieldData>) fieldDataList;
      }
      this.Data = (IEnumerable<FormData>) source.Select<KeyValuePair<Guid, FormData>, FormData>((Func<KeyValuePair<Guid, FormData>, FormData>) (pair => pair.Value)).ToList<FormData>();
    }
  }
}
