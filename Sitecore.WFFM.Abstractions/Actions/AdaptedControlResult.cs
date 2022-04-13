// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Actions.AdaptedControlResult
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;

namespace Sitecore.WFFM.Abstractions.Actions
{
  [Serializable]
  public class AdaptedControlResult : ControlResult
  {
    public AdaptedControlResult(
      ControlResult result,
      IFieldProvider fieldProvider,
      bool simpleAdapt = false)
    {
      Assert.ArgumentNotNull((object) result, nameof (result));
      Assert.ArgumentNotNull((object) fieldProvider, nameof (fieldProvider));
      this.FieldName = result.FieldName;
      this.Parameters = result.Parameters;
      this.FieldID = result.FieldID;
      this.Secure = result.Secure;
      ID fieldID;
      if (!ID.TryParse(result.FieldID, out fieldID))
        fieldID = ID.Null;
      this.Value = simpleAdapt ? (result.Value ?? (object) string.Empty).ToString() : fieldProvider.GetAdaptedResult(fieldID, result.Value);
    }

    public AdaptedControlResult(ControlResult result)
      : this(result, DependenciesManager.FieldProvider)
    {
    }

    public AdaptedControlResult(ControlResult result, bool simpleAdapt)
      : this(result, DependenciesManager.FieldProvider, simpleAdapt)
    {
    }

    public new string Value { get; private set; }
  }
}
