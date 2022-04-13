// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Data.PageCriteria
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

namespace Sitecore.WFFM.Abstractions.Data
{
  public class PageCriteria
  {
    public PageCriteria()
    {
      this.PageIndex = 0;
      this.PageSize = 20;
      this.Sorting = (SortCriteria) null;
    }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public SortCriteria Sorting { get; set; }
  }
}
