// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Data.UserExtensions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Security.Accounts;

namespace Sitecore.Form.Core.Data
{
  public static class UserExtensions
  {
    public static object GetProfileValue(this User user, string property)
    {
      Assert.ArgumentNotNull((object) user, nameof (user));
      object obj = (object) null;
      if (!string.IsNullOrEmpty(property))
      {
        if (string.Compare(property, "Comment", true) == 0)
          obj = (object) user.Profile.Comment;
        else if (string.Compare(property, "Full Name", true) == 0)
          obj = (object) user.Profile.FullName;
        else if (string.Compare(property, "Name", true) == 0)
        {
          obj = (object) user.Profile.Name;
        }
        else
        {
          if (string.Compare(property, "Email", true) != 0)
            return (object) user.Profile[property];
          obj = (object) user.Profile.Email;
        }
      }
      return obj;
    }
  }
}
