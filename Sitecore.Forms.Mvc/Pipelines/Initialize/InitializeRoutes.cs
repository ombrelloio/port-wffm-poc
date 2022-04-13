// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Pipelines.Initialize.InitializeRoutes
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Mvc.Pipelines.Loader;
using Sitecore.Pipelines;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sitecore.Forms.Mvc.Pipelines.Initialize
{
  public class InitializeRoutes : Sitecore.Mvc.Pipelines.Loader.InitializeRoutes
  {
    protected override void RegisterRoutes(RouteCollection routes, PipelineArgs args) => InitializeRoutes.RegisterClientEventController(routes);

    private static void RegisterClientEventController(RouteCollection routes)
    {
      RouteCollectionExtensions.MapRoute(routes, Constants.Routes.ClientEvent, "clientevent/{action}", (object) new
      {
        controller = "ClientEvent",
        action = "Process"
      });
      RouteCollectionExtensions.MapRoute(routes, Constants.Routes.Form, "form/{action}", (object) new
      {
        controller = "Form",
        action = "Process"
      });
    }
  }
}
