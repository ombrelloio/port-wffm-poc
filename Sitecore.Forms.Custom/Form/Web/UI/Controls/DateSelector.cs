// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.DateSelector
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.Core.Visual;
using Sitecore.Form.UI.Adapters;
using Sitecore.WFFM.Abstractions.Actions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  [Adapter(typeof (DateAdapter))]
  [ValidationProperty("Value")]
  public class DateSelector : ValidateControl, IHasTitle
  {
    private static readonly string baseCssClassName = "scfDateSelectorBorder";
    protected DropDownList day = new DropDownList();
    protected Panel generalPanel = new Panel();
    protected DropDownList month = new DropDownList();
    protected System.Web.UI.WebControls.Label title = new System.Web.UI.WebControls.Label();
    protected DropDownList year = new DropDownList();

    public DateSelector(HtmlTextWriterTag tag)
      : base(tag)
    {
      this.EnableViewState = false;
      this.CssClass = DateSelector.baseCssClassName;
      this.classAtributes[nameof (DateFormat)] = "yyyy-MMMM-dd";
      this.classAtributes["startDate"] = DateUtil.IsoDateToServerTimeIsoDate("20000101T120000");
      this.classAtributes["endDate"] = DateUtil.ToIsoDate(DateUtil.ToServerTime(DateTime.UtcNow.AddYears(1)));
      this.SelectedDate = DateUtil.ToIsoDate(DateTime.UtcNow);
      this.DayLabel = "Day";
      this.MonthLabel = "Month";
      this.YearLabel = "Year";
      Sitecore.Form.Core.Utility.Utils.SetUserCulture();
    }

    public DateSelector()
      : this(HtmlTextWriterTag.Div)
    {
    }

    public override void RenderControl(HtmlTextWriter writer) => this.DoRender(writer);

    protected virtual void DoRender(HtmlTextWriter writer) => base.RenderControl(writer);

    protected override void OnInit(EventArgs e)
    {
      this.day.CssClass = "scfDateSelectorDay";
      this.month.CssClass = "scfDateSelectorMonth";
      this.year.CssClass = "scfDateSelectorYear";
      this.help.CssClass = "scfDateSelectorUsefulInfo";
      this.generalPanel.CssClass = "scfDateSelectorGeneralPanel";
      this.title.CssClass = "scfDateSelectorLabel";
      this.Controls.AddAt(0, (Control) this.generalPanel);
      this.Controls.AddAt(0, (Control) this.title);
      this.generalPanel.Controls.AddAt(0, (Control) this.help);
      List<string> stringList = new List<string>((IEnumerable<string>) this.DateFormat.Split('-'));
      stringList.Reverse();
      stringList.ForEach(new Action<string>(this.InsertDateList));
      stringList.ForEach(new Action<string>(this.InsertLabelForDateList));
      this.RegisterCommonScript();
      this.month.Attributes["onclick"] = "javascript:return $scw.webform.controls.updateDateSelector(this);";
      this.year.Attributes["onclick"] = "javascript:return $scw.webform.controls.updateDateSelector(this);";
      this.day.Attributes["onclick"] = "javascript:return $scw.webform.controls.updateDateSelector(this);";
    }

    protected override void Render(HtmlTextWriter writer)
    {
      int count = this.day.Items.Count;
      while (count <= 31)
      {
        ++count;
        this.Page.ClientScript.RegisterForEventValidation(this.day.UniqueID, count.ToString());
      }
      base.Render(writer);
    }

    protected override void OnPreRender(EventArgs e)
    {
      int result = this.CurrentDate.Year;
      if (string.IsNullOrEmpty(this.year.SelectedValue) || !int.TryParse(this.year.SelectedValue, out result))
        result = this.CurrentDate.Year;
      while (this.day.Items.Count > CultureInfo.InvariantCulture.Calendar.GetDaysInMonth(result, this.month.SelectedIndex + 1))
        this.day.Items.RemoveAt(this.day.Items.Count - 1);
      HiddenField hiddenField1 = new HiddenField();
      hiddenField1.ID = (this.ID ?? string.Empty) + "_complexvalue";
      hiddenField1.Value = this.Value;
      HiddenField hiddenField2 = hiddenField1;
      if (this.FindControl(hiddenField2.ID) == null)
        this.generalPanel.Controls.AddAt(0, (Control) hiddenField2);
      base.OnPreRender(e);
      this.title.AssociatedControlID = (string) null;
    }

    private void InsertLabelForDateList(string marker)
    {
      switch (marker.ToLower()[0])
      {
        case 'd':
          ControlCollection controls1 = this.generalPanel.Controls;
          System.Web.UI.WebControls.Label label1 = new System.Web.UI.WebControls.Label();
          label1.AssociatedControlID = this.day.ID;
          label1.Text = Translate.Text(this.DayLabel ?? string.Empty);
          label1.CssClass = "scfDateSelectorShortLabelDay";
          controls1.AddAt(0, (Control) label1);
          break;
        case 'm':
          ControlCollection controls2 = this.generalPanel.Controls;
          System.Web.UI.WebControls.Label label2 = new System.Web.UI.WebControls.Label();
          label2.AssociatedControlID = this.month.ID;
          label2.Text = Translate.Text(this.MonthLabel ?? string.Empty);
          label2.CssClass = "scfDateSelectorShortLabelMonth";
          controls2.AddAt(0, (Control) label2);
          break;
        case 'y':
          ControlCollection controls3 = this.generalPanel.Controls;
          System.Web.UI.WebControls.Label label3 = new System.Web.UI.WebControls.Label();
          label3.AssociatedControlID = this.year.ID;
          label3.Text = Translate.Text(this.YearLabel ?? string.Empty);
          label3.CssClass = "scfDateSelectorShortLabelYear";
          controls3.AddAt(0, (Control) label3);
          break;
      }
    }

    private void InsertDateList(string marker)
    {
      switch (marker.ToLower()[0])
      {
        case 'd':
          this.InitDay(marker);
          this.generalPanel.Controls.AddAt(0, (Control) this.day);
          break;
        case 'm':
          this.InitMonth(marker);
          this.generalPanel.Controls.AddAt(0, (Control) this.month);
          break;
        case 'y':
          this.InitYear(marker);
          this.generalPanel.Controls.AddAt(0, (Control) this.year);
          break;
      }
    }

    private void InitDay(string marker)
    {
      this.day.Items.Clear();
      for (int index = 1; index <= 31; ++index)
        this.day.Items.Add(new ListItem(index.ToString(), index.ToString()));
      this.day.SelectedIndex = this.CurrentDate.Day - 1;
    }

    private void InitMonth(string marker)
    {
      DateTime dateTime = new DateTime();
      this.month.Items.Clear();
      for (int index = 1; index <= 12; ++index)
        this.month.Items.Add(new ListItem(string.Format("{0:" + marker + "}", (object) dateTime.AddMonths(index - 1)), index.ToString()));
      this.month.SelectedIndex = this.CurrentDate.Month - 1;
    }

    private void InitYear(string marker)
    {
      DateTime dateTime1 = new DateTime(this.StartDateTime.Year - 1, 1, 1);
      this.year.Items.Clear();
      int year1 = this.StartDateTime.Year;
      DateTime dateTime2;
      while (true)
      {
        int num = year1;
        dateTime2 = this.EndDateTime;
        int year2 = dateTime2.Year;
        if (num <= year2)
        {
          dateTime1 = dateTime1.AddYears(1);
          this.year.Items.Add(new ListItem(string.Format("{0:" + marker + "}", (object) dateTime1), year1.ToString()));
          ++year1;
        }
        else
          break;
      }
      DropDownList year3 = this.year;
      dateTime2 = this.CurrentDate;
      string str = dateTime2.Year.ToString();
      year3.SelectedValue = str;
    }

    protected void RegisterCommonScript()
    {
    }

    private DateTime CurrentDate => !string.IsNullOrEmpty(this.SelectedDate) ? DateUtil.IsoDateToDateTime(DateUtil.IsoDateToServerTimeIsoDate(this.SelectedDate)) : DateUtil.ToServerTime(DateTime.UtcNow);

    private DateTime StartDateTime => DateUtil.IsoDateToDateTime(this.StartDate);

    private DateTime EndDateTime => DateUtil.IsoDateToDateTime(this.EndDate);

    public string Value => DateUtil.ToIsoDate(new DateTime(this.StartDateTime.Year + (this.year.SelectedIndex > -1 ? this.year.SelectedIndex : 0), (this.month.SelectedIndex > -1 ? this.month.SelectedIndex : 0) + 1, (this.day.SelectedIndex > -1 ? this.day.SelectedIndex : 0) + 1).Date);

    [VisualProperty("Selected Date:", 100)]
    [System.ComponentModel.DefaultValue("today")]
    [VisualFieldType(typeof (SelectDateField))]
    public string SelectedDate { get; set; }

    public override string DefaultValue
    {
      set => this.SelectedDate = value;
    }

    [VisualProperty("Display Format:", 100)]
    [System.ComponentModel.DefaultValue("yyyy-MMMM-dd")]
    [VisualFieldType(typeof (DateFormatField))]
    public string DateFormat
    {
      get => this.classAtributes[nameof (DateFormat)];
      set => this.classAtributes[nameof (DateFormat)] = value;
    }

    [VisualProperty("Start Date:", 2000)]
    [System.ComponentModel.DefaultValue("20000101T000000")]
    [VisualCategory("Validation")]
    [VisualFieldType(typeof (SelectDateField))]
    public string StartDate
    {
      get => this.classAtributes["startDate"];
      set => this.classAtributes["startDate"] = DateUtil.IsIsoDate(value) ? value : throw new ArgumentException("The value is not an iso date.", nameof (value));
    }

    [VisualProperty("End Date:", 2000)]
    [VisualCategory("Validation")]
    [VisualFieldType(typeof (SelectDateField))]
    public string EndDate
    {
      get => this.classAtributes["endDate"];
      set => this.classAtributes["endDate"] = DateUtil.IsIsoDate(value) ? value : throw new ArgumentException("The value is not an iso date.", nameof (value));
    }

    public override string ID
    {
      get => base.ID;
      set
      {
        this.title.ID = value + "_text";
        this.day.ID = value + "_day";
        this.month.ID = value + "_month";
        this.year.ID = value + "_year";
        base.ID = value;
      }
    }

    public override ControlResult Result
    {
      get => new ControlResult(this.ControlName, (object) this.Value, (string) null);
      set
      {
      }
    }

    [System.ComponentModel.DefaultValue("scfDateSelectorBorder")]
    [VisualProperty("CSS Class:", 600)]
    [VisualFieldType(typeof (CssClassField))]
    public new string CssClass
    {
      get => base.CssClass;
      set => base.CssClass = value;
    }

    protected override Control InnerValidatorContainer => (Control) this.generalPanel;

    protected override Control ValidatorContainer => (Control) this;

    [Bindable(true)]
    [Description("Title")]
    public string Title
    {
      get => this.title.Text;
      set => this.title.Text = value;
    }

    public string DayLabel { get; set; }

    public string MonthLabel { get; set; }

    public string YearLabel { get; set; }
  }
}
