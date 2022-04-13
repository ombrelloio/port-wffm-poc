// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Validators.DynamicValidationBase
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.ViewModels;
using Sitecore.Globalization;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Validators
{
  public abstract class DynamicValidationBase : 
    ValidationAttribute,
    System.Web.Mvc.IClientValidatable,
    IHasAnalyticsEvent
  {
    private readonly IAnalyticsTracker analyticsTracker;

    protected DynamicValidationBase()
      : this(DependenciesManager.AnalyticsTracker)
    {
    }

    protected DynamicValidationBase(IAnalyticsTracker analyticsTracker)
    {
      Assert.ArgumentNotNull((object) analyticsTracker, nameof (analyticsTracker));
      this.EventId = PageEventIds.InvalidFieldSyntax.ToString();
      this.analyticsTracker = analyticsTracker;
      this.ValidateInvisible = false;
    }

    public string ParameterName { get; set; }

    public string EventId { get; set; }

    public bool ValidateInvisible { get; set; }

    public void RegisterAnalyticsEvent(ValidationContext context, string errorMessage)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      Assert.ArgumentNotNullOrEmpty(errorMessage, nameof (errorMessage));
      if (string.IsNullOrEmpty(this.EventId))
        return;
      FieldViewModel model = this.GetModel<FieldViewModel>(context);
      if (model == null)
        return;
      this.analyticsTracker.Register(model.FieldItemId, this.EventId, model.FormId, errorMessage);
    }

    public virtual string GetErrorMessageTemplate(object fieldModel)
    {
      string name = this.GetType().Name;
      string key = string.IsNullOrEmpty(this.ParameterName) ? name : this.ParameterName.ToLowerInvariant();
      FieldViewModel fieldViewModel = (FieldViewModel) fieldModel;
      if (fieldViewModel != null)
      {
        Dictionary<string, string> parameters = fieldViewModel.Parameters;
        if (parameters.ContainsKey(key))
          return parameters[key];
        if (parameters.ContainsKey(name))
          return parameters[name];
      }
      return Translate.Text(this.ErrorMessageString);
    }

    public virtual IEnumerable<ModelClientValidationRule> GetClientValidationRules(
      ModelMetadata metadata,
      ControllerContext context)
    {
      yield break;
    }

    protected TModel GetModel<TModel>(ValidationContext validationContext) where TModel : class
    {
      Assert.ArgumentNotNull((object) validationContext, nameof (validationContext));
      return validationContext.ObjectInstance as TModel;
    }

    protected TModel GetModel<TModel>(ModelMetadata modelMetadata)
    {
      Assert.ArgumentNotNull((object) modelMetadata, nameof (modelMetadata));
      return (TModel) (modelMetadata.AdditionalValues.ContainsKey(Sitecore.Forms.Mvc.Constants.Container) ? modelMetadata.AdditionalValues[Sitecore.Forms.Mvc.Constants.Container] : (object) null);
    }

    protected override sealed ValidationResult IsValid(
      object value,
      ValidationContext validationContext)
    {
            IViewModel viewModel = validationContext.ObjectInstance as IViewModel;
            if (viewModel != null && !ValidateInvisible && !viewModel.Visible)
            {
                return null;
            }
            ValidationResult validationResult = ValidateFieldValue(viewModel, value, validationContext);
            if (validationResult != ValidationResult.Success && !string.IsNullOrEmpty(EventId))
            {
                RegisterAnalyticsEvent(validationContext, validationResult.ErrorMessage);
            }
            return validationResult;
        }

    protected abstract ValidationResult ValidateFieldValue(
      IViewModel model,
      object value,
      ValidationContext validationContext);

    protected virtual string FormatError(object model, params object[] parameters)
    {
      Assert.ArgumentNotNull(model, nameof (model));
      FieldViewModel fieldViewModel = model as FieldViewModel;
      string str = string.Empty;
      if (fieldViewModel != null)
      {
        string errorMessageTemplate = this.GetErrorMessageTemplate((object) fieldViewModel);
        str = fieldViewModel.Title;
        if (!((IEnumerable<object>) parameters).Any<object>())
          return string.Format((IFormatProvider) CultureInfo.CurrentCulture, errorMessageTemplate, (object) str);
      }
      List<object> objectList = new List<object>()
      {
        (object) str
      };
      objectList.AddRange((IEnumerable<object>) parameters);
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, this.GetErrorMessageTemplate((object) fieldViewModel), objectList.ToArray());
    }
  }
}
