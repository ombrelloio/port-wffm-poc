// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.RazorGeneratorMvcStart
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using RazorGenerator.Mvc;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Sitecore.Forms.Core
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
