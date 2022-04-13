// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.Fields.DateField
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Forms.Mvc.Attributes;
using Sitecore.Forms.Mvc.TypeConverters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.ViewModels.Fields
{
  public class DateField : ValuedFieldViewModel<string>
  {
    public object Day { get; set; }

    public object Month { get; set; }

    public object Year { get; set; }

    public string DayTitle { get; set; }

    public string MonthTitle { get; set; }

    public string YearTitle { get; set; }

    [DefaultValue("yyyy-MMMM-dd")]
    public string DateFormat { get; set; }

    public List<SelectListItem> Years { get; private set; }

    public List<SelectListItem> Months { get; private set; }

    public List<SelectListItem> Days { get; private set; }

    [TypeConverter(typeof (IsoDateTimeConverter))]
    public DateTime StartDate { get; set; }

    [TypeConverter(typeof (IsoDateTimeConverter))]
    public DateTime EndDate { get; set; }

    [ParameterName("SelectedDate")]
    public override string Value
    {
      get => base.Value;
      set
      {
        base.Value = value;
        this.OnValueUpdated();
      }
    }

    public override string ResultParameters => this.DateFormat;

    protected void OnValueUpdated()
    {
      if (!string.IsNullOrEmpty(this.Value))
      {
        DateTime dateTime = DateUtil.IsoDateToDateTime(this.Value);
        this.Day = (object) dateTime.Day;
        this.Month = (object) dateTime.Month;
        this.Year = (object) dateTime.Year;
      }
      this.InitItems();
    }

    public override void Initialize()
    {
      if (string.IsNullOrEmpty(this.DateFormat))
        this.DateFormat = "yyyy-MMMM-dd";
      if (this.StartDate == DateTime.MinValue)
        this.StartDate = DateUtil.IsoDateToDateTime("20000101T120000");
      if (this.EndDate == DateTime.MinValue)
      {
        DateTime dateTime = DateTime.Now;
        dateTime = dateTime.AddYears(1);
        this.EndDate = dateTime.Date;
      }
      this.Years = new List<SelectListItem>();
      this.Months = new List<SelectListItem>();
      this.Days = new List<SelectListItem>();
      this.InitItems();
    }

    private void InitItems()
    {
      List<string> stringList = new List<string>((IEnumerable<string>) this.DateFormat.Split('-'));
      stringList.Reverse();
      stringList.ForEach(new Action<string>(this.InitDate));
    }

    private void InitDate(string marker)
    {
      DateTime? current = string.IsNullOrEmpty(this.Value) ? new DateTime?() : new DateTime?(DateUtil.IsoDateToDateTime(this.Value));
      switch (marker.ToLower()[0])
      {
        case 'd':
          this.InitDays(current);
          break;
        case 'm':
          this.InitMonth(marker, current);
          break;
        case 'y':
          this.InitYears(marker, current);
          break;
      }
    }

    private void InitYears(string marker, DateTime? current)
    {
      DateTime dateTime = new DateTime(this.StartDate.Year - 1, 1, 1);
      this.Years.Clear();
      int year1 = this.StartDate.Year;
      while (true)
      {
        int num1 = year1;
        DateTime endDate = this.EndDate;
        int year2 = endDate.Year;
        if (num1 <= year2)
        {
          dateTime = dateTime.AddYears(1);
          SelectListItem selectListItem = new SelectListItem();
          selectListItem.Text = string.Format("{0:" + marker + "}", (object) dateTime);
          selectListItem.Value = year1.ToString((IFormatProvider) CultureInfo.InvariantCulture);
          int num2;
          if (current.HasValue)
          {
            endDate = current.Value;
            num2 = endDate.Year == year1 ? 1 : 0;
          }
          else
            num2 = 0;
          selectListItem.Selected = num2 != 0;
          this.Years.Add(selectListItem);
          ++year1;
        }
        else
          break;
      }
    }

    private void InitDays(DateTime? current)
    {
      this.Days.Clear();
      int num = current.HasValue ? DateTime.DaysInMonth(current.Value.Year, current.Value.Month) : 31;
      for (int index = 1; index <= 31; ++index)
      {
        if (index <= num)
          this.Days.Add(new SelectListItem()
          {
            Selected = current.HasValue && current.Value.Day == index,
            Text = index.ToString((IFormatProvider) CultureInfo.InvariantCulture),
            Value = index.ToString((IFormatProvider) CultureInfo.InvariantCulture)
          });
      }
    }

    private void InitMonth(string marker, DateTime? current)
    {
      DateTime dateTime = new DateTime();
      this.Months.Clear();
      for (int index = 1; index <= 12; ++index)
        this.Months.Add(new SelectListItem()
        {
          Selected = current.HasValue && current.Value.Month == index,
          Text = string.Format("{0:" + marker + "}", (object) dateTime.AddMonths(index - 1)),
          Value = index.ToString((IFormatProvider) CultureInfo.InvariantCulture)
        });
    }
  }
}
