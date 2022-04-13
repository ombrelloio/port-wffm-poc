// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Model.FormContactsResult
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.WFFM.Abstractions.Analytics;
using System;

namespace Sitecore.WFFM.Analytics.Model
{
  public class FormContactsResult : IFormContactsResult
  {
    public FormContactsResult()
    {
      this.SubmissionAttempts = 0;
      this.Dropouts = 0;
      this.Failures = 0;
    }

    public Guid ContactId { get; set; }

    public string ContactName { get; set; }

    public long Value { get; set; }

    public long Visits { get; set; }

    public int SubmissionAttempts { get; set; }

    public int Dropouts { get; set; }

    public int Failures { get; set; }

    public string DateTime { get; set; }

    public string FinalResult { get; set; }
  }
}
