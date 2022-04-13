// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Controls.Html.HtmlLiteral
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.Web.UI;

namespace Sitecore.Form.Core.Controls.Html
{
  [ToolboxData("<div runat=\"server\"></div>")]
  [PersistChildren(true)]
  public class HtmlLiteral : HtmlBaseControl
  {
    public HtmlLiteral()
      : base(HtmlTextWriterTag.Div)
    {
    }

    public HtmlLiteral(string text)
      : this()
    {
      this.Text = text;
    }

    [PersistenceMode(PersistenceMode.EncodedInnerDefaultProperty)]
    public string Text
    {
      get => (this.ViewState["text"] ?? (object) string.Empty) as string;
      set => this.ViewState["text"] = (object) value;
    }

    protected override void RenderChildren(HtmlTextWriter writer) => writer.Write(this.Text);
  }
}
