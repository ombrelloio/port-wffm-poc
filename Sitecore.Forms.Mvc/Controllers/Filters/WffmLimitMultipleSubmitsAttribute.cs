// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Controllers.Filters.WffmLimitMultipleSubmitsAttribute
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Web.Caching;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Controllers.Filters
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  internal class WffmLimitMultipleSubmitsAttribute : FilterAttribute, IAuthorizationFilter
  {
    public void OnAuthorization(AuthorizationContext filterContext)
    {
      Cache cache = ((ControllerContext) filterContext).HttpContext.Cache;
      if (string.IsNullOrEmpty(((ControllerContext) filterContext).HttpContext.Request.Headers["X-RequestVerificationToken"]))
        return;
      int intervalInSeconds = Settings.LimitMultipleSubmits_IntervalInSeconds;
      if (intervalInSeconds <= 0)
        return;
      string md5Hash = this.CalculateMD5Hash(((ControllerContext) filterContext).HttpContext.Request.Headers["X-RequestVerificationToken"]);
      if (cache[md5Hash] != null)
      {
        if (cache[md5Hash] == (object) string.Empty)
        {
          cache.Remove(md5Hash);
          cache.Add(md5Hash, (object) "attempted", (CacheDependency) null, DateTime.Now.AddSeconds((double) intervalInSeconds), Cache.NoSlidingExpiration, CacheItemPriority.Normal, (CacheItemRemovedCallback) null);
          throw new SecurityException("There was an attempt to do multiple submits within a time interval, specified in the \"WFM.LimitMultipleSubmits.IntervalInSeconds\" setting!");
        }
        filterContext.Result = (ActionResult) new HttpUnauthorizedResult();
      }
      else
        cache.Add(md5Hash, (object) "", (CacheDependency) null, DateTime.Now.AddSeconds((double) intervalInSeconds), Cache.NoSlidingExpiration, CacheItemPriority.Normal, (CacheItemRemovedCallback) null);
    }

    public string CalculateMD5Hash(string input)
    {
      byte[] hash = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(input));
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < hash.Length; ++index)
        stringBuilder.Append(hash[index].ToString("X2"));
      return stringBuilder.ToString();
    }
  }
}
