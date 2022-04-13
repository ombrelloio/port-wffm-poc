// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Controllers.FormErrorResult`2
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Interfaces;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Sitecore.Forms.Mvc.Controllers
{
  public class FormErrorResult<TFormModel, TFormViewModel> : PartialViewResult
    where TFormModel : class, IModelEntity
    where TFormViewModel : class, IViewModel, IHasId
  {
    private readonly TFormViewModel viewModel;

    public FormErrorResult(
      IRepository<TFormModel> formRepository,
      IAutoMapper<TFormModel, TFormViewModel> autoMapper,
      IFormProcessor<TFormModel> formProcessor,
      Sitecore.WFFM.Abstractions.Actions.ExecuteResult.Failure failure)
    {
      Assert.ArgumentNotNull((object) formProcessor, nameof (formProcessor));
      TFormModel model = formRepository.GetModel();
      Assert.IsNotNull((object) model, "model");
      model.Failures.Add(failure);
      formProcessor.Run(model);
      this.viewModel = autoMapper.GetView(model);
    }

    public override void ExecuteResult(ControllerContext context)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      ((ViewResultBase) this).ViewData.Model = (object) this.viewModel;
      if ((object) this.viewModel != null && context.HttpContext.Session != null && context.HttpContext.Session.Mode == SessionStateMode.InProc)
        context.HttpContext.Session[this.viewModel.UniqueId.ToString()] = (object) this.viewModel;
      base.ExecuteResult(context);
    }
  }
}
