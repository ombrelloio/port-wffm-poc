// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Services.Pipelines.SaveContent
// Assembly: Sitecore.WFFM.Services, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A002B65D-6063-457D-AAC3-54DF69ADFBA9
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Services.dll

using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Pipeline;
using System.IO;

namespace Sitecore.WFFM.Services.Pipelines
{
  public class SaveContent
  {
    public void Process(ExportArgs args)
    {
      Assert.ArgumentNotNullOrEmpty(args.FileName, "file name");
      Context.Job?.Status.LogInfo(DependenciesManager.ResourceManager.Localize("DUMPING_DATA"));
      File.WriteAllText(args.FileName, args.Result);
    }
  }
}
