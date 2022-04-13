// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Validators.DynamicCompareAttribute
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
using System.Reflection;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Validators
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
  [DisplayName("TITLE_ERROR_MESSAGE_COMPARE")]
  public class DynamicCompareAttribute : DynamicValidationBase
  {
    public DynamicCompareAttribute(
      string otherProperty,
      string passwordTitleProperty = null,
      string compareTitleProperty = null)
    {
      Assert.ArgumentNotNullOrEmpty(otherProperty, nameof (otherProperty));
      this.OtherProperty = otherProperty;
      this.PasswordTitleProperty = passwordTitleProperty ?? string.Empty;
      this.CompareTitleProperty = compareTitleProperty ?? string.Empty;
    }

    public string PasswordTitleProperty { get; set; }

    public string CompareTitleProperty { get; set; }

    public string OtherProperty { get; set; }

    public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(
      ModelMetadata metadata,
      ControllerContext context)
    {

            FieldViewModel model = GetModel<FieldViewModel>(metadata);
            if (model != null)
            {
                yield return (ModelClientValidationRule)new ModelClientValidationEqualToRule(FormatError(model), (object)("*." + OtherProperty));
            }
        }

    protected override string FormatError(object fieldModel, params object[] parameters)
    {
      string propertyValue1 = fieldModel.GetPropertyValue<string>(this.PasswordTitleProperty);
      string propertyValue2 = fieldModel.GetPropertyValue<string>(this.CompareTitleProperty);
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, this.GetErrorMessageTemplate(fieldModel), (object) propertyValue1, (object) propertyValue2);
    }

    protected override ValidationResult ValidateFieldValue(
      IViewModel model,
      object value,
      ValidationContext validationContext)
    {
      FieldViewModel fieldViewModel = model as FieldViewModel;
      PropertyInfo property = validationContext.ObjectType.GetProperty(this.OtherProperty);
      if (property == (PropertyInfo) null)
        return new ValidationResult(string.Format((IFormatProvider) CultureInfo.CurrentCulture, "Unknown property {0}.", (object) this.OtherProperty));
      object objB = property.GetValue(validationContext.ObjectInstance, (object[]) null);
      if (string.IsNullOrEmpty(objB as string) && string.IsNullOrEmpty(value as string))
        return (ValidationResult) null;
      return !object.Equals(value, objB) ? new ValidationResult(this.FormatError((object) fieldViewModel)) : ValidationResult.Success;
    }
  }
}
