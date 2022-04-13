// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Commands.ConfigureGoal
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Data;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XamlSharp.Continuations;
using Sitecore.WFFM.Abstractions;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;

namespace Sitecore.Forms.Core.Commands
{
  [Required("IsXdbEnabled", true)]
  [Required("IsXdbTrackerEnabled", true)]
  [Serializable]
  public class ConfigureGoal : Command, ISupportsContinuation
  {
    private readonly IRequirementsChecker requirementsChecker;

    public ConfigureGoal()
      : this(DependenciesManager.RequirementsChecker)
    {
    }

    public ConfigureGoal(IRequirementsChecker requirementsChecker)
    {
      Assert.ArgumentNotNull((object) requirementsChecker, nameof (requirementsChecker));
      this.requirementsChecker = requirementsChecker;
    }

    public override void Execute(CommandContext context)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      if (context.Items.Length != 1)
        return;
      Item obj = context.Items[0];
      Item goal = new Tracking(((BaseItem) obj).Fields["__Tracking"].Value, obj.Database).Goal;
      if (goal != null)
      {
        CommandContext commandContext = new CommandContext(new Item[1]
        {
          goal
        });
        CommandManager.GetCommand("item:personalize")?.Execute(commandContext);
      }
      else
        SheerResponse.Alert(DependenciesManager.ResourceManager.Localize("CHOOSE_GOAL_AT_FIRST"), Array.Empty<string>());
    }

    public override CommandState QueryState(CommandContext context)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      return !this.requirementsChecker.CheckRequirements(((object) this).GetType()) ? (CommandState) 2 : base.QueryState(context);
    }
  }
}
