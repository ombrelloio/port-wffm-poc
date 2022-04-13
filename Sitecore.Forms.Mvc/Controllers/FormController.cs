// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Controllers.FormController
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Attributes;
using Sitecore.Forms.Mvc.Controllers.Filters;
using Sitecore.Forms.Mvc.Controllers.ModelBinders;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.Models;
using Sitecore.Forms.Mvc.ViewModels;
using Sitecore.Mvc.Controllers;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System.IO;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Controllers
{
  [ModelBinder(typeof (FormModelBinder))]
  public class FormController : SitecoreController
  {
    private readonly IAnalyticsTracker analyticsTracker;

    public FormController()
      : this((IRepository<FormModel>) Sitecore.Configuration.Factory.CreateObject(Constants.FormRepository, true), (IAutoMapper<IFormModel, FormViewModel>) Factory.CreateObject(Constants.FormAutoMapper, true), (IFormProcessor<FormModel>) Factory.CreateObject(Constants.FormProcessor, true), DependenciesManager.AnalyticsTracker)
    {
    }

    public FormController(
      IRepository<FormModel> repository,
      IAutoMapper<IFormModel, FormViewModel> mapper,
      IFormProcessor<FormModel> processor,
      IAnalyticsTracker analyticsTracker)
    {
      Assert.ArgumentNotNull((object) repository, nameof (repository));
      Assert.ArgumentNotNull((object) mapper, nameof (mapper));
      Assert.ArgumentNotNull((object) processor, nameof (processor));
      Assert.ArgumentNotNull((object) analyticsTracker, nameof (analyticsTracker));
      this.FormRepository = repository;
      this.Mapper = mapper;
      this.FormProcessor = processor;
      this.analyticsTracker = analyticsTracker;
    }

    public IRepository<FormModel> FormRepository { get; private set; }

    public IAutoMapper<IFormModel, FormViewModel> Mapper { get; private set; }

    public IFormProcessor<FormModel> FormProcessor { get; private set; }

    [FormErrorHandler]
    [AcceptVerbs]
    public override ActionResult Index() => (ActionResult) this.Form();

    [SubmittedFormHandler]
    [FormErrorHandler]
    [HttpPost]
    [WffmLimitMultipleSubmits]
    [WffmValidateAntiForgeryToken]
    public virtual ActionResult Index([ModelBinder(typeof (FormModelBinder))] FormViewModel formViewModel)
    {
      this.analyticsTracker.InitializeTracker();
      return (ActionResult) this.ProcessedForm(formViewModel);
    }

    [FormErrorHandler]
    [AllowCrossSiteJson]
    public virtual JsonResult Process([ModelBinder(typeof (FormModelBinder))] FormViewModel formViewModel)
    {
      this.analyticsTracker.InitializeTracker();
      ProcessedFormResult<FormModel, FormViewModel> processedFormResult = this.ProcessedForm(formViewModel, "~/Views/Form/Index.cshtml");
      ((ActionResult) processedFormResult).ExecuteResult(((ControllerBase) this).ControllerContext);
      string str;
      using (StringWriter stringWriter = new StringWriter())
      {
        ViewContext viewContext = new ViewContext(((ControllerBase) this).ControllerContext, ((ViewResultBase) processedFormResult).View, ((ControllerBase) this).ViewData, ((ControllerBase) this).TempData, (TextWriter) stringWriter);
        ((ViewResultBase) processedFormResult).View.Render(viewContext, (TextWriter) stringWriter);
        str = stringWriter.GetStringBuilder().ToString();
      }
      ((ControllerBase) this).ControllerContext.HttpContext.Response.Clear();
      return new JsonResult() { Data = (object) str };
    }

    public virtual FormResult<FormModel, FormViewModel> Form()
    {
      FormResult<FormModel, FormViewModel> formResult = new FormResult<FormModel, FormViewModel>(this.FormRepository, (IAutoMapper<FormModel, FormViewModel>) this.Mapper);
      ((ViewResultBase) formResult).ViewData = ((ControllerBase) this).ViewData;
      ((ViewResultBase) formResult).TempData = ((ControllerBase) this).TempData;
      ((ViewResultBase) formResult).ViewEngineCollection = ((Controller) this).ViewEngineCollection;
      return formResult;
    }

    public virtual ProcessedFormResult<FormModel, FormViewModel> ProcessedForm(
      FormViewModel viewModel,
      string viewName = "")
    {
      ProcessedFormResult<FormModel, FormViewModel> processedFormResult1 = new ProcessedFormResult<FormModel, FormViewModel>(this.FormRepository, (IAutoMapper<FormModel, FormViewModel>) this.Mapper, this.FormProcessor, viewModel);
      ((ViewResultBase) processedFormResult1).ViewData = ((ControllerBase) this).ViewData;
      ((ViewResultBase) processedFormResult1).TempData = ((ControllerBase) this).TempData;
      ((ViewResultBase) processedFormResult1).ViewEngineCollection = ((Controller) this).ViewEngineCollection;
      ProcessedFormResult<FormModel, FormViewModel> processedFormResult2 = processedFormResult1;
      if (!string.IsNullOrEmpty(viewName))
        ((ViewResultBase) processedFormResult2).ViewName = viewName;
      return processedFormResult2;
    }
  }
}
