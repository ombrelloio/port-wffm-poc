// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Validators.DynamicStringLengthAttribute
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
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Validators
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
  [DisplayName("TITLE_ERROR_MESSAGE_STRING_LENGTH")]
  public class DynamicStringLengthAttribute : DynamicValidationBase
  {
    public DynamicStringLengthAttribute(string minPropertyLength, string maxPropertyLength)
    {
      Assert.ArgumentNotNullOrEmpty(minPropertyLength, nameof (minPropertyLength));
      Assert.ArgumentNotNullOrEmpty(maxPropertyLength, nameof (maxPropertyLength));
      this.MinimumProperty = minPropertyLength;
      this.Maximum = maxPropertyLength;
      this.EventId = PageEventIds.FieldOutOfBoundary.ToString();
    }

    public string MinimumProperty { get; private set; }

    public string Maximum { get; private set; }

    public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(
      ModelMetadata metadata,
      ControllerContext context)
    {
            FieldViewModel model = GetModel<FieldViewModel>(metadata);
            int? containerPropertyValue = metadata.GetContainerPropertyValue<int?>(MinimumProperty);
            int? containerPropertyValue2 = metadata.GetContainerPropertyValue<int?>(Maximum);
            if (containerPropertyValue.HasValue && containerPropertyValue2.HasValue)
            {
                yield return (ModelClientValidationRule)new ModelClientValidationStringLengthRule(FormatError(model, containerPropertyValue.Value, containerPropertyValue2.Value), containerPropertyValue.Value, containerPropertyValue2.Value);
            }
        }

    protected override ValidationResult ValidateFieldValue(
      IViewModel model,
      object value,
      ValidationContext validationContext)
    {
      Assert.ArgumentNotNull((object) model, nameof (model));
      try
      {
        int num1 = model.GetPropertyValue<int>(this.MinimumProperty);
        int propertyValue = model.GetPropertyValue<int>(this.Maximum);
        if (num1 < 0)
          num1 = 0;
        int num2 = value == null ? 0 : value.ToString().Replace("\r\n", "\n").Length;
        ValidationResult validationResult;
        if (value != null && propertyValue != 0 && (num2 < num1 || num2 > propertyValue))
          validationResult = new ValidationResult(this.FormatError((object) model, (object) num1, (object) propertyValue));
        else
          validationResult = ValidationResult.Success;
        return validationResult;
      }
      catch (Exception ex)
      {
        Log.Error("Dynamic string length validation error occurs", ex, (object) this);
      }
      return new ValidationResult(this.ErrorMessage);
    }
  }
}
