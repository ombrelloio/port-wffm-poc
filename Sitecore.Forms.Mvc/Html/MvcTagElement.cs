// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Html.MvcTagElement
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using System;

namespace Sitecore.Forms.Mvc.Html
{
  public class MvcTagElement : IDisposable
  {
    private readonly Action end;

    public MvcTagElement()
    {
    }

    public MvcTagElement(Action begin, Action end)
    {
      Assert.ArgumentNotNull((object) begin, nameof (begin));
      Assert.ArgumentNotNull((object) end, nameof (end));
      this.end = end;
      begin();
    }

    ~MvcTagElement() => this.Dispose(false);

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this.end == null)
        return;
      this.end();
    }
  }
}
