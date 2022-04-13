// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.Fields.CheckboxField
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Forms.Mvc.Extensions;

namespace Sitecore.Forms.Mvc.ViewModels.Fields
{
  public class CheckboxField : ValuedFieldViewModel<bool>
  {
    public override bool Value { get; set; }

    public override void Initialize()
    {
      this.ShowTitle = false;
      string str = this.Parameters.GetValue("checked");
      this.Value = str != null && str.ToLower() == "yes";
    }

    public override void SetValueFromQuery(string valueFromQuery) => this.Value = MainUtil.GetBool(valueFromQuery, false);
  }
}
