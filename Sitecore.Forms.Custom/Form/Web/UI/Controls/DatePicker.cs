// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.DatePicker
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Visual;
using Sitecore.Form.UI.Adapters;
using Sitecore.WFFM.Abstractions.Actions;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  [ValidationProperty("Text")]
  [Adapter(typeof (DateAdapter))]
  public class DatePicker : InputControl
  {
    private static readonly string baseCssClassName = "scfDatePickerBorder";

    public DatePicker()
      : this(HtmlTextWriterTag.Div)
    {
    }

    public DatePicker(HtmlTextWriterTag tag)
      : base(tag)
    {
      this.CssClass = DatePicker.baseCssClassName;
      this.classAtributes[nameof (DateFormat)] = "yy-MM-dd";
      this.classAtributes["startDate"] = DateUtil.IsoDateToServerTimeIsoDate("20000101T120000");
      this.classAtributes["endDate"] = DateUtil.ToIsoDate(DateUtil.ToServerTime(DateTime.UtcNow.AddYears(1)));
      this.Text = DateUtil.ToIsoDate(DateTime.UtcNow);
    }

    public override ControlResult Result
    {
      get => new ControlResult(this.ControlName, (object) this.Text, this.DateFormat)
      {
        AdaptForAnalyticsTag = false
      };
      set
      {
      }
    }

    [System.ComponentModel.DefaultValue("scfDatePickerBorder")]
    [VisualProperty("CSS Class:", 600)]
    [VisualFieldType(typeof (CssClassField))]
    public new string CssClass
    {
      get => base.CssClass;
      set => base.CssClass = value;
    }

    [VisualProperty("Display Format:", 50)]
    [System.ComponentModel.DefaultValue("yy-MM-dd")]
    [VisualFieldType(typeof (DateFormatField))]
    public string DateFormat
    {
      get => this.classAtributes[nameof (DateFormat)];
      set => this.classAtributes[nameof (DateFormat)] = value;
    }

    public override string DefaultValue
    {
      set => this.Text = value;
    }

    [VisualProperty("End Date:", 2000)]
    [VisualCategory("Validation")]
    [VisualFieldType(typeof (SelectDateField))]
    public string EndDate
    {
      get => this.classAtributes["endDate"];
      set => this.classAtributes["endDate"] = DateUtil.IsIsoDate(value) ? value : throw new ArgumentException("The value is not an iso date.", nameof (value));
    }

    [VisualProperty("Selected Date:", 100)]
    [System.ComponentModel.DefaultValue("today")]
    [VisualFieldType(typeof (SelectDateField))]
    public override string Text
    {
      get => this.textbox.Text;
      set => this.textbox.Text = value;
    }

    [VisualProperty("Start Date:", 1900)]
    [System.ComponentModel.DefaultValue("20000101T000000")]
    [VisualCategory("Validation")]
    [VisualFieldType(typeof (SelectDateField))]
    public string StartDate
    {
      get => this.classAtributes["startDate"];
      set => this.classAtributes["startDate"] = DateUtil.IsIsoDate(value) ? value : throw new ArgumentException("The value is not an iso date.", nameof (value));
    }

    private DateTime CurrentDate => this.GetDate(this.Text);

    private DateTime MaxDate => this.GetDate(this.EndDate);

    private DateTime MinDate => this.GetDate(this.StartDate);

    public DateTime GetDate(string date)
    {
      try
      {
        return string.IsNullOrEmpty(date) ? DateUtil.ToServerTime(DateTime.UtcNow) : DateUtil.IsoDateToDateTime(date);
      }
      catch
      {
        return DateTime.UtcNow;
      }
    }

    private string GetLanguageIso()
    {
      try
      {
        string letterIsoLanguageName = Sitecore.Context.Language.CultureInfo.TwoLetterISOLanguageName;
        if (letterIsoLanguageName != "en")
          return letterIsoLanguageName;
      }
      catch (Exception ex)
      {
      }
      return string.Empty;
    }

    protected string GetOptions()
    {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{");
            if (!string.IsNullOrEmpty(Text))
            {
                stringBuilder.AppendFormat("'defaultDate' : new Date({0}, {1}, {2}),", CurrentDate.Year, CurrentDate.Month - 1, CurrentDate.Day);
            }
            stringBuilder.AppendFormat("'minDate' : new Date({0}, {1}, {2}),", MinDate.Year, MinDate.Month - 1, MinDate.Day);
            stringBuilder.AppendFormat("'maxDate' : new Date({0}, {1}, {2}),", MaxDate.Year, MaxDate.Month - 1, MaxDate.Day);
            stringBuilder.AppendFormat("'dateFormat' : '{0}'", ClientDateFormatConverter.ConvertToClientFormat(DateFormat));
            stringBuilder.Append("}");
            return stringBuilder.ToString();
        }

    protected override void OnInit(EventArgs e)
    {
      this.textbox.CssClass = "scfDatePickerTextBox";
      this.help.CssClass = "scfDatePickerUsefulInfo";
      this.generalPanel.CssClass = "scfDatePickerGeneralPanel";
      this.title.CssClass = "scfDatePickerLabel";
      this.textbox.TextMode = TextBoxMode.SingleLine;
      this.Controls.AddAt(0, (Control) this.generalPanel);
      this.Controls.AddAt(0, (Control) this.title);
      this.generalPanel.Controls.AddAt(0, (Control) this.help);
      this.generalPanel.Controls.AddAt(0, (Control) this.textbox);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      try
      {
        if (string.IsNullOrEmpty(this.Text) || DateUtil.IsIsoDate(this.Text))
          return;
        this.Text = DateUtil.ToIsoDate(DateTime.ParseExact(this.Text, this.DateFormat, (IFormatProvider) Sitecore.Context.Language.CultureInfo.DateTimeFormat));
      }
      catch
      {
      }
    }

    protected override void OnPreRender(EventArgs e)
    {
      if (!string.IsNullOrEmpty(this.Text))
        this.Text = this.CurrentDate.ToString(this.DateFormat);
      if (this.Page != null)
      {
        string str = Sitecore.StringExtensions.StringExtensions.FormatWith("$scw(document).ready(function() {0} \r\n$scw('#{1}').datepicker(\r\n$scw.extend($scw.datepicker.regional['{2}'], {3}));\r\n {4} );", new object[5]
        {
          (object) "{",
          (object) this.textbox.ClientID,
          (object) this.GetLanguageIso(),
          (object) this.GetOptions(),
          (object) "}"
        });
        this.Page.ClientScript.RegisterStartupScript(this.GetType(), str, str, true);
      }
      base.OnPreRender(e);
    }
  }
}
