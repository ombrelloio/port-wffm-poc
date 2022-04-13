// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Services.Requests.Controllers.FormReportsController
// Assembly: Sitecore.WFFM.Services, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A002B65D-6063-457D-AAC3-54DF69ADFBA9
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Services.dll

using Newtonsoft.Json;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Web.Http.Filters;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using Sitecore.WFFM.Services.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sitecore.WFFM.Services.Requests.Controllers
{
  [AuthorizeSitecore(Roles = "sitecore\\Sitecore Client Users")]
  public class FormReportsController : Controller
  {
    private readonly IItemRepository itemRepository;

    public FormReportsController()
      : this(DependenciesManager.DataProvider, DependenciesManager.Resolve<IItemRepository>())
    {
    }

    public FormReportsController(
      IWffmDataProvider formsDataProvider,
      IItemRepository itemRepository)
    {
      Assert.ArgumentNotNull((object) formsDataProvider, nameof (formsDataProvider));
      Assert.ArgumentNotNull((object) itemRepository, nameof (itemRepository));
      this.FormsDataProvider = formsDataProvider;
      this.itemRepository = itemRepository;
    }

    public IWffmDataProvider FormsDataProvider { get; private set; }

    [ValidateHttpAntiForgeryToken]
    public ActionResult GetFormContactsPage(Guid id, PageCriteria pageCriteria)
    {
      Assert.ArgumentNotNull((object) pageCriteria, nameof (pageCriteria));
      IEnumerable<IFormContactsResult> statisticsByContact = this.FormsDataProvider.GetFormsStatisticsByContact(id, pageCriteria);
      return (ActionResult) this.Json((object) new
      {
        Items = JsonConvert.SerializeObject((object) statisticsByContact),
        HasResults = statisticsByContact.Any<IFormContactsResult>()
      }, (JsonRequestBehavior) 0);
    }

    [ValidateHttpAntiForgeryToken]
    public ActionResult GetFormSummary(Guid id)
    {
      IFormStatistics formStatistics = this.FormsDataProvider.GetFormStatistics(id);
      string displayName = this.itemRepository.GetItemFromMasterDatabase(new ID(id)).DisplayName;
      formStatistics.Title = displayName;
      return (ActionResult) this.Json((object) JsonConvert.SerializeObject((object) formStatistics), (JsonRequestBehavior) 0);
    }

    [ValidateHttpAntiForgeryToken]
    public ActionResult GetFormFieldsStatistics(Guid id)
    {
      IEnumerable<IFormFieldStatistics> fieldsStatistics = this.FormsDataProvider.GetFormFieldsStatistics(id);
      foreach (IFormFieldStatistics formFieldStatistics in fieldsStatistics)
      {
        Item fromMasterDatabase = this.itemRepository.GetItemFromMasterDatabase(new ID(formFieldStatistics.FieldId));
        if (fromMasterDatabase != null && this.itemRepository.CreateFieldItem(fromMasterDatabase).ParametersDictionary.ContainsKey("islist"))
          formFieldStatistics.IsList = true;
      }
      return (ActionResult) this.Json((object) new
      {
        Items = JsonConvert.SerializeObject((object) fieldsStatistics),
        legend = DependenciesManager.TranslationProvider.Text("Responses")
      }, (JsonRequestBehavior) 0);
    }
  }
}
