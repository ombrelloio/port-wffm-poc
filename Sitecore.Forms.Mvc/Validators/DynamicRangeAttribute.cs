// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Validators.DynamicRangeAttribute
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Extensions;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.ViewModels;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Validators
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
  [DisplayName("TITLE_ERROR_MESSAGE_RANGE")]
  public class DynamicRangeAttribute : DynamicValidationBase
  {
    public DynamicRangeAttribute(string minValuePropertyName, string maxValuePropertyName)
      : this(minValuePropertyName, maxValuePropertyName, DependenciesManager.AnalyticsTracker)
    {
    }

    public DynamicRangeAttribute(
      string minValuePropertyName,
      string maxValuePropertyName,
      IAnalyticsTracker analyticsTracker)
      : base(analyticsTracker)
    {
      Assert.ArgumentNotNullOrEmpty(minValuePropertyName, nameof (minValuePropertyName));
      Assert.ArgumentNotNullOrEmpty(maxValuePropertyName, nameof (maxValuePropertyName));
      this.MinValuePropertyName = minValuePropertyName;
      this.MaxValuePropertyName = maxValuePropertyName;
    }

    public string MinValuePropertyName { get; set; }

    public string MaxValuePropertyName { get; set; }

    public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(
      ModelMetadata metadata,
      ControllerContext context)
    {
            Assert.ArgumentNotNull((object)metadata, "metadata");
            Assert.ArgumentNotNull((object)context, "context");
            FieldViewModel model = GetModel<FieldViewModel>(metadata);
            if (model != null)
            {
                Tuple<double?, double?> minMaxValues = GetMinMaxValues<double?>(model);
                double? item = minMaxValues.Item1;
                double? item2 = minMaxValues.Item2;
                if (item.HasValue && item2.HasValue)
                {
                    ModelClientValidationRangeRule val = new ModelClientValidationRangeRule(FormatError(model, item.Value, item2.Value), (object)item, (object)item2);
                    ((ModelClientValidationRule)val).ValidationParameters.Add("tracking", EventId);
                    yield return (ModelClientValidationRule)(object)val;
                }
            }
        }

    protected override ValidationResult ValidateFieldValue(
      IViewModel model,
      object value,
      ValidationContext validationContext)
    {
      Assert.ArgumentNotNull((object) validationContext, nameof (validationContext));
      if (value == null)
        return (ValidationResult) null;
      Tuple<double?, double?> minMaxValues = this.GetMinMaxValues<double?>(model);
      double? nullable1 = minMaxValues.Item1;
      double? nullable2 = minMaxValues.Item2;
      if (!nullable1.HasValue || !nullable2.HasValue)
        return (ValidationResult) null;
      double result;
      if (!double.TryParse(value.ToString(), NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result))
        return new ValidationResult(this.FormatError((object) model, (object) nullable1.Value, (object) nullable2.Value));
      double num1 = result;
      double? nullable3 = nullable1;
      double valueOrDefault1 = nullable3.GetValueOrDefault();
      int num2;
      if ((num1 >= valueOrDefault1 ? (nullable3.HasValue ? 1 : 0) : 0) != 0)
      {
        double num3 = result;
        nullable3 = nullable2;
        double valueOrDefault2 = nullable3.GetValueOrDefault();
        num2 = num3 <= valueOrDefault2 ? (nullable3.HasValue ? 1 : 0) : 0;
      }
      else
        num2 = 0;
      if (num2 != 0)
        return ValidationResult.Success;
      return new ValidationResult(this.FormatError((object) model, (object) nullable1.Value, (object) nullable2.Value));
    }

    private Tuple<T, T> GetMinMaxValues<T>(IViewModel fieldModel) => new Tuple<T, T>(fieldModel.GetPropertyValue<T>(this.MinValuePropertyName), fieldModel.GetPropertyValue<T>(this.MaxValuePropertyName));
  }
}
