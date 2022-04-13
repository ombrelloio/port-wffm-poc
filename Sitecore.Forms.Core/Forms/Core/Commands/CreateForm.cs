// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Commands.CreateForm
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web.UI.Sheer;
using System;

namespace Sitecore.Forms.Core.Commands
{
  [Serializable]
  public class CreateForm : Command
  {
    public override void Execute(CommandContext context)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      UrlString urlString = new UrlString(UIUtil.GetUri("control:Forms.CreateFormWizard"));
      if (!string.IsNullOrEmpty(context.Parameters["root"]))
        urlString.Add("root", context.Parameters["root"]);
      if (!string.IsNullOrEmpty(context.Parameters["db"]))
        urlString.Add("db", context.Parameters["db"]);
      Context.ClientPage.Start((object) this, "Run", new ClientPipelineArgs()
      {
        Properties = {
          ["url"] = (object) ((object) urlString).ToString()
        }
      });
    }

    protected void Run(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (!args.IsPostBack)
      {
        SheerResponse.ShowModalDialog((string) args.Properties["url"], true);
        args.WaitForPostBack();
      }
      else
      {
        if (!args.HasResult)
          return;
        CommandManager.GetCommand("forms:designer").Execute(new CommandContext(Database.GetItem(ItemUri.Parse(args.Result))));
      }
    }
  }
}
