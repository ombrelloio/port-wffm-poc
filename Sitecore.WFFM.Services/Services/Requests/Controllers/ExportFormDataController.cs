// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Services.Requests.Controllers.ExportFormDataController
// Assembly: Sitecore.WFFM.Services, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A002B65D-6063-457D-AAC3-54DF69ADFBA9
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Services.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines;
using Sitecore.Security.Accounts;
using Sitecore.Web.Http.Filters;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using Sitecore.WFFM.Abstractions.Utils;
using Sitecore.WFFM.Services.Filters;
using Sitecore.WFFM.Services.Pipelines;
using Sitecore.WFFM.Speak.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sitecore.WFFM.Services.Requests.Controllers
{
  [AuthorizeSitecore(Roles = "sitecore\\Sitecore Client Users")]
  public class ExportFormDataController : Controller
  {
    private readonly IItemRepository itemRepository;
    private readonly IWebUtil webUtil;
    private readonly IFormRegistryUtil formRegistryUtil;

    public ExportFormDataController()
      : this(DependenciesManager.DataProvider, DependenciesManager.Resolve<IItemRepository>(), DependenciesManager.WebUtil, DependenciesManager.FormRegistryUtil)
    {
    }

    public ExportFormDataController(
      IWffmDataProvider formsDataProvider,
      IItemRepository itemRepository,
      IWebUtil webUtil,
      IFormRegistryUtil formRegistryUtil)
    {
      Assert.ArgumentNotNull((object) formsDataProvider, nameof (formsDataProvider));
      Assert.ArgumentNotNull((object) itemRepository, nameof (itemRepository));
      Assert.ArgumentNotNull((object) webUtil, nameof (webUtil));
      Assert.ArgumentNotNull((object) formRegistryUtil, nameof (formRegistryUtil));
      this.FormsDataProvider = formsDataProvider;
      this.itemRepository = itemRepository;
      this.webUtil = webUtil;
      this.formRegistryUtil = formRegistryUtil;
    }

    public IWffmDataProvider FormsDataProvider { get; private set; }

    [ValidateHttpAntiForgeryToken]
    public ActionResult Export(Guid id, int format)
    {
      Assert.ArgumentNotNull((object) id, nameof (id));
      Assert.ArgumentNotNull((object) format, nameof (format));
      bool flag = format == 0;
      Item fromMasterDatabase = this.itemRepository.GetItemFromMasterDatabase(new ID(id));
      Assert.IsTrue(fromMasterDatabase != null, "Can't find the form.");
      Context.Job?.Status.LogInfo(DependenciesManager.ResourceManager.Localize("READING_DATA_FROM_DATABASE"));
      IOrderedEnumerable<FormData> orderedEnumerable = this.FormsDataProvider.GetFormData(id).OrderBy<FormData, DateTime>((Func<FormData, DateTime>) (formData => formData.Timestamp));
      FormExportArgs formExportArgs = new FormExportArgs(this.itemRepository.CreateFormItem(fromMasterDatabase), new FormPacket((IEnumerable<FormData>) orderedEnumerable), this.webUtil.GetTempFileName(), flag ? "text/xml" : "application/vnd.ms-excel");
      formExportArgs.Parameters.Add("contextUser", ((Account) Context.User).Name);
      CorePipeline.Run(flag ? "exportToXml" : "exportToExcel", (PipelineArgs) formExportArgs);
      return (ActionResult) this.Json((object) new
      {
        File = formExportArgs.FileName
      }, (JsonRequestBehavior) 0);
    }

    [ValidateHttpAntiForgeryToken]
    public ActionResult Download(string file, int format)
    {
      Assert.ArgumentNotNullOrEmpty(file, "fileName");
      string str1 = format == 0 ? "text/xml" : "application/vnd.ms-excel";
      string str2 = format == 0 ? "export.xml" : "export.xls";
      return (ActionResult) this.File(file, str1, str2);
    }
  }
}
