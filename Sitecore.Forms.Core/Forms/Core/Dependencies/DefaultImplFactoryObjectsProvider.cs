// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Dependencies.DefaultImplFactoryObjectsProvider
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;

namespace Sitecore.Forms.Core.Dependencies
{
  [Serializable]
  public class DefaultImplFactoryObjectsProvider : IFactoryObjectsProvider
  {
    private readonly IReflectionProvider reflectionProvider;
    private readonly ILogger logger;

    public DefaultImplFactoryObjectsProvider(IReflectionProvider reflectionProvider, ILogger logger)
    {
      Assert.IsNotNull((object) reflectionProvider, nameof (reflectionProvider));
      Assert.IsNotNull((object) logger, nameof (logger));
      this.reflectionProvider = reflectionProvider;
      this.logger = logger;
    }

    public T CreateObject<T>(string configPath, bool assert) where T : class => DependenciesManager.CorrectCreateObject<T>(configPath, assert);

    public object CreateObject(string configPath, bool assert) => Factory.CreateObject(configPath, assert);

    public T CreateAction<T>(string factoryObjectName, string assemblyName, string className) where T : IAction
    {
      T obj = default (T);
      if (!string.IsNullOrEmpty(factoryObjectName))
      {
        try
        {
          obj = (T) this.CreateObject(factoryObjectName, true);
        }
        catch (Exception ex)
        {
          this.logger.Warn(ex.Message, (object) this);
        }
        return obj;
      }
      if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(className))
        obj = (T) this.reflectionProvider.CreateObject(assemblyName, className, new object[0]);
      return obj;
    }

    public T CreateAction<T>(IActionItem actionItem) where T : IAction => this.CreateAction<T>(actionItem.FactoryObjectName, actionItem.Assembly, actionItem.Class);
  }
}
