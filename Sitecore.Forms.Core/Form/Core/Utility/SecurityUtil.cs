// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.SecurityUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Sitecore.Form.Core.Utility
{
  public class SecurityUtil
  {
    public static string CreateSecureGroupName(NetworkCredential credentials) => Encoding.Default.GetString(new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(credentials.UserName + credentials.Password + credentials.Domain)));

    public static int GetAuthenticationType(string value)
    {
      int result = 0;
      int.TryParse(value, out result);
      return result;
    }

    public static NetworkCredential CreateNetworkCredential(
      string login,
      string password)
    {
      NetworkCredential networkCredential;
      if (login.IndexOf("\\") > 0)
      {
        string[] strArray = login.Split(new string[1]
        {
          "\\"
        }, 2, StringSplitOptions.None);
        networkCredential = new NetworkCredential(strArray[1], password, strArray[0]);
      }
      else
        networkCredential = new NetworkCredential(login, password);
      return networkCredential;
    }
  }
}
