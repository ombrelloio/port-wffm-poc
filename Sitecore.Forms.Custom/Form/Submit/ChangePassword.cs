// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Submit.ChangePassword
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Data;
using Sitecore.Forms.Core.Data;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Web.Security;

namespace Sitecore.Form.Submit
{
  public class ChangePassword : UserBaseAction
  {
    private ID formID;

    public override void Execute(
      ID formId,
      AdaptedResultList adaptedFields,
      ActionCallContext actionCallContext = null,
      params object[] data)
    {
      this.formID = formId;
      AdaptedControlResult entry1 = adaptedFields.GetEntry(this.UserNameField, "User name");
      if (entry1 == null)
      {
        DependenciesManager.Logger.Warn("The Change Password action: the user name is not set.", (object) this);
        throw new Exception(DependenciesManager.ResourceManager.GetString("USER_NAME_IS_NOT_SET"));
      }
      string userNameIfExist = this.GetUserNameIfExist(entry1.Value);
      if (string.IsNullOrEmpty(userNameIfExist))
        return;
      AdaptedControlResult entry2 = adaptedFields.GetEntry(this.OldPasswordField, "Password");
      AdaptedControlResult entry3 = adaptedFields.GetEntry(this.PasswordField, "Old Password");
      string oldPassword = entry2 == null ? string.Empty : entry2.Value ?? string.Empty;
      string newPassword = entry3 == null ? string.Empty : entry3.Value ?? string.Empty;
      if (!Membership.Provider.ChangePassword(userNameIfExist, oldPassword, newPassword))
        throw new Exception(DependenciesManager.ResourceManager.GetString("OLD_PASSWORD_IS_INCORECT"));
    }

    public string OldPasswordField { get; set; }

    public override FormItem CurrentForm => FormItem.GetForm(this.formID);
  }
}
