// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Interfaces.IAutoMapper`2
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

namespace Sitecore.Forms.Mvc.Interfaces
{
  public interface IAutoMapper<in TFormModel, TFormViewModel>
    where TFormModel : class, IModelEntity
    where TFormViewModel : class, IViewModel
  {
    TFormViewModel GetView(TFormModel formModel);

    void SetModelResults(TFormViewModel view, TFormModel model);
  }
}
