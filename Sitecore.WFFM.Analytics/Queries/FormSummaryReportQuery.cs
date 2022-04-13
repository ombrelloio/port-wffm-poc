// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Queries.FormSummaryReportQuery
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Data;
using Sitecore.Xdb.Reporting;
using System;
using System.Collections.Generic;
using System.Data;

namespace Sitecore.WFFM.Analytics.Queries
{
  public class FormSummaryReportQuery : ItemBasedReportingQuery
  {
    public FormSummaryReportQuery(
      Guid formId,
      ID queryItemId,
      ReportDataProviderBase reportProvider = null,
      CachingPolicy cachingPolicy = null)
      : base(queryItemId, reportProvider, cachingPolicy)
    {
      this.FormId = formId;
    }

    public Guid FormId { get; set; }

    public int SubmitsCount { get; private set; }

    public int Dropouts { get; private set; }

    public int Success { get; private set; }

    public int Visits { get; private set; }

    public override void Execute()
    {
      DataTable dataTable = ExecuteQuery(new Dictionary<string, object>()
      {
        {
          "@FormId",
          (object) this.FormId
        }
      });
      if (dataTable.Rows.Count != 1)
        return;
      this.SubmitsCount = dataTable.Rows[0]["Submits"] is int ? (int) dataTable.Rows[0]["Submits"] : 0;
      this.Dropouts = dataTable.Rows[0]["Dropouts"] is int ? (int) dataTable.Rows[0]["Dropouts"] : 0;
      this.Success = dataTable.Rows[0]["Success"] is int ? (int) dataTable.Rows[0]["Success"] : 0;
      this.Visits = dataTable.Rows[0]["Visits"] is int ? (int) dataTable.Rows[0]["Visits"] : 0;
    }
  }
}
