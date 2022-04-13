// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.Fields.CreditCardField
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Forms.Mvc.Attributes;
using Sitecore.WFFM.Abstractions.Actions;
using System.ComponentModel.DataAnnotations;

namespace Sitecore.Forms.Mvc.ViewModels.Fields
{
  public class CreditCardField : SingleLineTextField
  {
    public CreditCardField() => this.Tracking = false;

    [ParameterName("CardNumberHelp")]
    public override string Information { get; set; }

    [CreditCard]
    public override string Value { get; set; }

    public override ControlResult GetResult() => new ControlResult(this.FieldItemId, this.Title, (object) this.Value, Sitecore.StringExtensions.StringExtensions.FormatWith("secure:<schidden>{0}</schidden>", new object[1]
    {
      (object) this.Value
    }), true);

    public override void SetValueFromQuery(string valueFromQuery)
    {
    }
  }
}
