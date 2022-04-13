// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Attributes.ParameterConverterAttribute
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Newtonsoft.Json;
using Sitecore.WFFM.Abstractions.Analytics;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Attributes
{
  public class ParameterConverterAttribute : FilterAttribute, IActionFilter
  {
    private readonly string name;

    public ParameterConverterAttribute(string parameterName) => this.name = parameterName;

    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if (string.IsNullOrEmpty(((ControllerContext) filterContext).HttpContext.Request.QueryString["callback"]))
        return;
      List<ClientEvent> clientEventList = JsonConvert.DeserializeObject<List<ClientEvent>>(((ControllerContext) filterContext).HttpContext.Request.QueryString[1]);
      filterContext.ActionParameters[this.name] = (object) clientEventList;
    }

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
    }
  }
}
