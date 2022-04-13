// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.EnrollInEngagementPlanPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using ComponentArt.Web.UI;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Forms.Shell.UI.Controls;
using Sitecore.Marketing.Definitions;
using Sitecore.Marketing.Definitions.AutomationPlans.Model;
using Sitecore.Text;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class EnrollInEngagementPlanPage : EditorBase
  {
    protected static string noPlanSpecified = "<a class='ui-link' onclick=\"javascript:scForm.postEvent(this,event,'OnSelectPlan')\" href='#'>" + DependenciesManager.ResourceManager.Localize("NO_ENGAGEMENT_PLAN_SPECIFIED") + "</a>";
    protected Sitecore.Web.UI.HtmlControls.Literal EngagementPlanLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal EnrollVisitorLiteral;
    protected ComboBox ModeCombobox;
    protected Border PlanBorder;
    protected Sitecore.Web.UI.HtmlControls.Literal PlanName;
    protected ControlledChecklist RegisterMode;
    protected HtmlInputButton SelectPlanButton;
    protected HtmlInputHidden SelectedPlanHolder;
    protected Border StateBorder;
    protected Sitecore.Web.UI.HtmlControls.Literal StateLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal StateName;

    protected virtual void BuildUpClientDictionary() => (this).Page.ClientScript.RegisterClientScriptBlock(((object) this).GetType(), "scWfmNeverLabel", Sitecore.StringExtensions.StringExtensions.FormatWith("var sc = new Object();sc.dictionary = [];sc.dictionary['Never'] = \"{0}\";", new object[1]
    {
      (object) DependenciesManager.ResourceManager.Localize("NEVER")
    }), true);

    protected override void Localize()
    {
      base.Localize();
      this.Header = DependenciesManager.ResourceManager.Localize("ENROLL_IN_ENGAGEMENT_PLAN");
      this.Text = DependenciesManager.ResourceManager.Localize("PLACE_VISITOR_IN_SPECIFIC_STATE");
      this.EnrollVisitorLiteral.Text = DependenciesManager.ResourceManager.Localize("ENROLL_VISITOR");
      this.EngagementPlanLiteral.Text = DependenciesManager.ResourceManager.Localize("ENGAGEMENT_PLAN");
      this.StateLiteral.Text = DependenciesManager.ResourceManager.Localize("STATE");
      this.SelectPlanButton.Value = DependenciesManager.ResourceManager.Localize("SELECT");
    }

    protected override void OK_Click()
    {
      if (string.IsNullOrEmpty(this.SelectedPlanHolder.Value))
        SheerResponse.Alert(DependenciesManager.ResourceManager.Localize("SELECT_ENGAGEMENT_PLAN_YOU_WANT_USE"), Array.Empty<string>());
      else
        base.OK_Click();
    }

    protected override void OnInit(EventArgs e)
    {
      this.RegisterMode.AddRange(ConditionalStatementUtil.GetConditionalItems(this.CurrentForm));
      this.RegisterMode.SelectRange(this.GetValueByKey("EnrollWhen", "Always"));
      this.ModeCombobox.Text = this.RegisterMode.SelectedTitle;
      this.SetEngagementPlanValue(this.GetValueByKey("PlanId"));
      base.OnInit(e);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if ((this).Page.IsPostBack)
        return;
      this.BuildUpClientDictionary();
    }

    protected override void SaveValues()
    {
      base.SaveValues();
      this.SetValue("PlanId", this.SelectedPlanHolder.Value);
      this.SetValue("EnrollWhen", string.Join("|", this.RegisterMode.GetManagedSelectedValues().ToArray<string>()));
      Item obj = this.CurrentDatabase.GetItem(this.SelectedPlanHolder.Value);
      if (obj == null)
        return;
      this.SetValue(Sitecore.WFFM.Abstractions.Constants.Core.Constants.ActionSubTitle, ((object) obj.Parent.ID).ToString());
    }

    private void OnSelectPlan()
    {
      ClientPipelineArgs currentArgs = ContinuationManager.Current.CurrentArgs as ClientPipelineArgs;
      if (currentArgs.IsPostBack)
      {
        if (currentArgs.HasResult)
          this.SetEngagementPlanValue(currentArgs.Result);
        SheerResponse.SetOuterHtml((this.PlanBorder).ID, this.PlanBorder);
        SheerResponse.SetOuterHtml((this.StateBorder).ID, this.StateBorder);
        SheerResponse.SetOuterHtml((this.StateLiteral).ID, this.StateLiteral);
      }
      else
      {
        SheerResponse.ShowModalDialog(((object) new UrlString(UIUtil.GetUri("control:Forms.SelectAutomationState"))).ToString(), "400", "400", string.Empty, true);
        currentArgs.WaitForPostBack();
      }
    }

    private void SetEngagementPlanValue(string value)
    {
      string str1 = value;
      string str2 = value;
      IItemRepository itemRepository = DependenciesManager.Resolve<IItemRepository>();
      this.SelectedPlanHolder.Value = value;
      IAutomationPlanDefinition iautomationPlanDefinition = (IAutomationPlanDefinition) null;
      IDefinitionManager<IAutomationPlanDefinition> definitionManager = ServiceProviderExtensions.GetDefinitionManagerFactory(ServiceLocator.ServiceProvider).GetDefinitionManager<IAutomationPlanDefinition>();
      Guid result;
      if (Guid.TryParse(value, out result))
        iautomationPlanDefinition = definitionManager.Get(result, CultureInfo.InvariantCulture);
      if (iautomationPlanDefinition != null)
      {
        str1 = str2 = ((BaseItem) itemRepository.GetItem(new ID(((IDefinition) iautomationPlanDefinition).Id))).Fields["Name"].Value;
      }
      else
      {
        foreach (DefinitionResult<IAutomationPlanDefinition> definitionResult in DefinitionManagerExtensions.GetAll<IAutomationPlanDefinition>(definitionManager, CultureInfo.InvariantCulture, false))
        {
          IAutomationPlanDefinition data = definitionResult.Data;
          IEnumerable<IAutomationActivityDefinition> activities = data.GetActivities();
          if (activities != null)
          {
            foreach (IAutomationActivityDefinition activityDefinition in activities)
            {
              if (((IAutomationActivityCommonDefinition) activityDefinition).Id == result)
              {
                iautomationPlanDefinition = data;
                str1 = ((BaseItem) itemRepository.GetItem(new ID(((IDefinition) iautomationPlanDefinition).Id))).Fields["Name"].Value;
                str2 = ((BaseItem) itemRepository.GetItem(new ID(((IAutomationActivityCommonDefinition) activityDefinition).ActivityTypeId)))?.Fields["Name"]?.Value;
              }
            }
          }
        }
      }
      if (iautomationPlanDefinition == null)
      {
        this.PlanName.Text = EnrollInEngagementPlanPage.noPlanSpecified;
        (this.StateName).Style["display"] = "none";
        (this.StateLiteral).Style["display"] = "none";
      }
      else
      {
        itemRepository.GetItem(new ID(((IDefinition) iautomationPlanDefinition).Id));
        this.PlanName.Text = "<i>" + str1 + "</i>";
        this.StateName.Text = "<i>" + str2 + "</i>";
        (this.StateLiteral).Style["display"] = "block";
        (this.StateName).Style["display"] = "block";
      }
    }
  }
}
