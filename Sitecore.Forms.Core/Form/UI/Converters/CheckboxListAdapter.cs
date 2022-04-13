// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.UI.Converters.CheckboxListAdapter
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Client.Submit;
using System.Collections;
using System.Collections.Generic;

namespace Sitecore.Form.UI.Converters
{
  public class CheckboxListAdapter : IListAdapter
  {
    public List<string> AdaptList(IList list)
    {
      List<string> stringList = list as List<string>;
      Assert.ArgumentNotNull((object) stringList, "items");
      return stringList;
    }

    public IEnumerable<string> AdaptList(string value) => (IEnumerable<string>) new List<string>()
    {
      value
    };
  }
}
