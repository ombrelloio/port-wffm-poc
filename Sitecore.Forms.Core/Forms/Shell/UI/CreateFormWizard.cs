// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.CreateFormWizard
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using HtmlAgilityPack;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Renderings;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.UI.Controls;
using Sitecore.Forms.Core.Data;
using Sitecore.Forms.Shell.UI.Controls;
using Sitecore.Globalization;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Pages;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XmlControls;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI
{
  public class CreateFormWizard : WizardForm
  {
    protected readonly IAnalyticsSettings AnalyticsSettings;
    protected readonly string multiTreeID = "Forms_MultiTreeView";
    protected readonly string newFormUri = "newFormUriKey";
    protected Edit EbFormName;
    protected Scrollbox ExistingForms;
    protected Frame GlobalForms;
    protected MultiTreeView multiTree;
    protected Edit GoalName;
    protected DataContext GoalsDataContext;
    protected Checkbox OpenNewForm;
    protected TreePickerEx Goals;
    protected WizardDialogBaseXmlControl CreateForm;
    protected Sitecore.Web.UI.HtmlControls.Literal FormNameLiteral;
    protected Radiobutton ChooseForm;
    protected Radiobutton CreateBlankForm;
    protected WizardDialogBaseXmlControl SelectForm;
    protected WizardDialogBaseXmlControl AnalyticsPage;
    protected Checkbox EnableFormDropoutTracking;
    protected Groupbox AnalyticsOptions;
    protected Radiobutton CreateGoal;
    protected Radiobutton SelectGoal;
    protected Sitecore.Web.UI.HtmlControls.Literal SelectGoalLiteral;
    protected Groupbox DropoutOptions;
    protected Sitecore.Web.UI.HtmlControls.Literal EnableFormDropoutTrackingLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal EnableDropoutSavedToLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal GoalNameLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal PointsLiteral;
    protected Edit Points;
    protected WizardDialogBaseXmlControl ConfirmationPage;
    protected Sitecore.Web.UI.HtmlControls.Literal ChoicesLiteral;
    private Item formsRoot;
    private Item goalsRoot;

    public CreateFormWizard() => this.AnalyticsSettings = DependenciesManager.Resolve<IAnalyticsSettings>();

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (!Context.ClientPage.IsEvent)
      {
        (Sitecore.Context.ClientPage).ClientScript.RegisterClientScriptInclude("jquery", "/sitecore modules/web/web forms for marketers/scripts/jquery.js");
        (Sitecore.Context.ClientPage).ClientScript.RegisterClientScriptInclude("jquery-ui.min", "/sitecore modules/web/web forms for marketers/scripts/jquery-ui.min.js");
        (Sitecore.Context.ClientPage).ClientScript.RegisterClientScriptInclude("jquery-ui-i18n", "/sitecore modules/web/web forms for marketers/scripts/jquery-ui-i18n.js");
        (Sitecore.Context.ClientPage).ClientScript.RegisterClientScriptInclude("json2.min", "/sitecore modules/web/web forms for marketers/scripts/json2.min.js");
        (Sitecore.Context.ClientPage).ClientScript.RegisterClientScriptInclude("head.load.min", "/sitecore modules/web/web forms for marketers/scripts/head.load.min.js");
        (Sitecore.Context.ClientPage).ClientScript.RegisterClientScriptInclude("sc.webform", "/sitecore modules/web/web forms for marketers/scripts/sc.webform.js?v=17072012");
        this.Localize();
        this.ChooseForm.Checked = true;
        this.CreateGoal.Checked = true;
        this.EnableFormDropoutTracking.Checked = true;
        (this.AnalyticsOptions).Visible = true;
        (this.DropoutOptions).Visible = true;
        (this.Goals).Value = string.Empty;
        ThemesManager.RegisterCssScript(null, this.FormsRoot, this.FormsRoot);
        (this.EbFormName).Value = this.GetUniqueName("Example Form");
        MultiTreeView multiTreeView = new MultiTreeView();
        multiTreeView.Roots = Sitecore.Form.Core.Utility.Utils.GetFormRoots();
        multiTreeView.Filter = "Contains('{C0A68A37-3C0A-4EEB-8F84-76A7DF7C840E},{A87A00B1-E6DB-45AB-8B54-636FEC3B5523},{FFB1DA32-2764-47DB-83B0-95B843546A7E}', @@templateid)";
        (multiTreeView).ID = this.multiTreeID;
        multiTreeView.DataViewName = "Master";
        multiTreeView.TemplateID = ((object) Sitecore.Form.Core.Configuration.IDs.FormTemplateID).ToString();
        multiTreeView.IsFullPath = true;
        this.multiTree = multiTreeView;
        (this.ExistingForms).Controls.Add(this.multiTree);
      }
      else
        this.multiTree = (this.ExistingForms).FindControl(this.multiTreeID) as MultiTreeView;
    }

    protected virtual void Localize()
    {
      (this.CreateForm)["Header"] = (object) DependenciesManager.ResourceManager.Localize("CREATE_NEW_FORM");
      (this.CreateForm)["Text"] = (object) DependenciesManager.ResourceManager.Localize("CREATE_BLANK_OR_COPY_EXISTING_FORM");
      this.FormNameLiteral.Text = DependenciesManager.ResourceManager.Localize("FORM_TEXT") + ":";
      this.CreateBlankForm.Header = DependenciesManager.ResourceManager.Localize("CREATE_BLANK_FORM");
      this.ChooseForm.Header = DependenciesManager.ResourceManager.Localize("SELECT_FORM_TO_COPY");
      (this.SelectForm)["Header"] = (object) DependenciesManager.ResourceManager.Localize("SELECT_FORM");
      (this.SelectForm)["Text"] = (object) DependenciesManager.ResourceManager.Localize("COPY_EXISTING_FORM");
      (this.AnalyticsPage)["Header"] = (object) DependenciesManager.ResourceManager.Localize("ANALYTICS");
      (this.AnalyticsPage)["Text"] = (object) DependenciesManager.ResourceManager.Localize("CHOOSE_WHICH_ANALYTICS_OPTIONS_WILL_BE_USED");
      ((HeaderedItemsControl) this.AnalyticsOptions).Header = DependenciesManager.ResourceManager.Localize("GOAL");
      this.CreateGoal.Header = DependenciesManager.ResourceManager.Localize("CREATE_NEW_GOAL");
      (this.GoalName).Value = DependenciesManager.ResourceManager.Localize("FORM_NAME_FORM_COMPLETED");
      this.GoalNameLiteral.Text = DependenciesManager.ResourceManager.Localize("NAME") + ":";
      this.PointsLiteral.Text = DependenciesManager.ResourceManager.Localize("ENGAGEMENT_VALUE") + ":";
      this.SelectGoal.Header = DependenciesManager.ResourceManager.Localize("SELECT_EXISTING_GOAL");
      this.SelectGoalLiteral.Text = DependenciesManager.ResourceManager.Localize("SELECT_NEW_OR_EXISTEN_GOAL");
      ((HeaderedItemsControl) this.DropoutOptions).Header = DependenciesManager.ResourceManager.Localize("DROPOUT_TRACKING");
      this.EnableFormDropoutTracking.Header = DependenciesManager.ResourceManager.Localize("ENABLE_FORM_DROPOUT_TRACKING");
      this.EnableFormDropoutTrackingLiteral.Text = DependenciesManager.ResourceManager.Localize("SELECT_IT_TO_TRACK_INFORMATION_ENTERED_IN_FORM");
      this.EnableDropoutSavedToLiteral.Text = DependenciesManager.ResourceManager.Localize("IF_ENABLED_ANY_DATA_ENTERED_IS_SAVED_IN_ANALYTICS");
      (this.ConfirmationPage)["Header"] = (object) DependenciesManager.ResourceManager.Localize("CONFIRMATION");
      (this.ConfirmationPage)["Text"] = (object) DependenciesManager.ResourceManager.Localize("CONFIRM_CONFIGURATION_OF_NEW_FORM");
      this.ChoicesLiteral.Text = DependenciesManager.ResourceManager.Localize("YOU_HAVE_SELECTED_THE_FOLLOWING_SETTINGS");
    }

    protected override void ActivePageChanged(string page, string oldPage)
    {
      base.ActivePageChanged(page, oldPage);
      if (page == "ConfirmationPage")
      {
        (this.NextButton).Visible = true;
        (this.BackButton).Visible = true;
        (this.NextButton).Disabled = false;
        (this.NextButton).Disabled = false;
        ((HeaderedItemsControl) this.CancelButton).Header = "Cancel";
        ((HeaderedItemsControl) this.NextButton).Header = "Create";
      }
      if (oldPage == "ConfirmationPage")
      {
        (this.NextButton).Disabled = false;
        (this.BackButton).Disabled = false;
        ((HeaderedItemsControl) this.CancelButton).Header = "Cancel";
        ((HeaderedItemsControl) this.NextButton).Header = "Next >";
      }
      if (!(oldPage == "CreateForm") || !this.AnalyticsSettings.IsAnalyticsAvailable)
        return;
      string name = Sitecore.StringExtensions.StringExtensions.FormatWith("{0} Form Completed", new object[1]
      {
        (object) (this.EbFormName).Value
      });
      if (this.GoalsDataContext.CurrentItem != null)
      {
        List<Item> objList = new List<Item>((IEnumerable<Item>) this.GoalsDataContext.CurrentItem.Children.ToArray());
        if (((IEnumerable<Item>) objList).Where<Item>((Func<Item, bool>) (s => s.Name == name)).Count<Item>() > 0)
        {
          int i = 1;
          while (((IEnumerable<Item>) objList).FirstOrDefault<Item>((Func<Item, bool>) (s => s.Name == Sitecore.StringExtensions.StringExtensions.FormatWith("{0} {1} Form Completed", new object[2]
          {
            (object) (this.EbFormName).Value,
            (object) i
          }))) != null)
            ++i;
          name = Sitecore.StringExtensions.StringExtensions.FormatWith("{0} {1} Form Completed", new object[2]
          {
            (object) (this.EbFormName).Value,
            (object) i
          });
        }
      }
      (this.GoalName).Value = name;
      SheerResponse.SetOuterHtml((this.GoalName).ID, this.GoalName);
    }

    protected override bool ActivePageChanging(string page, ref string newpage)
    {
      bool flag = base.ActivePageChanging(page, ref newpage);
      if (!this.CheckGoalSettings(page, ref newpage))
        return flag;
      if (!this.AnalyticsSettings.IsAnalyticsAvailable && newpage == "AnalyticsPage")
        newpage = "ConfirmationPage";
      if (page == "CreateForm" && newpage == "SelectForm")
      {
        if ((this.EbFormName).Value == string.Empty)
        {
          Context.ClientPage.ClientResponse.Alert(DependenciesManager.ResourceManager.Localize("EMPTY_FORM_NAME"));
          newpage = "CreateForm";
          return flag;
        }
        if (!Regex.IsMatch((this.EbFormName).Value, Sitecore.Configuration.Settings.ItemNameValidation, RegexOptions.ECMAScript))
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.AppendFormat("'{0}' ", (object) (this.EbFormName).Value);
          stringBuilder.Append(DependenciesManager.ResourceManager.Localize("IS_NOT_VALID_NAME"));
          Context.ClientPage.ClientResponse.Alert(stringBuilder.ToString());
          newpage = "CreateForm";
          return flag;
        }
        if (this.FormsRoot.Database.GetItem(this.FormsRoot.Paths.ContentPath + "/" + (this.EbFormName).Value) != null)
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.AppendFormat("'{0}' ", (object) (this.EbFormName).Value);
          stringBuilder.Append(DependenciesManager.ResourceManager.Localize("IS_NOT_UNIQUE_NAME"));
          Context.ClientPage.ClientResponse.Alert(stringBuilder.ToString());
          newpage = "CreateForm";
          return flag;
        }
        if (this.CreateBlankForm.Checked)
          newpage = this.AnalyticsSettings.IsAnalyticsAvailable ? "AnalyticsPage" : "ConfirmationPage";
      }
      if (page == "SelectForm" && (newpage == "ConfirmationPage" || newpage == "AnalyticsPage"))
      {
        string selected = this.multiTree.Selected;
        Item obj = this.FormsRoot.Database.GetItem(selected);
        if (selected == null || obj == null)
        {
          Context.ClientPage.ClientResponse.Alert(DependenciesManager.ResourceManager.Localize("PLEASE_SELECT_FORM"));
          newpage = "SelectForm";
          return flag;
        }
        if (obj.TemplateID != Sitecore.Form.Core.Configuration.IDs.FormTemplateID)
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.AppendFormat("'{0}' ", (object) obj.Name);
          stringBuilder.Append(DependenciesManager.ResourceManager.Localize("IS_NOT_FORM"));
          Context.ClientPage.ClientResponse.Alert(stringBuilder.ToString());
          newpage = "SelectForm";
          return flag;
        }
      }
      if (page == "ConfirmationPage" && newpage == "ConfirmationPage" && !this.AnalyticsSettings.IsAnalyticsAvailable)
        newpage = this.CreateBlankForm.Checked ? "CreateForm" : "SelectForm";
      if (page == "ConfirmationPage" && (newpage == "SelectForm" || newpage == "AnalyticsPage"))
      {
        ((HeaderedItemsControl) this.CancelButton).Header = "Cancel";
        ((HeaderedItemsControl) this.NextButton).Header = "Next >";
      }
      if ((page == "ConfirmationPage" || page == "AnalyticsPage") && newpage == "SelectForm" && this.CreateBlankForm.Checked)
        newpage = "CreateForm";
      if (newpage == "ConfirmationPage")
        this.ChoicesLiteral.Text = this.RenderSetting();
      return flag;
    }

    protected bool CheckGoalSettings(string page, ref string newpage)
    {
      if (page == "AnalyticsPage" && newpage == "ConfirmationPage")
      {
        if (!this.CreateGoal.Checked)
        {
          Item obj = StaticSettings.ContextDatabase.GetItem((this.Goals).Value);
          if (obj == null || obj.TemplateName != "Page Event" && obj.TemplateName != "Goal")
          {
            Context.ClientPage.ClientResponse.Alert(DependenciesManager.ResourceManager.Localize("CHOOSE_GOAL"));
            newpage = "AnalyticsPage";
            return false;
          }
        }
        else
        {
          if (string.IsNullOrEmpty((this.GoalName).Value))
          {
            Context.ClientPage.ClientResponse.Alert(DependenciesManager.ResourceManager.Localize("ENTER_NAME_FOR_GOAL"));
            newpage = "AnalyticsPage";
            return false;
          }
          if (((IEnumerable<Item>) new List<Item>((IEnumerable<Item>) this.GoalsDataContext.CurrentItem.Children.ToArray())).FirstOrDefault<Item>((Func<Item, bool>) (c => c.Name == (this.GoalName).Value)) != null)
          {
            Context.ClientPage.ClientResponse.Alert(DependenciesManager.ResourceManager.Localize("GOAL_ALREADY_EXISTS", (this.GoalName).Value));
            newpage = "AnalyticsPage";
            return false;
          }
          if (!Sitecore.Data.Items.ItemUtil.IsItemNameValid((this.GoalName).Value))
          {
            Context.ClientPage.ClientResponse.Alert(DependenciesManager.ResourceManager.Localize("GOAL_NAME_IS_NOT_VALID", (this.GoalName).Value));
            newpage = "AnalyticsPage";
            return false;
          }
        }
      }
      return true;
    }

    protected string RenderEndSection() => "</fieldset>";

    protected string RenderBeginSection(string name)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<fieldset class='scfGroupSection' >");
      stringBuilder.Append("<legend>");
      stringBuilder.Append(DependenciesManager.ResourceManager.Localize(name));
      stringBuilder.Append("</legend>");
      return stringBuilder.ToString();
    }

    protected virtual string RenderSetting()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.RenderConfirmationFormSection)
      {
        stringBuilder.Append(this.RenderBeginSection("FORM"));
        stringBuilder.Append(this.GenerateItemSetting());
        stringBuilder.Append(this.RenderEndSection());
      }
      string str = string.Compare(Sitecore.Web.WebUtil.GetQueryString("mode"), StaticSettings.DesignMode, true) != 0 ? this.GeneratePreview() : string.Empty;
      if (this.AnalyticsSettings.IsAnalyticsAvailable)
      {
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
      }
      if (!string.IsNullOrEmpty(str))
      {
        stringBuilder.Append(this.RenderBeginSection("PREVIEW"));
        stringBuilder.Append(str);
        stringBuilder.Append(this.RenderEndSection());
      }
      return stringBuilder.ToString();
    }

    protected virtual string GenerateItemSetting()
    {
      string str = (this.EbFormName).Value;
      return string.Join("", new string[3]
      {
        "<p>",
        string.Format(DependenciesManager.ResourceManager.Localize("FORM_ADDED_IN_MESSAGE"), (object) this.FormsRoot.Paths.FullPath, (object) str),
        "</p>"
      });
    }

    protected virtual string GeneratePreview()
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (!this.ChooseForm.Checked)
        return string.Empty;
      stringBuilder.Append("<p>");
      HtmlTextWriter output = new HtmlTextWriter((TextWriter) new StringWriter());
      this.RenderFormPreview(StaticSettings.GlobalFormsRoot.Database.GetItem(this.CreateBlankForm.Checked ? string.Empty : this.multiTree.Selected), output);
      stringBuilder.Append(output.InnerWriter.ToString());
      stringBuilder.Append("</p>");
      return stringBuilder.ToString();
    }

    protected virtual string GenerateFutherInfo()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<p>");
      stringBuilder.Append(DependenciesManager.ResourceManager.Localize("MARKETING_INFO"));
      stringBuilder.Append("</p>");
      if (this.EnableFormDropoutTracking.Checked)
      {
        stringBuilder.Append("<p>");
        stringBuilder.Append(DependenciesManager.ResourceManager.Localize("DROPOUT_INFO"));
        stringBuilder.Append("</p>");
      }
      return stringBuilder.ToString();
    }

    protected virtual string GenerateAnalytics()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("<p>");
      stringBuilder.Append("<table>");
      stringBuilder.Append("<tr><td class='scwfmOptionName'>");
      stringBuilder.Append(DependenciesManager.ResourceManager.Localize("ASOCIATED_GOAL"));
      stringBuilder.Append("</td><td class='scwfmOptionValue'>");
      string name = (this.GoalName).Value;
      if (!this.CreateGoal.Checked)
        name = this.FormsRoot.Database.GetItem((this.Goals).Value).Name;
      stringBuilder.AppendFormat(": {0}", (object) name);
      stringBuilder.Append("</td></tr>");
      stringBuilder.Append("<tr><td class='scwfmOptionName'>");
      stringBuilder.Append(DependenciesManager.ResourceManager.Localize("FORM_DROPOUT_TRACKING"));
      stringBuilder.Append("</td><td class='scwfmOptionValue'>");
      stringBuilder.AppendFormat(": {0}", this.EnableFormDropoutTracking.Checked ? (object) "Enabled" : (object) "Disabled");
      stringBuilder.Append("</td></tr>");
      stringBuilder.Append("</table>");
      stringBuilder.Append("</p>");
      return stringBuilder.ToString();
    }

    protected override void OnNext(object sender, EventArgs formEventArgs)
    {
      if (((HeaderedItemsControl) this.NextButton).Header == "Create")
      {
        this.SaveForm();
        SheerResponse.SetModified(false);
      }
      this.Next();
    }

    protected virtual void SaveAnalytics(FormItem form, string goalID)
    {
      if (this.AnalyticsSettings.IsAnalyticsAvailable)
      {
        ITracking tracking = form.Tracking;
        tracking.Update(true, this.EnableFormDropoutTracking.Checked);
        Item obj = this.CreateGoal.Checked ? this.GoalsRoot.Add((this.GoalName).Value, new TemplateID(Sitecore.Form.Core.Configuration.IDs.GoalTemplateID)) : this.GoalsRoot.Database.GetItem(goalID);
        obj.Editing.BeginEdit();
        if (this.CreateGoal.Checked)
          ((BaseItem) obj)["Points"] = string.IsNullOrEmpty((this.Points).Value) ? "0" : (this.Points).Value;
        ((BaseItem) obj)["__Workflow state"] = "{EDCBB550-BED3-490F-82B8-7B2F14CCD26E}";
        obj.Editing.EndEdit();
        tracking.AddEvent(obj.ID.Guid);
        form.BeginEdit();
        ((BaseItem) form.InnerItem).Fields["__Tracking"].Value = tracking.ToString();
        form.EndEdit();
      }
      else
      {
        if (((BaseItem) form.InnerItem).Fields["__Tracking"] == null)
          return;
        form.BeginEdit();
        ((BaseItem) form.InnerItem).Fields["__Tracking"].Value = "<tracking ignore=\"1\"/>";
        form.EndEdit();
      }
    }

    protected virtual void SaveForm()
    {
      string goalID = (this.Goals).Value;
      Item formsRoot = this.FormsRoot;
      Assert.IsNotNull((object) formsRoot, "forms root");
      string queryString = Sitecore.Web.WebUtil.GetQueryString("la");
      Language contentLanguage = Context.ContentLanguage;
      if (!string.IsNullOrEmpty(queryString))
        Language.TryParse(Sitecore.Web.WebUtil.GetQueryString("la"), out contentLanguage);
      Item obj1 = this.FormsRoot.Database.GetItem(this.CreateBlankForm.Checked ? string.Empty : this.multiTree.Selected, contentLanguage);
      string str1 = (this.EbFormName).Value;
      string str2 = Sitecore.Data.Items.ItemUtil.ProposeValidItemName(str1);
      Item obj2;
      if (obj1 != null)
      {
        Item obj3 = obj1;
        obj2 = Context.Workflow.CopyItem(obj1, formsRoot, str2, new ID(), true);
        FormItemSynchronizer.UpdateIDReferences((FormItem) obj3, (FormItem) obj2);
      }
      else
      {
        if (formsRoot.Language != contentLanguage)
          formsRoot = this.FormsRoot.Database.GetItem(formsRoot.ID, contentLanguage);
        obj2 = Context.Workflow.AddItem(str2, new TemplateID(Sitecore.Form.Core.Configuration.IDs.FormTemplateID), formsRoot);
        obj2.Editing.BeginEdit();
        ((BaseItem) obj2).Fields[Sitecore.Form.Core.Configuration.FieldIDs.ShowFormTitleID].Value = "1";
        obj2.Editing.EndEdit();
      }
      obj2.Editing.BeginEdit();
      ((BaseItem) obj2)[Sitecore.Form.Core.Configuration.FieldIDs.FormTitleID] = str1;
      ((BaseItem) obj2)[Sitecore.Form.Core.Configuration.FieldIDs.DisplayNameFieldID] = str1;
      obj2.Editing.EndEdit();
      this.SaveAnalytics((FormItem) obj2, goalID);
      ((BaseForm) this).ServerProperties[this.newFormUri] = (object) ((object) obj2.Uri).ToString();
      Registry.SetString("/Current_User/Dialogs//sitecore/shell/default.aspx?xmlcontrol=Forms.FormDesigner", "1250,500");
      SheerResponse.SetDialogValue(((object) obj2.Uri).ToString());
    }

    protected void RenderFormPreview(Item form, HtmlTextWriter output)
    {
      HtmlTextWriter writer = new HtmlTextWriter((TextWriter) new StringWriter());
      FormRender formRender = new FormRender();
      formRender.FormID = form != null ? ((object) form.ID).ToString() : string.Empty;
      formRender.IsFastPreview = true;
      formRender.InitControls();
      (formRender).RenderControl(writer);
      if (writer.InnerWriter.ToString() == string.Empty)
        return;
      string html = writer.InnerWriter.ToString();
      HtmlDocument htmlDocument = new HtmlDocument();
      using (new ThreadCultureSwitcher(Language.Parse("en").CultureInfo))
        htmlDocument.LoadHtml(html);
      this.RemoveScipts(htmlDocument.DocumentNode);
      string str = Regex.Replace(htmlDocument.DocumentNode.InnerHtml, "on\\w*=\".*?\"", string.Empty);
      output.Write(str);
      output.Write("<img height='1px' alt='' src='/sitecore/images/blank.gif' width='1' border='0'onload='javascript:Sitecore.Wfm.Utils.zoom(this.previousSibling)'/>");
    }

    protected string GetUniqueName(string name)
    {
      string str = name;
      int num = 0;
      while (this.FormsRoot.Database.GetItem(this.FormsRoot.Paths.ContentPath + "/" + str) != null)
        str = name + (object) ++num;
      return str;
    }

    [HandleMessage("form:creategoal", true)]
    public void OnCreateGoalChanged(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      (this.Goals).Disabled = true;
      (this.Goals).Disabled = this.CreateGoal.Checked;
      ((Input) this.GoalName).ReadOnly = false;
      ((Input) this.Points).ReadOnly = false;
      ((Input) this.GoalName).ReadOnly = !this.CreateGoal.Checked;
      ((Input) this.Points).ReadOnly = !this.CreateGoal.Checked;
      if ((this.Goals).Disabled)
        (this.Goals).Value = string.Empty;
      ((WebControl) this.GoalName).Style["color"] = this.CreateGoal.Checked ? "black" : "#999999";
      ((WebControl) this.Points).Style["color"] = this.CreateGoal.Checked ? "black" : "#999999";
      SheerResponse.SetOuterHtml((this.GoalName).ID, this.GoalName);
      SheerResponse.SetOuterHtml((this.Points).ID, this.Points);
      SheerResponse.Eval("$j('.Range').numeric();$$('.scComboboxDropDown')[0].disabled = " + (this.Goals).Disabled.ToString().ToLower() + ";");
    }

    private string GetWindowKey(string url)
    {
      switch (url)
      {
        case "":
        case null:
          return string.Empty;
        default:
          string str = url;
          int startIndex = str.IndexOf("?xmlcontrol=");
          if (startIndex >= 0)
          {
            int num = str.IndexOf("&", startIndex);
            if (num >= 0)
              str = StringUtil.Left(str, num);
          }
          else if (str.IndexOf("?") >= 0)
            str = StringUtil.Left(str, str.IndexOf("?"));
          if (str.StartsWith(Sitecore.Web.WebUtil.GetServerUrl(), StringComparison.OrdinalIgnoreCase))
            str = StringUtil.Mid(str, Sitecore.Web.WebUtil.GetServerUrl().Length);
          return str;
      }
    }

    private void RemoveScipts(HtmlNode node)
    {
      for (int index = 0; index < node.ChildNodes.Count; ++index)
      {
        HtmlNode childNode = node.ChildNodes[index];
        this.RemoveScipts(childNode);
        if (childNode.Name.ToLower() == "script")
          childNode.InnerHtml = " ";
      }
    }

    protected virtual bool RenderConfirmationFormSection => true;

    protected virtual Item FormsRoot
    {
      get
      {
        if (this.formsRoot == null)
          this.formsRoot = Factory.GetDatabase(this.DatabaseName).GetItem(this.Root);
        return this.formsRoot;
      }
    }

    protected virtual Item GoalsRoot
    {
      get
      {
        if (this.goalsRoot == null)
          this.goalsRoot = Factory.GetDatabase(this.DatabaseName).GetItem(StaticSettings.GoalsRootID);
        return this.goalsRoot;
      }
    }

    private string Root => Sitecore.Web.WebUtil.GetQueryString("root", StaticSettings.GlobalFormsRootID);

    private string DatabaseName => Sitecore.Web.WebUtil.GetQueryString("db", "master");
  }
}
