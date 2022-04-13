// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.Fields.NumberField
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Forms.Mvc.Validators;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Sitecore.Forms.Mvc.ViewModels.Fields
{
  public class NumberField : ValuedFieldViewModel<string>
  {
    [DefaultValue(0)]
    public double MinimumValue { get; set; }

    [DefaultValue(double.MaxValue)]
    public double MaximumValue { get; set; }

    [RegularExpression("^[-,+]{0,1}\\d*\\.{0,1}\\d+$", ErrorMessage = "Field contains an invalid number.")]
    [DynamicRange("MinimumValue", "MaximumValue", ErrorMessage = "The number in {0} must be at least {1} and no more than {2}.")]
    public override string Value { get; set; }

    public override void Initialize()
    {
      if (this.MaximumValue != 0.0)
        return;
      this.MaximumValue = (double) int.MaxValue;
    }
  }
}
