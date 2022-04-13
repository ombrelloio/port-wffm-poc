// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.HtmlNodeCollectionExtensions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using HtmlAgilityPack;
using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;

namespace Sitecore.Form.Core.Utility
{
  public static class HtmlNodeCollectionExtensions
  {
    public static void ForEach(this HtmlNodeCollection collection, Action<HtmlNode> action)
    {
      Assert.ArgumentNotNull((object) collection, nameof (collection));
      foreach (HtmlNode htmlNode in (IEnumerable<HtmlNode>) collection)
      {
        if (!string.IsNullOrEmpty(htmlNode.Name))
          action(htmlNode);
      }
    }
  }
}
