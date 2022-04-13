// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Rules.RuleRenderer
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Shell.Applications.Rules;
using Sitecore.WFFM.Abstractions.Dependencies;
using System.IO;
using System.Web.UI;
using System.Xml.Linq;

namespace Sitecore.Forms.Core.Rules
{
  public class RuleRenderer
  {
    public static readonly string DefualtCondition;
    public static string DefaultConditionName = DependenciesManager.ResourceManager.GetString("NEW_CONDITION");

    static RuleRenderer() => RuleRenderer.DefualtCondition = string.Format("<ruleset><rule uid=\"{0}\" name=\"{1}\"></rule></ruleset>", new object[2]
    {
      (object) "{0}",
      (object) DependenciesManager.TranslationProvider.Text(RuleRenderer.DefaultConditionName)
    });

    public static string Render(XElement rule)
    {
      Assert.ArgumentNotNull((object) rule, nameof (rule));
      return RuleRenderer.Render(rule.ToString());
    }

    public static string Render(string rule)
    {
      HtmlTextWriter htmlTextWriter = new HtmlTextWriter((TextWriter) new StringWriter());
      if (rule != "<ruleset />")
      {
        string str;
        if (!string.IsNullOrEmpty(rule))
          str = rule;
        else
          str = Sitecore.StringExtensions.StringExtensions.FormatWith(RuleRenderer.DefualtCondition, new object[1]
          {
            (object) ID.NewID
          });
        new RulesRenderer(str)
        {
          SkipActions = false,
          AllowMultiple = true,
          IsEditable = false
        }.Render(htmlTextWriter);
      }
      return htmlTextWriter.InnerWriter.ToString();
    }
  }
}
