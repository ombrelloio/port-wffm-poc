// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.RootQueryProvider
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Utility;
using System.Collections.Generic;

namespace Sitecore.Forms.Core.Data
{
  public class RootQueryProvider : QueryProvider
  {
    public override IEnumerable<Item> SelectItems(QuerySettings query)
    {
      Assert.ArgumentNotNull((object) query, nameof (query));
      return Sitecore.Form.Core.Utility.ItemUtil.GetChildren(string.IsNullOrEmpty(query.QueryText) ? ((object) ItemIDs.RootID).ToString() : query.QueryText);
    }
  }
}
