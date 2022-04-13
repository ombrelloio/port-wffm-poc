// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Extensions.DictionaryExtensions
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using System.Collections.Generic;

namespace Sitecore.Forms.Mvc.Extensions
{
  public static class DictionaryExtensions
  {
    public static string GetValue(this Dictionary<string, string> dict, string key)
    {
      Assert.ArgumentNotNull((object) dict, nameof (dict));
      Assert.ArgumentNotNullOrEmpty(key, nameof (key));
      if (dict.ContainsKey(key))
        return dict[key];
      return !dict.ContainsKey(key.ToLower()) ? (string) null : dict[key.ToLower()];
    }
  }
}
