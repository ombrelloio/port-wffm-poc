// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Submit.MembershipCheckAction
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Core.Data;
using System.Web.Security;

namespace Sitecore.Form.Submit
{
  public abstract class MembershipCheckAction : CheckUserAction
  {
    public FailedCondition FailedWhen { get; set; }

    protected string GetUserNameIfExist(string preUserName)
    {
      Assert.ArgumentNotNull((object) preUserName, nameof (preUserName));
      string username1 = preUserName;
      if (string.IsNullOrEmpty(username1))
        return (string) null;
      if (!username1.Contains("\\"))
        username1 = string.Join("\\", new string[2]
        {
          this.DomainField,
          username1
        });
      if (Membership.GetUser(username1) != null)
        return username1;
      string username2 = username1.Replace("@", "_at_").Replace(".", "_dot_");
      return Membership.GetUser(username2) != null ? username2 : (string) null;
    }
  }
}
