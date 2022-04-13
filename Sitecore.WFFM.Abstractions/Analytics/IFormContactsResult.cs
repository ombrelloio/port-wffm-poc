// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Analytics.IFormContactsResult
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Newtonsoft.Json;
using System;

namespace Sitecore.WFFM.Abstractions.Analytics
{
  public interface IFormContactsResult
  {
    [JsonProperty("contactId")]
    Guid ContactId { get; set; }

    [JsonProperty("contactName")]
    string ContactName { get; set; }

    [JsonProperty("value")]
    long Value { get; set; }

    [JsonProperty("visits")]
    long Visits { get; set; }

    [JsonProperty("submitCount")]
    int SubmissionAttempts { get; set; }

    [JsonProperty("dropouts")]
    int Dropouts { get; set; }

    [JsonProperty("failures")]
    int Failures { get; set; }

    [JsonProperty("dateTime")]
    string DateTime { get; set; }

    [JsonProperty("finalResult")]
    string FinalResult { get; set; }
  }
}
