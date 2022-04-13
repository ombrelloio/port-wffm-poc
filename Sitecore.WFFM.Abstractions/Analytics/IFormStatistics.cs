// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Analytics.IFormStatistics
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Newtonsoft.Json;
using System;

namespace Sitecore.WFFM.Abstractions.Analytics
{
  public interface IFormStatistics
  {
    [JsonProperty("formId")]
    Guid FormId { get; set; }

    [JsonProperty("submitsCount")]
    int SubmitsCount { get; set; }

    [JsonProperty("dropouts")]
    int Dropouts { get; set; }

    [JsonProperty("success")]
    int SuccessSubmits { get; set; }

    [JsonProperty("visits")]
    int Visits { get; set; }

    [JsonProperty("title")]
    string Title { get; set; }
  }
}
