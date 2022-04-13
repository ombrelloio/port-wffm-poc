// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.Fields.DatePickerField
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Forms.Mvc.TypeConverters;
using Sitecore.WFFM.Abstractions.Actions;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Sitecore.Forms.Mvc.ViewModels.Fields
{
  public class DatePickerField : ValuedFieldViewModel<string>
  {
    public DatePickerField()
    {
      this.DateFormat = "yy-MM-dd";
      this.StartDate = DateUtil.IsoDateToDateTime("20000101T120000");
      DateTime dateTime = DateTime.Now;
      dateTime = dateTime.AddYears(1);
      this.EndDate = dateTime.Date;
    }

    public override string Value
    {
      get => base.Value;
      set => base.Value = DateUtil.IsIsoDate(value) ? DateUtil.IsoDateToDateTime(value).ToString(this.DateFormat ?? "yy-MM-dd") : value;
    }

    public string DateFormat { get; set; }

    [TypeConverter(typeof (IsoDateTimeConverter))]
    public DateTime StartDate { get; set; }

    [TypeConverter(typeof (IsoDateTimeConverter))]
    public DateTime EndDate { get; set; }

    public override void Initialize()
    {
      if (!string.IsNullOrEmpty(this.Value))
        return;
      this.Value = DateTime.Now.ToString(this.DateFormat);
    }

    public override ControlResult GetResult() => new ControlResult(this.FieldItemId, this.Title, (object) DateUtil.ToIsoDate(DateTime.ParseExact(this.Value, this.DateFormat, (IFormatProvider) DateTimeFormatInfo.InvariantInfo)), this.ResultParameters);
  }
}
