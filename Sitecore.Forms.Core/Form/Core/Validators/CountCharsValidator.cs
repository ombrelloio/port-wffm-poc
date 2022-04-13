// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Validators.CountCharsValidator
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Validators
{
  public class CountCharsValidator : RegularExpressionValidator, ICloneable
  {
    private int maxLength = 256;
    private int minLength;

    public CountCharsValidator() => this.ValidationExpression = "^(.|\\n){" + (object) this.minLength + "," + (object) this.maxLength + "}$";

    [Browsable(true)]
    public int MaxLength
    {
      get => this.maxLength;
      set
      {
        this.maxLength = value;
        this.ValidationExpression = "(.|\\n){" + (object) this.minLength + "," + (object) value + "}$";
      }
    }

    [Browsable(true)]
    public int MinLength
    {
      get => this.minLength;
      set
      {
        this.minLength = value;
        this.ValidationExpression = "(.|\\n){" + (object) value + "," + (object) this.maxLength + "}$";
      }
    }

    public new string ValidationExpression
    {
      get => base.ValidationExpression;
      set => base.ValidationExpression = value ?? string.Empty;
    }

    public virtual object Clone()
    {
      CountCharsValidator countCharsValidator = new CountCharsValidator();
      countCharsValidator.CopyBaseAttributes((WebControl) this);
      countCharsValidator.ErrorMessage = this.ErrorMessage;
      countCharsValidator.Text = this.Text;
      countCharsValidator.MinLength = this.MinLength;
      countCharsValidator.MaxLength = this.MaxLength;
      countCharsValidator.ToolTip = this.ToolTip;
      countCharsValidator.ValidationExpression = this.ValidationExpression;
      countCharsValidator.ValidationGroup = this.ValidationGroup;
      countCharsValidator.Display = this.Display;
      countCharsValidator.ID = this.ID;
      countCharsValidator.ControlToValidate = this.ControlToValidate;
      countCharsValidator.CssClass = this.CssClass;
      countCharsValidator.EnableClientScript = this.EnableClientScript;
      countCharsValidator.Enabled = this.Enabled;
      countCharsValidator.ForeColor = this.ForeColor;
      countCharsValidator.SetFocusOnError = this.SetFocusOnError;
      return (object) countCharsValidator;
    }

    protected override bool EvaluateIsValid()
    {
      string str = string.Empty;
      string controlToValidate = this.ControlToValidate;
      if (controlToValidate.Length > 0)
      {
        str = this.GetControlValidationValue(controlToValidate);
        if (str == null)
          return true;
      }
      try
      {
        return str.Length >= this.minLength && str.Length <= this.maxLength;
      }
      catch
      {
        return true;
      }
    }

    protected override void OnLoad(EventArgs e)
    {
      try
      {
        this.Text = Sitecore.StringExtensions.StringExtensions.FormatWith(this.Text, new object[3]
        {
          (object) string.Empty,
          (object) this.minLength,
          (object) this.maxLength
        });
        this.ErrorMessage = Sitecore.StringExtensions.StringExtensions.FormatWith(this.ErrorMessage, new object[3]
        {
          (object) string.Empty,
          (object) this.minLength,
          (object) this.maxLength
        });
      }
      catch (FormatException ex)
      {
        this.StripCurlyBracket();
      }
      base.OnLoad(e);
    }

    protected void StripCurlyBracket()
    {
      int length = this.Text.Length;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < length; ++index)
      {
        if (this.Text[index] == '{')
        {
          if (index + 2 < length && !Regex.IsMatch(this.ErrorMessage.Substring(index, 3), "{\\d+}"))
          {
            stringBuilder.Append("{{");
            continue;
          }
        }
        else if (this.Text[index] == '}' && index - 3 >= 0 && !Regex.IsMatch(this.ErrorMessage.Substring(index - 3, 4), "{\\d+}"))
        {
          stringBuilder.Append("}}");
          continue;
        }
        stringBuilder.Append(this.Text[index]);
      }
      if (this.ErrorMessage == this.Text)
        this.Text = stringBuilder.ToString();
      this.Text = Sitecore.StringExtensions.StringExtensions.FormatWith(this.Text, new object[3]
      {
        (object) string.Empty,
        (object) this.minLength,
        (object) this.maxLength
      });
      this.ErrorMessage = stringBuilder.ToString();
      this.ErrorMessage = Sitecore.StringExtensions.StringExtensions.FormatWith(this.ErrorMessage, new object[3]
      {
        (object) string.Empty,
        (object) this.minLength,
        (object) this.maxLength
      });
    }
  }
}
