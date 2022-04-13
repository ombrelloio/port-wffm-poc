// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Shared.IReflectionProvider
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

namespace Sitecore.WFFM.Abstractions.Shared
{
  public interface IReflectionProvider
  {
    object CreateObject(string assembly, string className, object[] parameters);
  }
}
