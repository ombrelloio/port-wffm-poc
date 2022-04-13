// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.ScriptUtils
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;

namespace Sitecore.Form.Core.Utility
{
  public class ScriptUtils
  {
    internal static string MergeScript(string firstScript, string secondScript)
    {
      if (!string.IsNullOrEmpty(firstScript))
        return firstScript + secondScript;
      return secondScript.TrimStart().StartsWith("javascript:", StringComparison.Ordinal) ? secondScript : "javascript:" + secondScript;
    }

    internal static string EnsureEndWithSemiColon(string value)
    {
      if (!string.IsNullOrEmpty(value))
      {
        int length = value.Length;
        if (value[length - 1] != ';')
          return value + ";";
      }
      return value;
    }
  }
}
