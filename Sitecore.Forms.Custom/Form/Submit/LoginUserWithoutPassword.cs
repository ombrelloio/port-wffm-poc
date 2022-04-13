// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Submit.LoginUserWithoutPassword
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Data;
using Sitecore.Forms.Core.Data;
using Sitecore.Security.Accounts;
using Sitecore.Security.Authentication;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Configuration;

namespace Sitecore.Form.Submit
{
  [Serializable]
  public class LoginUserWithoutPassword : UserBaseAction
  {
    private ID formID;

    public override void Execute(
      ID formId,
      AdaptedResultList adaptedFields,
      ActionCallContext actionCallContext = null,
      params object[] data)
    {
      this.formID = formId;
      AdaptedControlResult entry = adaptedFields.GetEntry(this.UserNameField, "User name");
      if (entry == null)
        DependenciesManager.Logger.Warn("The Login User action: the user name is not set.", (object) this);
      string userNameIfExist = this.GetUserNameIfExist(entry.Value);
      if (string.IsNullOrEmpty(userNameIfExist))
        return;
      this.UpdateGlobalSession(userNameIfExist);
      if (!this.LoginUser(userNameIfExist, adaptedFields))
        return;
      this.UpdateAudit(formId, User.FromName(userNameIfExist, true));
    }

    protected virtual bool LoginUser(string userName, AdaptedResultList fields) => AuthenticationManager.Login(userName, true);

    protected virtual void UpdateAudit(ID formid, User user)
    {
      if (string.IsNullOrEmpty(this.AuditField) || !(this.AuditField != "NoAudit"))
        return;
      this.AuditMessage(DependenciesManager.ResourceManager.Localize("AUDIT_USER_LOGIN", ((Account) user).Name));
      this.UpdateProfileProperty(user.Profile, this.AuditField, this.DumpAuditInfomration(this.GetProfileProperty(user.Profile, this.AuditField)));
      ((SettingsBase) user.Profile).Save();
    }

    public string Mapping { get; set; }

    public override FormItem CurrentForm => FormItem.GetForm(this.formID);
  }
}
