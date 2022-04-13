// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Models.MetadataProviders.ModelTypeMetadataProvider
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Forms.Mvc.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Models.MetadataProviders
{
  public class ModelTypeMetadataProvider : DataAnnotationsModelMetadataProvider
  {
    private object containerModel;

    protected override ModelMetadata CreateMetadata(
      IEnumerable<Attribute> attributes,
      Type containerType,
      Func<object> modelAccessor,
      Type modelType,
      string propertyName)
    {
      ModelMetadata metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
      if (typeof (IContainerMetadata).IsAssignableFrom(modelType))
        this.containerModel = modelAccessor != null ? modelAccessor() : (object) null;
      if (typeof (IContainerMetadata).IsAssignableFrom(containerType))
        metadata.AdditionalValues.Add(Constants.Container, this.containerModel);
      return metadata;
    }
  }
}
