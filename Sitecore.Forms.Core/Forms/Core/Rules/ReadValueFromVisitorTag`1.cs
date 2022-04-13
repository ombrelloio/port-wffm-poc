// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Rules.ReadValueFromVisitorTag`1
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.WFFM.Abstractions.Dependencies;

namespace Sitecore.Forms.Core.Rules
{
  public class ReadValueFromVisitorTag<T> : ReadValue<T> where T : ConditionalRuleContext
  {
    public override void Apply(T ruleContext)
    {
      if (!Context.User.IsAuthenticated)
        return;
      base.Apply(ruleContext);
    }

    protected override object GetValue() => (object) DependenciesManager.AnalyticsTracker.GetTag(this.Name);
  }
}
