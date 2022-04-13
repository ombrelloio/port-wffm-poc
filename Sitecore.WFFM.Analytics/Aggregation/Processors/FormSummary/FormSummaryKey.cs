// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Aggregation.Processors.FormSummary.FormSummaryKey
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Analytics.Aggregation.Data.Model;
using System;

namespace Sitecore.WFFM.Analytics.Aggregation.Processors.FormSummary
{
  public class FormSummaryKey : DictionaryKey
  {
    public Guid Id { get; set; }

    public Guid ContactId { get; set; }

    public Guid FormId { get; set; }

    public Guid InteractionId { get; set; }

    public DateTime Created { get; set; }
  }
}
