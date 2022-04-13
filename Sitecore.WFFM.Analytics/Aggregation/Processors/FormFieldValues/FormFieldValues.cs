// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Aggregation.Processors.FormFieldValues.FormFieldValues
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Analytics.Aggregation.Data.Model;
using Sitecore.Diagnostics;
using System;

namespace Sitecore.WFFM.Analytics.Aggregation.Processors.FormFieldValues
{
  public class FormFieldValues : Dimension<FormFieldValuesKey, FormFieldValuesValue>
  {
    public void Add(Guid submitId, Guid fieldId, string fieldName, string fieldValue)
    {
      Assert.ArgumentNotNull((object) submitId, nameof (submitId));
      Assert.ArgumentNotNull((object) fieldId, nameof (fieldId));
      Assert.ArgumentNotNull((object) fieldName, nameof (fieldName));
      if (string.IsNullOrEmpty(fieldValue))
        return;
      this.Add(new FormFieldValuesKey(submitId, fieldId, fieldName, fieldValue), new FormFieldValuesValue());
    }
  }
}
