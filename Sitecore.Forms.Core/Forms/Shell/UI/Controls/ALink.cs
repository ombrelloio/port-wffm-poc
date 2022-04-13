// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.ALink
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public class ALink : XmlControl
  {
    protected override void DoRender(HtmlTextWriter output)
    {
      output.Write("<a id='" + ((Control) this).ID + "' ");
      if (!string.IsNullOrEmpty(((WebControl) this).Attributes["onclick"]))
        output.Write(" href=\"#\"");
      ((WebControl) this).Attributes.Render(output);
      output.Write(" style='" + ((WebControl) this).Style.Value + "'>");
      (this).RenderChildren(output);
      output.Write("</a>");
    }
  }
}
