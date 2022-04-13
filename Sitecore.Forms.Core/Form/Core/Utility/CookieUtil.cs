// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.CookieUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.Web;

namespace Sitecore.Form.Core.Utility
{
  public class CookieUtil
  {
    private static readonly string KeyPrefix = "scWffm_";

    public static void SetCookie(string cookieKey, string cookieValue, TimeSpan? expires = null)
    {
      HttpCookie cookie1 = HttpContext.Current.Request.Cookies[CookieUtil.KeyPrefix + cookieKey];
      if (cookie1 != null)
      {
        cookie1.Value = cookieValue;
        if (expires.HasValue)
          cookie1.Expires = DateTime.Now.Add(expires.Value);
        HttpContext.Current.Response.Cookies.Add(cookie1);
      }
      else
      {
        HttpCookie cookie2 = new HttpCookie(CookieUtil.KeyPrefix + cookieKey, cookieValue);
        if (expires.HasValue)
          cookie2.Expires = DateTime.Now.Add(expires.Value);
        HttpContext.Current.Response.Cookies.Add(cookie2);
      }
    }

    public static string GetCookieValue(string cookieName) => HttpContext.Current.Request.Cookies[CookieUtil.KeyPrefix + cookieName]?.Value ?? (string) null;
  }
}
