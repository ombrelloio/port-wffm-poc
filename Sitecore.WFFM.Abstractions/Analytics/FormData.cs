// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Analytics.FormData
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using System;
using System.Collections.Generic;

namespace Sitecore.WFFM.Abstractions.Analytics
{
  [Serializable]
  public class FormData
  {
    public FormData() => this.Fields = (IEnumerable<FieldData>) new List<FieldData>();

    public Guid FormID { get; set; }

    public Guid Id { get; set; }

    public Guid ContactId { get; set; }

    public Guid InteractionId { get; set; }

    public DateTime Timestamp { get; set; }

    public IEnumerable<FieldData> Fields { get; set; }
  }
}
