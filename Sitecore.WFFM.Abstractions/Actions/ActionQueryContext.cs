// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Actions.ActionQueryContext
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.WFFM.Abstractions.Data;

namespace Sitecore.WFFM.Abstractions.Actions
{
  public class ActionQueryContext
  {
    public ActionQueryContext(IFormItem form)
      : this(form, (ITracking) null, (IFormDefinition) null)
    {
    }

    public ActionQueryContext(IFormItem form, ITracking tracking, IFormDefinition definition)
    {
      this.Form = form;
      this.Tracking = tracking;
      this.Structure = definition;
    }

    public IFormItem Form { get; private set; }

    public ITracking Tracking { get; private set; }

    public IFormDefinition Structure { get; private set; }
  }
}
