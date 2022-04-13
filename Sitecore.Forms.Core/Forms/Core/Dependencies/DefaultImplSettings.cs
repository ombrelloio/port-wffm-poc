// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Dependencies.DefaultImplSettings
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.WFFM.Abstractions.Shared;
using Sitecore.WFFM.Abstractions.Wrappers;
using System;

namespace Sitecore.Forms.Core.Dependencies
{
  [Serializable]
  public class DefaultImplSettings : ISettings
  {
    private readonly ISitecoreContextWrapper sitecoreContextWrapper;

    public DefaultImplSettings(ISitecoreContextWrapper sitecoreContextWrapper)
    {
      Assert.ArgumentNotNull((object) sitecoreContextWrapper, nameof (sitecoreContextWrapper));
      this.sitecoreContextWrapper = sitecoreContextWrapper;
    }

    public bool IsXdbEnabled => Sitecore.Configuration.Settings.GetBoolSetting(ConfigKey.XdbEnabled, true);

    public bool IsXdbTrackerEnabled => Sitecore.Configuration.Settings.GetBoolSetting(ConfigKey.XdbTrackerEnabled, true);

    public bool InsertIdToAnalytics => Sitecore.Configuration.Settings.GetBoolSetting(ConfigKey.InsertIdToAnalytics, false);

    public bool IsPreview => this.sitecoreContextWrapper.PageModeIsPreview;

    public bool IsPageEditorEditing => this.sitecoreContextWrapper.PageModeIsExperienceEditorEditing;

    public string EmailFromAddress => Sitecore.Configuration.Settings.GetSetting(ConfigKey.EmailFromAddress);

    public string MailServer => Sitecore.Configuration.Settings.GetSetting(ConfigKey.MailServer);

    public string MailServerUserName => Sitecore.Configuration.Settings.GetSetting(ConfigKey.MailServerUserName);

    public string MailServerPassword => Sitecore.Configuration.Settings.GetSetting(ConfigKey.MailServerPassword);

    public string MailServerPort => Sitecore.Configuration.Settings.GetSetting(ConfigKey.MailServerPort);

    public bool IsRemoteActions => Sitecore.Form.Core.Configuration.Settings.GetBoolValueWithWarning(ConfigKey.IsRemoteActions);

    public string CMInstanceName => Sitecore.Configuration.Settings.GetSetting(ConfigKey.CMInstanceName, string.Empty);

    public string InstanceName => Sitecore.Configuration.Settings.GetSetting(ConfigKey.InstanceName, string.Empty);

    public string RemoteActionsUserName => Sitecore.Configuration.Settings.GetSetting(ConfigKey.RemoteActionsUserName, string.Empty);

    public string RemoteActionsUserPassword => Sitecore.Configuration.Settings.GetSetting(ConfigKey.RemoteActionsUserPassword, string.Empty);

    public string ContextDatabaseName
    {
      get
      {
        if (this.sitecoreContextWrapper.Database == null)
          return this.MasterDatabaseName;
        return this.sitecoreContextWrapper.DatabaseName == this.CoreDatabaseName ? this.sitecoreContextWrapper.ContentDatabaseName : this.sitecoreContextWrapper.DatabaseName;
      }
    }

    public string MasterDatabaseName => Sitecore.Configuration.Settings.GetSetting(ConfigKey.MasterDatabase, "master");

    public string CoreDatabaseName => Sitecore.Configuration.Settings.GetSetting(ConfigKey.CoreDatabase, "core");

    public string GetConnectionString(string connectionName) => System.Configuration.ConfigurationManager.ConnectionStrings[connectionName] == null ? string.Empty : System.Configuration.ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;

    public string ListFieldItemsDelimiterCharacter => Sitecore.Configuration.Settings.GetSetting(ConfigKey.ListFieldItemsDelimiterCharacter, string.Empty);

    public string SharedDatabaseName => Sitecore.Configuration.Settings.GetSetting(ConfigKey.SharedDatabase, "");
  }
}
