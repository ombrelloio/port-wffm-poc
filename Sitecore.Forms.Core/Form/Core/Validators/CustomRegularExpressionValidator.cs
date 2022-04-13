// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Validators.CustomRegularExpressionValidator
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Validators
{
  public class CustomRegularExpressionValidator : RegularExpressionValidator
  {
    public CustomRegularExpressionValidator() => this.ValidationExpression = "^(.|\\n)*$";

    public string PredefinedValidatorTextMessage
    {
      get => this.Text;
      set => this.Text = value;
    }

    [Browsable(false)]
    public string RegexPattern
    {
      get => this.ValidationExpression;
      set
      {
        if (string.IsNullOrEmpty(value))
          return;
        this.ValidationExpression = value;
      }
    }

    protected override bool EvaluateIsValid()
    {
      string input = string.Empty;
      string controlToValidate = this.ControlToValidate;
      if (controlToValidate.Length > 0)
      {
        input = this.GetControlValidationValue(controlToValidate);
        if (input == null || input.Trim().Length == 0)
          return true;
      }
      try
      {
        return Regex.Match(input, this.RegexPattern).Success;
      }
      catch
      {
        return true;
      }
    }
  }
}
