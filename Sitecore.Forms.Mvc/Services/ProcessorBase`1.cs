// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Services.ProcessorBase`1
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.WFFM.Abstractions.Actions;
using System.Linq;

namespace Sitecore.Forms.Mvc.Services
{
  public abstract class ProcessorBase<TFormModel> : IFormProcessor<TFormModel> where TFormModel : class, IModelEntity
  {
    public void Run(TFormModel model)
    {
      Assert.ArgumentNotNull((object) model, nameof (model));
      this.Submit(model);
      if (!model.IsValid)
        return;
      this.Validate(model);
      if (!model.IsValid || model.Failures.Any<ExecuteResult.Failure>())
      {
        this.Error(model);
      }
      else
      {
        this.ExecuteSaveActions(model);
        if (model.Failures.Any<ExecuteResult.Failure>())
          this.Error(model);
        else
          this.Success(model);
      }
    }

    protected abstract void Submit(TFormModel model);

    protected abstract void ExecuteSaveActions(TFormModel model);

    protected abstract void Validate(TFormModel model);

    protected abstract void Error(TFormModel model);

    protected abstract void Success(TFormModel model);
  }
}
