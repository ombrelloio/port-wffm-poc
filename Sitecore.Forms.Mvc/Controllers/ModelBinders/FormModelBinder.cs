// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Controllers.ModelBinders.FormModelBinder
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Data.Wrappers;
using System;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Controllers.ModelBinders
{
  public class FormModelBinder : DefaultFormModelBinder
  {
    public FormModelBinder()
    {
    }

    public FormModelBinder(IRenderingContext renderingContext)
      : base(renderingContext)
    {
    }

    public override object BindModel(
      ControllerContext controllerContext,
      ModelBindingContext bindingContext)
    {
      Assert.ArgumentNotNull((object) controllerContext, nameof (controllerContext));
      Assert.ArgumentNotNull((object) bindingContext, nameof (bindingContext));
      string empty1 = string.Empty;
      Guid empty2 = Guid.Empty;
      if (!this.RenderingContext.Rendering.IsFormRendering)
        return (object) null;
      Guid uniqueId = this.RenderingContext.Rendering.UniqueId;
      string prefix = this.GetPrefix(uniqueId);
      if (string.IsNullOrEmpty(prefix))
        return (object) null;
      ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(prefix + "." + Constants.Id);
      if (valueProviderResult == null)
        return (object) null;
      Guid result;
      if (!Guid.TryParse(valueProviderResult.AttemptedValue, out result) || uniqueId != Guid.Empty && result != uniqueId)
        return (object) null;
      bindingContext.ModelName = prefix;
      return base.BindModel(controllerContext, bindingContext);
    }

    protected override object CreateModel(
      ControllerContext controllerContext,
      ModelBindingContext bindingContext,
      Type modelType)
    {
      return (object) this.GetFormViewModel(controllerContext) ?? base.CreateModel(controllerContext, bindingContext, modelType);
    }
  }
}
