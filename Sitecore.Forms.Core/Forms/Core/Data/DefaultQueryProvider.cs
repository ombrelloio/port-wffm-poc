// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.DefaultQueryProvider
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Globalization;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Sitecore.Forms.Core.Data
{
  public class DefaultQueryProvider : QueryProvider
  {
    public override NameValueCollection Select(QuerySettings query)
    {
      Assert.ArgumentNotNull((object) query, nameof (query));
      NameValueCollection nameValueCollection = new NameValueCollection();
      string[] strArray = query.QueryText.Split(new char[1]
      {
        '|'
      }, 2);
      string str = (string) null;
      query.LocalizedTexts.TryGetValue(Language.Current.GetLowerCaseName(), out str);
      if (strArray.Length == 2)
        nameValueCollection[strArray[0]] = !query.ShowOnlyValue ? (string.IsNullOrEmpty(str) ? strArray[1] : str) : strArray[0];
      else
        nameValueCollection[query.QueryText] = query.ShowOnlyValue ? query.QueryText : (string.IsNullOrEmpty(str) ? query.QueryText : str);
      return nameValueCollection;
    }

    public override IEnumerable<Item> SelectItems(QuerySettings query) => (IEnumerable<Item>) new List<Item>();
  }
}
