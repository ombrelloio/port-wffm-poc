// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.Fields.RadioListField
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Form.Core.Utility;
using Sitecore.Forms.Mvc.Attributes;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.TypeConverters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.ViewModels.Fields
{
  public class RadioListField : ValuedFieldViewModel<string>, ISelectList
  {
    public RadioListField() => this.Items = new List<SelectListItem>();

    public override string ResultParameters
    {
      get
      {
        if (this.Items != null)
        {
          SelectListItem selectListItem = ((IEnumerable<SelectListItem>) this.Items).SingleOrDefault<SelectListItem>((Func<SelectListItem, bool>) (x => x.Selected));
          if (selectListItem != null)
            return selectListItem.Text;
        }
        return string.Empty;
      }
    }

    [ParameterName("Direction")]
    [TypeConverter(typeof (StringConverter))]
    public string Direction { get; set; }

    [ParameterName("Columns")]
    [TypeConverter(typeof (Int32Converter))]
    public int Columns { get; set; }

    [TypeConverter(typeof (ListSelectItemsConverter))]
    public List<SelectListItem> Items { get; set; }

    [ParameterName("selectedvalue")]
    public override string Value { get; set; }

    public override void Initialize()
    {
      if (this.Items == null)
        this.Items = new List<SelectListItem>();
      if (string.IsNullOrEmpty(this.Value))
        return;
      this.Value = string.Join(",", ParametersUtil.XmlToStringArray(this.Value));
    }
  }
}
