// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Controllers.ModelBinders.FieldModelBinder
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Attributes;
using Sitecore.Forms.Mvc.Interfaces;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Controllers.ModelBinders
{
  public class FieldModelBinder : DefaultModelBinder
  {
    protected override object GetPropertyValue(
      ControllerContext controllerContext,
      ModelBindingContext bindingContext,
      PropertyDescriptor propertyDescriptor,
      IModelBinder propertyBinder)
    {
      Assert.ArgumentNotNull((object) controllerContext, nameof (controllerContext));
      Assert.ArgumentNotNull((object) bindingContext, nameof (bindingContext));
      Assert.ArgumentNotNull((object) propertyDescriptor, nameof (propertyDescriptor));
      Assert.ArgumentNotNull((object) propertyBinder, nameof (propertyBinder));
      RequestFormValueAttribute formValueAttribute = propertyDescriptor.Attributes.OfType<RequestFormValueAttribute>().FirstOrDefault<RequestFormValueAttribute>();
      if (formValueAttribute != null)
      {
        string str = controllerContext.RequestContext.HttpContext.Request.Form[formValueAttribute.Name];
        if (str != null)
          return (object) str;
      }
      object obj = base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, propertyBinder);
      PropertyBinderAttribute propertyBinderAttribute = propertyDescriptor.Attributes.OfType<PropertyBinderAttribute>().FirstOrDefault<PropertyBinderAttribute>();
      if (propertyBinderAttribute != null)
      {
        IPropertyBinder propertyBinder1 = this.CreatePropertyBinder(propertyBinderAttribute);
        if (propertyBinder1 != null)
          obj = propertyBinder1.BindProperty(controllerContext, bindingContext, obj);
      }
      return obj;
    }

    protected override ICustomTypeDescriptor GetTypeDescriptor(
      ControllerContext controllerContext,
      ModelBindingContext bindingContext)
    {
      return base.GetTypeDescriptor(controllerContext, bindingContext);
    }

    private IPropertyBinder CreatePropertyBinder(
      PropertyBinderAttribute propertyBinderAttribute)
    {
      Assert.ArgumentNotNull((object) propertyBinderAttribute, nameof (propertyBinderAttribute));
      return (IPropertyBinder) DependencyResolver.Current.GetService(propertyBinderAttribute.BinderType);
    }
  }
}
