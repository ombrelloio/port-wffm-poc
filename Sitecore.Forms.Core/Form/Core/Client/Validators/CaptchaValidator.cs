// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Client.Validators.CaptchaValidator
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Validators;
using Sitecore.Form.Web.UI.Controls;
using System;
using System.Web.UI;

namespace Sitecore.Form.Core.Client.Validators
{
  public class CaptchaValidator : FormCustomValidator
  {
    protected override bool EvaluateIsValid()
    {
      string str = string.Empty;
      string controlToValidate = this.ControlToValidate;
      if (controlToValidate.Length > 0)
        str = this.GetControlValidationValue(controlToValidate);
      return this.OnServerValidate(str);
    }

    protected override bool OnServerValidate(string value)
    {
      if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(this.classAttributes["captchaCodeControlId"]))
        return false;
      Control control = this.FindControl(this.classAttributes["captchaCodeControlId"]);
      if (control != null)
      {
        Captcha captcha = (Captcha) control;
        try
        {
          captcha.ValidateCaptcha(value);
        }
        catch (Exception ex)
        {
          return false;
        }
        if (captcha.UserValidated)
          return true;
      }
      return false;
    }
  }
}
