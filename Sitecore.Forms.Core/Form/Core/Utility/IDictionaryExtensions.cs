// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.IDictionaryExtensions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using System.Collections.Generic;

namespace Sitecore.Form.Core.Utility
{
  public static class IDictionaryExtensions
  {
    public static void Set<T, V>(this IDictionary<T, V> dict, T key, V value)
    {
      Assert.ArgumentNotNull((object) dict, nameof (dict));
      if (dict.ContainsKey(key))
        dict[key] = value;
      else
        dict.Add(key, value);
    }
  }
}
