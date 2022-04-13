// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Shared.IDatabaseRepository
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Data;
using Sitecore.WFFM.Abstractions.Dependencies;

namespace Sitecore.WFFM.Abstractions.Shared
{
  [DependencyPath("wffm/data/databaseRepository")]
  public interface IDatabaseRepository
  {
    Database GetDatabase(string name);
  }
}
