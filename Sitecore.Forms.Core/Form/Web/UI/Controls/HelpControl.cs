// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.HelpControl
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Visual;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  public abstract class HelpControl : CssClassControl
  {
    protected Label help = new Label();

    protected HelpControl()
      : this(HtmlTextWriterTag.Span)
    {
    }

    protected HelpControl(HtmlTextWriterTag tag)
      : base(tag)
    {
      this.help.Style["display"] = "none";
    }

    protected override void OnInit(EventArgs e) => this.Controls.Add((Control) this.help);

    [VisualProperty("Help:", 500)]
    [VisualCategory("Appearance")]
    [VisualFieldType(typeof (TextAreaField))]
    [Localize]
    public string Information
    {
      set
      {
        this.help.Text = value ?? string.Empty;
        if (!string.IsNullOrEmpty(value))
          this.help.Style.Remove("display");
        else
          this.help.Style["display"] = "none";
      }
      get => this.help.Text;
    }
  }
}
