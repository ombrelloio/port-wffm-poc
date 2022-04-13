// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Visual.CategoryComparer
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.Collections.Generic;

namespace Sitecore.Form.Core.Visual
{
  internal class CategoryComparer : IComparer<VisualPropertyInfo>
  {
    public int Compare(VisualPropertyInfo x, VisualPropertyInfo y)
    {
      if (x == null)
        return y == null ? 0 : -1;
      if (y == null)
        return 1;
      if (x.CategorySortOrder != -1 && y.CategorySortOrder != -1)
        return x.CategorySortOrder.CompareTo(y.CategorySortOrder);
      if (x.CategorySortOrder != -1)
        return x.CategorySortOrder.CompareTo(x.CategorySortOrder + 1);
      return y.CategorySortOrder != -1 ? (y.CategorySortOrder + 1).CompareTo(y.CategorySortOrder) : x.Category.CompareTo(y.Category);
    }
  }
}
