// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Client.Submit.IListAdapter
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.Collections;
using System.Collections.Generic;

namespace Sitecore.Form.Core.Client.Submit
{
  public interface IListAdapter
  {
    List<string> AdaptList(IList list);

    IEnumerable<string> AdaptList(string value);
  }
}
