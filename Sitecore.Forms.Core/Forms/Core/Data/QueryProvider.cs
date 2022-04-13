// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.QueryProvider
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Form.Core.Utility;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;

namespace Sitecore.Forms.Core.Data
{
  public abstract class QueryProvider : ProviderBase
  {
    public virtual NameValueCollection Select(QuerySettings query)
    {
      NameValueCollection nameValueCollection = new NameValueCollection();
      foreach (Item selectItem in this.SelectItems(query))
      {
        string itemValue = Sitecore.Form.Core.Utility.ItemUtil.GetItemValue(selectItem, query.ValueFieldName);
        if (!string.IsNullOrEmpty(itemValue))
        {
          string str = Sitecore.Form.Core.Utility.ItemUtil.GetItemValue(selectItem, query.TextFieldName);
          if (string.IsNullOrEmpty(str))
            str = selectItem.DisplayName;
          nameValueCollection[itemValue] = string.IsNullOrEmpty(str) ? itemValue : str;
        }
      }
      return nameValueCollection;
    }

    public abstract IEnumerable<Item> SelectItems(QuerySettings query);
  }
}
