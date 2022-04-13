// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Queries.FormFieldsStatisticsReportQuery
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Data;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Analytics.Model;
using Sitecore.Xdb.Reporting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Sitecore.WFFM.Analytics.Queries
{
  public class FormFieldsStatisticsReportQuery : ItemBasedReportingQuery
  {
    public FormFieldsStatisticsReportQuery(
      Guid formId,
      ID queryItemId,
      ReportDataProviderBase reportProvider = null,
      CachingPolicy cachingPolicy = null)
      : base(queryItemId, reportProvider, cachingPolicy)
    {
      this.FormId = formId;
    }

    public Guid FormId { get; set; }

    public IEnumerable<IFormFieldStatistics> Data { get; private set; }

    public override void Execute()
    {
      DataTable dataTable = ExecuteQuery(new Dictionary<string, object>()
      {
        {
          "@FormId",
          (object) this.FormId
        }
      });
      List<IFormFieldStatistics> source = new List<IFormFieldStatistics>();
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        Guid fieldId = dataTable.Rows[index]["FieldId"] is Guid ? ID.Parse(dataTable.Rows[index]["FieldId"]).Guid : Guid.Empty;
        if (!(fieldId == Guid.Empty))
        {
          IFormFieldStatistics formFieldStatistics = source.FirstOrDefault<IFormFieldStatistics>((Func<IFormFieldStatistics, bool>) (x => x.FieldId == fieldId));
          if (formFieldStatistics == null)
          {
            formFieldStatistics = (IFormFieldStatistics) new FormFieldStatistics();
            formFieldStatistics.Values = new List<IFieldValuesStatistic>();
            formFieldStatistics.FieldId = fieldId;
            source.Add(formFieldStatistics);
          }
          formFieldStatistics.FieldName = dataTable.Rows[index]["FieldName"] is string ? (string) dataTable.Rows[index]["FieldName"] : string.Empty;
          string str = dataTable.Rows[index]["FieldValue"] is string ? (string) dataTable.Rows[index]["FieldValue"] : string.Empty;
          int num = dataTable.Rows[index]["Count"] is int ? (int) dataTable.Rows[index]["Count"] : 0;
          formFieldStatistics.Values.Add((IFieldValuesStatistic) new FieldValueStatistics()
          {
            FieldValue = str,
            Count = num
          });
        }
      }
      source.ForEach((Action<IFormFieldStatistics>) (x => x.Count = x.Values.Where<IFieldValuesStatistic>((Func<IFieldValuesStatistic, bool>) (y => !string.IsNullOrEmpty(y.FieldValue))).Sum<IFieldValuesStatistic>((Func<IFieldValuesStatistic, int>) (f => f.Count))));
      this.Data = (IEnumerable<IFormFieldStatistics>) source;
    }
  }
}
