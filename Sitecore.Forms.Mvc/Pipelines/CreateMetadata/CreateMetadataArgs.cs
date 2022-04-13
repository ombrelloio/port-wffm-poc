// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Pipelines.CreateMetadata.CreateMetadataArgs
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Pipelines;
using System;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Pipelines.CreateMetadata
{
  public class CreateMetadataArgs : PipelineArgs
  {
    public CreateMetadataArgs(ModelMetadata metadata, Type containerType, object model)
    {
      Assert.ArgumentNotNull((object) metadata, nameof (metadata));
      Assert.ArgumentNotNull((object) containerType, nameof (containerType));
      this.Metadata = metadata;
      this.ContainerType = containerType;
      this.Model = model;
    }

    public ModelMetadata Metadata { get; private set; }

    public Type ContainerType { get; private set; }

    public object Model { get; private set; }
  }
}
