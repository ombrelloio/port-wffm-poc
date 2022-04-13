// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Validators.DynamicRequiredAttribute
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.Validators.Rules;
using Sitecore.Forms.Mvc.ViewModels.Fields;
using Sitecore.WFFM.Abstractions.Analytics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Validators
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
  [DisplayName("TITLE_ERROR_MESSAGE_FIELD_REQUIRED")]
  public class DynamicRequiredAttribute : DynamicValidationBase
  {
    public DynamicRequiredAttribute() => this.EventId = PageEventIds.FieldNotCompleted.ToString();

    public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(
      ModelMetadata metadata,
      ControllerContext context)
    {
            Assert.ArgumentNotNull((object)metadata, nameof(metadata));
            Assert.ArgumentNotNull((object)context, nameof(context));
            IHasIsRequired model = this.GetModel<IHasIsRequired>(metadata);
            if (model != null && model.IsRequired)
            {
                string errorMessage = this.FormatError((object)model);
                ModelClientValidationRule clientValidationRule;
                switch (model)
                {
                    case CheckboxField _:
                    case CheckboxListField _:
                        clientValidationRule = (ModelClientValidationRule)new ModelClientValidationCheckedRule(errorMessage);
                        break;
                    default:
                        clientValidationRule = (ModelClientValidationRule)new ModelClientValidationRequiredRule(errorMessage);
                        break;
                }
                clientValidationRule.ValidationParameters.Add("tracking", (object)this.EventId);
                yield return clientValidationRule;
            }
        }

    protected override ValidationResult ValidateFieldValue(
      IViewModel model,
      object value,
      ValidationContext validationContext)
    {
      Assert.ArgumentNotNull((object) model, nameof (model));
      Assert.ArgumentNotNull((object) validationContext, nameof (validationContext));
      if (!(model is IHasIsRequired hasIsRequired) || !hasIsRequired.IsRequired)
        return ValidationResult.Success;
      string str;
      switch (value)
      {
        case List<string> stringList:
          return stringList.Count <= 0 || !stringList.TrueForAll((Predicate<string>) (x => !string.IsNullOrWhiteSpace(x))) ? new ValidationResult(this.FormatError((object) hasIsRequired)) : ValidationResult.Success;
        case bool flag:
          return !flag ? new ValidationResult(this.FormatError((object) hasIsRequired)) : ValidationResult.Success;
        case HttpPostedFileBase httpPostedFileBase when httpPostedFileBase.ContentLength > 0:
          return ValidationResult.Success;
        case null:
          str = (string) null;
          break;
        default:
          str = value.ToString();
          break;
      }
      return !string.IsNullOrWhiteSpace(str) ? ValidationResult.Success : new ValidationResult(this.FormatError((object) hasIsRequired));
    }
  }
}
