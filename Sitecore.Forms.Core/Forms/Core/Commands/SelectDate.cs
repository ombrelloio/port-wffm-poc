// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Commands.SelectDate
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Applications.WebEdit.Dialogs.DateSelector;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Specialized;

namespace Sitecore.Forms.Core.Commands
{
  [Serializable]
  public class SelectDate : Command
  {
    private readonly IResourceManager resourceManager;

    public SelectDate()
      : this(DependenciesManager.ResourceManager)
    {
    }

    public SelectDate(IResourceManager resourceManager)
    {
      Assert.IsNotNull((object) resourceManager, "Dependency resourceManager is null");
      this.resourceManager = resourceManager;
    }

    public override void Execute(CommandContext context)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      Context.ClientPage.Start((object) this, "Run", new NameValueCollection()
      {
        ["target"] = context.Parameters["target"],
        ["value"] = context.Parameters["value"]
      });
    }

    protected void Run(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (!args.IsPostBack)
      {
        SheerResponse.ShowModalDialog(new SelectDateOptions()
        {
          Header = this.resourceManager.Localize("SELECT_DATE_TITLE"),
          Text = this.resourceManager.Localize("SELECT_DATE_DESC"),
          SelectedDate = DateUtil.IsoDateToDateTime(args.Parameters["value"])
        }.ToString(), true);
        args.WaitForPostBack();
      }
      else
      {
        if (!args.HasResult)
          return;
        SheerResponse.Eval("Sitecore.PropertiesBuilder.setValue($('" + args.Parameters["target"] + "'), '" + args.Result + "');");
      }
    }
  }
}
