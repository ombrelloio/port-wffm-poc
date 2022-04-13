// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Submit.CheckUserPassword
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace Sitecore.Form.Submit
{
  public class CheckUserPassword : CheckUserAction
  {
    public override void Execute(
      ID formid,
      IEnumerable<ControlResult> fields,
      ActionCallContext actionCallContext = null)
    {
      string message = this.FailedMessage ?? DependenciesManager.ResourceManager.GetString("USER_NAME_OR_PASSWORD_IS_INCORRECT");
      ControlResult controlResult1 = fields.FirstOrDefault<ControlResult>((Func<ControlResult, bool>) (f => f.FieldID == this.UserNameField));
      ControlResult controlResult2 = fields.FirstOrDefault<ControlResult>((Func<ControlResult, bool>) (f => f.FieldID == this.PasswordField));
      if (controlResult1 == null)
      {
        DependenciesManager.Logger.Warn("Check User and Password action: the user name is not set.", (object) this);
        throw new ArgumentException(message);
      }
      string username = (string) controlResult1.Value;
      if (string.IsNullOrEmpty(username))
        throw new Exception(message);
      if (!username.Contains("\\"))
        username = string.Join("\\", new string[2]
        {
          this.DomainField,
          username
        });
      string password = controlResult2 != null ? (string) controlResult2.Value ?? string.Empty : string.Empty;
      if (Membership.ValidateUser(username, password))
        return;
      Assert.IsTrue(Membership.ValidateUser(username.Replace("@", "_at_").Replace(".", "_dot_"), password), message);
    }

    public string PasswordField { get; set; }
  }
}
