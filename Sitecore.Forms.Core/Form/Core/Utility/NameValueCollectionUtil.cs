// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.NameValueCollectionUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using System.Collections.Specialized;
using System.Web;

namespace Sitecore.Form.Core.Utility
{
  public static class NameValueCollectionUtil
  {
    public static NameValueCollection Concat(
      NameValueCollection a,
      NameValueCollection b)
    {
      Assert.ArgumentNotNull((object) a, nameof (a));
      Assert.ArgumentNotNull((object) b, nameof (b));
      NameValueCollection nameValueCollection = new NameValueCollection()
      {
        a
      };
      foreach (string key in b.Keys)
      {
        if (nameValueCollection.Get(key) == null)
          nameValueCollection[key] = b[key];
      }
      return nameValueCollection;
    }

    public static string GetString(NameValueCollection a, NameValueCollection b, bool encode) => NameValueCollectionUtil.NameValuesToString(NameValueCollectionUtil.Concat(a, b), " ", ".", encode);

    public static string GetString(
      NameValueCollection a,
      NameValueCollection b,
      string equal,
      string separator,
      bool encode)
    {
      return NameValueCollectionUtil.NameValuesToString(NameValueCollectionUtil.Concat(a, b), separator, equal, encode);
    }

    public static NameValueCollection GetNameValues(string list, bool decode) => NameValueCollectionUtil.GetNameValues(list, '.', ' ', decode);

    public static NameValueCollection GetNameValues(
      string list,
      char equal,
      char separator,
      bool decode)
    {
      NameValueCollection nameValueCollection = new NameValueCollection();
      if (!string.IsNullOrEmpty(list))
      {
        string str1 = list;
        char[] chArray1 = new char[1]{ separator };
        foreach (string str2 in str1.Split(chArray1))
        {
          char[] chArray2 = new char[1]{ equal };
          string[] strArray = str2.Split(chArray2);
          if (!string.IsNullOrEmpty(strArray[0]))
            nameValueCollection[strArray[0]] = strArray.Length > 1 ? (decode ? HttpUtility.UrlDecode(strArray[1]) : strArray[1]) : string.Empty;
        }
      }
      return nameValueCollection;
    }

    public static string NameValuesToString(
      NameValueCollection collection,
      string divider,
      string equal,
      bool encode)
    {
      if (collection == null || collection.Count <= 0)
        return string.Empty;
      Assert.ArgumentNotNullOrEmpty(divider, nameof (divider));
      Assert.ArgumentNotNullOrEmpty(equal, nameof (equal));
      string str1 = string.Empty;
      foreach (string key in collection.Keys)
      {
        string str2 = encode ? HttpUtility.UrlEncode(collection[key]) : collection[key];
        str1 = string.Join(string.Empty, new string[5]
        {
          str1,
          key,
          string.IsNullOrEmpty(str2) ? string.Empty : equal,
          str2,
          divider
        });
      }
      if (str1.Length > 0)
        str1 = StringUtil.TrimEnd(str1, divider.Length);
      return str1;
    }

    public static NameValueCollection GetNameValues(string collection) => NameValueCollectionUtil.GetNameValues(collection, true);

    public static string GetString(NameValueCollection a, NameValueCollection b) => NameValueCollectionUtil.GetString(a, b, true);

    public static string GetString(NameValueCollection collection) => NameValueCollectionUtil.GetString(collection, new NameValueCollection());
  }
}
