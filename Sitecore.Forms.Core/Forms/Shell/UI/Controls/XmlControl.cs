// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.XmlControl
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Web.UI;
using Sitecore.Web.UI.XmlControls;
using System.Web.UI;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public class XmlControl : Sitecore.Web.UI.XmlControls.XmlControl
  {
    public virtual void RenderControl(HtmlTextWriter writer) => DoRender(writer);
  }
}
