// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Html.HtmlHelperExtensions
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Data;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.ViewModels;
using Sitecore.Forms.Mvc.ViewModels.Fields;
using Sitecore.Mvc;
using Sitecore.WFFM.Abstractions.Data.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Sitecore.Forms.Mvc.Html
{
  public static class HtmlHelperExtensions
  {
    public static MvcTagElement BeginField(this HtmlHelper helper, IViewModel model = null)
    {
      Assert.ArgumentNotNull((object) helper, nameof (helper));
      Assert.IsNotNull((object) (((object) model ?? helper.ViewData.Model) as IViewModel), "view");
      return helper.BeginField(true, model);
    }

    public static MvcTagElement BeginField(
      this HtmlHelper helper,
      bool renderBody,
      IViewModel model = null)
    {
      Assert.ArgumentNotNull((object) helper, nameof (helper));
      FieldViewModel view = ((object) model ?? helper.ViewData.Model) as FieldViewModel;
      Assert.IsNotNull((object) view, "view");
      return new MvcTagElement((Action) (() => helper.BeginFormField((IViewModel) view, renderBody)), (Action) (() => helper.EndFormField(view, renderBody)));
    }

    public static MvcTagElement BeginTag(
      this HtmlHelper helper,
      string tag,
      Func<bool> renderTagCondition,
      object htmlAttributes = null)
    {
      Assert.ArgumentNotNull((object) helper, nameof (helper));
      Assert.ArgumentNotNullOrEmpty(tag, nameof (tag));
      return renderTagCondition() ? new MvcTagElement((Action) (() => helper.WriteBeginTag(tag, htmlAttributes)), (Action) (() => helper.WriteEndTag(tag))) : new MvcTagElement();
    }

    public static MvcTagElement BeginTag(
      this HtmlHelper helper,
      string tag,
      object htmlAttributes = null)
    {
      Assert.ArgumentNotNull((object) helper, nameof (helper));
      Assert.ArgumentNotNullOrEmpty(tag, nameof (tag));
      return new MvcTagElement((Action) (() => helper.WriteBeginTag(tag, htmlAttributes)), (Action) (() => helper.WriteEndTag(tag)));
    }

    private static void BeginFormField(this HtmlHelper helper, IViewModel model, bool renderBody)
    {
      Assert.ArgumentNotNull((object) helper, nameof (helper));
      helper.ViewContext.Writer.Write((object) helper.OpenFormField(model, renderBody));
    }

    public static MvcHtmlString OpenFormField(
      this HtmlHelper helper,
      IViewModel model,
      bool renderBody)
    {
      Assert.ArgumentNotNull((object) helper, nameof (helper));
      Assert.ArgumentNotNull((object) model, nameof (model));
      StringBuilder stringBuilder = new StringBuilder();
      TagBuilder tagBuilder1 = new TagBuilder("div");
      tagBuilder1.AddCssClass("form-group");
      if (model is IStyleSettings styleSettings1)
        tagBuilder1.AddCssClass(styleSettings1.CssClass);
      if (model is IHasIsRequired hasIsRequired && hasIsRequired.IsRequired)
        tagBuilder1.AddCssClass("required-field");
      if (!model.Visible)
        tagBuilder1.AddCssClass("hidden");
      stringBuilder.Append(tagBuilder1.ToString((TagRenderMode) 1));
      if (model is IHasItem hasItem)
        stringBuilder.Append((object) InputExtensions.Hidden(helper, "Id", (object) hasItem.Item.ID));
      if (!renderBody)
      {
        helper.ViewContext.Writer.Write(stringBuilder.ToString());
        return (MvcHtmlString) MvcHtmlString.Empty;
      }
      if (model.ShowTitle)
      {
        MvcHtmlString mvcHtmlString = (MvcHtmlString) null;
        if (Context.PageMode.IsExperienceEditor)
          mvcHtmlString = helper.BootstrapSitecoreField("Title");
        stringBuilder.Append((object) (mvcHtmlString ?? helper.BootstrapLabel("Value", model.Title)));
      }
      if (model is IStyleSettings styleSettings2 && styleSettings2.FormType == FormType.Horizontal)
      {
        TagBuilder tagBuilder2 = new TagBuilder("div");
        tagBuilder2.AddCssClass(styleSettings2.RightColumnStyle);
        stringBuilder.Append(tagBuilder2.ToString((TagRenderMode) 1));
      }
      return MvcHtmlString.Create(stringBuilder.ToString());
    }

    private static void EndFormField(this HtmlHelper helper, FieldViewModel model, bool renderBody = true) => helper.ViewContext.Writer.Write((object) helper.CloseFormField(model, renderBody));

    public static MvcHtmlString CloseFormField(
      this HtmlHelper helper,
      FieldViewModel model,
      bool renderBody = true)
    {
      Assert.ArgumentNotNull((object) helper, nameof (helper));
      Assert.ArgumentNotNull((object) model, nameof (model));
      StringBuilder stringBuilder = new StringBuilder();
      if (renderBody)
      {
        stringBuilder.Append((object) helper.BootstrapValidationMessage("Value"));
        if (model.ShowInformation && !string.IsNullOrEmpty(model.Information))
          stringBuilder.Append((object) helper.HelpBlock(model.Information));
      }
      TagBuilder tagBuilder = new TagBuilder("div");
      return MvcHtmlString.Create(stringBuilder.ToString() + tagBuilder.ToString((TagRenderMode) 2) + (model.FormType == FormType.Horizontal ? (object) tagBuilder.ToString((TagRenderMode) 2) : (object) string.Empty));
    }

    public static void WriteBeginTag(this HtmlHelper helper, string tag, object htmlAttributes)
    {
      Assert.ArgumentNotNull((object) helper, nameof (helper));
      Assert.ArgumentNotNullOrEmpty(tag, nameof (tag));
      TagBuilder tagBuilder = new TagBuilder(tag);
      if (htmlAttributes != null)
      {
        RouteValueDictionary source = HtmlHelperExtensions.RemoveEmptyAttributes((IEnumerable<KeyValuePair<string, object>>) HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        if (source.Any<KeyValuePair<string, object>>())
          tagBuilder.MergeAttributes<string, object>((IDictionary<string, object>) source);
      }
      helper.ViewContext.Writer.Write(tagBuilder.ToString((TagRenderMode) 1));
    }

    public static MvcHtmlString BootstrapValidationMessage(
      this HtmlHelper helper,
      string expression)
    {
      RouteValueDictionary routeValueDictionary = new RouteValueDictionary()
      {
        {
          "class",
          (object) ("help-block" + (helper.IsFormInline() ? " sr-only" : string.Empty))
        }
      };
      return ValidationExtensions.ValidationMessage(helper, expression, (IDictionary<string, object>) routeValueDictionary);
    }

    public static MvcHtmlString BootstrapValidationMessageFor<TModel, TProperty>(
      this HtmlHelper<TModel> helper,
      Expression<Func<TModel, TProperty>> expression)
    {
      RouteValueDictionary routeValueDictionary = new RouteValueDictionary()
      {
        {
          "class",
          (object) ("help-block" + (((HtmlHelper) helper).IsFormInline() ? " sr-only" : string.Empty))
        }
      };
      return ValidationExtensions.ValidationMessageFor<TModel, TProperty>(helper, expression, (string) null, (IDictionary<string, object>) routeValueDictionary);
    }

    public static MvcHtmlString HelpBlock(
      this HtmlHelper helper,
      string help,
      params string[] classes)
    {
      if (string.IsNullOrEmpty(help))
        return (MvcHtmlString) MvcHtmlString.Empty;
      TagBuilder tag = new TagBuilder("span");
      tag.AddCssClass("help-block");
      if (helper.IsFormInline())
        tag.AddCssClass("sr-only");
      HtmlHelperExtensions.MergeCssClasses(tag, classes);
      tag.InnerHtml += (string) (object) helper.Raw(help);
      return MvcHtmlString.Create(((object) tag).ToString());
    }

    public static MvcHtmlString BootstrapHeader(
      this HtmlHelper helper,
      string headerText,
      string tag = null)
    {
      string str = tag ?? "h1";
      TagBuilder tagBuilder1 = new TagBuilder("div");
      tagBuilder1.AddCssClass("page-header");
      TagBuilder tagBuilder2 = new TagBuilder(str);
      tagBuilder2.InnerHtml += headerText;
      tagBuilder1.InnerHtml += (string) (object) tagBuilder2;
      return MvcHtmlString.Create(((object) tagBuilder1).ToString());
    }

    public static MvcHtmlString BootstrapEditor(
      this HtmlHelper helper,
      string expression,
      params string[] classes)
    {
      IViewModel model = helper.ViewData.Model as IViewModel;
      return helper.BootstrapEditor(expression, model != null ? model.Title : string.Empty, classes);
    }

    public static MvcHtmlString BootstrapEditor(
      this HtmlHelper helper,
      string expression,
      string placeholderText,
      params string[] classes)
    {
      return helper.BootstrapEditor(expression, placeholderText, (string) null, classes);
    }

    public static MvcHtmlString BootstrapEditor(
      this HtmlHelper helper,
      string expression,
      string placeholderText,
      string inlineStyle)
    {
      return helper.BootstrapEditor(expression, placeholderText, inlineStyle, new string[0]);
    }

    private static MvcHtmlString BootstrapEditor(
      this HtmlHelper helper,
      string expression,
      string placeholderText,
      string inlineStyle,
      string[] classes)
    {
      return EditorExtensions.Editor(helper, expression, (object) new
      {
        htmlAttributes = new
        {
          @class = (string.Join(" ", classes) + " form-control"),
          placeholder = (helper.IsFormInline() ? placeholderText : string.Empty),
          style = (inlineStyle ?? string.Empty)
        }
      });
    }

    public static MvcHtmlString BootstrapLabelFor<TModel, TValue>(
      this HtmlHelper<TModel> helper,
      Expression<Func<TModel, TValue>> expression,
      string labelText,
      params string[] classes)
    {
      return LabelExtensions.LabelFor<TModel, TValue>(helper, expression, labelText, (object) new
      {
        @class = ((HtmlHelper) helper).GetControlLabelClass(classes)
      });
    }

    public static MvcHtmlString BootstrapText(
      this HtmlHelper helper,
      string expression)
    {
      MvcHtmlString mvcHtmlString = helper.BootstrapSitecoreField(expression);
      return mvcHtmlString != null && !string.IsNullOrEmpty(((HtmlString) mvcHtmlString).ToHtmlString()) ? mvcHtmlString : MvcHtmlString.Create(ModelMetadata.FromStringExpression(expression, helper.ViewContext.ViewData).SimpleDisplayText);
    }

    public static MvcHtmlString BootstrapLabel(
      this HtmlHelper helper,
      string expression,
      string labelText,
      params string[] classes)
    {
      return LabelExtensions.Label(helper, expression, labelText, (object) new
      {
        @class = helper.GetControlLabelClass(classes)
      });
    }

    public static MvcHtmlString BootstrapWarningsList(
      this HtmlHelper helper,
      List<string> warnings)
    {
      if (!warnings.Any<string>())
        return (MvcHtmlString) MvcHtmlString.Empty;
      TagBuilder tagBuilder1 = new TagBuilder("div");
      tagBuilder1.AddCssClass("form-group has-error has-feedback bg-warning");
      TagBuilder tagBuilder2 = new TagBuilder("ul");
      tagBuilder2.AddCssClass("list-group");
      foreach (string warning in warnings)
      {
        TagBuilder tagBuilder3 = new TagBuilder("li");
        tagBuilder3.AddCssClass("list-group-item list-group-item-warning");
        tagBuilder3.SetInnerText(warning);
        tagBuilder2.InnerHtml += (string) (object) tagBuilder3;
      }
      tagBuilder1.InnerHtml += (string) (object) tagBuilder2;
      return MvcHtmlString.Create(((object) tagBuilder1).ToString());
    }

    public static MvcHtmlString BootstrapValidationSammary(
      this HtmlHelper htmlHelper,
      bool excludePropertyErrors)
    {
      Assert.ArgumentNotNull((object) htmlHelper, nameof (htmlHelper));
      FormContext clientValidation = HtmlHelperExtensions.GetFormContextForClientValidation(htmlHelper.ViewContext);
      if (htmlHelper.ViewData.ModelState.IsValid)
      {
        if (clientValidation == null)
          return (MvcHtmlString) null;
        if (htmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled & excludePropertyErrors)
          return (MvcHtmlString) null;
      }
      StringBuilder stringBuilder = new StringBuilder();
      TagBuilder tagBuilder1 = new TagBuilder("ul");
      tagBuilder1.AddCssClass("list-group");
      foreach (ModelState modelState in HtmlHelperExtensions.GetModelStateList(htmlHelper, excludePropertyErrors))
      {
        foreach (ModelError error in (Collection<ModelError>) modelState.Errors)
        {
          string messageOrDefault = HtmlHelperExtensions.GetUserErrorMessageOrDefault(((ControllerContext) htmlHelper.ViewContext).HttpContext, error, (ModelState) null);
          if (!string.IsNullOrEmpty(messageOrDefault))
          {
            TagBuilder tagBuilder2 = new TagBuilder("li");
            tagBuilder2.AddCssClass("list-group-item list-group-item-danger");
            tagBuilder2.SetInnerText(messageOrDefault);
            stringBuilder.AppendLine(tagBuilder2.ToString((TagRenderMode) 0));
          }
        }
      }
      if (stringBuilder.Length == 0)
        stringBuilder.AppendLine("<li style=\"display:none\"></li>");
      tagBuilder1.InnerHtml = stringBuilder.ToString();
      TagBuilder tagBuilder3 = new TagBuilder("div");
      tagBuilder3.AddCssClass(htmlHelper.ViewData.ModelState.IsValid ? (string) HtmlHelper.ValidationSummaryValidCssClassName : (string) HtmlHelper.ValidationSummaryCssClassName);
      tagBuilder3.InnerHtml = tagBuilder1.ToString((TagRenderMode) 0);
      if (clientValidation != null)
      {
        if (htmlHelper.ViewContext.UnobtrusiveJavaScriptEnabled)
        {
          if (!excludePropertyErrors)
            tagBuilder3.MergeAttribute("data-valmsg-summary", "true");
        }
        else
        {
          tagBuilder3.GenerateId("validationSummary");
          clientValidation.ValidationSummaryId = tagBuilder3.Attributes["id"];
          clientValidation.ReplaceValidationSummary = !excludePropertyErrors;
        }
      }
      return MvcHtmlString.Create(tagBuilder3.ToString((TagRenderMode) 0));
    }

    public static MvcHtmlString BootstrapSubmit(this HtmlHelper helper, string title = null)
    {
      TagBuilder tagBuilder1 = new TagBuilder("input");
      tagBuilder1.Attributes.Add("type", "submit");
      ISubmitSettings model1 = helper.ViewData.Model as ISubmitSettings;
      string str1 = string.Empty;
      string str2 = string.Empty;
      string str3 = string.Empty;
      string str4 = string.Empty;
      if (model1 != null)
      {
        str1 = model1.SubmitButtonSize;
        str2 = model1.SubmitButtonType;
        str3 = model1.SubmitButtonName;
        str4 = model1.SubmitButtonPosition;
      }
      tagBuilder1.Attributes.Add("value", title ?? str3);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("btn").Append(" ").Append(str1).Append(" ").Append(str2);
      if (helper.ViewData.Model is IStyleSettings model2 && model2.FormType != FormType.Inline)
      {
        TagBuilder tagBuilder2 = new TagBuilder("div");
        tagBuilder2.AddCssClass(("form-submit-border " + str4).Trim());
        tagBuilder1.AddCssClass(stringBuilder.ToString().Trim());
        tagBuilder2.InnerHtml += (string) (object) tagBuilder1;
        return MvcHtmlString.Create(((object) tagBuilder2).ToString());
      }
      tagBuilder1.AddCssClass(stringBuilder.ToString().Trim());
      return MvcHtmlString.Create(((object) tagBuilder1).ToString());
    }

    public static MvcHtmlString Recaptcha(this HtmlHelper helper, IViewModel model = null)
    {
      RecaptchaField recaptchaField = ((object) model ?? helper.ViewData.Model) as RecaptchaField;
      Assert.IsNotNull((object) recaptchaField, "view");
      ProtectionSchema robotDetection = recaptchaField.RobotDetection;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append((object) helper.OpenFormField((IViewModel) recaptchaField, robotDetection == null || !robotDetection.Enabled));
      stringBuilder.Append((object) InputExtensions.Hidden(helper, "Value"));
      if (recaptchaField.Visible)
      {
        TagBuilder tagBuilder1 = new TagBuilder("div");
        tagBuilder1.AddCssClass("g-recaptcha");
        tagBuilder1.MergeAttribute("data-sitekey", recaptchaField.SiteKey);
        tagBuilder1.MergeAttribute("data-theme", recaptchaField.Theme);
        tagBuilder1.MergeAttribute("data-type", recaptchaField.CaptchaType);
        TagBuilder tagBuilder2 = new TagBuilder("script");
        string str = "https://www.google.com/recaptcha/api.js";
        if (Sitecore.Configuration.Settings.GetBoolSetting("WFM.RecaptchaUseContextLanguage", false))
          str += string.Format("?hl={0}", (object) Context.Language);
        tagBuilder2.MergeAttribute("src", str);
        stringBuilder.Append((object) tagBuilder1);
        stringBuilder.Append((object) tagBuilder2);
      }
      stringBuilder.Append((object) helper.CloseFormField((FieldViewModel) recaptchaField));
      return MvcHtmlString.Create(stringBuilder.ToString());
    }

    public static MvcHtmlString GenerateCaptcha(
      this HtmlHelper helper,
      CaptchaField captchaField)
    {
      Assert.ArgumentNotNull((object) captchaField, nameof (captchaField));
      if (!captchaField.Visible)
        return (MvcHtmlString) MvcHtmlString.Empty;
      HttpContextBase httpContext = ((ControllerContext) helper.ViewContext).HttpContext;
      if (!captchaField.UseSession)
        httpContext.Cache.Insert(captchaField.PostedCaptchaUniqueId, (object) captchaField.Captcha, (CacheDependency) null, DateTime.Now.AddSeconds(System.Convert.ToDouble(captchaField.CaptchaMaxTimeout == 0 ? 90 : captchaField.CaptchaMaxTimeout)), TimeSpan.Zero, CacheItemPriority.NotRemovable, (CacheItemRemovedCallback) null);
      else if (httpContext.Session != null)
        httpContext.Session.Add(captchaField.PostedCaptchaUniqueId, (object) captchaField.Captcha);
      return (MvcHtmlString) MvcHtmlString.Empty;
    }

    public static void WriteEndTag(this HtmlHelper helper, string tag)
    {
      Assert.ArgumentNotNull((object) helper, nameof (helper));
      Assert.ArgumentNotNullOrEmpty(tag, nameof (tag));
      TagBuilder tagBuilder = new TagBuilder(tag);
      helper.ViewContext.Writer.Write(tagBuilder.ToString((TagRenderMode) 2));
    }

    private static void MergeCssClasses(TagBuilder tag, params string[] classes)
    {
      Assert.ArgumentNotNull((object) tag, nameof (tag));
      if (classes == null)
        return;
      foreach (string str in classes)
      {
        if (!string.IsNullOrEmpty(str))
          tag.AddCssClass(str);
      }
    }

    private static RouteValueDictionary RemoveEmptyAttributes(
      IEnumerable<KeyValuePair<string, object>> dict)
    {
      Assert.ArgumentNotNull((object) dict, nameof (dict));
      return new RouteValueDictionary((IDictionary<string, object>) dict.Where<KeyValuePair<string, object>>((Func<KeyValuePair<string, object>, bool>) (x => x.Value != null)).Where<KeyValuePair<string, object>>((Func<KeyValuePair<string, object>, bool>) (x => !(x.Value is string) || !string.IsNullOrEmpty(x.Value.ToString()))).ToDictionary<KeyValuePair<string, object>, string, object>((Func<KeyValuePair<string, object>, string>) (x => x.Key), (Func<KeyValuePair<string, object>, object>) (y => y.Value)));
    }

    private static string GetControlLabelClass(this HtmlHelper helper, params string[] classes)
    {
      StringBuilder stringBuilder = new StringBuilder(Constants.Bootstrap.ControlLabel);
      if (classes != null)
      {
        foreach (string str in classes)
        {
          if (str != null)
            stringBuilder.Append(" ").Append(str);
        }
      }
      if (helper.ViewData.Model is IStyleSettings model)
      {
        if (model.FormType == FormType.Inline)
          stringBuilder.Append(" ").Append("sr-only");
        if (model.FormType == FormType.Horizontal)
          stringBuilder.Append(" ").Append(model.LeftColumnStyle);
      }
      return stringBuilder.ToString();
    }

    private static MvcHtmlString BootstrapSitecoreField(
      this HtmlHelper helper,
      string fieldName)
    {
      if (Context.PageMode.IsExperienceEditor)
      {
        IHasItem model = helper.ViewData.Model as IHasItem;
        if (helper.ViewData.Model is IHasItem)
          return MvcHtmlString.Create(Sitecore.Mvc.HtmlHelperExtensions.Sitecore(helper).Field(fieldName, model.Item, (object) new
          {
            Class = "control-label"
          }).ToHtmlString());
      }
      return (MvcHtmlString) null;
    }

    private static bool IsFormInline(this HtmlHelper helper) => helper.ViewData.Model is IStyleSettings model && model.FormType == FormType.Inline;

    private static string GetUserErrorMessageOrDefault(
      HttpContextBase httpContext,
      ModelError error,
      ModelState modelState)
    {
      if (!string.IsNullOrEmpty(error.ErrorMessage))
        return error.ErrorMessage;
      if (modelState == null)
        return (string) null;
      string str = modelState.Value != null ? modelState.Value.AttemptedValue : (string) null;
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, HtmlHelperExtensions.GetInvalidPropertyValueResource(httpContext), new object[1]
      {
        (object) str
      });
    }

    private static string GetInvalidPropertyValueResource(HttpContextBase httpContext)
    {
      string str = (string) null;
      if (!string.IsNullOrEmpty(ValidationExtensions.ResourceClassKey) && httpContext != null)
        str = httpContext.GetGlobalResourceObject(ValidationExtensions.ResourceClassKey, "InvalidPropertyValue", CultureInfo.CurrentUICulture) as string;
      return str ?? string.Empty;
    }

    private static IEnumerable<ModelState> GetModelStateList(
      HtmlHelper htmlHelper,
      bool excludePropertyErrors)
    {
      if (excludePropertyErrors)
      {
        ModelState modelState;
        htmlHelper.ViewData.ModelState.TryGetValue(htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix, out modelState);
        if (modelState == null)
          return (IEnumerable<ModelState>) new ModelState[0];
        return (IEnumerable<ModelState>) new ModelState[1]
        {
          modelState
        };
      }
      Dictionary<string, int> ordering = new Dictionary<string, int>();
      ModelMetadata modelMetadata = htmlHelper.ViewData.ModelMetadata;
      if (modelMetadata != null)
      {
        foreach (ModelMetadata property in modelMetadata.Properties)
          ordering[property.PropertyName] = property.Order;
      }
      return ((IEnumerable<KeyValuePair<string, ModelState>>) htmlHelper.ViewData.ModelState).Select(kv => new
      {
        kv = kv,
        name = kv.Key
      }).OrderBy(param0 => ordering.GetOrDefault<string, int>(param0.name, 10000)).Select(param0 => param0.kv.Value);
    }

    private static FormContext GetFormContextForClientValidation(ViewContext viewContext) => !viewContext.ClientValidationEnabled ? (FormContext) null : viewContext.FormContext;

    private static TValue GetOrDefault<TKey, TValue>(
      this IDictionary<TKey, TValue> dict,
      TKey key,
      TValue @default)
    {
      TValue obj;
      return dict.TryGetValue(key, out obj) ? obj : @default;
    }
  }
}
