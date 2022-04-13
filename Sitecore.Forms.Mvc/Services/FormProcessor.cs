// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Services.FormProcessor
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.Models;
using Sitecore.Forms.Mvc.Pipelines;
using Sitecore.Pipelines;

namespace Sitecore.Forms.Mvc.Services
{
  public class FormProcessor : ProcessorBase<FormModel>
  {
    protected override void Submit(FormModel model)
    {
      Assert.ArgumentNotNull((object) model, nameof (model));
      this.RunPipeline("wffm.submit", model);
    }

    protected override void ExecuteSaveActions(FormModel model)
    {
      Assert.ArgumentNotNull((object) model, nameof (model));
      this.RunPipeline("wffm.executeSaveActions", model);
    }

    protected override void Validate(FormModel model)
    {
      Assert.ArgumentNotNull((object) model, nameof (model));
      this.RunPipeline("wffm.validate", model);
    }

    protected override void Error(FormModel model)
    {
      Assert.ArgumentNotNull((object) model, nameof (model));
      this.RunPipeline("wffm.error", model);
    }

    protected override void Success(FormModel model)
    {
      Assert.ArgumentNotNull((object) model, nameof (model));
      this.RunPipeline("wffm.success", model);
    }

    protected virtual void RunPipeline(string pipelineName, FormModel model)
    {
      Assert.ArgumentNotNullOrEmpty(pipelineName, nameof (pipelineName));
      Assert.ArgumentNotNull((object) model, nameof (model));
      FormProcessorArgs<IFormModel> formProcessorArgs = new FormProcessorArgs<IFormModel>((IFormModel) model);
      CorePipeline.Run(pipelineName, (PipelineArgs) formProcessorArgs);
    }
  }
}
