// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Configuration.RemoteSettings
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.Collections.Specialized;
using System.Configuration;

namespace Sitecore.Form.Core.Configuration
{
  public class RemoteSettings
  {
    public static readonly string AuthenticationType = "authentication type";
    public static readonly string CrmUrlKey = "crm:url";
    public static readonly string EnvironmentName = "environment";
    public static readonly string OrganizationName = "organization";
    public static readonly string PartnerName = "partner";
    public static readonly string PasswordKey = "password";
    public static readonly string TimeOutKey = "timeout";
    public static readonly string UrlKey = "url";
    public static readonly string UseTicket = "use ticket";
    public static readonly string UserId = "user id";
    public static readonly string UserKey = "user";

    internal static NameValueCollection GetServiceParameters(
      string connectionName)
    {
      if (!string.IsNullOrEmpty(connectionName))
      {
        ConnectionStringSettings connectionString = ConfigurationManager.Current.GetConnectionString(connectionName);
        if (connectionString != null)
        {
          NameValueCollection nameValues = StringUtil.GetNameValues(connectionString.ConnectionString, '=', ';');
          foreach (string allKey in nameValues.AllKeys)
          {
            string str = nameValues[allKey];
            nameValues.Remove(allKey);
            nameValues.Add(allKey.ToLower(), str);
          }
          return nameValues;
        }
      }
      return (NameValueCollection) null;
    }
  }
}
