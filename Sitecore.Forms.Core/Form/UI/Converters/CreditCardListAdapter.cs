// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.UI.Converters.CreditCardListAdapter
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Form.UI.Converters
{
  public class CreditCardListAdapter : ListItemsAdapter
  {
    public override IEnumerable<string> AdaptList(string value) => this.GetFriendlyListList(value).Select<string, string>((Func<string, string>) (val => ((IEnumerable<string>) val.Split(':')).FirstOrDefault<string>()?.Trim()));

    private IEnumerable<string> GetFriendlyListList(string value)
    {
      List<string> stringList1 = new List<string>();
      if (string.IsNullOrEmpty(value))
        return (IEnumerable<string>) stringList1;
      List<string> stringList2;
      if (!value.Contains("<item>"))
        stringList2 = new List<string>() { value };
      else
        stringList2 = new List<string>(ParametersUtil.XmlToStringArray(value));
      return (IEnumerable<string>) stringList2;
    }
  }
}
