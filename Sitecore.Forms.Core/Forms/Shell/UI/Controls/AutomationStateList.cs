// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.AutomationStateList
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Newtonsoft.Json;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.Marketing.Core;
using Sitecore.Marketing.Definitions;
using Sitecore.Marketing.Definitions.AutomationPlans.Model;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public class AutomationStateList : XmlControl
  {
    protected Scrollbox Container;

    public AutomationStateList() => (this).ID = "StateList";

    public string Value => WebUtil.GetFormValue((this).ID + "_hidden");

    public override void RenderControl(HtmlTextWriter writer)
    {
      writer.Write("<input id='" + (this).ID + "_hidden' type='hidden'/>");
      base.RenderControl(writer);
    }

    protected override void OnInit(EventArgs e)
    {
      // ISSUE: explicit non-virtual call
      base.OnInit(e);
      WebUtil.GetFormValue("lv-search");
    }

    protected override void OnLoad(EventArgs e)
    {
      // ISSUE: explicit non-virtual call
      base.OnLoad(e);
      if ((this.Container).ID == null)
        (this.Container).ID = "Container";
      string str1 = "$sc(document).ready(function(){ $sc('#" + (this.Container).ID + "').listview(" + this.GetOptions() + ");});";
      (this).Page.ClientScript.RegisterStartupScript((this).Page.GetType(), str1, str1, true);
      if (string.IsNullOrEmpty((this).ID))
        return;
      string str2 = "$sc(document).ready(function(){$sc('#" + (this.Container).ID + "').bind('listview:change', function(e, v){ $sc('#" + (this).ID + "_hidden').val(v)});});";
      (this).Page.ClientScript.RegisterStartupScript((this).Page.GetType(), str2, str2, true);
    }

    private string GetOptions()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("{ 'watemark' : ");
      stringBuilder.AppendFormat("'{0}',", (object) DependenciesManager.ResourceManager.Localize("ENTER_SEARCH_CRITERIA"));
      stringBuilder.AppendFormat("'data' : {0},", (object) JsonConvert.SerializeObject(this.GetModel((string) null)));
      stringBuilder.AppendFormat("'loading' : '{0}',", (object) DependenciesManager.ResourceManager.Localize("LOADING"));
      stringBuilder.AppendFormat("'nodata' : '{0}'", (object) DependenciesManager.ResourceManager.Localize("THERE_ARE_NO_PLANS"));
      stringBuilder.Append("}");
      return stringBuilder.ToString();
    }

    private object GetModel(string search)
    {
      List<AutomationStateList.Automation> automationList = new List<AutomationStateList.Automation>();
      this.Filter(search);
      foreach (IAutomationPlanDefinition iautomationPlanDefinition in (IEnumerable<IAutomationPlanDefinition>) this.Filter(search).OrderBy<IAutomationPlanDefinition, string>((Func<IAutomationPlanDefinition, string>) (i => ((IDefinition) i).Name)))
      {
        AutomationStateList.Automation automation = new AutomationStateList.Automation()
        {
          Name = ((IDefinition) iautomationPlanDefinition).Name
        };
        List<AutomationStateList.State> stateList = new List<AutomationStateList.State>();
        stateList.Add(new AutomationStateList.State()
        {
          Id = ((IDefinition) iautomationPlanDefinition).Id,
          Name = ((IDefinition) iautomationPlanDefinition).Name
        });
        IEnumerable<IAutomationActivityDefinition> activities = iautomationPlanDefinition.GetActivities();
        if (activities != null)
        {
          IItemRepository itemRepository = DependenciesManager.Resolve<IItemRepository>();
          foreach (IAutomationActivityDefinition activityDefinition in activities)
          {
            string str = ((BaseItem) itemRepository.GetItem(new ID(((IAutomationActivityCommonDefinition) activityDefinition).ActivityTypeId)))?.Fields["Name"]?.Value;
            stateList.Add(new AutomationStateList.State()
            {
              Id = ((IAutomationActivityCommonDefinition) activityDefinition).Id,
              Name = string.IsNullOrEmpty(str) ? ((IAutomationActivityCommonDefinition) activityDefinition).Id.ToString() : str
            });
          }
        }
        automation.States = stateList.ToArray();
        automationList.Add(automation);
      }
      return (object) automationList;
    }

    private IEnumerable<IAutomationPlanDefinition> Filter(
      string search)
    {
      List<IAutomationPlanDefinition> automationPlans = this.GetAutomationPlans();
      if (string.IsNullOrEmpty(search))
        return (IEnumerable<IAutomationPlanDefinition>) automationPlans;
      search = search.ToLower();
      return ((IEnumerable<IAutomationPlanDefinition>) automationPlans).Where<IAutomationPlanDefinition>((Func<IAutomationPlanDefinition, bool>) (plan => ((IDefinition) plan).Name.Contains(search) || ((IDefinition) plan).Alias.Contains(search)));
    }

    private List<IAutomationPlanDefinition> GetAutomationPlans()
    {
      IItemRepository itemRepository = DependenciesManager.Resolve<IItemRepository>();
      ResultSet<DefinitionResult<IAutomationPlanDefinition>> all = DefinitionManagerExtensions.GetAll<IAutomationPlanDefinition>(ServiceProviderExtensions.GetDefinitionManagerFactory(ServiceLocator.ServiceProvider).GetDefinitionManager<IAutomationPlanDefinition>(), CultureInfo.InvariantCulture, false);
      List<IAutomationPlanDefinition> iautomationPlanDefinitionList = new List<IAutomationPlanDefinition>();
      foreach (DefinitionResult<IAutomationPlanDefinition> definitionResult in all)
      {
        IAutomationPlanDefinition data = definitionResult.Data;
        Item obj = itemRepository.GetItem(new ID(((IDefinition) data).Id));
        ((IDefinition) data).Name = ((BaseItem) obj)?.Fields["Name"]?.Value;
        iautomationPlanDefinitionList.Add(data);
      }
      return iautomationPlanDefinitionList;
    }

    private class Automation
    {
      [JsonProperty("n")]
      public string Name { get; set; }

      [JsonProperty("s")]
      public AutomationStateList.State[] States { get; set; }
    }

    private class State
    {
      [JsonProperty("id")]
      public Guid Id { get; set; }

      [JsonProperty("n")]
      public string Name { get; set; }
    }
  }
}
