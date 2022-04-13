// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Controllers.Filters.SubmittedFormHandler
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Interfaces;
using System.Linq;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Controllers.Filters
{
  public class SubmittedFormHandler : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      Assert.ArgumentNotNull((object) filterContext, nameof (filterContext));
      if (!(((ControllerContext) filterContext).Controller is FormController controller) || filterContext.ActionParameters.Values.First<object>() is IViewModel)
        return;
      filterContext.Result = (ActionResult) controller.Form();
    }
  }
}
