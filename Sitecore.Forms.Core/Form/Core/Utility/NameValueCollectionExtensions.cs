// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.NameValueCollectionExtensions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Sitecore.Form.Core.Utility
{
  public static class NameValueCollectionExtensions
  {
    public static void ForEach(this NameValueCollection collection, Action<string, string> action)
    {
      Assert.ArgumentNotNull((object) collection, nameof (collection));
      foreach (string allKey in collection.AllKeys)
        action(allKey, collection[allKey]);
    }

    public static IEnumerable<TResult> Select<TResult>(
      this NameValueCollection collection,
      Func<string, string, TResult> func)
    {
      Assert.ArgumentNotNull((object) collection, nameof (collection));
      List<TResult> list = new List<TResult>();
      collection.ForEach((Action<string, string>) ((k, v) => list.Add(func(k, v))));
      return (IEnumerable<TResult>) list;
    }

    public static NameValueCollection Where(
      this NameValueCollection collection,
      Func<string, string, bool> func)
    {
      Assert.ArgumentNotNull((object) collection, nameof (collection));
      NameValueCollection list = new NameValueCollection();
      collection.ForEach((Action<string, string>) ((k, v) =>
      {
        if (!func(k, v))
          return;
        list[k] = v;
      }));
      return list;
    }

    public static IDictionary<string, string> ToDictionary(
      this NameValueCollection collection)
    {
      Assert.ArgumentNotNull((object) collection, nameof (collection));
      IDictionary<string, string> dictionary = (IDictionary<string, string>) new Dictionary<string, string>();
      collection.ForEach(new Action<string, string>(dictionary.Add));
      return dictionary;
    }
  }
}
