// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.UrlStringExtensions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Text;
using System.Collections.Specialized;

namespace Sitecore.Form.Core.Utility
{
  public static class UrlStringExtensions
  {
    public static void Append(this UrlString url, string query)
    {
      NameValueCollection nameValues = NameValueCollectionUtil.GetNameValues(query.StartsWith("?") ? query.Substring(1) : query, '=', '&', false);
      url.Append(nameValues);
    }
  }
}
