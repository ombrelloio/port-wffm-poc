// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Validators.RecaptchaResponseValidatorAttribute
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Newtonsoft.Json;
using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.ViewModels.Fields;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Sitecore.Forms.Mvc.Validators
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
  [DisplayName("TITLE_ERROR_MESSAGE_RECAPTCHA")]
  public class RecaptchaResponseValidatorAttribute : DynamicValidationBase
  {
    protected override ValidationResult ValidateFieldValue(
      IViewModel model,
      object value,
      ValidationContext validationContext)
    {
      Assert.IsNotNull((object) model, nameof (model));
      if (value == null)
        return new ValidationResult(this.FormatError((object) model, (object) "Invalid captcha text"));
      ReCaptchaResponse reCaptchaResponse = JsonConvert.DeserializeObject<ReCaptchaResponse>(new WebClient().DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", (object) ((RecaptchaField) model).SecretKey, value)));
      return reCaptchaResponse.Success ? ValidationResult.Success : new ValidationResult(this.FormatError((object) model, (object[]) reCaptchaResponse.ErrorCodes.ToArray()));
    }
  }
}
