﻿// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Analytics.ServerEvent
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using System;

namespace Sitecore.WFFM.Abstractions.Analytics
{
  public class ServerEvent : IEventBase
  {
    public string FieldID { get; set; }

    public string FormID { get; set; }

    public string Type { get; set; }

    public string Value { get; set; }

    public Guid EventId { get; set; }

    public string Text { get; set; }

    public string Data { get; set; }
  }
}
