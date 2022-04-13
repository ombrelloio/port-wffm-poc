// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.RegisterConversionPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using ComponentArt.Web.UI;
using Sitecore.Data.Items;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Forms.Core.Data;
using Sitecore.Forms.Shell.UI.Controls;
using Sitecore.Shell.Applications.Dialogs.ItemLister;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using Sitecore.Web.UI.XamlSharp.Xaml;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class RegisterConversionPage : EditorBase
  {
    protected ControlledChecklist RegisterMode;
    protected ComboBox ModeCombobox;
    protected HtmlInputHidden SelectedGoalHolder;
    protected Edit GoalName;
    protected Border GoalBorder;
    protected HtmlInputButton SelectGoalButton;
    protected Sitecore.Web.UI.HtmlControls.Literal RegisterConversionLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal GoalLiteral;

    protected override void OnInit(EventArgs e)
    {
      this.RegisterMode.AddRange(ConditionalStatementUtil.GetConditionalItems(this.CurrentForm));
      this.RegisterMode.SelectRange(this.GetValueByKey("RegisterMode", "Always"));
      this.ModeCombobox.Text = this.RegisterMode.SelectedTitle;
      this.SelectedGoalHolder.Value = this.GetValueByKey("Goal");
      base.OnInit(e);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (!(this).Page.IsPostBack)
        this.BuildUpClientDictionary();
      (this.GoalName).Value = string.Empty;
      if (string.IsNullOrEmpty(this.SelectedGoalHolder.Value))
        return;
      Item obj = this.CurrentDatabase.GetItem(this.SelectedGoalHolder.Value);
      if (obj == null)
        return;
      (this.GoalName).Value = string.Join("/", new string[2]
      {
        obj.Parent.Name,
        obj.Name
      });
    }

    protected virtual void BuildUpClientDictionary() => (this).Page.ClientScript.RegisterClientScriptBlock(((object) this).GetType(), "scWfmNeverLabel", Sitecore.StringExtensions.StringExtensions.FormatWith("var sc = new Object();sc.dictionary = [];sc.dictionary['Never'] = \"{0}\";", new object[1]
    {
      (object) DependenciesManager.ResourceManager.Localize("NEVER")
    }), true);

    protected override void Localize()
    {
      base.Localize();
      this.Header = DependenciesManager.ResourceManager.Localize("REGISTER_CONVERSION");
      this.Text = DependenciesManager.ResourceManager.Localize("REGISTER_SELECTED_GOAL_AS_CONVERSION");
      this.RegisterConversionLiteral.Text = DependenciesManager.ResourceManager.Localize("REGISTER_CONVERSION_COLON");
      this.GoalLiteral.Text = DependenciesManager.ResourceManager.Localize("GOAL_COLON");
      this.SelectGoalButton.Value = DependenciesManager.ResourceManager.Localize("SELECT_GOAL_CAPTION");
    }

    protected void FillRegisterGoalMode(
      DropDownList listBox,
      string defaultValue,
      string allowedTypes)
    {
      listBox.Items.Clear();
      FormItem formItem = new FormItem(this.CurrentDatabase.GetItem(this.CurrentID));
      System.Web.UI.WebControls.ListItem listItem1 = new System.Web.UI.WebControls.ListItem(DependenciesManager.ResourceManager.Localize("ALWAYS"), "Always");
      if (string.Compare("Always", defaultValue, true) == 0)
        listItem1.Selected = true;
      listBox.Items.Add(listItem1);
      foreach (IFieldItem field in formItem.Fields)
      {
        if (string.IsNullOrEmpty(allowedTypes) || allowedTypes.Contains(((object) field.TypeID).ToString()))
        {
          System.Web.UI.WebControls.ListItem listItem2 = new System.Web.UI.WebControls.ListItem(DependenciesManager.ResourceManager.Localize("WHEN_IS_SELECTED", field.Name), ((object) field.ID).ToString());
          if (string.Compare(listItem2.Value, defaultValue, true) == 0)
            listItem2.Selected = true;
          listBox.Items.Add(listItem2);
        }
      }
    }

    protected override void OK_Click()
    {
      if (string.IsNullOrEmpty(this.SelectedGoalHolder.Value))
        XamlControl.AjaxScriptManager.Alert(DependenciesManager.ResourceManager.Localize("SELECT_GOAL_CONVERSION"));
      else
        base.OK_Click();
    }

    protected override void SaveValues()
    {
      this.SetValue("RegisterMode", string.Join("|", this.RegisterMode.GetManagedSelectedValues().ToArray<string>()));
      this.SetValue("Goal", this.SelectedGoalHolder.Value);
    }

    private void OnSelectGoal()
    {
      ClientPipelineArgs currentArgs = ContinuationManager.Current.CurrentArgs as ClientPipelineArgs;
      if (currentArgs.IsPostBack)
      {
        if (!currentArgs.HasResult)
          return;
        Item obj = StaticSettings.ContextDatabase.GetItem(currentArgs.Result);
        this.SelectedGoalHolder.Value = ((object) obj.ID).ToString();
        (this.GoalName).Value = string.Join("/", new string[2]
        {
          obj.Parent.Name,
          obj.Name
        });
        SheerResponse.SetOuterHtml((this.GoalBorder).ID, this.GoalBorder);
      }
      else
      {
        SheerResponse.ShowModalDialog(((object) new SelectItemOptions()
        {
          Root = StaticSettings.ContextDatabase.GetItem(IDs.GoalRoot),
          Icon = "Applications/32x32/folder_cubes.png",
          SelectedItem = (this.SelectedGoalHolder.Value.Length <= 0 ? StaticSettings.ContextDatabase.GetItem(IDs.GoalRoot) : StaticSettings.ContextDatabase.SelectSingleItem(this.SelectedGoalHolder.Value)),
          Title = DependenciesManager.ResourceManager.Localize("SELECT_GOAL"),
          Text = DependenciesManager.ResourceManager.Localize("SELECT_GOAL_TEXT"),
          ButtonText = DependenciesManager.ResourceManager.Localize("SELECT")
        }.ToUrlString()).ToString(), true);
        currentArgs.WaitForPostBack();
      }
    }

    public string RegisterModeAllowedTypes => Sitecore.Web.WebUtil.GetQueryString(nameof (RegisterModeAllowedTypes), (string) null);
  }
}
