// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Validators.DynamicEmailAttribute
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Extensions;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.ViewModels;
using Sitecore.WFFM.Abstractions.Analytics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Validators
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
  [DisplayName("TITLE_ERROR_MESSAGE_EMAIL")]
  public class DynamicEmailAttribute : DynamicValidationBase
  {
    private const string DefaultEmailRegularExpression = "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,17}$";

    public DynamicEmailAttribute(string property = "")
    {
      this.EventId = PageEventIds.FieldOutOfBoundary.ToString();
      if (string.IsNullOrEmpty(property))
        return;
      this.EmailRegExpProperty = property;
    }

    protected string EmailRegExpProperty { get; private set; }

    public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(
      ModelMetadata metadata,
      ControllerContext context)
    {
            string pattern = "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,17}$";
            FieldViewModel model = GetModel<FieldViewModel>(metadata);
            if (!string.IsNullOrEmpty(EmailRegExpProperty))
            {
                string propertyValue = model.GetPropertyValue<string>(EmailRegExpProperty);
                if (!string.IsNullOrEmpty(propertyValue))
                {
                    pattern = propertyValue;
                }
            }
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
            yield return (ModelClientValidationRule)new ModelClientValidationRegexRule(FormatError(model), regex.ToString());
        }

    protected override ValidationResult ValidateFieldValue(
      IViewModel model,
      object value,
      ValidationContext validationContext)
    {
      Assert.ArgumentNotNull((object) validationContext, nameof (validationContext));
      if (value == null)
        return ValidationResult.Success;
      string pattern = "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,17}$";
      if (!string.IsNullOrEmpty(this.EmailRegExpProperty))
      {
        string propertyValue = validationContext.ObjectInstance.GetPropertyValue<string>(this.EmailRegExpProperty);
        if (!string.IsNullOrEmpty(propertyValue))
          pattern = propertyValue;
      }
      Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
      return string.IsNullOrEmpty((string) value) || regex.IsMatch((string) value) ? ValidationResult.Success : new ValidationResult(model != null ? this.FormatError((object) model) : this.ErrorMessage);
    }
  }
}
