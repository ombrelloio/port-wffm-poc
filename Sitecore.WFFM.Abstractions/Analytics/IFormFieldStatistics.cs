// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Analytics.IFormFieldStatistics
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Sitecore.WFFM.Abstractions.Analytics
{
  public interface IFormFieldStatistics
  {
    Guid FieldId { get; set; }

    [JsonProperty("fieldName")]
    string FieldName { get; set; }

    [JsonProperty("count")]
    int Count { get; set; }

    [JsonProperty("isList")]
    bool IsList { get; set; }

    List<IFieldValuesStatistic> Values { get; set; }
  }
}
