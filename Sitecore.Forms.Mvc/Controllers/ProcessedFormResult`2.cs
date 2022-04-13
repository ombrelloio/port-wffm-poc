// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Controllers.ProcessedFormResult`2
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Controllers
{
  public class ProcessedFormResult<TFormModel, TFormViewModel> : 
    FormResult<TFormModel, TFormViewModel>
    where TFormModel : class, IModelEntity
    where TFormViewModel : class, IViewModel, IHasId
  {
    public ProcessedFormResult(
      IRepository<TFormModel> formRepository,
      IAutoMapper<TFormModel, TFormViewModel> autoMapper,
      IFormProcessor<TFormModel> formProcessor,
      TFormViewModel viewModel)
      : base(formRepository, autoMapper)
    {
      Assert.ArgumentNotNull((object) formProcessor, nameof (formProcessor));
      this.FormProcessor = formProcessor;
      this.ViewModel = viewModel;
    }

    public IFormProcessor<TFormModel> FormProcessor { get; private set; }

    public TFormViewModel ViewModel { get; set; }

    public override void ExecuteResult(ControllerContext context)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      if ((object) this.ViewModel == null)
        base.ExecuteResult(context);
      else if (!context.Controller.ViewData.ModelState.IsValid)
      {
        ((ViewResultBase) this).ViewData.Model = (object) this.ViewModel;
        this.BaseExecuteResult(context);
      }
      else
      {
        TFormModel model = this.FormRepository.GetModel(this.ViewModel.UniqueId);
        Assert.IsNotNull((object) model, "model");
        this.Mapper.SetModelResults(this.ViewModel, model);
        this.FormProcessor.Run(model);
        bool flag = model.IsValid;
        if (model.Failures.Count > 0)
        {
          flag = false;
          if (this.ViewModel is IHasErrors viewModel8)
          {
            List<string> list = model.Failures.Select<Sitecore.WFFM.Abstractions.Actions.ExecuteResult.Failure, string>((Func<Sitecore.WFFM.Abstractions.Actions.ExecuteResult.Failure, string>) (x => x.ErrorMessage)).Distinct<string>().ToList<string>();
            viewModel8.Errors = list;
          }
        }
                ISubmitSettings submitSettings = ViewModel as ISubmitSettings;
                if (submitSettings != null)
                {
                    submitSettings.SuccessSubmit = flag;
                }
                if (flag && model.RedirectOnSuccess && !string.IsNullOrEmpty(model.SuccessRedirectUrl))
                {
                    RedirectToSuccessPage(context, model.SuccessRedirectUrl);
                    return;
                }
        ((ViewResultBase)this).ViewData.Model = ((object)submitSettings);
                BaseExecuteResult(context);
            }
    }

    private void RedirectToSuccessPage(ControllerContext context, string url)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      Assert.ArgumentNotNullOrEmpty(url, nameof (url));
      if (context.IsChildAction)
        throw new InvalidOperationException("Cannot redirect in child action");
      ((ViewResultBase) this).ViewName = "SuccessRedirect";
      ((ViewResultBase) this).ViewData.Model = (object) UrlHelper.GenerateContentUrl(url, context.HttpContext);
      this.BaseExecuteResult(context);
    }
  }
}
