// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Wrappers.UI.SheerResponseWrapper
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Diagnostics;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;

namespace Sitecore.WFFM.Abstractions.Wrappers.UI
{
  [DependencyPath("wffm/wrappers/sheerResponseWrapper")]
  [Serializable]
  public class SheerResponseWrapper : ISheerResponseWrapper
  {
    public virtual void ShowModalDialog(string url, bool response)
    {
      Assert.ArgumentNotNull((object) url, nameof (url));
      SheerResponse.ShowModalDialog(url, response);
    }

    public virtual ClientCommand SetDialogValue(string value)
    {
      Assert.ArgumentNotNull((object) value, nameof (value));
      return SheerResponse.SetDialogValue(value);
    }
  }
}
