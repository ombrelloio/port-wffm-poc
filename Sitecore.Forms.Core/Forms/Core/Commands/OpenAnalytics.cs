// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Commands.OpenAnalytics
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;
using Sitecore.WFFM.Abstractions;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Specialized;

namespace Sitecore.Forms.Core.Commands
{
  [Required("IsXdbEnabled", true)]
  [Required("IsXdbTrackerEnabled", true)]
  [Serializable]
  public class OpenAnalytics : Command
  {
    private readonly IRequirementsChecker _requirementsChecker;

    public OpenAnalytics()
      : this(DependenciesManager.RequirementsChecker)
    {
    }

    public OpenAnalytics(IRequirementsChecker requirementsChecker) => this._requirementsChecker = requirementsChecker;

    public override void Execute(CommandContext context)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      Error.AssertObject((object) context, nameof (context));
      if (context.Items.Length != 1)
        return;
      Item obj = context.Items[0];
      Context.ClientPage.Start((object) this, "Run", new NameValueCollection()
      {
        ["id"] = ((object) obj.ID).ToString(),
        ["db"] = obj.Database.Name
      });
    }

    public override CommandState QueryState(CommandContext context)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      return !this._requirementsChecker.CheckRequirements(((object) this).GetType()) ? (CommandState) 2 : base.QueryState(context);
    }

    protected void Run(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args.Parameters["id"], "id");
      Assert.ArgumentNotNull((object) args.Parameters["db"], "db");
      Item obj = Database.GetItem(new ItemUri(ID.Parse(args.Parameters["id"]), Language.Current, Sitecore.Data.Version.Latest, args.Parameters["db"]));
      if (!SheerResponse.CheckModified() || obj == null)
        return;
      if (args.IsPostBack)
      {
        if (!args.HasResult)
          return;
        obj.Editing.BeginEdit();
        ((BaseItem) obj).Fields["__Tracking"].Value = args.Result;
        obj.Editing.EndEdit();
      }
      else
      {
        UrlString urlString = new UrlString(UIUtil.GetUri("control:Forms.CustomizeAnalyticsWizard"));
        new UrlHandle()
        {
          ["tracking"] = ((BaseItem) obj).Fields["__Tracking"].Value
        }.Add(urlString);
        Context.ClientPage.ClientResponse.ShowModalDialog(((object) urlString).ToString(), true);
        args.WaitForPostBack();
      }
    }
  }
}
