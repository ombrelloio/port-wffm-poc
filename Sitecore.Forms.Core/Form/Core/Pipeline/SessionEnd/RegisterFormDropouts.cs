// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipeline.SessionEnd.RegisterFormDropouts
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Pipelines.SessionEnd;
using Sitecore.WFFM.Abstractions.Dependencies;

namespace Sitecore.Form.Core.Pipeline.SessionEnd
{
  public class RegisterFormDropouts
  {
    public void Process(SessionEndArgs endArgs)
    {
      Assert.ArgumentNotNull((object) endArgs, nameof (endArgs));
      DependenciesManager.AnalyticsTracker.RegisterFormDropouts();
    }
  }
}
