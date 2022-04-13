// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Controls.Html.HtmlBaseControl
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Controls.Html
{
  public abstract class HtmlBaseControl : WebControl
  {
    protected HtmlBaseControl()
    {
    }

    protected HtmlBaseControl(string tag)
      : base(tag)
    {
    }

    protected HtmlBaseControl(HtmlTextWriterTag tag)
      : base(tag)
    {
    }

    public string Class
    {
      get => this.CssClass;
      set => this.CssClass = value;
    }

    private new string CssClass
    {
      get => this.Attributes["class"];
      set => this.Attributes["class"] = value;
    }
  }
}
