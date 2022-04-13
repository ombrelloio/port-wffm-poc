// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Submit.EnrollInEngagementPlan
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Microsoft.Extensions.DependencyInjection;
using Sitecore.Analytics;
using Sitecore.Data;
using Sitecore.DependencyInjection;
using Sitecore.Marketing.Definitions;
using Sitecore.Marketing.Definitions.AutomationPlans.Model;
using Sitecore.WFFM.Abstractions;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Actions.Base;
using Sitecore.Xdb.MarketingAutomation.Core.Requests;
using Sitecore.Xdb.MarketingAutomation.Core.Results;
using Sitecore.Xdb.MarketingAutomation.OperationsClient;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Sitecore.Form.Submit
{
  [Required("IsXdbTrackerEnabled", true)]
  public class EnrollInEngagementPlan : WffmSaveAction
  {
    public string PlanId { get; set; }

    public string EnrollWhen { get; set; }

    public override void Execute(
      ID formId,
      AdaptedResultList adaptedFields,
      ActionCallContext actionCallContext = null,
      params object[] data)
    {
      if (Tracker.Current == null || Tracker.Current.Session == null || string.IsNullOrEmpty(this.PlanId) || !adaptedFields.IsTrueStatement(this.EnrollWhen))
        return;
      Guid guid = Guid.Parse(this.PlanId);
      IAutomationActivityDefinition activityDefinition1 = (IAutomationActivityDefinition) null;
      IDefinitionManager<IAutomationPlanDefinition> definitionManager = ServiceProviderExtensions.GetDefinitionManagerFactory(ServiceLocator.ServiceProvider).GetDefinitionManager<IAutomationPlanDefinition>();
      IAutomationPlanDefinition iautomationPlanDefinition = definitionManager.Get(guid, CultureInfo.InvariantCulture);
      if (iautomationPlanDefinition == null)
      {
        foreach (DefinitionResult<IAutomationPlanDefinition> definitionResult in DefinitionManagerExtensions.GetAll<IAutomationPlanDefinition>(definitionManager, CultureInfo.InvariantCulture, false))
        {
          IAutomationPlanDefinition data1 = definitionResult.Data;
          IEnumerable<IAutomationActivityDefinition> activities = data1.GetActivities();
          if (activities != null)
          {
            foreach (IAutomationActivityDefinition activityDefinition2 in activities)
            {
              if (((IAutomationActivityCommonDefinition) activityDefinition2).Id == guid)
              {
                iautomationPlanDefinition = data1;
                activityDefinition1 = activityDefinition2;
              }
            }
          }
        }
      }
      if (iautomationPlanDefinition == null)
        throw new InvalidOperationException(string.Format("The plan {0} was not found", (object) this.PlanId));
            IAutomationOperationsClient service = ServiceProviderServiceExtensions.GetService<IAutomationOperationsClient>(ServiceLocator.ServiceProvider);
            EnrollmentRequest enrollmentRequest = new EnrollmentRequest(Tracker.Current.Contact.ContactId, guid);
      if (activityDefinition1 != null)
        enrollmentRequest.ActivityId = new Guid?(((IAutomationActivityCommonDefinition) activityDefinition1).Id);
      EnrollmentRequest[] enrollmentRequestArray = new EnrollmentRequest[1]
      {
        enrollmentRequest
      };
      if (!((BatchRequestResult<EnrollmentRequestResult, EnrollmentRequest>) service.EnrollInPlanDirect((IEnumerable<EnrollmentRequest>) enrollmentRequestArray)).Success)
        throw new InvalidOperationException(string.Format(DependenciesManager.ResourceManager.GetString("THE_VISITOR_CANNOT_BE_ENROLL"), (object) guid));
    }

    public override ActionState QueryState(ActionQueryContext queryContext) => queryContext.Tracking != null && DependenciesManager.RequirementsChecker.CheckRequirements(this.GetType()) && !queryContext.Tracking.Ignore || queryContext.Tracking == null && queryContext.Form.IsAnalyticsEnabled ? ActionState.Enabled : ActionState.Hidden;
  }
}
