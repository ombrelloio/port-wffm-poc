// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Pipelines.FormProcessorBase`1
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Forms.Mvc.Interfaces;

namespace Sitecore.Forms.Mvc.Pipelines
{
  public abstract class FormProcessorBase<TFormModel> where TFormModel : IFormModel
  {
    public abstract void Process(FormProcessorArgs<TFormModel> args);
  }
}
