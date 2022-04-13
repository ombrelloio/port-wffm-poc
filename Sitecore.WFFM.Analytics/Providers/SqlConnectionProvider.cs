// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Providers.SqlConnectionProvider
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.WFFM.Abstractions.Shared;
using System.Data;
using System.Data.SqlClient;

namespace Sitecore.WFFM.Analytics.Providers
{
  public class SqlConnectionProvider : IDbConnectionProvider
  {
    public IDbConnection GetConnection(string connectionString) => (IDbConnection) new SqlConnection(connectionString);
  }
}
