// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Providers.AnalyticsFormsDataProvider
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Shared;
using Sitecore.WFFM.Analytics.Dependencies;
using Sitecore.WFFM.Analytics.Model;
using Sitecore.WFFM.Analytics.Queries;
using Sitecore.Xdb.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.WFFM.Analytics.Providers
{
  public class AnalyticsFormsDataProvider : IWffmDataProvider
  {
    private readonly ReportDataProviderBase _reportDataProvider;
    private readonly ILogger _logger;
    private readonly ISettings _settings;

    public AnalyticsFormsDataProvider(
      ReportDataProviderWrapper reportDataProviderWrapper,
      ILogger logger,
      ISettings settings)
    {
      Assert.IsNotNull((object) reportDataProviderWrapper, nameof (reportDataProviderWrapper));
      Assert.IsNotNull((object) logger, nameof (logger));
      Assert.ArgumentNotNull((object) settings, nameof (settings));
      this._reportDataProvider = reportDataProviderWrapper.GetReportDataProviderBase();
      this._logger = logger;
      this._settings = settings;
    }

    public virtual IEnumerable<Sitecore.WFFM.Abstractions.Analytics.FormData> GetFormData(
      Guid formId)
    {
      if (!this._settings.IsXdbEnabled)
        return (IEnumerable<Sitecore.WFFM.Abstractions.Analytics.FormData>) new List<Sitecore.WFFM.Abstractions.Analytics.FormData>();
      ID formDataReportQuery1 = IDs.FormDataReportQuery;
      FormDataReportQuery formDataReportQuery2 = new FormDataReportQuery(formId, formDataReportQuery1, this._reportDataProvider, CachingPolicy.WithCacheDisabled);
      ((ReportingQueryBase) formDataReportQuery2).Execute();
      return formDataReportQuery2.Data;
    }

    public virtual void InsertFormData(Sitecore.WFFM.Abstractions.Analytics.FormData form)
    {
      if (this._settings.IsXdbTrackerEnabled)
        return;
      this._logger.Warn("Cannot save form data to Db", (object) this);
    }

    public virtual IEnumerable<IFormContactsResult> GetFormsStatisticsByContact(
      Guid formId,
      PageCriteria pageCriteria)
    {
      if (!this._settings.IsXdbEnabled)
        return (IEnumerable<IFormContactsResult>) new List<IFormContactsResult>();
      ID contactsReportQuery = IDs.FormStatisticsByContactsReportQuery;
      FormStatisticsByContactReportQuery contactReportQuery = new FormStatisticsByContactReportQuery(formId, contactsReportQuery, this._reportDataProvider, cachingPolicy: CachingPolicy.WithCacheDisabled);
      ((ReportingQueryBase) contactReportQuery).Execute();
      return (IEnumerable<IFormContactsResult>) contactReportQuery.Data.Skip<IFormContactsResult>(pageCriteria.PageIndex).Take<IFormContactsResult>(pageCriteria.PageSize);
    }

    public virtual IFormStatistics GetFormStatistics(Guid formId)
    {
      if (!this._settings.IsXdbEnabled)
        return (IFormStatistics) new FormStatistics();
      ID statisticsReportQuery = IDs.FormSubmitStatisticsReportQuery;
      FormSummaryReportQuery summaryReportQuery = new FormSummaryReportQuery(formId, statisticsReportQuery, this._reportDataProvider, CachingPolicy.WithCacheDisabled);
      ((ReportingQueryBase) summaryReportQuery).Execute();
      return (IFormStatistics) new FormStatistics()
      {
        FormId = formId,
        Dropouts = summaryReportQuery.Dropouts,
        SubmitsCount = summaryReportQuery.SubmitsCount,
        Visits = summaryReportQuery.Visits,
        SuccessSubmits = summaryReportQuery.Success
      };
    }

    public virtual IEnumerable<IFormFieldStatistics> GetFormFieldsStatistics(
      Guid formId)
    {
      if (!this._settings.IsXdbEnabled)
        return (IEnumerable<IFormFieldStatistics>) new List<IFormFieldStatistics>();
      ID statisticsReportQuery1 = IDs.FormFieldsStatisticsReportQuery;
      FormFieldsStatisticsReportQuery statisticsReportQuery2 = new FormFieldsStatisticsReportQuery(formId, statisticsReportQuery1, this._reportDataProvider, CachingPolicy.WithCacheDisabled);
      ((ReportingQueryBase) statisticsReportQuery2).Execute();
      return statisticsReportQuery2.Data;
    }
  }
}
