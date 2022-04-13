// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.FormDesigner.ExportToAscx
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Pipeline.ParseAscx;
using Sitecore.Form.Core.Renderings;
using Sitecore.Form.Core.SchemaGenerator;
using Sitecore.Form.Core.Utility;
using Sitecore.Globalization;
using Sitecore.Layouts;
using Sitecore.Pipelines;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;
using System;
using System.Collections.Specialized;
using System.Web.UI;

namespace Sitecore.Form.Core.FormDesigner
{
  [Serializable]
  public class ExportToAscx : Command
  {
    public override void Execute(CommandContext context)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      Error.AssertObject((object) context, nameof (context));
      if (context.Items.Length == 0)
        return;
      Item obj = context.Items[0];
      Context.ClientPage.Start((object) this, "Run", new NameValueCollection()
      {
        ["id"] = ((object) obj.ID).ToString(),
        ["la"] = obj.Language.Name,
        ["vs"] = obj.Version.Number.ToString(),
        ["db"] = obj.Database.Name
      });
    }

    protected void Run(ClientPipelineArgs args)
    {
      Item contextItem = Database.GetItem(new ItemUri(ID.Parse(args.Parameters["id"]), Language.Parse(args.Parameters["la"]), Sitecore.Data.Version.Parse(args.Parameters["vs"]), args.Parameters["db"]));
      if (contextItem == null || args.IsPostBack)
        return;
      using (new LanguageSwitcher(contextItem.Language))
      {
        RenderingReference renderingReference = new RenderingReference((RenderingItem)(contextItem.Database.GetItem(IDs.FormInterpreterID)));
        renderingReference.Settings.Parameters = "FormID=" + (object) contextItem.ID;
        FormRender control = (FormRender) renderingReference.RenderingItem.GetControl(renderingReference.Settings);
        control.IsClearDepend = true;
        control.InitControls();
        ParseAscxArgs parseAscxArgs = new ParseAscxArgs(SchemaGeneratorManager.GetSchema(((Control) control).Controls[0], "<%@ Control Language=\"C#\" AutoEventWireup=\"true\" CodeBehind=\"SimpleForm.cs\" Inherits=\"Sitecore.Form.Core.Ascx.Controls.SimpleForm\" %>", ThemesManager.ScriptsTags(SiteUtils.GetFormsRootItemForItem(contextItem), contextItem)));
        CorePipeline.Run("parseAscx", (PipelineArgs) parseAscxArgs);
        Sitecore.Web.WebUtil.SetSessionValue("filecontent", (object) parseAscxArgs.AscxContent);
      }
      SheerResponse.ShowModalDialog(((object) new UrlString(UIUtil.GetUri("control:Forms.SaveToAscx.Preview"))).ToString(), true);
      args.WaitForPostBack();
    }
  }
}
