// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Data.ValidationItemAdapter
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Collections;
using Sitecore.Form.Core.Utility;
using System.Collections.Generic;

namespace Sitecore.Form.Core.Data
{
  public class ValidationItemAdapter
  {
    public static IEnumerable<Pair<string, string>> AdaptItem(ValidationItem item)
    {
      List<Pair<string, string>> pairList1 = new List<Pair<string, string>>(ParametersUtil.XmlToPairArray(item.GlobalParameters));
      pairList1.Add(new Pair<string, string>("Display", item.Display.ToString()));
      pairList1.Add(new Pair<string, string>("EnableClientScript", item.EnableClientScript.ToString()));
      List<Pair<string, string>> pairList2 = pairList1;
      if (!string.IsNullOrEmpty(item.ErrorMessage))
        pairList2.Add(new Pair<string, string>("ErrorMessage", item.ErrorMessage));
      if (!string.IsNullOrEmpty(item.Text))
        pairList2.Add(new Pair<string, string>("Text", item.Text));
      if (!string.IsNullOrEmpty(item.ValidationExpression))
        pairList2.Add(new Pair<string, string>("ValidationExpression", item.ValidationExpression));
      return (IEnumerable<Pair<string, string>>) pairList2;
    }
  }
}
