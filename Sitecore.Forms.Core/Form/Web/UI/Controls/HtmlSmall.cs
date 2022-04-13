// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.HtmlSmall
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Sitecore.Form.Web.UI.Controls
{
  public class HtmlSmall : HtmlContainerControl
  {
    private string text = string.Empty;

    public HtmlSmall()
      : this("small")
    {
    }

    public HtmlSmall(string tag)
      : base(tag)
    {
    }

    protected override void RenderChildren(HtmlTextWriter writer) => writer.Write(this.Text);

    public string Class
    {
      get => this.Attributes["class"];
      set => this.Attributes["class"] = value ?? string.Empty;
    }

    public string Text
    {
      get => this.text;
      set => this.text = value ?? string.Empty;
    }
  }
}
