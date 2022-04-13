// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Shared.IContextProvider
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.WFFM.Abstractions.Actions;
using System.Collections.Generic;

namespace Sitecore.WFFM.Abstractions.Shared
{
  public interface IContextProvider
  {
    List<ExecuteResult.Failure> FormFailures { get; set; }
  }
}
