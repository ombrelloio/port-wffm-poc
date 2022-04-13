// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Services.Services.WebApiConfig
// Assembly: Sitecore.WFFM.Services, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A002B65D-6063-457D-AAC3-54DF69ADFBA9
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Services.dll

using System.Web.Http;

namespace Sitecore.WFFM.Services.Services
{
  public class WebApiConfig
  {
    public static void Register(HttpConfiguration config) => HttpRouteCollectionExtensions.MapHttpRoute(config.Routes, "WFFM_FormReports", "sitecore/api/WFFM/FormReports/{action}", (object) new
    {
      controller = "FormReports"
    });
  }
}
