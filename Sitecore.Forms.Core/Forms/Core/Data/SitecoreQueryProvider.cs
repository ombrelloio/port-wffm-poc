// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.SitecoreQueryProvider
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;

namespace Sitecore.Forms.Core.Data
{
  public class SitecoreQueryProvider : QueryProvider
  {
    public override IEnumerable<Item> SelectItems(QuerySettings query)
    {
      Assert.ArgumentNotNull((object) query, nameof (query));
      Item obj = StaticSettings.ContextDatabase.GetItem(query.ContextRoot);
      if (obj != null)
      {
        if (!string.IsNullOrEmpty(query.QueryText))
        {
          try
          {
            return (IEnumerable<Item>) (Sitecore.Data.Query.Query.SelectItems(query.QueryText, obj) ?? new Item[0]);
          }
          catch (Exception ex)
          {
            DependenciesManager.Logger.Warn("Sitecore Query: " + ex.Message, (object) this);
          }
        }
      }
      return (IEnumerable<Item>) new List<Item>();
    }
  }
}
