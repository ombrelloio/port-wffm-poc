// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.SessionUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Web;
using System.Web;

namespace Sitecore.Form.Core.Utility
{
  public class SessionUtil
  {
    private static readonly string keyPrefix = "scWfm_";

    public static string GetSessionKey() => SessionUtil.GetSessionKey(ID.NewID);

    public static string GetSessionKey(ID id)
    {
      Assert.ArgumentNotNull((object) id, nameof (id));
      return SessionUtil.GetSessionKey(((object) id).ToString());
    }

    public static string GetSessionKey(string id)
    {
      Assert.ArgumentNotNullOrEmpty(id, nameof (id));
      return string.Join(string.Empty, new string[2]
      {
        SessionUtil.keyPrefix,
        id
      });
    }

    public static bool IsSessionKey(string key)
    {
      Assert.ArgumentNotNull((object) key, nameof (key));
      return key.StartsWith(SessionUtil.keyPrefix);
    }

    public static T GetSessionValue<T>(ID key)
    {
      Assert.ArgumentNotNull((object) key, nameof (key));
      return SessionUtil.GetSessionValue<T>(((object) key).ToString());
    }

    public static T GetSessionValue<T>(string key) => (T) Sitecore.Web.WebUtil.GetSessionValue(SessionUtil.GetSessionKey(key));

    public static void ClearSessionValue(string key)
    {
      string sessionKey = SessionUtil.GetSessionKey(key);
      if (HttpContext.Current.Session[sessionKey] == null)
        return;
      HttpContext.Current.Session.Remove(sessionKey);
    }

    public static void SetSessionValue(ID key, object value) => Sitecore.Web.WebUtil.SetSessionValue(SessionUtil.GetSessionKey(key), value);

    public static void SetSessionValue(string key, object value) => Sitecore.Web.WebUtil.SetSessionValue(SessionUtil.GetSessionKey(key), value);
  }
}
