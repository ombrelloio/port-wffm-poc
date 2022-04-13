// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Interfaces.ISubmitSettings
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

namespace Sitecore.Forms.Mvc.Interfaces
{
  public interface ISubmitSettings
  {
    bool IsAjaxForm { get; set; }

    bool IsSaveFormDataToStorage { get; set; }

    string SubmitButtonName { get; set; }

    string SubmitButtonPosition { get; set; }

    string SubmitButtonType { get; set; }

    string SubmitButtonSize { get; set; }

    bool SuccessSubmit { get; set; }
  }
}
