// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Controls.Html.Legend
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Controls.Html
{
  [ToolboxData("<legend runat=\"server\"></div>")]
  public class Legend : HtmlBaseControl
  {
    public Legend()
      : base(HtmlTextWriterTag.Legend)
    {
    }

    public Legend(string title)
      : this()
    {
      this.Title = title;
    }

    [PersistenceMode(PersistenceMode.EncodedInnerDefaultProperty)]
    public string Title
    {
      get => (this.ViewState["title"] ?? (object) string.Empty) as string;
      set => this.ViewState["title"] = (object) value;
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.Controls.Add((Control) new Literal()
      {
        Text = this.Title
      });
    }
  }
}
