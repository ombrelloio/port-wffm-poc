// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipeline.InsertRenderings.Processors.GetDialogUrl
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Pipelines;
using Sitecore.Pipelines.GetRenderingDatasource;
using Sitecore.Text;

namespace Sitecore.Form.Core.Pipeline.InsertRenderings.Processors
{
  public class GetDialogUrl
  {
    public void Process(GetRenderingDatasourceArgs args)
    {
      Assert.IsNotNull((object) args, nameof (args));
      if (args.ContextItemPath == null || !(args.RenderingItem.ID==IDs.FormInterpreterID) && !(args.RenderingItem.ID==IDs.FormMvcInterpreterID))
        return;
      Assert.IsNotNull((object) args.ContentDatabase, "args.ContentDatabase");
      Assert.IsNotNull((object) args.ContextItemPath, "args.ContextItemPath");
      Item obj1 = args.ContentDatabase.GetItem(args.ContextItemPath, Context.Language);
      Assert.IsNotNull((object) obj1, "currentItem");
      object obj2 = Context.ClientData.GetValue(StaticSettings.PrefixId + StaticSettings.PlaceholderKeyId);
      string str1 = obj2 != null ? obj2.ToString() : string.Empty;
      string designMode = StaticSettings.DesignMode;
      if (((BaseItem) obj1).Fields["__renderings"] == null || !(((BaseItem) obj1).Fields["__renderings"].Value != string.Empty))
        return;
      UrlString urlString = new UrlString(UIUtil.GetUri("control:Forms.InsertFormWizard"));
      urlString.Add("id", ((object) obj1.ID).ToString());
      urlString.Add("db", obj1.Database.Name);
      urlString.Add("la", obj1.Language.Name);
      urlString.Add("vs", obj1.Version.Number.ToString());
      urlString.Add("pe", "1");
      if (!string.IsNullOrEmpty(str1))
        urlString.Add("placeholder", str1);
      if (!string.IsNullOrEmpty(designMode))
        urlString.Add("mode", designMode);
      args.DialogUrl = ((object) urlString).ToString();
      if (string.IsNullOrEmpty(args.CurrentDatasource))
      {
        string str2 = ((BaseItem) args.RenderingItem)["data source"];
        if (!string.IsNullOrEmpty(str2))
          args.CurrentDatasource = str2;
      }
      ((PipelineArgs) args).AbortPipeline();
    }
  }
}
