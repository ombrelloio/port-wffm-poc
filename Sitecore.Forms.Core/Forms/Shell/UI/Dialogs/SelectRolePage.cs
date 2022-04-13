// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.SelectRolePage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using ComponentArt.Web.UI;
using Sitecore.Controls;
using Sitecore.Security.Accounts;
using Sitecore.Web.UI.Grids;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XamlSharp.Xaml;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Web.UI;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class SelectRolePage : DialogPage
  {
    protected Grid Roles;

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (!((Control) this).Page.IsPostBack)
        this.Localize();
      ComponentArtGridHandler<Role>.Manage(this.Roles, (IGridSource<Role>) new GridSource<Role>(Sitecore.Context.User.Delegation.GetManagedRoles(false)), !((Control) this).Page.IsPostBack);
    }

    protected override void OK_Click()
    {
      string selectedValue = GridUtil.GetSelectedValue(((Control) this.Roles).ID);
      if (!string.IsNullOrEmpty(selectedValue) && selectedValue != "null")
      {
        SheerResponse.SetDialogValue(selectedValue);
        base.OK_Click();
      }
      else
        SheerResponse.Alert(DependenciesManager.ResourceManager.Localize("SELECT_ROLE_TO_CONTINUE"), new string[0]);
    }

    protected virtual void Localize()
    {
      this.Header = DependenciesManager.ResourceManager.Localize("SELECT_ROLE");
      this.Text = DependenciesManager.ResourceManager.Localize("SELECT_ROLE_THAT_YOU_WANT_TO_USE");
      this.Roles.Levels[(object) 0].Columns[(object) 1].HeadingText = DependenciesManager.ResourceManager.Localize("NAME");
      this.Roles.SearchText = DependenciesManager.ResourceManager.Localize("SEARCH");
      this.Roles.GroupingNotificationText = string.Empty;
    }
  }
}
