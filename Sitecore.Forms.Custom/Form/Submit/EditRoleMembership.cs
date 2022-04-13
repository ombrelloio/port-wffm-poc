// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Submit.EditRoleMembership
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Data;
using Sitecore.Forms.Core.Data;
using Sitecore.Security.Accounts;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Security;

namespace Sitecore.Form.Submit
{
  public class EditRoleMembership : UserBaseAction
  {
    private ID formID;

    public override void Execute(
      ID formId,
      AdaptedResultList adaptedFields,
      ActionCallContext actionCallContext = null,
      params object[] data)
    {
      this.formID = formId;
      this.PasswordField = "randomPassword";
      string userName = this.ProccessBaseOperations(formId, adaptedFields, true);
      if (string.IsNullOrEmpty(userName))
        return;
      if (adaptedFields.IsTrueStatement(this.ChangeMemebrshipMode))
      {
        this.AddUserToRoles(userName);
        this.RemoveUserFromRoles(userName);
      }
      this.UpdateAudit(formId, User.FromName(userName, true));
    }

    protected virtual void RemoveUserFromRoles(string userName)
    {
            if (string.IsNullOrEmpty(RemoveFromRoles))
            {
                return;
            }
            string[] source = RemoveFromRoles.Split('|');
            string[] rolesForUser = Roles.GetRolesForUser(userName);
            IEnumerable<string> enumerable = from r in ((IEnumerable<string>)source).Where((Func<string, bool>)((IEnumerable<string>)rolesForUser).Contains)
                                             where !string.IsNullOrEmpty(r)
                                             select r;
            if (!enumerable.Any())
            {
                return;
            }
            Roles.RemoveUserFromRoles(userName, enumerable.ToArray());
            foreach (string item in enumerable)
            {
                AuditMessage(DependenciesManager.ResourceManager.Localize("AUDIT_REMOVE_FROM_ROLE", item));
            }
        }

    protected virtual void AddUserToRoles(string userName)
    {
      if (string.IsNullOrEmpty(this.AddToRoles))
        return;
      IEnumerable<string> source = ((IEnumerable<string>) this.AddToRoles.Split('|')).Except<string>((IEnumerable<string>) Roles.GetRolesForUser(userName)).Where<string>((Func<string, bool>) (r => !string.IsNullOrEmpty(r)));
      if (!source.Any<string>())
        return;
      Roles.AddUserToRoles(userName, source.ToArray<string>());
      foreach (string str in source)
        this.AuditMessage(DependenciesManager.ResourceManager.Localize("AUDIT_ADD_TO_ROLE", str));
    }

    protected virtual void UpdateAudit(ID formid, User user)
    {
      if (string.IsNullOrEmpty(this.AuditField) || !(this.AuditField != "NoAudit"))
        return;
      this.UpdateProfileProperty(user.Profile, this.AuditField, this.DumpAuditInfomration(this.GetProfileProperty(user.Profile, this.AuditField)));
      ((SettingsBase) user.Profile).Save();
    }

    public string AddToRoles { get; set; }

    public string RemoveFromRoles { get; set; }

    public string ChangeMemebrshipMode { get; set; }

    public override FormItem CurrentForm => FormItem.GetForm(this.formID);
  }
}
