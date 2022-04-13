// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Model.FieldValueStatistics
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.WFFM.Abstractions.Analytics;

namespace Sitecore.WFFM.Analytics.Model
{
  public class FieldValueStatistics : IFieldValuesStatistic
  {
    public string FieldValue { get; set; }

    public int Count { get; set; }
  }
}
