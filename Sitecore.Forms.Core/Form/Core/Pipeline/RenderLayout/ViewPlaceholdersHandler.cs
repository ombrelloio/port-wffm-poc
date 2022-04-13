// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipeline.RenderLayout.ViewPlaceholdersHandler
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Data;
using Sitecore.Forms.Shell.UI.Controls;
using Sitecore.Pipelines;
using Sitecore.Pipelines.RenderLayout;
using Sitecore.Publishing;
using Sitecore.Web;
using Sitecore.Web.UI.WebControls;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Sitecore.Form.Core.Pipeline.RenderLayout
{
  public class ViewPlaceholdersHandler : RenderLayoutProcessor
  {
    public override void Process(RenderLayoutArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (!(WebUtil.GetQueryString(PlaceholderManager.placeholderQueryKey, "0") == "1"))
        return;
      PreviewManager.RestoreUser();
      if (Context.Page == null || !PlaceholderManager.IsValid(WebUtil.GetRawUrl()))
        return;
      HttpResponse response = HttpContext.Current.Response;
      response.ClearContent();
      response.ClearHeaders();
      response.ContentType = "text/plain";
      response.Cache.SetCacheability(HttpCacheability.NoCache);
      HtmlTextWriter output = new HtmlTextWriter((TextWriter) new StringWriter());
      if (WebUtil.GetQueryString(PlaceholderManager.onlyNamesPlaceholdersKey, string.Empty) != "1")
      {
        Sitecore.Forms.Shell.UI.Controls.PlaceholderList.RenderPlaceholderPalette(output, WebUtil.GetQueryString(PlaceholderManager.allowedRenderingKey), WebUtil.GetQueryString(PlaceholderManager.selectedPlaceholderKey));
      }
      else
      {
        Dictionary<string, Placeholder> dictionary = new Dictionary<string, Placeholder>();
        Dictionary<string, string> availablePlaceholders = Sitecore.Forms.Shell.UI.Controls.PlaceholderList.GetAvailablePlaceholders(WebUtil.GetQueryString(PlaceholderManager.allowedRenderingKey));
        output.Write(string.Join<KeyValuePair<string, string>>(",", (IEnumerable<KeyValuePair<string, string>>) availablePlaceholders.ToArray<KeyValuePair<string, string>>()));
      }
      response.Write(output.InnerWriter.ToString());
      response.StatusCode = 200;
      response.Flush();
      ((PipelineArgs) args).AbortPipeline();
      response.End();
    }
  }
}
