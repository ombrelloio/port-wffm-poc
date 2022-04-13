// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Validators.DynamicRegularExpressionAttribute
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Extensions;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Validators
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
  [DisplayName("TITLE_ERROR_MESSAGE_REGULAR_EXPRESSION")]
  public class DynamicRegularExpressionAttribute : DynamicValidationBase, IValidationRuleMetadata
  {
    public DynamicRegularExpressionAttribute(string pattern, string property)
    {
      this.Property = property;
      this.Pattern = pattern;
    }

    public string Property { get; private set; }

    public string Pattern { get; private set; }

    public virtual string ClientRuleName => "regex";

    public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(
      ModelMetadata metadata,
      ControllerContext context)
    {
            FieldViewModel model = GetModel<FieldViewModel>(metadata);
            string value = model.GetPropertyValue<string>(Property) ?? Pattern;
            if (!string.IsNullOrEmpty(value))
            {
                ModelClientValidationRule val = new ModelClientValidationRule();
                val.ErrorMessage = (FormatError(model));
                val.ValidationType = (ClientRuleName);
                ModelClientValidationRule val2 = val;
                val2.ValidationParameters.Add("pattern", value);
                val2.ValidationParameters.Add("tracking", EventId);
                yield return val2;
            }
        }

    protected override ValidationResult ValidateFieldValue(
      IViewModel model,
      object value,
      ValidationContext validationContext)
    {
      Assert.ArgumentNotNull((object) model, nameof (model));
      string pattern = model.GetPropertyValue<string>(this.Property) ?? this.Pattern;
      string stringValue = System.Convert.ToString(value, (IFormatProvider) CultureInfo.CurrentCulture);
      return string.IsNullOrEmpty(stringValue) || string.IsNullOrEmpty(pattern) || this.IsMatch(stringValue, pattern) ? ValidationResult.Success : new ValidationResult(this.FormatError((object) model));
    }

    protected virtual bool IsMatch(string stringValue, string pattern)
    {
      Match match = Regex.Match(stringValue, pattern);
      return match.Success && match.Index == 0 && match.Length == stringValue.Length;
    }
  }
}
