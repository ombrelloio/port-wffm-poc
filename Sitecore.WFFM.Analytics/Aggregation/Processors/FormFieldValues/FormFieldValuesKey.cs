// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Aggregation.Processors.FormFieldValues.FormFieldValuesKey
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Analytics.Aggregation.Data.Model;
using System;

namespace Sitecore.WFFM.Analytics.Aggregation.Processors.FormFieldValues
{
  public class FormFieldValuesKey : DictionaryKey
  {
    public FormFieldValuesKey(Guid submitId, Guid fieldId, string fieldName, string fieldValue)
    {
      this.SubmitId = submitId;
      this.FieldId = fieldId;
      this.FieldName = fieldName;
      this.FieldValue = fieldValue;
    }

    public Guid SubmitId { get; }

    public Guid FieldId { get; }

    public string FieldName { get; }

    public string FieldValue { get; }
  }
}
