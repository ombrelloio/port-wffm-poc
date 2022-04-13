// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Extensions.ListExtensions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.Collections.Generic;

namespace Sitecore.Forms.Core.Extensions
{
  public static class ListExtensions
  {
    public static bool IsNullOrEmpty<T>(this IList<T> list) => list == null || list.Count == 0;
  }
}
