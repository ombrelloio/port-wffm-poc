// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Validators.NumberRangeValidator
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.Globalization;

namespace Sitecore.Form.Core.Validators
{
  public class NumberRangeValidator : FormCustomValidator
  {
    public NumberRangeValidator()
    {
      this.MinimumValue = "0";
      this.MaximumValue = double.MaxValue.ToString((IFormatProvider) CultureInfo.InvariantCulture);
    }

    public string MaximumValue
    {
      get => this.classAttributes["maximum"] ?? string.Empty;
      set => this.classAttributes["maximum"] = value;
    }

    public string MinimumValue
    {
      get => this.classAttributes["minimum"] ?? string.Empty;
      set => this.classAttributes["minimum"] = value;
    }

    protected override void OnLoad(EventArgs e)
    {
      this.ErrorMessage = string.Format(this.ErrorMessage, (object) "{0}", (object) this.MinimumValue, (object) this.MaximumValue);
      this.Text = string.Format(this.Text, (object) "{0}", (object) this.MinimumValue, (object) this.MaximumValue);
      base.OnLoad(e);
    }

    protected override bool OnServerValidate(string value)
    {
      if (string.IsNullOrEmpty(value))
        return true;
      double result1;
      double.TryParse(this.MinimumValue, out result1);
      double result2;
      if (!double.TryParse(this.MaximumValue, out result2))
        result2 = double.MaxValue;
      double result3;
      return double.TryParse(value, out result3) && result3 >= result1 && result3 <= result2;
    }
  }
}
