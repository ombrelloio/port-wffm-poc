// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Dependencies.FieldData
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.XConnect;
using System;

namespace Sitecore.WFFM.Analytics.Dependencies
{
  public class FieldData : Sitecore.XConnect.Event
    {
    private string SomeProp { get; set; }

    public FieldData(Guid definitionId, DateTime timestamp)
      : base(definitionId, timestamp)
    {
    }
  }
}
