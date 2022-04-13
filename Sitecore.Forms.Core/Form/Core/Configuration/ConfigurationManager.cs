// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Configuration.ConfigurationManager
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using System.Configuration;

namespace Sitecore.Form.Core.Configuration
{
  public class ConfigurationManager
  {
    private static ConfigurationManager currentManager = new ConfigurationManager();

    public static ConfigurationManager Current => ConfigurationManager.currentManager;

    public static void SetCurrentConfigurationManager(ConfigurationManager manager)
    {
      Assert.ArgumentNotNull((object) manager, nameof (manager));
      ConfigurationManager.currentManager = manager;
    }

    public virtual ConnectionStringSettings GetConnectionString(
      string connectionString)
    {
      return string.IsNullOrEmpty(connectionString) ? (ConnectionStringSettings) null : System.Configuration.ConfigurationManager.ConnectionStrings[connectionString];
    }
  }
}
