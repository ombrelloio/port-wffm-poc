// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Controllers.ModelBinders.SectionModelBinder
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Forms.Mvc.Data.Wrappers;
using Sitecore.Forms.Mvc.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Controllers.ModelBinders
{
  [ModelBinder(typeof (SectionModelBinder))]
  public class SectionModelBinder : DefaultFormModelBinder
  {
    public SectionModelBinder()
    {
    }

    public SectionModelBinder(IRenderingContext renderingContext)
      : base(renderingContext)
    {
    }

    protected override object CreateModel(
      ControllerContext controllerContext,
      ModelBindingContext bindingContext,
      Type modelType)
    {
      FormViewModel formViewModel = this.GetFormViewModel(controllerContext);
      if (formViewModel == null)
        return base.CreateModel(controllerContext, bindingContext, modelType);
      int index = int.Parse(bindingContext.ModelName.Substring(bindingContext.ModelName.IndexOf('[') + 1, bindingContext.ModelName.IndexOf(']') - bindingContext.ModelName.IndexOf('[') - 1));
      return (object) formViewModel.Sections[index];
    }

    protected override object GetPropertyValue(
      ControllerContext controllerContext,
      ModelBindingContext bindingContext,
      PropertyDescriptor propertyDescriptor,
      IModelBinder propertyBinder)
    {
      return propertyDescriptor.DisplayName == "Fields" ? this.BindFieldCollection(controllerContext, bindingContext, (IEnumerable<FieldViewModel>) (bindingContext.Model as List<FieldViewModel>)) : base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, propertyBinder);
    }

    private object BindFieldCollection(
      ControllerContext controllerContext,
      ModelBindingContext bindingContext,
      IEnumerable<FieldViewModel> list)
    {
      int num = 0;
      List<FieldViewModel> fieldViewModelList = new List<FieldViewModel>();
      foreach (FieldViewModel fieldViewModel in list)
      {
        IModelBinder imodelBinder = this.Binders.GetBinder(fieldViewModel.GetType());
        if (((object) imodelBinder).GetType() == typeof (DefaultModelBinder))
          imodelBinder = (IModelBinder) new FieldModelBinder();
        string subIndexName = DefaultModelBinder.CreateSubIndexName(bindingContext.ModelName, num);
        if (bindingContext.ValueProvider.ContainsPrefix(subIndexName))
        {
          ModelBindingContext modelBindingContext1 = new ModelBindingContext()
          {
            ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType((Func<object>) null, fieldViewModel.GetType()),
            ModelName = subIndexName
          };
          modelBindingContext1.ModelMetadata.Model = (object) fieldViewModel;
          modelBindingContext1.ModelState = bindingContext.ModelState;
          modelBindingContext1.PropertyFilter = bindingContext.PropertyFilter;
          modelBindingContext1.ValueProvider = bindingContext.ValueProvider;
          ModelBindingContext modelBindingContext2 = modelBindingContext1;
          object obj = imodelBinder.BindModel(controllerContext, modelBindingContext2);
          fieldViewModelList.Add((FieldViewModel) obj);
        }
        ++num;
      }
      return (object) fieldViewModelList;
    }
  }
}
