// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Dependencies.DependencyPathAttribute
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Diagnostics;
using System;

namespace Sitecore.WFFM.Abstractions.Dependencies
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
  public class DependencyPathAttribute : Attribute
  {
    public DependencyPathAttribute(string path)
    {
      Assert.ArgumentNotNullOrEmpty(path, nameof (path));
      this.Path = path;
    }

    public string Path { get; private set; }
  }
}
