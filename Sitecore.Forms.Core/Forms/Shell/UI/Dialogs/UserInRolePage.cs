// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.UserInRolePage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Core.Data;
using Sitecore.Text;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using Sitecore.Web.UI.XamlSharp.Xaml;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class UserInRolePage : MemberhipActionPage
  {
    protected Literal ChoiсesDescLiteral;
    protected Literal UserInRoleLiteral;
    protected Literal UserIsNotInRoleLiteral;
    protected Literal RoleLiteral;
    protected Button BrowseRole;
    protected Edit SelectedRole;
    protected HtmlInputRadioButton UserIsInRoleCheckbox;
    protected HtmlInputRadioButton UserIsNotInRoleCheckbox;

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.UserIsNotInRoleCheckbox.Checked = this.GetValueByKey("FailedWhen", "Denied") == FailedCondition.Denied.ToString();
      this.UserIsInRoleCheckbox.Checked = !this.UserIsNotInRoleCheckbox.Checked;
      (this.SelectedRole).Value = this.GetValueByKey("Role", string.Empty);
    }

    protected override void Localize()
    {
      base.Localize();
      this.Header = DependenciesManager.ResourceManager.Localize("IS_USER_IN_ROLE");
      this.Text = DependenciesManager.ResourceManager.Localize("CHECK_WHETHER_USER_IN_SELECTED_ROLE");
      this.ChoiсesDescLiteral.Text = DependenciesManager.ResourceManager.Localize("FORM_VARIFICATION_WILL_FAIL_IF");
      this.RoleLiteral.Text = DependenciesManager.ResourceManager.Localize("ROLE");
      this.UserInRoleLiteral.Text = DependenciesManager.ResourceManager.Localize("USER_IS_IN_ROLE", string.Empty);
      this.UserIsNotInRoleLiteral.Text = DependenciesManager.ResourceManager.Localize("USER_NOT_IN_ROLE", string.Empty);
      ((HeaderedItemsControl) this.BrowseRole).Header = DependenciesManager.ResourceManager.Localize("EDIT");
    }

    protected void OnBrowseRole()
    {
      ClientPipelineArgs currentArgs = ContinuationManager.Current.CurrentArgs as ClientPipelineArgs;
      Assert.ArgumentNotNull((object) currentArgs, "args");
      Assert.ArgumentNotNull((object) currentArgs, "args");
      if (!currentArgs.IsPostBack)
      {
        SheerResponse.ShowModalDialog(((object) new UrlString("/sitecore/shell/~/xaml/Sitecore.Forms.Shell.UI.Dialogs.SelectRole.aspx")
        {
          ["ro"] = (this.SelectedRole).Value
        }).ToString(), true);
        currentArgs.WaitForPostBack();
      }
      else
      {
        if (!currentArgs.HasResult)
          return;
        (this.SelectedRole).Value = currentArgs.Result;
        XamlControl.AjaxScriptManager.SetOuterHtml((this.SelectedRole).ID, this.SelectedRole);
      }
    }

    protected override void OK_Click()
    {
      if (string.IsNullOrEmpty((this.SelectedRole).Value))
        XamlControl.AjaxScriptManager.Alert(DependenciesManager.ResourceManager.Localize("SELECT_ROLE_TO_CONTINUE"));
      else
        base.OK_Click();
    }

    protected override void SaveValues()
    {
      this.SetValue("FailedWhen", this.UserIsNotInRoleCheckbox.Checked ? FailedCondition.Denied.ToString() : FailedCondition.Confirmed.ToString());
      this.SetValue("Role", (this.SelectedRole).Value);
      base.SaveValues();
    }
  }
}
