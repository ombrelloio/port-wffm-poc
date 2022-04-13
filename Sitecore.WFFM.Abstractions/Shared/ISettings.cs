// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Shared.ISettings
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.WFFM.Abstractions.Dependencies;

namespace Sitecore.WFFM.Abstractions.Shared
{
  [DependencyPath("wffm/settings")]
  public interface ISettings
  {
    bool IsXdbEnabled { get; }

    bool IsXdbTrackerEnabled { get; }

    bool InsertIdToAnalytics { get; }

    bool IsPreview { get; }

    bool IsPageEditorEditing { get; }

    string EmailFromAddress { get; }

    string MailServer { get; }

    string MailServerUserName { get; }

    string MailServerPassword { get; }

    string MailServerPort { get; }

    bool IsRemoteActions { get; }

    string CMInstanceName { get; }

    string InstanceName { get; }

    string RemoteActionsUserName { get; }

    string RemoteActionsUserPassword { get; }

    string ContextDatabaseName { get; }

    string MasterDatabaseName { get; }

    string CoreDatabaseName { get; }

    string GetConnectionString(string connectionName);

    string ListFieldItemsDelimiterCharacter { get; }

    string SharedDatabaseName { get; }
  }
}
