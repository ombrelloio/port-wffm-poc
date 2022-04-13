// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Dependencies.DependenciesManager
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Configuration;
using Sitecore.DependencyInjection;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Shared;
using Sitecore.WFFM.Abstractions.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.WFFM.Abstractions.Dependencies
{
    public static class DependenciesManager
    {
        private static readonly object ActionExecutorLocker = new object();
        private static readonly object SettingsLocker = new object();
        private static readonly object LoggerLocker = new object();
        private static readonly object ResourceManagerLocker = new object();
        private static readonly object ItemRepositoryLocker = new object();
        private static readonly object FactoryObjectsProviderLocker = new object();
        private static readonly object DataProviderLocker = new object();
        private static readonly object RequirementsCheckerLocker = new object();
        private static readonly object AnalyticsTrackerLocker = new object();
        private static readonly object FieldProviderLocker = new object();
        private static IActionExecutor _actionExecutor;
        private static ISettings _settings;
        private static ILogger _logger;
        private static IResourceManager _resourceManager;
        private static IItemRepository _itemRepository;
        private static IFactoryObjectsProvider _factoryObjectsProvider;
        private static IWffmDataProvider _dataProvider;
        private static IRequirementsChecker _requirementsChecker;
        private static IAnalyticsTracker _analyticsTracker;
        private static IFieldProvider _fieldProvider;

        public static IActionExecutor ActionExecutor
        {
            get
            {
                IActionExecutor actionExecutor = DependenciesManager._actionExecutor;
                if (actionExecutor == null)
                {
                    lock (DependenciesManager.ActionExecutorLocker)
                    {
                        if (DependenciesManager._actionExecutor == null)
                        {
                            actionExecutor = DependenciesManager.CorrectCreateObject<IActionExecutor>("wffm/wffmActionExecutor", false);
                            DependenciesManager._actionExecutor = actionExecutor;
                        }
                        else
                            actionExecutor = DependenciesManager._actionExecutor;
                    }
                }
                return actionExecutor;
            }
        }

        public static ISettings Settings
        {
            get
            {
                ISettings settings = DependenciesManager._settings;
                if (settings == null)
                {
                    lock (DependenciesManager.SettingsLocker)
                    {
                        if (DependenciesManager._settings == null)
                        {
                            settings = DependenciesManager.CorrectCreateObject<ISettings>("wffm/settings", false);
                            DependenciesManager._settings = settings;
                        }
                        else
                            settings = DependenciesManager._settings;
                    }
                }
                return settings;
            }
        }

        public static ILogger Logger
        {
            get
            {
                ILogger logger = DependenciesManager._logger;
                if (logger == null)
                {
                    lock (DependenciesManager.LoggerLocker)
                    {
                        if (DependenciesManager._logger == null)
                        {
                            logger = DependenciesManager.CorrectCreateObject<ILogger>("wffm/logger", false);
                            DependenciesManager._logger = logger;
                        }
                        else
                            logger = DependenciesManager._logger;
                    }
                }
                return logger;
            }
        }

        public static IResourceManager ResourceManager
        {
            get
            {
                IResourceManager resourceManager = DependenciesManager._resourceManager;
                if (resourceManager == null)
                {
                    lock (DependenciesManager.ResourceManagerLocker)
                    {
                        if (DependenciesManager._resourceManager == null)
                        {
                            resourceManager = DependenciesManager.CorrectCreateObject<IResourceManager>("wffm/resourceManager", false);
                            DependenciesManager._resourceManager = resourceManager;
                        }
                        else
                            resourceManager = DependenciesManager._resourceManager;
                    }
                }
                return resourceManager;
            }
        }

        public static IItemRepository ItemRepository
        {
            get
            {
                IItemRepository itemRepository = DependenciesManager._itemRepository;
                if (itemRepository == null)
                {
                    lock (DependenciesManager.ItemRepositoryLocker)
                    {
                        if (DependenciesManager._itemRepository == null)
                        {
                            itemRepository = DependenciesManager.CorrectCreateObject<IItemRepository>("wffm/itemRepository", false);
                            DependenciesManager._itemRepository = itemRepository;
                        }
                        else
                            itemRepository = DependenciesManager._itemRepository;
                    }
                }
                return itemRepository;
            }
        }

        public static IFactoryObjectsProvider FactoryObjectsProvider
        {
            get
            {
                IFactoryObjectsProvider factoryObjectsProvider = DependenciesManager._factoryObjectsProvider;
                if (factoryObjectsProvider == null)
                {
                    lock (DependenciesManager.FactoryObjectsProviderLocker)
                    {
                        if (DependenciesManager._factoryObjectsProvider == null)
                        {
                            factoryObjectsProvider = DependenciesManager.CorrectCreateObject<IFactoryObjectsProvider>("wffm/factoryObjectsProvider", false);
                            DependenciesManager._factoryObjectsProvider = factoryObjectsProvider;
                        }
                        else
                            factoryObjectsProvider = DependenciesManager._factoryObjectsProvider;
                    }
                }
                return factoryObjectsProvider;
            }
        }

        public static IWffmDataProvider DataProvider
        {
            get
            {
                IWffmDataProvider dataProvider = DependenciesManager._dataProvider;
                if (dataProvider == null)
                {
                    lock (DependenciesManager.DataProviderLocker)
                    {
                        if (DependenciesManager._dataProvider == null)
                        {
                            dataProvider = DependenciesManager.CorrectCreateObject<IWffmDataProvider>("wffm/analytics/formsDataProvider", false);
                            DependenciesManager._dataProvider = dataProvider;
                        }
                        else
                            dataProvider = DependenciesManager._dataProvider;
                    }
                }
                return dataProvider;
            }
        }

        public static IRequirementsChecker RequirementsChecker
        {
            get
            {
                IRequirementsChecker requirementsChecker = DependenciesManager._requirementsChecker;
                if (requirementsChecker == null)
                {
                    lock (DependenciesManager.RequirementsCheckerLocker)
                    {
                        if (DependenciesManager._requirementsChecker == null)
                        {
                            requirementsChecker = DependenciesManager.CorrectCreateObject<IRequirementsChecker>("wffm/requirementsChecker", false);
                            DependenciesManager._requirementsChecker = requirementsChecker;
                        }
                        else
                            requirementsChecker = DependenciesManager._requirementsChecker;
                    }
                }
                return requirementsChecker;
            }
        }

        public static IAnalyticsTracker AnalyticsTracker
        {
            get
            {
                IAnalyticsTracker analyticsTracker = DependenciesManager._analyticsTracker;
                if (analyticsTracker == null)
                {
                    lock (DependenciesManager.AnalyticsTrackerLocker)
                    {
                        if (DependenciesManager._analyticsTracker == null)
                        {
                            analyticsTracker = DependenciesManager.CorrectCreateObject<IAnalyticsTracker>("wffm/analytics/analyticsTracker", false);
                            DependenciesManager._analyticsTracker = analyticsTracker;
                        }
                        else
                            analyticsTracker = DependenciesManager._analyticsTracker;
                    }
                }
                return analyticsTracker;
            }
        }

        public static IFieldProvider FieldProvider
        {
            get
            {
                IFieldProvider fieldProvider = DependenciesManager._fieldProvider;
                if (fieldProvider == null)
                {
                    lock (DependenciesManager.FieldProviderLocker)
                    {
                        if (DependenciesManager._fieldProvider == null)
                        {
                            fieldProvider = DependenciesManager.CorrectCreateObject<IFieldProvider>("wffm/fieldProvider", false);
                            DependenciesManager._fieldProvider = fieldProvider;
                        }
                        else
                            fieldProvider = DependenciesManager._fieldProvider;
                    }
                }
                return fieldProvider;
            }
        }

        public static IParametersUtil ParametersUtil => DependenciesManager.CorrectCreateObject<IParametersUtil>("wffm/parametersUtil", false);

        public static ITranslationProvider TranslationProvider => DependenciesManager.CorrectCreateObject<ITranslationProvider>("wffm/translationProvider", false);

        public static IConvertionUtil ConvertionUtil => DependenciesManager.CorrectCreateObject<IConvertionUtil>("wffm/convertionUtil", false);

        public static IWebUtil WebUtil => DependenciesManager.CorrectCreateObject<IWebUtil>("wffm/webUtil", false);

        public static IFormRegistryUtil FormRegistryUtil => DependenciesManager.CorrectCreateObject<IFormRegistryUtil>("wffm/formRegistryUtil", false);

        public static IFacetFactory FacetFactory => DependenciesManager.CorrectCreateObject<IFacetFactory>("wffm/analytics/facetFactory", false);

        public static IMailSender MailSender => Factory.CreateObject("wffm/mailSender", false) as IMailSender;

        public static T Resolve<T>() where T : class => ((IEnumerable<object>)typeof(T).GetCustomAttributes(typeof(DependencyPathAttribute), false)).FirstOrDefault<object>() is DependencyPathAttribute dependencyPathAttribute ? DependenciesManager.Resolve<T>(dependencyPathAttribute.Path) : throw new Exception(Sitecore.StringExtensions.StringExtensions.FormatWith("Could not create instance of the {0} class. Path not found", new object[1]
        {
      (object) typeof (T)
        }));

        public static T Resolve<T>(string path) where T : class
        {
            if (Factory.CreateObject(path, false) is T service
                      && !((object)service is string))
                return service;


            if (ServiceLocator.ServiceProvider.GetService(typeof(T)) is T service2
                          && !((object)service2 is string)
               )
                return service2;
            throw new Exception(Sitecore.StringExtensions.StringExtensions.FormatWith("Could not create instance of the {0} class.", new object[1]
            {
          (object) typeof (T)
            }));
        }

        public static T CorrectCreateObject<T>(string configPath, bool assert) where T : class
        {
            object obj = Factory.CreateObject(configPath, assert);
            if (obj == null || obj is string)
                obj = (object)(ServiceLocator.ServiceProvider.GetService(typeof(T)) as T);
            if (obj != null && string.IsNullOrEmpty(obj.ToString()))
                obj = (object)null;
            return obj as T;
        }
    }
}
