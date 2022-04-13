// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Configuration.Settings
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Security.Accounts;
using Sitecore.WFFM.Abstractions.Dependencies;

namespace Sitecore.Form.Core.Configuration
{
  public class Settings
  {
    public static string AuditAllowedTypes => Sitecore.Configuration.Settings.GetSetting(ConfigKey.AuditAllowedTypes, "|Rich Text|html|text|Multi-Line Text|Single-Line Text|memo|");

    public static bool AbortSaveActionPipelineIfSaveActionFails => Sitecore.Configuration.Settings.GetBoolSetting("WFM.SaveActionPipeline.AbortIfActionFails", false);

    public static string AbortSaveActionPipelineErrorMessage => Sitecore.Configuration.Settings.GetSetting("WFM.SaveActionPipeline.ErrorMessage", "");

    public static bool BindFormCount => Sitecore.Configuration.Settings.GetBoolSetting("WFM.BindFormCount", true);

    public static int? CommandTimeout => new int?(Sitecore.Configuration.Settings.GetIntSetting(ConfigKey.CommandTimeout, 180));

    public static string CoreDatabaseName => Sitecore.Configuration.Settings.GetSetting(ConfigKey.CoreDatabase, "core");

    public static string SharedDatabaseName => Sitecore.Configuration.Settings.GetSetting(ConfigKey.SharedDatabase, "");

    public static string WebDatabaseName => Sitecore.Configuration.Settings.GetSetting(ConfigKey.WebDatabase, "web");

    public static string CrmGateType => Sitecore.Configuration.Settings.GetSetting(ConfigKey.CrmGatewayType, string.Empty);

    public static string DefaultDateFormat => Sitecore.Configuration.Settings.GetSetting(ConfigKey.DefaultDateFormat, "D");

    public static string EmailFromAddress => Sitecore.Configuration.Settings.GetSetting(ConfigKey.EmailFromAddress);

    public static bool HideInnerError => Sitecore.Configuration.Settings.GetBoolSetting("Exception.HideInner", true);

    public static long InitialValuesCacheSize => StringUtil.ParseSizeString(Sitecore.Configuration.Settings.GetSetting(ConfigKey.InitialValuesCacheSize, "100KB"));

    public static bool InsertIdToAnalytics => Sitecore.Configuration.Settings.GetBoolSetting(ConfigKey.InsertIdToAnalytics, false);

    public static string MasterDatabaseName => Sitecore.Configuration.Settings.GetSetting(ConfigKey.MasterDatabase, "master");

    public static bool OpenFormDesignerAsModalDialog => Sitecore.Configuration.Settings.GetBoolSetting(ConfigKey.OpenFormDesignerAsModalDialod, false);

    public static double RelevantScale => Sitecore.Configuration.Settings.GetDoubleSetting(ConfigKey.RelevantScale, 0.8);

    public static bool UseThemeFromParent => Settings.GetBoolValueWithWarning(ConfigKey.UseThemeFromParent);

    public static bool IsRemoteActions => Settings.GetBoolValueWithWarning(ConfigKey.IsRemoteActions);

    public static string RemoteActionsUserName => Sitecore.Configuration.Settings.GetSetting(ConfigKey.RemoteActionsUserName, string.Empty);

    public static string RemoteActionsUserPassword => Sitecore.Configuration.Settings.GetSetting(ConfigKey.RemoteActionsUserPassword, string.Empty);

    public static bool GetBoolValueWithWarning(string settingName)
    {
      string setting = Sitecore.Configuration.Settings.GetSetting(settingName);
      bool result;
      if (bool.TryParse(setting, out result))
        return result;
      if (!string.IsNullOrEmpty(setting))
        DependenciesManager.Logger.Warn(string.Format("Setting {0} has incorrect value {1}. Should be true or false", (object) settingName, (object) setting), (object) null);
      return false;
    }

    public class AttackProtection
    {
      public static string ServerThreshold => Sitecore.Configuration.Settings.GetSetting("WFM.ServerThreshold", "1/2-100/60");

      public static string SessionThreshold => Sitecore.Configuration.Settings.GetSetting("WFM.SessionThreshold", "1/2-100/60");
    }

    public class Roles
    {
      public static string AnalyticsMaintaining => "sitecore\\Analytics Maintaining";

      public static string AnalyticsReporting => "sitecore\\Analytics Reporting";

      public static string CRMClientFormAuthor => "sitecore\\CRM Client Form Author";

      public static string ClientDeveloping => "sitecore\\Sitecore Client Developing";

      public static string ClientFormAuthor => "sitecore\\Sitecore Client Form Author";

      public static string ClientSecuring => "sitecore\\Sitecore Client Securing";

      public static bool IsAnalyticsMaintaining => User.Current.IsAdministrator || User.Current.IsInRole(Settings.Roles.AnalyticsMaintaining);

      public static bool IsAnalyticsReporting => User.Current.IsAdministrator || User.Current.IsInRole(Settings.Roles.AnalyticsReporting);

      public static string MarketerFormAuthor => "sitecore\\Sitecore Marketer Form Author";
    }
  }
}
