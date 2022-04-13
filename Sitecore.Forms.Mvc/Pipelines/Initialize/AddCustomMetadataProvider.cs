// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Pipelines.Initialize.AddCustomMetadataProvider
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Abstractions;
using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Controllers.ModelBinders;
using Sitecore.Forms.Mvc.Metadata;
using Sitecore.Forms.Mvc.ViewModels;
using Sitecore.Pipelines;
using Sitecore.WFFM.Abstractions.Shared;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Pipelines.Initialize
{
  public class AddCustomMetadataProvider
  {
    private readonly IPerRequestStorage _perRequestStorage;
    private readonly BaseCorePipelineManager _baseCorePipelineManager;

    public AddCustomMetadataProvider(
      IPerRequestStorage perRequestStorage,
      BaseCorePipelineManager baseCorePipelineManager)
    {
      Assert.IsNotNull((object) perRequestStorage, nameof (perRequestStorage));
      Assert.IsNotNull((object) baseCorePipelineManager, nameof (baseCorePipelineManager));
      this._perRequestStorage = perRequestStorage;
      this._baseCorePipelineManager = baseCorePipelineManager;
    }

    public virtual void Process(PipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      ModelMetadataProviders.Current = (ModelMetadataProvider) new PipelineBasedDataAnnotationsModelMetadataProvider(this._perRequestStorage, this._baseCorePipelineManager);
      System.Web.Mvc.ModelBinders.Binders.Add(typeof (SectionViewModel), (IModelBinder) new SectionModelBinder());
      System.Web.Mvc.ModelBinders.Binders.Add(typeof (FieldViewModel), (IModelBinder) new FieldModelBinder());
    }
  }
}
