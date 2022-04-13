// Decompiled with JetBrains decompiler
// Type: Sitecore.Shell.Applications.WebEdit.Dialogs.DateSelector.DateSelectorPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using ComponentArt.Web.UI;
using Sitecore.Applications.WebEdit.Dialogs.DateSelector;
using Sitecore.Controls;
using Sitecore.Diagnostics;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XamlSharp.Xaml;
using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Sitecore.Shell.Applications.WebEdit.Dialogs.DateSelector
{
  public class DateSelectorPage : DialogPage
  {
    protected ComponentArt.Web.UI.Calendar Calendar1;
    protected HtmlTableCell AMPMInputCell;
    protected HtmlTableCell AMPMArrowsCell;
    protected XamlControl Dialog;

    public string Active
    {
      get => StringUtil.GetString(ViewState[nameof (Active)]);
      set => ViewState[nameof (Active)] = (object) value;
    }

    public bool MilitaryTime
    {
      get => MainUtil.GetBool(ViewState["MT"], true);
      set => ViewState["MT"] = (object) value;
    }

    protected override void OnLoad(EventArgs e)
    {
      Assert.ArgumentNotNull((object) e, nameof (e));
      Assert.CanRunApplication("/sitecore/content/Applications/WebEdit");
      base.OnLoad(e);
      CultureInfo culture = Sitecore.Context.Culture;
      DateTimeFormatInfo dateTimeFormat = culture.DateTimeFormat;
      this.MilitaryTime = string.IsNullOrEmpty(dateTimeFormat.PMDesignator) && string.IsNullOrEmpty(dateTimeFormat.AMDesignator);
      SelectDateOptions selectDateOptions = SelectDateOptions.Parse();
      this.Header = selectDateOptions.Header;
      this.Text = selectDateOptions.Text;
      string newValue1 = !this.MilitaryTime ? selectDateOptions.SelectedDate.ToString("hh", (IFormatProvider) culture) : selectDateOptions.SelectedDate.Hour.ToString().PadLeft(2, '0');
      string newValue2 = selectDateOptions.SelectedDate.ToString("mm", (IFormatProvider) culture);
      string str1 = string.Empty;
      if (!this.MilitaryTime)
      {
        string str2 = selectDateOptions.SelectedDate.ToString("tt", (IFormatProvider) culture);
        str1 = Sitecore.StringExtensions.StringExtensions.FormatWith("var sc_amDesignator = '{0}'; var sc_pmDesignator = '{1}'; $('AMPMInput').value='{2}';", new object[3]
        {
          (object) dateTimeFormat.AMDesignator,
          (object) dateTimeFormat.PMDesignator,
          (object) str2
        });
      }
      ((Control) this).Page.ClientScript.RegisterStartupScript(typeof (DateSelectorPage), "settime", str1 + "if ($('Hour') && $('Minute')) { $('Hour').value = '{Hour}'; $('Minute').value = '{Minute}'; }".Replace("{Hour}", newValue1).Replace("{Minute}", newValue2), true);
    }

    protected override void OnPreRender(EventArgs e)
    {
      // ISSUE: explicit non-virtual call
      OnPreRender(e);
      if (XamlControl.AjaxScriptManager.IsEvent)
        return;
      SelectDateOptions selectDateOptions = SelectDateOptions.Parse();
      this.Calendar1.SelectedDate = selectDateOptions.SelectedDate;
      this.Calendar1.VisibleDate = selectDateOptions.SelectedDate;
      this.Calendar1.Culture = Sitecore.Context.Culture;
    }

    protected override void OK_Click()
    {
      DateTime dateTime = this.Calendar1.SelectedDate;
      if (dateTime.Kind == DateTimeKind.Local)
        dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
      DateTimeFormatInfo dateTimeFormat = Sitecore.Context.Culture.DateTimeFormat;
      int hours = MainUtil.GetInt(WebUtil.GetFormValue("Hour"), -1);
      int minutes = MainUtil.GetInt(WebUtil.GetFormValue("Minute"), -1);
      bool flag = WebUtil.GetFormValue("AMPMInput") == dateTimeFormat.PMDesignator;
      if ((this.MilitaryTime ? (hours < 0 ? 1 : (hours > 24 ? 1 : 0)) : (hours < 1 ? 1 : (hours > 12 ? 1 : 0))) != 0)
        SheerResponse.Alert("The hour is not a valid hour.", Array.Empty<string>());
      else if (minutes < 0 || minutes > 59)
        SheerResponse.Alert("The minute is not a valid minute.", Array.Empty<string>());
      else if (hours == 24 && minutes != 0)
        SheerResponse.Alert("The hour is not a valid hour.", Array.Empty<string>());
      else if (!this.MilitaryTime && WebUtil.GetFormValue("AMPMInput") != dateTimeFormat.AMDesignator && WebUtil.GetFormValue("AMPMInput") != dateTimeFormat.PMDesignator)
      {
        SheerResponse.Alert("AM/PM designator is not valid", Array.Empty<string>());
      }
      else
      {
        dateTime = dateTime.Subtract(new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second));
        dateTime = dateTime.Add(new TimeSpan(hours, minutes, 0));
        if (!this.MilitaryTime)
        {
          if (flag && dateTime.Hour < 12)
            dateTime = dateTime.AddHours(12.0);
          else if (!flag && dateTime.Hour == 12)
            dateTime = dateTime.AddHours(-12.0);
        }
        SheerResponse.SetDialogValue(DateUtil.ToIsoDate(dateTime));
        base.OK_Click();
      }
    }
  }
}
