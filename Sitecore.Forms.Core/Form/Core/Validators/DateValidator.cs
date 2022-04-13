// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Validators.DateValidator
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Utility;
using System;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Validators
{
  public class DateValidator : FormCustomValidator
  {
    public DateValidator()
    {
      this.ServerValidate += new ServerValidateEventHandler(this.OnDateValidate);
      this.StartDate = "20000101T000000";
      this.EndDate = DateUtil.ToIsoDate(DateTime.UtcNow.AddYears(1));
      Sitecore.Form.Core.Utility.Utils.SetUserCulture();
    }

    public string EndDate
    {
      get => this.classAttributes["enddate"];
      set => this.classAttributes["enddate"] = value;
    }

    public string StartDate
    {
      get => this.classAttributes["startdate"];
      set => this.classAttributes["startdate"] = value;
    }

    protected DateTime EndDateTime => DateUtil.IsoDateToDateTime(this.EndDate).Date;

    protected DateTime StartDateTime => DateUtil.IsoDateToDateTime(this.StartDate).Date;

    protected override bool EvaluateIsValid()
    {
      try
      {
        return string.IsNullOrEmpty(this.ControlToValidate) || base.EvaluateIsValid();
      }
      catch (ArgumentOutOfRangeException ex)
      {
        return false;
      }
    }

    protected override void OnLoad(EventArgs e)
    {
      string errorMessage = this.ErrorMessage;
      DateTime dateTime1 = this.StartDateTime;
      string str1 = dateTime1.ToString(Sitecore.WFFM.Abstractions.Constants.Core.Constants.LongDateFormat);
      dateTime1 = this.EndDateTime;
      string str2 = dateTime1.ToString(Sitecore.WFFM.Abstractions.Constants.Core.Constants.LongDateFormat);
      this.ErrorMessage = string.Format(errorMessage, (object) "{0}", (object) str1, (object) str2);
      string text = this.Text;
      DateTime dateTime2 = this.StartDateTime;
      string str3 = dateTime2.ToString(Sitecore.WFFM.Abstractions.Constants.Core.Constants.LongDateFormat);
      dateTime2 = this.EndDateTime;
      string str4 = dateTime2.ToString(Sitecore.WFFM.Abstractions.Constants.Core.Constants.LongDateFormat);
      this.Text = string.Format(text, (object) "{0}", (object) str3, (object) str4);
      base.OnLoad(e);
    }

    private void OnDateValidate(object source, ServerValidateEventArgs args)
    {
      DateTime date = DateUtil.IsoDateToDateTime(args.Value).Date;
      if (this.StartDateTime <= date && date <= this.EndDateTime)
        args.IsValid = true;
      else
        args.IsValid = false;
    }
  }
}
