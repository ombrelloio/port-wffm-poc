// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Model.FormStatistics
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.WFFM.Abstractions.Analytics;
using System;

namespace Sitecore.WFFM.Analytics.Model
{
  public class FormStatistics : IFormStatistics
  {
    public Guid FormId { get; set; }

    public int SubmitsCount { get; set; }

    public int Dropouts { get; set; }

    public int SuccessSubmits { get; set; }

    public int Visits { get; set; }

    public string Title { get; set; }
  }
}
