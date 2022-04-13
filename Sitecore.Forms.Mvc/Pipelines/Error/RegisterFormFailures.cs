// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Pipelines.Error.RegisterFormFailures
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Interfaces;

namespace Sitecore.Forms.Mvc.Pipelines.Error
{
  public class RegisterFormFailures : FormProcessorBase<IFormModel>
  {
    public override void Process(FormProcessorArgs<IFormModel> args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      int num = args.Model.Item.IsAnalyticsEnabled ? 1 : 0;
    }
  }
}
