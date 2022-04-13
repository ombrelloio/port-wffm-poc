// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Submit.UserInRole
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
using System.Linq;
using System.Web.Security;

namespace Sitecore.Form.Submit
{
  public class UserInRole : MembershipCheckAction
  {
    public override void Execute(
      ID formid,
      IEnumerable<ControlResult> fields,
      ActionCallContext actionCallContext = null)
    {
      if (string.IsNullOrEmpty(this.Role))
      {
        DependenciesManager.Logger.Warn("The Is User In Role action: the role is not set.", (object) this);
      }
      else
      {
        if (!Roles.RoleExists(this.Role))
          return;
        string message1 = string.Format(this.FailedMessage ?? DependenciesManager.ResourceManager.GetString("USER_NOT_IN_ROLE"), (object) this.Role);
        string message2 = string.Format(this.FailedIfUserInRoleMessage ?? DependenciesManager.ResourceManager.GetString("USER_IS_IN_ROLE"), (object) this.Role);
        ControlResult controlResult = fields.FirstOrDefault<ControlResult>((Func<ControlResult, bool>) (f => f.FieldID == this.UserNameField));
        if (controlResult == null)
        {
          DependenciesManager.Logger.Warn("The Is User In Role action: the user name is not set.", (object) this);
          throw new ArgumentException(message1);
        }
        string userNameIfExist = this.GetUserNameIfExist((string) controlResult.Value);
        if (string.IsNullOrEmpty(userNameIfExist))
          return;
        bool flag = RolesInRolesManager.IsUserInRole(User.FromName(userNameIfExist, false), Sitecore.Security.Accounts.Role.FromName(this.Role), true);
        if (FailedCondition.Denied == this.FailedWhen && !flag)
          throw new Exception(message1);
        if (this.FailedWhen == FailedCondition.Confirmed & flag)
          throw new Exception(message2);
      }
    }

    public string Role { get; set; }

    public string FailedIfUserInRoleMessage { get; set; }

    public string RoleIsNotDefinedMessage { get; set; }
  }
}
