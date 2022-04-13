// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Commands.OpenForm
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using Sitecore.Web.UI.XamlSharp.Continuations;
using System;

namespace Sitecore.Forms.Core.Commands
{
  [Serializable]
  public class OpenForm : Command, ISupportsContinuation
  {
    public override void Execute(CommandContext context)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      ContinuationManager.Current.Start((ISupportsContinuation) this, "Run");
    }

    protected void Run(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (args.IsPostBack)
      {
        if (!args.HasResult)
          return;
        ItemUri itemUri = ItemUri.Parse(args.Result);
        Client.AjaxScriptManager.Dispatch(string.Format("forms:openuri(id={0},db={1},la={2},vs={3})", (object) itemUri.ItemID, (object) itemUri.DatabaseName, (object) itemUri.Language.Name, (object) itemUri.Version.Number));
      }
      else
      {
        SheerResponse.ShowModalDialog(((object) new UrlString(UIUtil.GetUri("control:Forms.SelectForm"))).ToString(), true);
        args.WaitForPostBack();
      }
    }
  }
}
