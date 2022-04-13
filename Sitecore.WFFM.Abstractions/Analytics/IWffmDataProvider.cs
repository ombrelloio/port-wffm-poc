// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Analytics.IWffmDataProvider
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.WFFM.Abstractions.Data;
using System;
using System.Collections.Generic;

namespace Sitecore.WFFM.Abstractions.Analytics
{
  public interface IWffmDataProvider
  {
    IFormStatistics GetFormStatistics(Guid formId);

    IEnumerable<IFormFieldStatistics> GetFormFieldsStatistics(
      Guid formId);

    IEnumerable<IFormContactsResult> GetFormsStatisticsByContact(
      Guid formId,
      PageCriteria pageCriteria);

    IEnumerable<FormData> GetFormData(Guid formId);

    void InsertFormData(FormData form);
  }
}
