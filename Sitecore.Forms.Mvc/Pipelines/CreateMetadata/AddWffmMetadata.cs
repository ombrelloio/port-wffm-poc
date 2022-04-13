// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Pipelines.CreateMetadata.AddWffmMetadata
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Interfaces;

namespace Sitecore.Forms.Mvc.Pipelines.CreateMetadata
{
  public class AddWffmMetadata : CreateMetadataProcessorBase
  {
    public override void Process(CreateMetadataArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (!typeof (IContainerMetadata).IsAssignableFrom(args.ContainerType) || args.Model == null)
        return;
      args.Metadata.AdditionalValues[Constants.Container] = args.Model;
    }
  }
}
