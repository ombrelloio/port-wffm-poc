// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Controllers.FormResult`2
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Interfaces;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Sitecore.Forms.Mvc.Controllers
{
  public class FormResult<TFormModel, TFormViewModel> : PartialViewResult
    where TFormModel : class, IModelEntity
    where TFormViewModel : class, IViewModel, IHasId
  {
    public FormResult(
      IRepository<TFormModel> formRepository,
      IAutoMapper<TFormModel, TFormViewModel> autoMapper)
    {
      Assert.ArgumentNotNull((object) formRepository, nameof (formRepository));
      Assert.ArgumentNotNull((object) autoMapper, nameof (autoMapper));
      this.FormRepository = formRepository;
      this.Mapper = autoMapper;
    }

    public IRepository<TFormModel> FormRepository { get; private set; }

    public IAutoMapper<TFormModel, TFormViewModel> Mapper { get; private set; }

    public override void ExecuteResult(ControllerContext context)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      TFormViewModel view = this.GetView();
      ((ViewResultBase) this).ViewData.Model = (object) view;
      if ((object) view != null && context.HttpContext.Session != null && context.HttpContext.Session.Mode == SessionStateMode.InProc)
        context.HttpContext.Session[view.UniqueId.ToString()] = (object) view;
      this.BaseExecuteResult(context);
    }

    protected virtual void BaseExecuteResult(ControllerContext context) => ((ViewResultBase) this).ExecuteResult(context);

    protected virtual TFormViewModel GetView()
    {
      TFormModel model = this.FormRepository.GetModel();
      Assert.IsNotNull((object) model, "model");
      return this.Mapper.GetView(model);
    }
  }
}
