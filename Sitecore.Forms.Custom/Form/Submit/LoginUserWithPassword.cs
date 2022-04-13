// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Submit.LoginUserWithPassword
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Security.Authentication;
using Sitecore.WFFM.Abstractions.Actions;

namespace Sitecore.Form.Submit
{
  public class LoginUserWithPassword : LoginUserWithoutPassword
  {
    protected override bool LoginUser(string userName, AdaptedResultList fields)
    {
      AdaptedControlResult entry = fields.GetEntry(this.PasswordField, "Password");
      string str = entry != null ? entry.Value ?? string.Empty : string.Empty;
      return AuthenticationManager.Login(userName, str, true);
    }
  }
}
