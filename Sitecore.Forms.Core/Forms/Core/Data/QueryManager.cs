// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.QueryManager
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Utility;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Sitecore.Forms.Core.Data
{
  public class QueryManager
  {
    private static readonly ProviderHelper<QueryProvider, QueryProviderCollection> _data = new ProviderHelper<QueryProvider, QueryProviderCollection>("queryselector");

    public static QueryProvider Provider => QueryManager._data.Provider;

    public static QueryProviderCollection Providers => QueryManager._data.Providers;

    public static NameValueCollection Select(IEnumerable<QuerySettings> queries)
    {
      Assert.ArgumentNotNull((object) queries, nameof (queries));
      NameValueCollection a = new NameValueCollection();
      foreach (QuerySettings query in queries)
      {
        if (query != null)
          a = NameValueCollectionUtil.Concat(a, QueryManager.Select(query));
      }
      return a;
    }

    public static NameValueCollection Select(QuerySettings query)
    {
      Assert.ArgumentNotNull((object) query, nameof (query));
      QueryProvider provider = QueryManager.Providers[query.QueryType];
      return provider != null ? provider.Select(query) : new NameValueCollection();
    }

    public static IEnumerable<Item> SelectItems(QuerySettings query)
    {
      Assert.ArgumentNotNull((object) query, nameof (query));
      QueryProvider provider = QueryManager.Providers[query.QueryType];
      return provider != null ? provider.SelectItems(query) : (IEnumerable<Item>) new List<Item>();
    }
  }
}
