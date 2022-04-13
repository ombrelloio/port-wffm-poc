// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.CustomizeAnalyticsWizard
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Forms.Shell.UI.Controls;
using Sitecore.Web;
using Sitecore.Web.UI;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sitecore.Forms.Shell.UI
{
  public class CustomizeAnalyticsWizard : WizardForm
  {
    protected DataContext GoalsDataContext;
    protected TreePickerEx Goals;
    protected Literal ChoicesLiteral;
    protected WizardDialogBaseXmlControl AnalyticsPage;
    protected Checkbox EnableFormDropoutTracking;
    protected Groupbox AnalyticsOptions;
    protected Groupbox DropoutOptions;
    protected Literal EnableFormDropoutTrackingLiteral;
    protected Literal EnableDropoutSavedToLiteral;
    protected Literal SlectExistingGoalLiteral;
    protected Literal SelectGoalDescriptionLiteral;
    protected WizardDialogBaseXmlControl ConfirmationPage;
    private readonly IResourceManager resourceManager;
    private ICustomizeAnalyticsWizardPage[] analyticsPages;

    public CustomizeAnalyticsWizard()
      : this(DependenciesManager.ResourceManager, DependenciesManager.Resolve<ICustomizeAnalyticsWizardPagesProvider>())
    {
    }

    public CustomizeAnalyticsWizard(
      IResourceManager resourceManager,
      ICustomizeAnalyticsWizardPagesProvider pagesProvider)
    {
      Assert.ArgumentNotNull((object) resourceManager, nameof (resourceManager));
      Assert.ArgumentNotNull((object) pagesProvider, nameof (pagesProvider));
      this.resourceManager = resourceManager;
      this.analyticsPages = pagesProvider.GetPages().ToArray<ICustomizeAnalyticsWizardPage>();
    }

    public ICustomizeAnalyticsWizardPage ActiveAnalyticPage { get; private set; }

    public string Tracking
    {
      get => StringUtil.GetString(((BaseForm) this).ServerProperties[nameof (Tracking)]);
      set => ((BaseForm) this).ServerProperties[nameof (Tracking)] = (object) value;
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.SetActivePage();
      if (Context.ClientPage.IsEvent)
        return;
      UrlHandle urlHandle = UrlHandle.Get();
      Database database = Factory.GetDatabase(((WebControl) this.GoalsDataContext).Database);
      Sitecore.Form.Core.Data.Tracking tracking = new Sitecore.Form.Core.Data.Tracking(urlHandle["tracking"], database);
      this.EnableFormDropoutTracking.Checked = tracking.IsDropoutTrackingEnabled;
      ((Control) this.AnalyticsOptions).Visible = true;
      ((Control) this.DropoutOptions).Visible = true;
      Item goal = tracking.Goal;
      ((Control) this.Goals).Value = goal != null ? ((object) goal.ID).ToString() : string.Empty;
      this.Tracking = urlHandle["tracking"];
      this.Localize();
    }

    protected virtual void Localize()
    {
      (this.AnalyticsPage)["Header"] = (object) this.resourceManager.Localize("ANALYTICS");
      (this.AnalyticsPage)["Text"] = (object) this.resourceManager.Localize("CHOOSE_WHICH_ANALYTICS_OPTIONS_WILL_BE_USED");
      ((HeaderedItemsControl) this.AnalyticsOptions).Header = this.resourceManager.Localize("GOAL");
      this.SlectExistingGoalLiteral.Text = this.resourceManager.Localize("SELECT_EXISTING_GOAL_COLON");
      this.SelectGoalDescriptionLiteral.Text = this.resourceManager.Localize("SELECT_EXISTING_GOAL_TO_ARCHIVE");
      ((HeaderedItemsControl) this.DropoutOptions).Header = this.resourceManager.Localize("DROPOUT_TRACKING");
      this.EnableFormDropoutTracking.Header = this.resourceManager.Localize("ENABLE_FORM_DROPOUT_TRACKING");
      this.EnableFormDropoutTrackingLiteral.Text = this.resourceManager.Localize("SELECT_IT_TO_TRACK_INFORMATION_ENTERED_IN_FORM");
      this.EnableDropoutSavedToLiteral.Text = this.resourceManager.Localize("IF_ENABLED_ANY_DATA_ENTERED_IS_SAVED_IN_ANALYTICS");
      (this.ConfirmationPage)["Header"] = (object) this.resourceManager.Localize("CONFIRMATION");
      (this.ConfirmationPage)["Text"] = (object) this.resourceManager.Localize("CONFIRM_CONFIGURATION_OF_FORM");
      this.ChoicesLiteral.Text = this.resourceManager.Localize("YOU_HAVE_SELECTED_THE_FOLLOWING_SETTINGS");
    }

    protected override bool ActivePageChanging(string page, ref string newpage)
    {
      Assert.ArgumentNotNull((object) page, nameof (page));
      Assert.ArgumentNotNull((object) newpage, nameof (newpage));
      bool flag = base.ActivePageChanging(page, ref newpage);
      if (page == "AnalyticsPage" && newpage == "ConfirmationPage")
      {
        Item obj = StaticSettings.ContextDatabase.GetItem(((Control) this.Goals).Value);
        if (obj == null || obj.TemplateName != "Page Event" && obj.TemplateName != "Goal")
        {
          Context.ClientPage.ClientResponse.Alert(this.resourceManager.Localize("CHOOSE_GOAL"));
          newpage = "AnalyticsPage";
          return flag;
        }
      }
      if (newpage == "ConfirmationPage")
        this.ChoicesLiteral.Text = this.RenderSetting();
      return flag;
    }

    protected override void ActivePageChanged(string page, string oldPage)
    {
      base.ActivePageChanged(page, oldPage);
      this.SetActivePage();
    }

    protected virtual string GenerateAnalytics()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<p>");
      stringBuilder.Append("<table>");
      stringBuilder.Append("<tr><td class='scwfmOptionName'>");
      stringBuilder.Append(this.resourceManager.Localize("ASOCIATED_GOAL"));
      stringBuilder.Append("</td><td class='scwfmOptionValue'>");
      stringBuilder.AppendFormat(": {0}", (object) StaticSettings.ContextDatabase.GetItem(((Control) this.Goals).Value).Name);
      stringBuilder.Append("</td></tr>");
      stringBuilder.Append("<tr><td class='scwfmOptionName'>");
      stringBuilder.Append(this.resourceManager.Localize("FORM_DROPOUT_TRACKING"));
      stringBuilder.Append("</td><td class='scwfmOptionValue'>");
      stringBuilder.AppendFormat(": {0}", this.EnableFormDropoutTracking.Checked ? (object) "Enabled" : (object) "Disabled");
      stringBuilder.Append("</td></tr>");
      stringBuilder.Append("</table>");
      stringBuilder.Append("</p>");
      return stringBuilder.ToString();
    }

    protected virtual string GenerateFutherInfo()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<p>");
      stringBuilder.Append(this.resourceManager.Localize("MARKETING_INFO"));
      stringBuilder.Append("</p>");
      if (this.EnableFormDropoutTracking.Checked)
      {
        stringBuilder.Append("<p>");
        stringBuilder.Append(this.resourceManager.Localize("DROPOUT_INFO"));
        stringBuilder.Append("</p>");
      }
      return stringBuilder.ToString();
    }

    protected virtual string RenderSetting()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.RenderBeginSection("ANALYTICS"));
      stringBuilder.Append(this.GenerateAnalytics());
      stringBuilder.Append(this.RenderEndSection());
      string futherInfo = this.GenerateFutherInfo();
      if (futherInfo.Length > 0)
      {
        stringBuilder.Append(this.RenderBeginSection("FURTHER_INFORMATION"));
        stringBuilder.Append(futherInfo);
        stringBuilder.Append(this.RenderEndSection());
      }
      return stringBuilder.ToString();
    }

    protected string RenderEndSection() => "</fieldset>";

    protected string RenderBeginSection(string name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<fieldset class='scfGroupSection' >");
      stringBuilder.Append("<legend>");
      stringBuilder.Append(this.resourceManager.Localize(name));
      stringBuilder.Append("</legend>");
      return stringBuilder.ToString();
    }

    protected override void EndWizard()
    {
      base.EndWizard();
      if (this.ActiveAnalyticPage == null || this.ActiveAnalyticPage != ((IEnumerable<ICustomizeAnalyticsWizardPage>) this.analyticsPages).Last<ICustomizeAnalyticsWizardPage>())
        return;
      this.ActiveAnalyticPage.Close(this.GetSettings());
    }

    protected virtual CustomizeAnalyticsWizardPageSettings GetSettings() => new CustomizeAnalyticsWizardPageSettings(this.Tracking, this.EnableFormDropoutTracking.Checked, Guid.Parse(((Control) this.Goals).Value), ((WebControl) this.GoalsDataContext).Database);

    private void SetActivePage()
    {
      if (string.IsNullOrEmpty(this.Active))
        return;
      this.ActiveAnalyticPage = ((IEnumerable<ICustomizeAnalyticsWizardPage>) this.analyticsPages).Single<ICustomizeAnalyticsWizardPage>((Func<ICustomizeAnalyticsWizardPage, bool>) (p => p.Name == this.Active));
    }
  }
}
