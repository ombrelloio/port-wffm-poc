// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.RegexUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using System.Text.RegularExpressions;

namespace Sitecore.Form.Core.Utility
{
  public class RegexUtil
  {
    public static readonly string EmailRegex = "^[a-zA-Z0-9_-]+(?:\\.[a-zA-Z0-9_-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$";

    public static bool IsValidEmail(string value) => !string.IsNullOrEmpty(value) && new Regex(Settings.GetSetting("EmailValidation", RegexUtil.EmailRegex)).Matches(value).Count == 1;
  }
}
