// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Shared.IFieldProvider
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Data;
using Sitecore.WFFM.Abstractions.Data;
using System.Collections.Generic;

namespace Sitecore.WFFM.Abstractions.Shared
{
  public interface IFieldProvider
  {
    string GetAdaptedResult(ID fieldID, object value);

    string GetAdaptedValue(ID fieldId, string value);

    string GetAdaptedValue(string fieldId, string value);

    string GetAdaptedValue(IFieldItem fieldItem, string value);

    string GetFieldDisplayName(string key);

    bool FieldCanListAdapt(IFieldItem field);

    IEnumerable<string> ListAdapt(IFieldItem field, string value);
  }
}
