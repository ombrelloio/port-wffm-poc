// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.StringList
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.Collections.Generic;

namespace Sitecore.Forms.Core.Data
{
  public class StringList : List<string>
  {
    public StringList(IEnumerable<string> collection)
      : base(collection)
    {
    }

    public StringList(int capacity)
      : base(capacity)
    {
    }

    public StringList()
    {
    }
  }
}
