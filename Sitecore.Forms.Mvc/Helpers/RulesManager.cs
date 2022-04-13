// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Helpers.RulesManager
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Forms.Core.Rules;
using Sitecore.Rules;

namespace Sitecore.Forms.Mvc.Helpers
{
  public class RulesManager
  {
    public static void RunRules(string rulesStr, object obj)
    {
      Assert.ArgumentNotNullOrEmpty(rulesStr, nameof (rulesStr));
      Assert.ArgumentNotNull(obj, nameof (obj));
      RuleList<ConditionalRuleContext> rules = RuleFactory.ParseRules<ConditionalRuleContext>(StaticSettings.ContextDatabase, rulesStr);
      if (rules.Count <= 0)
        return;
      ConditionalRuleContext conditionalRuleContext1 = new ConditionalRuleContext(obj);
      conditionalRuleContext1.Item = Context.Item;
      ConditionalRuleContext conditionalRuleContext2 = conditionalRuleContext1;
      rules.Run(conditionalRuleContext2);
    }
  }
}
