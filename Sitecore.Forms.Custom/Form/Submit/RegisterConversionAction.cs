// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Submit.RegisterConversionAction
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Data;
using Sitecore.Form.Core.Utility;
using Sitecore.Marketing.Definitions;
using Sitecore.Marketing.Definitions.Goals;
using Sitecore.WFFM.Abstractions;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Actions.Base;

namespace Sitecore.Form.Submit
{
  [Required("IsXdbTrackerEnabled", true)]
  public class RegisterConversionAction : WffmSaveAction
  {
    public string RegisterMode { get; set; }

    public string Goal { get; set; }

    public override void Execute(
      ID formId,
      AdaptedResultList adaptedFields,
      ActionCallContext actionCallContext = null,
      params object[] data)
    {
      ID id;
      if (string.IsNullOrEmpty(this.Goal) || !adaptedFields.IsTrueStatement(this.RegisterMode) || !ID.TryParse(this.Goal, out id))
        return;
      IGoalDefinition pageGoal = DependenciesManager.AnalyticsTracker.GetPageGoal(id.Guid);
      if (pageGoal == null)
        return;
      DependenciesManager.AnalyticsTracker.TriggerGoal(formId.Guid, ((IDefinition) pageGoal).Id, ItemUtil.GetFieldDisplayName(((object) id).ToString()));
    }

    public override ActionState QueryState(ActionQueryContext queryContext) => DependenciesManager.RequirementsChecker.CheckRequirements(this.GetType()) && queryContext.Tracking != null ? ActionState.Enabled : ActionState.Hidden;
  }
}
