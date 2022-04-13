// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Rules.DisableControlAction`1
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Rules.Actions;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Core.Rules
{
  public class DisableControlAction<T> : RuleAction<T> where T : ConditionalRuleContext
  {
    public override void Apply(T ruleContext)
    {
      Assert.ArgumentNotNull((object) ruleContext, nameof (ruleContext));
      if (ruleContext.Control == null || !(ruleContext.Control is WebControl))
        return;
      ((WebControl) ruleContext.Control).Enabled = false;
    }
  }
}
