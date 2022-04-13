// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Data.SortCriteria
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Sitecore.WFFM.Abstractions.Data
{
  [Serializable]
  public class SortCriteria
  {
    public SortCriteria(string field, SortDirection sortDirection)
    {
      this.Field = field;
      this.Direction = sortDirection;
    }

    [JsonProperty("direction")]
    [JsonConverter(typeof (StringEnumConverter))]
    public SortDirection Direction { get; set; }

    [JsonProperty("field")]
    public string Field { get; set; }

    public override string ToString() => string.Format("{0} {1}", (object) this.Field, this.Direction == SortDirection.Asc ? (object) "ASC" : (object) "DESC");
  }
}
