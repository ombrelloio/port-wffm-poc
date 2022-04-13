// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.XPathQueryProvider
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using System.Collections.Generic;

namespace Sitecore.Forms.Core.Data
{
  public class XPathQueryProvider : QueryProvider
  {
    public override IEnumerable<Item> SelectItems(QuerySettings query)
    {
      Assert.ArgumentNotNull((object) query, nameof (query));
      Item contextNode = StaticSettings.ContextDatabase.GetItem(query.ContextRoot);
      return contextNode != null ? (IEnumerable<Item>)Sitecore.Form.Core.Utility.Utils.EvaluateRealXPath(contextNode, query.QueryText) ?? (IEnumerable<Item>) new Item[0] : (IEnumerable<Item>) new List<Item>();
    }
  }
}
