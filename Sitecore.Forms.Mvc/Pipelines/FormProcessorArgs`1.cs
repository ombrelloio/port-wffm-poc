// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Pipelines.FormProcessorArgs`1
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Pipelines;

namespace Sitecore.Forms.Mvc.Pipelines
{
  public class FormProcessorArgs<TFormModel> : PipelineArgs where TFormModel : IFormModel
  {
    public FormProcessorArgs(TFormModel model)
    {
      Assert.ArgumentNotNull((object) model, nameof (model));
      this.Model = model;
    }

    public TFormModel Model { get; private set; }
  }
}
