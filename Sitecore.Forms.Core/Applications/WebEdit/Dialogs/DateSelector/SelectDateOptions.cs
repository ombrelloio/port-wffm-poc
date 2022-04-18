// Decompiled with JetBrains decompiler
// Type: Sitecore.Applications.WebEdit.Dialogs.DateSelector.SelectDateOptions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Text;
using System;
using System.Web;

namespace Sitecore.Applications.WebEdit.Dialogs.DateSelector
{
  public class SelectDateOptions
  {
    private UrlString _url = new UrlString("/sitecore/shell/~/xaml/Sitecore.Shell.Applications.WebEdit.Dialogs.DateSelector.aspx");

    public string Header
    {
      get => this._url["h"];
      set => this._url["h"] = value;
    }

    public string Text
    {
      get => this._url["t"];
      set => this._url["t"] = value;
    }

    public DateTime SelectedDate
    {
      get => DateUtil.ToServerTime(DateUtil.IsoDateToDateTime(this._url["da"]));
      set => this._url["da"] = DateUtil.ToIsoDate(value);
    }

    public string SelectedIsoDate
    {
      get => this._url["da"];
      set => this._url["da"] = value;
    }

    public SelectDateOptions()
    {
    }

    private SelectDateOptions(UrlString url) => this._url = url;

    public static SelectDateOptions Parse() => new SelectDateOptions(new UrlString(HttpUtility.UrlDecode(Context.Request.QueryString.ToString())));

    public override string ToString() => this._url.ToString();
  }
}
