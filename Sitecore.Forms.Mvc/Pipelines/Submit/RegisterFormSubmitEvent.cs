// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Pipelines.Submit.RegisterFormSubmitEvent
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.Models;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Dependencies;

namespace Sitecore.Forms.Mvc.Pipelines.Submit
{
  public class RegisterFormSubmitEvent : FormProcessorBase<IFormModel>
  {
    public override void Process(FormProcessorArgs<IFormModel> args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (!args.Model.Item.IsAnalyticsEnabled)
        return;
      DependenciesManager.AnalyticsTracker.BasePageTime = args.Model.RenderedTime;
      FormModel model = (FormModel) args.Model;
      DependenciesManager.AnalyticsTracker.TriggerEvent(PageEventIds.FormSubmit, "Form Submit", model.Item.ID, string.Empty, ((object) model.Item.ID).ToString());
    }
  }
}
