// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Web.ExportPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Utility;
using Sitecore.IO;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Web;
using System.Web.UI;

namespace Sitecore.Form.Core.Web
{
  public class ExportPage : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (DependenciesManager.AnalyticsTracker.Current != null && DependenciesManager.AnalyticsTracker.Current.CurrentPage != null)
        DependenciesManager.AnalyticsTracker.Current.CurrentPage.Cancel();
      string str = this.Session["exportcontent"] as string;
      string filename = this.Session["filename"] as string;
      if (string.IsNullOrEmpty(filename) || string.IsNullOrEmpty(str))
        return;
      string tempFileName = WebUtil.GetTempFileName();
      FileUtil.WriteToFile(tempFileName, str);
      long fileSize = FileUtil.GetFileSize(tempFileName);
      this.WriteCacheHeaders(filename, fileSize);
      this.Response.TransmitFile(tempFileName, 0L, fileSize);
      this.Response.End();
    }

    private void WriteCacheHeaders(string filename, long length)
    {
      HttpResponse response = HttpContext.Current.Response;
      response.ClearHeaders();
      response.AddHeader("Content-Type", (string) this.Session["contentType"] ?? "application/octet-stream");
      response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
      response.AddHeader("Content-Length", length.ToString());
      response.Cache.SetNoStore();
    }
  }
}
