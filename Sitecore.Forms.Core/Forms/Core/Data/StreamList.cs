// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.StreamList
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace Sitecore.Forms.Core.Data
{
  internal class StreamList : List<Stream>, IDisposable
  {
    private bool isDisposed;

    public void Dispose()
    {
      if (this.isDisposed)
        return;
      foreach (Stream stream in (List<Stream>) this)
        stream?.Close();
      this.isDisposed = true;
    }
  }
}
