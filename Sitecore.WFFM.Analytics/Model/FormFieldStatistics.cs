// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Model.FormFieldStatistics
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.WFFM.Abstractions.Analytics;
using System;
using System.Collections.Generic;

namespace Sitecore.WFFM.Analytics.Model
{
  public class FormFieldStatistics : IFormFieldStatistics
  {
    public Guid FieldId { get; set; }

    public string FieldName { get; set; }

    public int Count { get; set; }

    public bool IsList { get; set; }

    public List<IFieldValuesStatistic> Values { get; set; }
  }
}
