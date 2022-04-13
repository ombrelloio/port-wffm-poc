// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Rules.ReadValue`1
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Rules.Actions;
using System.Web.UI;

namespace Sitecore.Forms.Core.Rules
{
  public abstract class ReadValue<T> : RuleAction<T> where T : ConditionalRuleContext
  {
    public override void Apply(T ruleContext)
    {
      Assert.ArgumentNotNull((object) ruleContext, nameof (ruleContext));
      if (string.IsNullOrEmpty(this.Name))
        return;
      object obj = this.GetValue();
      if (obj == null)
        return;
      if (ruleContext.Control != null)
      {
        this.SetValue(ruleContext.Control, obj);
      }
      else
      {
        if (ruleContext.Model == null)
          return;
        ReflectionUtils.SetProperty(ruleContext.Model, "Value", obj, false);
      }
    }

    protected abstract object GetValue();

    protected virtual void SetValue(Control control, object value) => this.SetValue(control, value, "DefaultValue");

    protected virtual void SetValue(Control control, object value, string property)
    {
      if (control is IResult)
        ((IResult) control).DefaultValue = value.ToString();
      else
        ReflectionUtils.SetProperty((object) control, property, value, false);
    }

    public string Name { get; set; }
  }
}
