// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.Fields.ListBoxField
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Forms.Core.Data;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System.ComponentModel;

namespace Sitecore.Forms.Mvc.ViewModels.Fields
{
  public class ListBoxField : CheckboxListField
  {
    public ListBoxField()
      : this(new ListFieldValueFormatter(DependenciesManager.Resolve<ISettings>()))
    {
    }

    public ListBoxField(ListFieldValueFormatter listFieldValueFormatter)
      : base(listFieldValueFormatter)
    {
      this.SelectionMode = "Single";
      this.Rows = 4;
    }

    [DefaultValue(4)]
    public int Rows { get; set; }

    public string SelectionMode { get; set; }
  }
}
