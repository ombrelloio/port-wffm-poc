// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.RazorGeneratorMvcStart
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using RazorGenerator.Mvc;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Sitecore.Forms.Mvc
{
  public static class RazorGeneratorMvcStart
  {
    public static void Start()
    {
      PrecompiledMvcEngine precompiledMvcEngine = new PrecompiledMvcEngine(typeof (RazorGeneratorMvcStart).Assembly)
      {
        UsePhysicalViewsIfNewer = HttpContext.Current.Request.IsLocal
      };
      ((Collection<IViewEngine>) ViewEngines.Engines).Insert(0, (IViewEngine) precompiledMvcEngine);
      VirtualPathFactoryManager.RegisterVirtualPathFactory((IVirtualPathFactory) precompiledMvcEngine);
    }
  }
}
