// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Queries.FormStatisticsByContactReportQuery
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Data;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Analytics.Model;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.Configuration;
using Sitecore.XConnect.Collection.Model;
using Sitecore.Xdb.Reporting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;

namespace Sitecore.WFFM.Analytics.Queries
{
  public class FormStatisticsByContactReportQuery : ItemBasedReportingQuery
  {
    public FormStatisticsByContactReportQuery(
      Guid formId,
      ID queryItemId,
      ReportDataProviderBase reportProvider = null,
      SortCriteria sortCriteria = null,
      CachingPolicy cachingPolicy = null)
      : base(queryItemId, reportProvider, cachingPolicy)
    {
      this.FormId = formId;
      this.SortCriteria = sortCriteria ?? new SortCriteria("LastInteractionDate", SortDirection.Descending);
    }

    public SortCriteria SortCriteria { get; set; }

    public Guid FormId { get; set; }

    public IQueryable<IFormContactsResult> Data { get; private set; }

    public override void Execute()
    {
            //TODO: must be reworked

      //DataTable dataTable = ((ReportDataProviderBase) ((ReportingQueryBase) this).ReportProvider).GetData(((ReportingQueryBase) this).QueryDataSource, new ReportDataQuery(((ReportingQueryBase) this).Query, new Dictionary<string, object>()
      //{
      //  {
      //    "@FormId",
      //    (object) this.FormId
      //  }
      //}), (CachingPolicy) ((ReportingQueryBase) this).CachingPolicy).GetDataTable();
      //List<IFormContactsResult> source = new List<IFormContactsResult>();
      //for (int index = 0; index < dataTable.Rows.Count; ++index)
      //{
      //  FormContactsResult formContactsResult = new FormContactsResult()
      //  {
      //    ContactId = dataTable.Rows[index]["ContactId"] is Guid ? ID.Parse(dataTable.Rows[index]["ContactId"]).Guid : Guid.Empty
      //  };
      //  if (!(formContactsResult.ContactId == Guid.Empty))
      //  {
      //    using (XConnectClient client = SitecoreXConnectClientConfiguration.GetClient("xconnect/clientconfig"))
      //    {
      //      ContactReference contactReference = new ContactReference(formContactsResult.ContactId);
      //      Contact contact = XConnectSynchronousExtensions.Get<Contact>((IXdbContext) client, (IEntityReference<Contact>) contactReference, (ExpandOptions) new ContactExpandOptions(Array.Empty<string>()));
      //      if (contact != null)
      //      {
      //        PersonalInformation facet = ((Entity) contact).GetFacet<PersonalInformation>("Personal");
      //        if (!string.IsNullOrEmpty(facet?.FirstName) && !string.IsNullOrEmpty(facet.LastName))
      //        {
      //          formContactsResult.ContactName = facet.FirstName + " " + facet.LastName;
      //        }
      //        else
      //        {
      //          string str = (string) null;
      //          if (contact.Identifiers != null)
      //            str = ((IEnumerable<ContactIdentifier>) contact.Identifiers).FirstOrDefault<ContactIdentifier>((Func<ContactIdentifier, bool>) (c => !string.IsNullOrEmpty(c.Identifier)))?.Identifier;
      //          formContactsResult.ContactName = !string.IsNullOrEmpty(str) ? str : "Unknown";
      //        }
      //      }
      //      else
      //        formContactsResult.ContactName = dataTable.Rows[index]["BusinessName"] is string ? (string) dataTable.Rows[index]["BusinessName"] : string.Empty;
      //    }
      //    formContactsResult.Visits = dataTable.Rows[index]["Visits"] is int ? (long) (int) dataTable.Rows[index]["Visits"] : 0L;
      //    formContactsResult.Value = dataTable.Rows[index]["Value"] is int ? (long) (int) dataTable.Rows[index]["Value"] : 0L;
      //    formContactsResult.Dropouts = dataTable.Rows[index]["Dropouts"] is int ? (int) dataTable.Rows[index]["Dropouts"] : 0;
      //    formContactsResult.SubmissionAttempts = dataTable.Rows[index]["Submits"] is int ? (int) dataTable.Rows[index]["Submits"] : 0;
      //    formContactsResult.Failures = dataTable.Rows[index]["Failures"] is int ? (int) dataTable.Rows[index]["Failures"] : 0;
      //    formContactsResult.DateTime = (dataTable.Rows[index]["LastInteractionDate"] is DateTime ? (DateTime) dataTable.Rows[index]["LastInteractionDate"] : DateTime.MinValue).ToString((IFormatProvider) CultureInfo.InvariantCulture);
      //    formContactsResult.FinalResult = (dataTable.Rows[index]["FinalResult"] is int ? (FormSubmitFinalResult) dataTable.Rows[index]["FinalResult"] : FormSubmitFinalResult.Success).ToString();
      //    source.Add((IFormContactsResult) formContactsResult);
      //  }
      //}
      //this.Data = source.AsQueryable<IFormContactsResult>();
    }
  }
}
