// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.SectionViewModel
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Data.Items;
using Sitecore.Forms.Mvc.Interfaces;
using System.Collections.Generic;

namespace Sitecore.Forms.Mvc.ViewModels
{
  public class SectionViewModel : IViewModel
  {
    public SectionViewModel() => this.Fields = new List<FieldViewModel>();

    public string Name { get; set; }

    public string Information { get; set; }

    public string Title { get; set; }

    public bool ShowInformation { get; set; }

    public string ShowLegend { get; set; }

    public bool ShowTitle { get; set; }

    public bool Visible { get; set; }

    public Item Item { get; set; }

    public Dictionary<string, string> Parameters { get; set; }

    public List<FieldViewModel> Fields { get; set; }

    public string CssClass { get; set; }
  }
}
