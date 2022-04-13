// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.Fields.CheckboxListField
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Core.Data;
using Sitecore.Forms.Mvc.Attributes;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.TypeConverters;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.ViewModels.Fields
{
  public class CheckboxListField : ValuedFieldViewModel<List<string>>, ISelectList
  {
    private readonly ListFieldValueFormatter listFieldValueFormatter;

    public CheckboxListField()
      : this(new ListFieldValueFormatter(DependenciesManager.Resolve<ISettings>()))
    {
    }

    public CheckboxListField(ListFieldValueFormatter listFieldValueFormatter)
    {
      Assert.ArgumentNotNull((object) listFieldValueFormatter, nameof (listFieldValueFormatter));
      this.listFieldValueFormatter = listFieldValueFormatter;
      this.Items = new List<SelectListItem>();
    }

    [ParameterName("Direction")]
    [TypeConverter(typeof (StringConverter))]
    public string Direction { get; set; }

    [ParameterName("Columns")]
    [TypeConverter(typeof (Int32Converter))]
    public int Columns { get; set; }

    [TypeConverter(typeof (ListSelectItemsConverter))]
    public List<SelectListItem> Items { get; set; }

    [TypeConverter(typeof (ListItemsConverter))]
    [ParameterName("selectedvalue")]
    public override List<string> Value { get; set; }

    public override ControlResult GetResult() => this.listFieldValueFormatter.GetFormattedResult(this.FieldItemId, this.Title, (IEnumerable<string>) this.Value, (IEnumerable<string>) this.Value);

    public override void Initialize()
    {
      List<string> selectedValues = this.Value;
      if (this.Items == null)
        this.Items = new List<SelectListItem>();
      if (selectedValues == null)
        return;
      this.Items.ForEach((Action<SelectListItem>) (x => x.Selected = selectedValues.Contains(x.Value)));
    }

    public override void SetValueFromQuery(string valueFromQuery)
    {
      if (string.IsNullOrEmpty(valueFromQuery))
        return;
      string str1 = valueFromQuery;
      char[] chArray = new char[1]{ ',' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (this.Items != null)
        {
          foreach (SelectListItem selectListItem in this.Items)
          {
            if (selectListItem.Value.Equals(str2, StringComparison.OrdinalIgnoreCase))
              selectListItem.Selected = true;
          }
        }
      }
    }
  }
}
