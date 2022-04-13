// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Controllers.ModelBinders.DefaultFormModelBinder
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Data.Wrappers;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.Models;
using Sitecore.Forms.Mvc.ViewModels;
using System;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Sitecore.Forms.Mvc.Controllers.ModelBinders
{
  public class DefaultFormModelBinder : DefaultModelBinder
  {
    public DefaultFormModelBinder()
      : this((IRenderingContext) Factory.CreateObject(Constants.FormRenderingContext, true))
    {
    }

    public DefaultFormModelBinder(IRenderingContext renderingContext)
    {
      Assert.ArgumentNotNull((object) renderingContext, nameof (renderingContext));
      this.RenderingContext = renderingContext;
    }

    public virtual FormViewModel GetFormViewModel(ControllerContext controllerContext)
    {
      Assert.ArgumentNotNull((object) controllerContext, nameof (controllerContext));
      if (this.RenderingContext == null || this.RenderingContext.Rendering == null)
        return (FormViewModel) null;
      Guid uniqueId = this.RenderingContext.Rendering.UniqueId;
      if (controllerContext.HttpContext.Session != null && controllerContext.HttpContext.Session.Mode == SessionStateMode.InProc && controllerContext.HttpContext.Session[uniqueId.ToString()] is FormViewModel formViewModel)
      {
        formViewModel.SuccessSubmit = false;
        return formViewModel;
      }
      if (controllerContext.Controller is FormController controller)
      {
        FormModel model = controller.FormRepository.GetModel(uniqueId);
        if (model != null)
          return controller.Mapper.GetView((IFormModel) model);
      }
      return (FormViewModel) null;
    }

    public virtual string GetPrefix(Guid id) => !(id == Guid.Empty) ? FormViewModel.GetClientId(id) : (string) null;

    public IRenderingContext RenderingContext { get; private set; }
  }
}
