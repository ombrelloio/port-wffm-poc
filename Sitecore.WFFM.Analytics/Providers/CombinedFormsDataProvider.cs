// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Providers.CombinedFormsDataProvider
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Data;
using System;
using System.Collections.Generic;

namespace Sitecore.WFFM.Analytics.Providers
{
  public class CombinedFormsDataProvider : IWffmDataProvider
  {
    private readonly IWffmDataProvider mainDataProvider;
    private readonly IWffmDataProvider alternativeDataProvider;

    public CombinedFormsDataProvider(
      IWffmDataProvider mainDataProvider,
      IWffmDataProvider alternativeDataProvider)
    {
      Assert.IsNotNull((object) mainDataProvider, nameof (mainDataProvider));
      Assert.IsNotNull((object) alternativeDataProvider, nameof (alternativeDataProvider));
      this.mainDataProvider = mainDataProvider;
      this.alternativeDataProvider = alternativeDataProvider;
    }

    public virtual IEnumerable<FormData> GetFormData(Guid formId) => this.alternativeDataProvider.GetFormData(formId);

    public virtual void InsertFormData(FormData form) => this.alternativeDataProvider.InsertFormData(form);

    public virtual IFormStatistics GetFormStatistics(Guid formId) => this.mainDataProvider.GetFormStatistics(formId);

    public virtual IEnumerable<IFormFieldStatistics> GetFormFieldsStatistics(
      Guid formId)
    {
      return this.mainDataProvider.GetFormFieldsStatistics(formId);
    }

    public virtual IEnumerable<IFormContactsResult> GetFormsStatisticsByContact(
      Guid formId,
      PageCriteria pageCriteria)
    {
      return this.mainDataProvider.GetFormsStatisticsByContact(formId, pageCriteria);
    }
  }
}
