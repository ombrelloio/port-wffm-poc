// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Controllers.Filters.WffmValidateAntiForgeryTokenAttribute
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Data.Wrappers;
using Sitecore.Forms.Mvc.ViewModels;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Controllers.Filters
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  public sealed class WffmValidateAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
  {
    public WffmValidateAntiForgeryTokenAttribute()
      : this((IRenderingContext) Factory.CreateObject(Constants.FormRenderingContext, true))
    {
    }

    public WffmValidateAntiForgeryTokenAttribute(IRenderingContext renderingContext)
    {
      Assert.ArgumentNotNull((object) renderingContext, nameof (renderingContext));
      this.RenderingContext = renderingContext;
    }

    public IRenderingContext RenderingContext { get; set; }

    public void OnAuthorization(AuthorizationContext filterContext)
    {
      Guid uniqueId = this.RenderingContext.Rendering.UniqueId;
      if (string.IsNullOrEmpty(((ControllerContext) filterContext).RequestContext.HttpContext.Request.Form[FormViewModel.GetClientId(uniqueId) + ".Id"]))
        return;
      if (AjaxRequestExtensions.IsAjaxRequest(((ControllerContext) filterContext).HttpContext.Request))
        this.ValidateRequestHeader(((ControllerContext) filterContext).HttpContext.Request);
      else
        AntiForgery.Validate();
    }

    private void ValidateRequestHeader(HttpRequestBase request)
    {
      NameValueCollection headers = request.Headers;
      AntiForgery.Validate(request.Cookies[AntiForgeryConfig.CookieName]?.Value, headers["X-RequestVerificationToken"]);
    }
  }
}
