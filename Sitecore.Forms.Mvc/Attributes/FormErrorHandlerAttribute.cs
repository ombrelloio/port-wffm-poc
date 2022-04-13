// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Attributes.FormErrorHandlerAttribute
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Controllers;
using Sitecore.Forms.Mvc.Data.Wrappers;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.Models;
using Sitecore.Forms.Mvc.ViewModels;
using Sitecore.WFFM.Abstractions.Actions;
using System;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Attributes
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
  public class FormErrorHandlerAttribute : HandleErrorAttribute
  {
    private readonly IRenderingContext renderingContext;

    public FormErrorHandlerAttribute()
      : this((IRenderingContext) Factory.CreateObject(Constants.FormRenderingContext, true))
    {
    }

    public FormErrorHandlerAttribute(IRenderingContext renderingContext)
    {
      Assert.ArgumentNotNull((object) renderingContext, nameof (renderingContext));
      this.renderingContext = renderingContext;
    }

    public override void OnException(ExceptionContext filterContext)
    {
            if (filterContext.ExceptionHandled)
                return;
            Guid uniqueId = this.renderingContext.Rendering.UniqueId;
            if (string.IsNullOrEmpty(filterContext.RequestContext.HttpContext.Request.Form[FormViewModel.GetClientId(uniqueId) + ".Id"]))
                return;
            Log.Error(filterContext.Exception.Message, filterContext.Exception, (object)this);
            FormController controller = filterContext.Controller as FormController;
            FormErrorResult<FormModel, FormViewModel> formErrorResult1 = (FormErrorResult<FormModel, FormViewModel>)null;
            if (controller != null)
            {
                FormErrorResult<FormModel, FormViewModel> formErrorResult2 = new FormErrorResult<FormModel, FormViewModel>(controller.FormRepository, (IAutoMapper<FormModel, FormViewModel>)controller.Mapper, controller.FormProcessor, new ExecuteResult.Failure()
                {
                    ErrorMessage = filterContext.Exception.Message,
                    StackTrace = filterContext.Exception.StackTrace,
                    IsCustom = false
                });
                formErrorResult2.ViewData = controller.ViewData;
                formErrorResult2.TempData = controller.TempData;
                formErrorResult2.ViewEngineCollection = controller.ViewEngineCollection;
                formErrorResult1 = formErrorResult2;
            }
            filterContext.Result = formErrorResult1 != null ? (ActionResult)formErrorResult1 : (ActionResult)new EmptyResult();
            filterContext.ExceptionHandled = true;
        }
  }
}
