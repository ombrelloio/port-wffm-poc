// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Controllers.Filters.AllowCrossSiteJsonAttribute
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Controllers.Filters
{
  public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      ((ControllerContext) filterContext).RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", ((ControllerContext) filterContext).HttpContext.Request.UrlReferrer.Scheme + "://" + ((ControllerContext) filterContext).HttpContext.Request.UrlReferrer.Host);
      ((ControllerContext) filterContext).RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Credentials", "true");
      base.OnActionExecuting(filterContext);
    }
  }
}
