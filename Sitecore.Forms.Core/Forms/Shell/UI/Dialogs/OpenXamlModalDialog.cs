// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.OpenXamlModalDialog
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using System;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class OpenXamlModalDialog : BaseForm
  {
    protected Frame ApplicationFrame;

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      string queryString1 = WebUtil.GetQueryString("control");
      if (string.IsNullOrEmpty(queryString1))
        return;
      string queryString2 = WebUtil.GetQueryString("type");
      if (!string.IsNullOrEmpty(queryString2) && queryString2 == "xaml")
      {
        this.ApplicationFrame.SourceUri = OpenXamlModalDialog.GetXamlUrl(WebUtil.GetQueryString());
      }
      else
      {
        UrlString urlString = new UrlString(UIUtil.GetUri("control:" + queryString1, WebUtil.GetQueryString()));
        urlString.Remove("control");
        this.ApplicationFrame.SourceUri = ((object) urlString).ToString();
      }
    }

    protected static string GetXamlUrl(string QueryString)
    {
      UrlString urlString = new UrlString(UIUtil.GetUri(string.Format("/sitecore/shell/~/xaml/{0}.aspx", (object) WebUtil.GetQueryString("control")), WebUtil.GetQueryString()));
      urlString.Remove("control");
      urlString.Remove("type");
      return ((object) urlString).ToString();
    }
  }
}
