// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Analytics.FieldData
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using System;
using System.Collections.Generic;

namespace Sitecore.WFFM.Abstractions.Analytics
{
  [Serializable]
  public class FieldData
  {
    public string Data { get; set; }

    public Guid Id { get; set; }

    public Guid FieldId { get; set; }

    public Guid FormId { get; set; }

    public string FieldName { get; set; }

    public string Value { get; set; }

    public List<string> Values { get; set; }
  }
}
