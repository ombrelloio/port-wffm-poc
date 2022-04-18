// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Metadata.PipelineBasedDataAnnotationsModelMetadataProvider
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Abstractions;
using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.Pipelines.CreateMetadata;
using Sitecore.Pipelines;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Metadata
{
  public class PipelineBasedDataAnnotationsModelMetadataProvider : 
    DataAnnotationsModelMetadataProvider
  {
    public const string StorageKey = "wffm.ModelMetadataKey";
    private readonly IPerRequestStorage _perRequestStorage;
    private readonly BaseCorePipelineManager _baseCorePipelineManager;

    public PipelineBasedDataAnnotationsModelMetadataProvider(
      IPerRequestStorage perRequestStorage,
      BaseCorePipelineManager baseCorePipelineManager)
    {
      Assert.ArgumentNotNull((object) perRequestStorage, nameof (perRequestStorage));
      Assert.IsNotNull((object) baseCorePipelineManager, nameof (baseCorePipelineManager));
      this._perRequestStorage = perRequestStorage;
      this._baseCorePipelineManager = baseCorePipelineManager;
    }

    public override IEnumerable<ModelMetadata> GetMetadataForProperties(
      object container,
      Type containerType)
    {
      IEnumerable<ModelMetadata> metadataForProperties = base.GetMetadataForProperties(container, containerType);
      if (!(metadataForProperties is ModelMetadata[] modelMetadataArray1))
        modelMetadataArray1 = metadataForProperties.ToArray<ModelMetadata>();
      ModelMetadata[] modelMetadataArray2 = modelMetadataArray1;
      foreach (ModelMetadata metaData in modelMetadataArray2)
        this.RunCreateMetadataPipeline(containerType, metaData);
      return (IEnumerable<ModelMetadata>) modelMetadataArray2;
    }

    public override ModelMetadata GetMetadataForProperty(
      Func<object> modelAccessor,
      Type containerType,
      string propertyName)
    {
      ModelMetadata metadataForProperty = base.GetMetadataForProperty(modelAccessor, containerType, propertyName);
      this.RunCreateMetadataPipeline(containerType, metadataForProperty);
      return metadataForProperty;
    }

    public override ModelMetadata GetMetadataForType(
      Func<object> modelAccessor,
      Type modelType)
    {
      ModelMetadata metadataForType = base.GetMetadataForType(modelAccessor, modelType);
      this.SaveModel(modelAccessor, modelType);
      return metadataForType;
    }

    private void SaveModel(Func<object> modelAccessor, Type modelType)
    {
      if (modelAccessor == null || !typeof (IContainerMetadata).IsAssignableFrom(modelType))
        return;
      object obj = modelAccessor();
      if (obj == null)
        return;
      this._perRequestStorage.Put("wffm.ModelMetadataKey", obj);
    }

    private void RunCreateMetadataPipeline(Type containerType, ModelMetadata metaData)
    {
      object model = this._perRequestStorage.Get("wffm.ModelMetadataKey");
      if (model == null)
        return;
      this._baseCorePipelineManager.Run("wffm.createMetadata", (PipelineArgs) new CreateMetadataArgs(metaData, containerType, model));
    }
  }
}
