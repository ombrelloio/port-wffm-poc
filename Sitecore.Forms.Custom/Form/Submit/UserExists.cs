// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Submit.UserExists
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Data;
using Sitecore.Forms.Core.Data;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Form.Submit
{
  public class UserExists : MembershipCheckAction
  {
    public override void Execute(
      ID formid,
      IEnumerable<ControlResult> fields,
      ActionCallContext actionCallContext = null)
    {
      string message1 = this.FailedMessage ?? DependenciesManager.ResourceManager.GetString("USER_DOES_NOT_EXIST");
      string message2 = this.FailedIfUserExistsMessage ?? DependenciesManager.ResourceManager.GetString("USER_ALREADY_EXISTS");
      ControlResult controlResult = fields.FirstOrDefault<ControlResult>((Func<ControlResult, bool>) (f => f.FieldID == this.UserNameField));
      if (controlResult == null)
      {
        DependenciesManager.Logger.Warn("The User Exists action: the user name is not set.", (object) this);
        throw new ArgumentException(message1);
      }
      string userNameIfExist = this.GetUserNameIfExist((string) controlResult.Value);
      if (FailedCondition.Denied == this.FailedWhen && string.IsNullOrEmpty(userNameIfExist))
        throw new Exception(message1);
      if (this.FailedWhen == FailedCondition.Confirmed && !string.IsNullOrEmpty(userNameIfExist))
        throw new Exception(message2);
    }

    public FailedCondition FailWhen
    {
      get => this.FailedWhen;
      set => this.FailedWhen = value;
    }

    public string FailedIfUserExistsMessage { get; set; }
  }
}
