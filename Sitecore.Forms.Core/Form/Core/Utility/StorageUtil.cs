// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.StorageUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using System.Web;

namespace Sitecore.Form.Core.Utility
{
  internal class StorageUtil
  {
    private static readonly string keyPrefix = "scWfm_";

    public static string GetKey() => StorageUtil.GetKey(ID.NewID);

    public static string GetKey(ID id)
    {
      Assert.ArgumentNotNull((object) id, nameof (id));
      return StorageUtil.GetKey(((object) id).ToString());
    }

    public static string GetKey(string id)
    {
      Assert.ArgumentNotNullOrEmpty(id, nameof (id));
      return string.Join(string.Empty, new string[2]
      {
        StorageUtil.keyPrefix,
        id
      });
    }

    public static bool IsKey(string key)
    {
      Assert.ArgumentNotNull((object) key, nameof (key));
      return key.StartsWith(StorageUtil.keyPrefix);
    }

    public static T GetValue<T>(ID key)
    {
      Assert.ArgumentNotNull((object) key, nameof (key));
      return StorageUtil.GetValue<T>(((object) key).ToString());
    }

    public static T GetValue<T>(string key) => HttpContext.Current.Session[StorageUtil.GetKey(key)] != null ? (T) HttpContext.Current.Session[StorageUtil.GetKey(key)] : (T) HttpContext.Current.Application[StorageUtil.GetKey(key)];

    public static void ClearValue(string key)
    {
      string key1 = StorageUtil.GetKey(key);
      if (HttpContext.Current.Session[key1] != null)
        HttpContext.Current.Session.Remove(key1);
      if (HttpContext.Current.Application.Get(key1) == null)
        return;
      HttpContext.Current.Application.Remove(key1);
    }

    public static void SetValue(ID key, object value)
    {
      if (HttpContext.Current.Session.IsNewSession)
        HttpContext.Current.Application[StorageUtil.GetKey(key)] = value;
      else
        HttpContext.Current.Session[StorageUtil.GetKey(key)] = value;
    }

    public static void SetValue(string key, object value)
    {
      if (HttpContext.Current.Session.IsNewSession)
        HttpContext.Current.Application[StorageUtil.GetKey(key)] = value;
      else
        HttpContext.Current.Session[StorageUtil.GetKey(key)] = value;
    }
  }
}
