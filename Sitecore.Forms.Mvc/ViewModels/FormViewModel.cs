// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.FormViewModel
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Data.Items;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.WFFM.Abstractions.Data.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Sitecore.Forms.Mvc.ViewModels
{
  public class FormViewModel : 
    IViewModel,
    IHasItem,
    IHasId,
    ISubmitSettings,
    IStyleSettings,
    IHasErrors
  {
    public FormViewModel()
    {
      this.Errors = new List<string>();
      this.Sections = new List<SectionViewModel>();
      this.FormType = FormType.Horizontal;
    }

    public Guid UniqueId { get; set; }

    public string ClientId => FormViewModel.GetClientId(this.UniqueId);

    public string Information { get; set; }

    public string Title { get; set; }

    public string Name { get; set; }

    public string TitleTag { get; set; }

    public bool ShowInformation { get; set; }

    public bool ShowTitle { get; set; }

    public bool Visible { get; set; }

    public Item Item { get; set; }

    public Dictionary<string, string> Parameters { get; set; }

    public bool ShowFooter { get; set; }

    public bool IsAjaxForm { get; set; }

    public bool IsSaveFormDataToStorage { get; set; }

    public List<string> Errors { get; set; }

    public bool SuccessSubmit { get; set; }

    public List<SectionViewModel> Sections { get; set; }

    public string SubmitButtonName { get; set; }

    public string SubmitButtonPosition { get; set; }

    public string SubmitButtonType { get; set; }

    public string SubmitButtonSize { get; set; }

    public string SuccessMessage { get; set; }

    public string CssClass { get; set; }

    public bool ReadQueryString { get; set; }

    public NameValueCollection QueryParameters { get; set; }

    public FormType FormType { get; set; }

    public string LeftColumnStyle { get; set; }

    public string RightColumnStyle { get; set; }

    public string Footer { get; set; }

    public static string GetClientId(Guid id) => string.Format("wffm{0}", (object) id.ToString("N"));
  }
}
