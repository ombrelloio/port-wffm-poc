// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Shared.IFactoryObjectsProvider
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.WFFM.Abstractions.Actions;

namespace Sitecore.WFFM.Abstractions.Shared
{
  public interface IFactoryObjectsProvider
  {
    T CreateObject<T>(string configPath, bool assert) where T : class;

    object CreateObject(string configPath, bool assert);

    T CreateAction<T>(string factoryObjectName, string assemblyName, string className) where T : IAction;

    T CreateAction<T>(IActionItem actionItem) where T : IAction;
  }
}
