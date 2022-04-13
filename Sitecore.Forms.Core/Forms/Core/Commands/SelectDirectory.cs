// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Commands.SelectDirectory
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Shell.Applications.Dialogs.ItemLister;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Specialized;
using System.Text;

namespace Sitecore.Forms.Core.Commands
{
  [Serializable]
  public class SelectDirectory : Command
  {
    public override void Execute(CommandContext context)
    {
      Assert.ArgumentNotNull((object) context.Parameters["value"], "value");
      Assert.ArgumentNotNullOrEmpty(context.Parameters["target"], "target");
      Context.ClientPage.Start((object) this, "Run", new NameValueCollection()
      {
        ["target"] = context.Parameters["target"],
        ["value"] = context.Parameters["value"]
      });
    }

    protected void Run(ClientPipelineArgs args)
    {
      if (args.IsPostBack)
      {
        if (!args.HasResult)
          return;
        Item obj = StaticSettings.ContextDatabase.SelectSingleItem(args.Result);
        if (obj == null)
          return;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Sitecore.PropertiesBuilder.setValue($('");
        stringBuilder.Append(args.Parameters["target"]);
        stringBuilder.Append("'), '");
        stringBuilder.Append(obj.Paths.FullPath);
        stringBuilder.Append("');");
        SheerResponse.Eval(stringBuilder.ToString());
      }
      else
      {
        SheerResponse.ShowModalDialog(((object) new SelectItemOptions()
        {
          Root = StaticSettings.ContextDatabase.GetItem((ID) ItemIDs.RootID),
          Icon = "Applications/32x32/folder_cubes.png",
          SelectedItem = StaticSettings.ContextDatabase.SelectSingleItem(args.Parameters["value"]),
          Title = DependenciesManager.ResourceManager.Localize("SELECT_ITEM_TITLE"),
          Text = DependenciesManager.ResourceManager.Localize("SELECT_ITEM"),
          ButtonText = DependenciesManager.ResourceManager.Localize("SELECT")
        }.ToUrlString()).ToString(), true);
        args.WaitForPostBack();
      }
    }
  }
}
