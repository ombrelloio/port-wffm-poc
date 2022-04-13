// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Rules.HideControlAction`1
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Utility;
using Sitecore.Rules.Actions;

namespace Sitecore.Forms.Core.Rules
{
  public class HideControlAction<T> : RuleAction<T> where T : ConditionalRuleContext
  {
    public override void Apply(T ruleContext)
    {
      Assert.ArgumentNotNull((object) ruleContext, nameof (ruleContext));
      if (ruleContext.Control != null)
      {
        ruleContext.Control.Visible = false;
      }
      else
      {
        if (ruleContext.Model == null)
          return;
        ReflectionUtils.SetProperty(ruleContext.Model, "Visible", (object) false);
      }
    }
  }
}
