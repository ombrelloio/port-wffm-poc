﻿// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Aggregation.Processors.FormSummary.FormSummaryValue
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Analytics.Aggregation.Data.Model;

namespace Sitecore.WFFM.Analytics.Aggregation.Processors.FormSummary
{
  public class FormSummaryValue : DictionaryValue
  {
    public int Count { get; set; }

    internal static FormSummaryValue Reduce(
      FormSummaryValue left,
      FormSummaryValue right)
    {
      return right;
    }
  }
}
