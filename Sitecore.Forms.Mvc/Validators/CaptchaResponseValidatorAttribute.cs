// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Validators.CaptchaResponseValidatorAttribute
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.ViewModels.Fields;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sitecore.Forms.Mvc.Validators
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
  public class CaptchaResponseValidatorAttribute : DynamicValidationBase
  {
    protected override ValidationResult ValidateFieldValue(
      IViewModel model,
      object value,
      ValidationContext validationContext)
    {
      if (string.IsNullOrEmpty(value as string))
        return new ValidationResult(this.FormatError((object) model, (object) "Invalid captcha text"));
      CaptchaField model1 = this.GetModel<CaptchaField>(validationContext);
      if (model1 == null || !(model1.Passkey != value.ToString()))
        return ValidationResult.Success;
      return new ValidationResult(this.FormatError((object) model, (object) "Invalid captcha text"));
    }
  }
}
