// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Shared.IResourceManager
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.WFFM.Abstractions.Dependencies;
using System.IO;

namespace Sitecore.WFFM.Abstractions.Shared
{
  [DependencyPath("wffm/resourceManager")]
  public interface IResourceManager
  {
    string GetString(string resIdentifier);

    string Localize(string resIdentifier);

    string Localize(string resIdentifier, params string[] parameters);

    UnmanagedMemoryStream GetObject(string resIdentifier);
  }
}
