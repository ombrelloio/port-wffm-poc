// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Data.IFieldItem
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Collections;
using Sitecore.Data;
using System.Collections.Generic;

namespace Sitecore.WFFM.Abstractions.Data
{
  public interface IFieldItem : IFieldTypeItem
  {
    string Conditions { get; }

    string FieldDisplayName { get; }

    FieldCollection Fields { get; }

    bool IsSaveToStorage { get; }

    bool IsTag { get; }

    Dictionary<string, string> LocalizedParametersDictionary { get; }

    Dictionary<string, string> ParametersDictionary { get; }

    Dictionary<string, string> MvcValidationMessages { get; }

    string Title { get; }

    ID TypeID { get; }

    string GetSubFieldTitle(string key);
  }
}
