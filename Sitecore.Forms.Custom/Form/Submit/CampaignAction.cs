// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Submit.CampaignAction
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Form.Core.Configuration;
using Sitecore.Forms.Core.Data;
using Sitecore.Marketing.Definitions.Campaigns;
using Sitecore.WFFM.Abstractions;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Actions.Base;

namespace Sitecore.Form.Submit
{
  [Required("IsXdbTrackerEnabled", true)]
  public class CampaignAction : WffmSaveAction
  {
    public override void Execute(
      ID formId,
      AdaptedResultList adaptedFields,
      ActionCallContext actionCallContext = null,
      params object[] data)
    {
      Item innerItem = StaticSettings.ContextDatabase.GetItem(formId);
      if (innerItem == null)
        return;
      FormItem formItem = new FormItem(innerItem);
      if (formItem.Tracking.Ignore)
        return;
      bool flag = false;
      foreach (ICampaignActivityDefinition campaign in formItem.Tracking.Campaigns)
      {
        DependenciesManager.AnalyticsTracker.TriggerCampaign(campaign);
        flag = true;
      }
      if (flag)
        return;
      DependenciesManager.Logger.Warn("The Register a Campaign action: no associated campaigns found", (object) this);
    }

    public override ActionState QueryState(ActionQueryContext queryContext) => queryContext.Tracking != null && DependenciesManager.RequirementsChecker.CheckRequirements(this.GetType()) && !queryContext.Tracking.Ignore || queryContext.Tracking == null && queryContext.Form.IsAnalyticsEnabled ? ActionState.SingleCall : ActionState.Hidden;
  }
}
