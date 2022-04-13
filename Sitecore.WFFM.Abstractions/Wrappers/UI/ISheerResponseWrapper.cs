// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Wrappers.UI.ISheerResponseWrapper
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Web.UI.HtmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;

namespace Sitecore.WFFM.Abstractions.Wrappers.UI
{
  [DependencyPath("wffm/wrappers/sheerResponseWrapper")]
  public interface ISheerResponseWrapper
  {
    void ShowModalDialog(string url, bool response);

    ClientCommand SetDialogValue(string value);
  }
}
