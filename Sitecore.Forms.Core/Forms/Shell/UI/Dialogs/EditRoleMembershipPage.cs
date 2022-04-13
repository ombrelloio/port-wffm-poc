// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.EditRoleMembershipPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using ComponentArt.Web.UI;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Forms.Core.Data;
using Sitecore.Forms.Shell.UI.Controls;
using Sitecore.Security.Accounts;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.WebControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class EditRoleMembershipPage : AuditMembershipActionPage
  {
    protected CallBack AddUserToRolesCallBack;
    protected CallBack RemoveUserToRolesCallBack;
    protected PlaceHolder AddRolesContent;
    protected PlaceHolder RemoveRolesContent;
    protected ListBox AddRolesList;
    protected ListBox RemoveRolesList;
    protected HtmlInputHidden AddedRolesValue;
    protected HtmlInputHidden RemovedRolesValue;
    protected ControlledChecklist ChangeRoleMode;
    protected HtmlInputButton AddRoleButton;
    protected HtmlInputButton RemoveRoleButton;
    protected Checkbox AssociateUserWithVisitor;
    protected ComboBox ModeCombobox;
    protected Border ModeComboboxHolder;
    protected Groupbox IdentityUserGroupbox;
    protected Groupbox RoleMembershipGroupbox;
    protected Sitecore.Web.UI.HtmlControls.Literal AddUserToRoleLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal RemoveUserFromRoleLiteral;
    protected Sitecore.Web.UI.HtmlControls.Literal ChangeRoleMemebrhipLiteral;

    protected override void OnInit(EventArgs e)
    {
            base.OnInit(e);
            ChangeRoleMode.AddRange(ConditionalStatementUtil.GetConditionalItems(base.CurrentForm));
            ChangeRoleMode.SelectRange(GetValueByKey("ChangeMemebrshipMode", "Always"));
            ModeCombobox.Text = (ChangeRoleMode.SelectedTitle);
            AssociateUserWithVisitor.Checked = (MainUtil.GetBool(GetValueByKey("AssociateUserWithVisitor"), true));
            AddedRolesValue.Value = GetValueByKey("AddToRoles");
            RemovedRolesValue.Value = GetValueByKey("RemoveFromRoles");
            AddUserToRolesCallBack.Callback += OnAddRoleCallBack;
            RemoveUserToRolesCallBack.Callback += OnRemoveRoleCallBack;
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form[AddedRolesValue.ID]))
            {
                AddedRolesValue.Value = HttpContext.Current.Request.Form[AddedRolesValue.ID];
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form[RemovedRolesValue.ID]))
            {
                RemovedRolesValue.Value = HttpContext.Current.Request.Form[RemovedRolesValue.ID];
            }
            UpdateRoleList(AddRolesList, AddedRolesValue.Value);
            UpdateRoleList(RemoveRolesList, RemovedRolesValue.Value);
        }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if ((this).Page.IsPostBack || (this.AddUserToRolesCallBack).IsCallback || (this.RemoveUserToRolesCallBack).IsCallback)
        return;
      this.AddUserToRolesCallBack.Content = new CallBackContent();
      (this.AddUserToRolesCallBack.Content).Controls.Add(this.AddRolesContent);
      this.RemoveUserToRolesCallBack.Content = new CallBackContent();
      (this.RemoveUserToRolesCallBack.Content).Controls.Add(this.RemoveRolesContent);
      this.BuildUpClientDictionary();
    }

    protected override void Localize()
    {
      base.Localize();
      this.Header = DependenciesManager.ResourceManager.Localize("EDIT_ROLE_MEMEBRSHIP");
      this.Text = DependenciesManager.ResourceManager.Localize("ADD_OR_REMOVE_USER_FROM_ROLE");
      this.AssociateUserWithVisitor.Header = DependenciesManager.ResourceManager.Localize("ASSOCIATE_EXISTING_USER_WITH_THIS_WISITOR");
      ((HeaderedItemsControl) this.IdentityUserGroupbox).Header = DependenciesManager.ResourceManager.Localize("IDENTIFY_USER");
      this.AddUserToRoleLiteral.Text = DependenciesManager.ResourceManager.Localize("ADD_USER_TO_ROLE");
      this.RemoveUserFromRoleLiteral.Text = DependenciesManager.ResourceManager.Localize("REMOVE_USER_FROM_ROLE");
      this.AddRoleButton.Value = DependenciesManager.ResourceManager.Localize("EDIT");
      this.RemoveRoleButton.Value = DependenciesManager.ResourceManager.Localize("EDIT");
      ((HeaderedItemsControl) this.RoleMembershipGroupbox).Header = DependenciesManager.ResourceManager.Localize("ROLE_MEMEBRSHIP");
      this.ChangeRoleMemebrhipLiteral.Text = DependenciesManager.ResourceManager.Localize("CHANGE_ROLE_MEMEBRSHIP");
    }

    protected virtual void BuildUpClientDictionary() => (this).Page.ClientScript.RegisterClientScriptBlock(((object) this).GetType(), "scWfmNeverLabel", Sitecore.StringExtensions.StringExtensions.FormatWith("var sc = new Object();sc.dictionary = [];sc.dictionary['Never'] = \"{0}\";", new object[1]
    {
      (object) DependenciesManager.ResourceManager.Localize("NEVER")
    }), true);

    protected void Remove_Click() => this.ChangeRoleList(this.RemoveRolesList, (this.RemoveUserToRolesCallBack).ID);

    protected void Add_Click() => this.ChangeRoleList(this.AddRolesList, (this.AddUserToRolesCallBack).ID);

    protected override void SaveValues()
    {
      base.SaveValues();
      this.SetValue("AssociateUserWithVisitor", this.AssociateUserWithVisitor.Checked.ToString());
      this.SetValue("ChangeMemebrshipMode", string.Join("|", this.ChangeRoleMode.GetManagedSelectedValues().ToArray<string>()));
      this.SetValue("AddToRoles", this.AddedRolesValue.Value);
      this.SetValue("RemoveFromRoles", this.RemovedRolesValue.Value);
      string str = new FormItem(this.CurrentDatabase.GetItem(this.CurrentID)).ProfileItem;
      if (string.IsNullOrEmpty(str))
        str = "{AE4C4969-5B7E-4B4E-9042-B2D8701CE214}";
      this.SetValue("ProfileItemId", str);
    }

    private void OnAddRoleCallBack(object sender, CallBackEventArgs e)
    {
      this.AddedRolesValue.Value = (string) e.Parameter;
      this.UpdateRoleList(this.AddRolesList, (string) e.Parameter);
      this.AddRolesContent.RenderControl((HtmlTextWriter) e.Output);
    }

    private void OnRemoveRoleCallBack(object sender, CallBackEventArgs e)
    {
      this.RemovedRolesValue.Value = (string) e.Parameter;
      this.UpdateRoleList(this.RemoveRolesList, (string) e.Parameter);
      this.RemoveRolesContent.RenderControl((HtmlTextWriter) e.Output);
    }

    private void UpdateRoleList(ListBox roleList, string roles)
    {
      roleList.Items.Clear();
      if (string.IsNullOrEmpty(roles))
        return;
      string[] strArray = roles.Split('|');
      List<string> stringList = new List<string>();
      foreach (string str in strArray)
      {
        roleList.Items.Add(str);
        stringList.Add(str);
      }
      if (!RolesInRolesManager.RolesInRolesSupported)
        return;
      foreach (string str in strArray)
      {
        foreach (Role role in RolesInRolesManager.GetRolesForRole(Role.FromName(str), true))
        {
          if (!stringList.Contains(((Account) role).Name))
          {
            roleList.Items.Add("<" + ((Account) role).Name + ">");
            stringList.Add(((Account) role).Name);
          }
        }
      }
    }

    private void ChangeRoleList(ListBox roleList, string callbackId)
    {
      ClientPipelineArgs currentArgs = ContinuationManager.Current.CurrentArgs as ClientPipelineArgs;
      Assert.IsNotNull((object) currentArgs, "args");
      if (currentArgs.IsPostBack)
      {
        if (!currentArgs.HasResult)
          return;
        string str = currentArgs.Result;
        if (str == "-")
          str = string.Empty;
        SheerResponse.Eval(string.Join(string.Empty, new string[4]
        {
          callbackId,
          ".callback('",
          str.Replace("\\", "\\\\"),
          "');"
        }));
      }
      else
      {
        UrlString urlString = new UrlString("/sitecore/shell/~/xaml/Sitecore.Shell.Applications.Security.SelectRoles.aspx");
        new UrlHandle()
        {
          ["roles"] = this.GetRoles(roleList)
        }.Add(urlString);
        SheerResponse.ShowModalDialog(((object) urlString).ToString(), "1050", "600", string.Empty, true);
        currentArgs.WaitForPostBack();
      }
    }

    private string GetRoles(ListBox listBox)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (System.Web.UI.WebControls.ListItem listItem in listBox.Items)
        stringBuilder.AppendFormat("{0}|", (object) listItem.Text);
      return stringBuilder.Length <= 0 ? stringBuilder.ToString() : stringBuilder.ToString(0, stringBuilder.Length - 1);
    }
  }
}
