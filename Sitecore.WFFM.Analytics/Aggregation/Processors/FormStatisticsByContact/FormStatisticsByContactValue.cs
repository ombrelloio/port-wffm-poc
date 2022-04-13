// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Aggregation.Processors.FormStatisticsByContact.FormStatisticsByContactValue
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Analytics.Aggregation.Data.Model;
using System;

namespace Sitecore.WFFM.Analytics.Aggregation.Processors.FormStatisticsByContact
{
  public class FormStatisticsByContactValue : DictionaryValue
  {
    public int Submits { get; set; }

    public int Success { get; set; }

    public int Dropouts { get; set; }

    public int Failures { get; set; }

    public int FinalResult { get; set; }

    public DateTime LastInteractionDate { get; set; }

    public int Value { get; set; }

    public int Visits { get; set; }

    internal static FormStatisticsByContactValue Reduce(
      FormStatisticsByContactValue left,
      FormStatisticsByContactValue right)
    {
      FormStatisticsByContactValue statisticsByContactValue = new FormStatisticsByContactValue()
      {
        Submits = left.Submits + right.Submits,
        Success = left.Success + right.Success,
        Dropouts = left.Dropouts + right.Dropouts,
        Failures = left.Failures + right.Failures,
        LastInteractionDate = left.LastInteractionDate,
        FinalResult = left.FinalResult,
        Value = right.Value,
        Visits = left.Visits + right.Visits
      };
      if (left.LastInteractionDate <= right.LastInteractionDate)
      {
        statisticsByContactValue.LastInteractionDate = right.LastInteractionDate;
        statisticsByContactValue.FinalResult = right.FinalResult;
      }
      return statisticsByContactValue;
    }
  }
}
