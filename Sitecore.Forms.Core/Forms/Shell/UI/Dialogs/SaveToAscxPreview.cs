// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.SaveToAscxPreview
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.Pipelines;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Pipeline;
using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class SaveToAscxPreview : DialogForm
  {
    protected Scrollbox Preview;
    protected XmlControl Dialog;
    protected Toolbutton DownloadToolbutton;

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (Context.ClientPage.IsEvent)
        return;
      (this.Cancel).Visible = false;
      (this.Preview).Controls.Add(new Sitecore.Web.UI.HtmlControls.Literal(Sitecore.Form.Core.Utility.HtmlUtil.HighlighteCode(this.FileContent)));
      (this.Preview).Controls.Add(new Sitecore.Web.UI.HtmlControls.Literal("<input ID=\"FileContent\" Value=\"" + HttpUtility.HtmlEncode(this.FileContent) + "\" Type=\"hidden\" />"));
      this.Localize();
    }

    protected virtual void Localize()
    {
      this.Dialog["Header"] = (object) DependenciesManager.ResourceManager.Localize("EXPORT_TO_ASCX");
      this.Dialog["Text"] = (object) DependenciesManager.ResourceManager.Localize("EXPORT_TO_ASCX_FILE");
      ((HeaderedItemsControl) this.DownloadToolbutton).Header = DependenciesManager.ResourceManager.Localize("DOWNLOAD");
      ((WebControl) this.DownloadToolbutton).ToolTip = DependenciesManager.ResourceManager.Localize("DOWNLOAD_SELECTED_FILE");
    }

    [HandleMessage("dialog:download")]
    private void StartDownload(Message message)
    {
      string tempFile = this.CreateTempFile();
      CorePipeline.Run("exportToAscx", (PipelineArgs) new ExportArgs(Context.ClientPage.ClientRequest.Form["FileContent"], "text/xml", tempFile));
      Context.ClientPage.Dispatch("forms:export:completed(filename=export.ascx,contentType=text/xml,tempfile=" + tempFile + ")");
    }

    private string CreateTempFile()
    {
      string path;
      do
      {
        path = Path.Combine(MainUtil.MapPath(Settings.TempFolderPath), Path.GetRandomFileName());
      }
      while (File.Exists(path));
      return path;
    }

    private string FileContent => Sitecore.Web.WebUtil.GetSessionValue("filecontent") is string sessionValue ? sessionValue : string.Empty;
  }
}
