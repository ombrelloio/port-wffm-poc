// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Rules.Rule
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Rules;
using System;
using System.Web.UI;

namespace Sitecore.Forms.Core.Rules
{
  public class Rule
  {
    public static void Run(string ruleXml, Control control)
    {
      Assert.ArgumentNotNull((object) control, nameof (control));
      if (string.IsNullOrEmpty(ruleXml))
        return;
      try
      {
        RuleList<ConditionalRuleContext> rules = RuleFactory.ParseRules<ConditionalRuleContext>(StaticSettings.ContextDatabase, ruleXml);
        ConditionalRuleContext conditionalRuleContext1 = new ConditionalRuleContext(control);
        conditionalRuleContext1.Item = Context.Item;
        ConditionalRuleContext conditionalRuleContext2 = conditionalRuleContext1;
        rules.Run(conditionalRuleContext2);
      }
      catch (Exception ex)
      {
      }
    }
  }
}
