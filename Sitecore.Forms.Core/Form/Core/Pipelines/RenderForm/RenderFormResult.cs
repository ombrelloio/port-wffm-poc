// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.RenderForm.RenderFormResult
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;

namespace Sitecore.Form.Core.Pipelines.RenderForm
{
  public class RenderFormResult
  {
    private string _firstPart;
    private string _lastPart;

    public RenderFormResult()
      : this(string.Empty, string.Empty)
    {
    }

    public RenderFormResult(string start)
    {
      Assert.ArgumentNotNull((object) start, nameof (start));
      this._firstPart = start;
      this._lastPart = string.Empty;
    }

    public RenderFormResult(string start, string end)
    {
      Assert.ArgumentNotNull((object) start, nameof (start));
      Assert.ArgumentNotNull((object) end, nameof (end));
      this._firstPart = start;
      this._lastPart = end;
    }

    public override string ToString() => this.FirstPart + this.LastPart;

    public bool Empty => string.IsNullOrEmpty(this.FirstPart) && string.IsNullOrEmpty(this.LastPart);

    public string FirstPart
    {
      get => this._firstPart;
      set
      {
        Assert.ArgumentNotNull((object) value, nameof (value));
        this._firstPart = value;
      }
    }

    public string LastPart
    {
      get => this._lastPart;
      set
      {
        Assert.ArgumentNotNull((object) value, nameof (value));
        this._lastPart = value;
      }
    }
  }
}
