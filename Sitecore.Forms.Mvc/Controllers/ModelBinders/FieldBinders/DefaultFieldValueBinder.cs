// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Controllers.ModelBinders.FieldBinders.DefaultFieldValueBinder
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Controllers.ModelBinders.FieldBinders
{
  public class DefaultFieldValueBinder : IPropertyBinder
  {
    protected IViewModel FieldModel { get; set; }

    public virtual object BindProperty(
      ControllerContext controllerContext,
      ModelBindingContext bindingContext,
      object value)
    {
      Assert.ArgumentNotNull((object) controllerContext, nameof (controllerContext));
      Assert.ArgumentNotNull((object) bindingContext, nameof (bindingContext));
      if (!bindingContext.ModelMetadata.AdditionalValues.ContainsKey(Constants.Container))
        return (object) null;
      if (!(bindingContext.ModelMetadata.AdditionalValues[Constants.Container] is IViewModel additionalValue))
        return (object) null;
      this.FieldModel = additionalValue;
      if (value == null)
        return (object) null;
      if (!value.GetType().IsArray || !(value is Array source))
        return value;
      List<object> list = source.Cast<object>().Select<object, object>((Func<object, object>) (element => element)).ToList<object>();
      return source.Length != 1 ? (object) string.Join<object>(",", (IEnumerable<object>) list) : source.GetValue(0);
    }
  }
}
