// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.StringExtensions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.Text;

namespace Sitecore.Form.Core.Utility
{
  public static class StringExtensions
  {
    public static char[] WhiteSpaces = new char[4]
    {
      ' ',
      '\t',
      '\r',
      '\n'
    };

    public static string ToLower(this string value, int startIndex)
    {
      if (string.IsNullOrEmpty(value) || startIndex >= value.Length)
        return value;
      StringBuilder stringBuilder = new StringBuilder(value.Length);
      for (int index = 0; index < value.Length; ++index)
      {
        if (index >= startIndex)
          stringBuilder.Append(char.ToLower(value[index]));
        else
          stringBuilder.Append(value[index]);
      }
      return stringBuilder.ToString();
    }

    public static string TrimWhiteSpaces(this string value)
    {
      string str1 = value;
      if (!string.IsNullOrEmpty(value))
      {
        string str2;
        do
        {
          str2 = str1;
          str1 = str2.Trim(StringExtensions.WhiteSpaces);
        }
        while (str1 != str2);
      }
      return str1;
    }
  }
}
