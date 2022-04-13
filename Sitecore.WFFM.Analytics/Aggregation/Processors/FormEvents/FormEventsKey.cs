// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Aggregation.Processors.FormEvents.FormEventsKey
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Analytics.Aggregation.Data.Model;
using System;

namespace Sitecore.WFFM.Analytics.Aggregation.Processors.FormEvents
{
  public class FormEventsKey : DictionaryKey
  {
    public Guid ContactId { get; set; }

    public Guid InteractionId { get; set; }

    public DateTime InteractionStartDate { get; set; }

    public Guid PageEventDefinitionId { get; set; }

    public Guid FormId { get; set; }
  }
}
