// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.Email
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Visual;
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  public class Email : InputControl
  {
    private static readonly string baseCssClassName = "scfEmailBorder";

    public Email(HtmlTextWriterTag tag)
      : base(tag)
    {
      this.CssClass = Email.baseCssClassName;
    }

    public Email()
      : this(HtmlTextWriterTag.Div)
    {
    }

    protected override void OnInit(EventArgs e)
    {
      this.textbox.CssClass = "scfEmailTextBox";
      this.help.CssClass = "scfEmailUsefulInfo";
      this.title.CssClass = "scfEmailLabel";
      this.generalPanel.CssClass = "scfEmailGeneralPanel";
      this.textbox.TextMode = TextBoxMode.SingleLine;
      this.Controls.AddAt(0, (Control) this.generalPanel);
      this.Controls.AddAt(0, (Control) this.title);
      this.generalPanel.Controls.AddAt(0, (Control) this.help);
      this.generalPanel.Controls.AddAt(0, (Control) this.textbox);
    }

    [DefaultValue("scfEmailBorder")]
    [VisualProperty("CSS Class:", 600)]
    [VisualFieldType(typeof (CssClassField))]
    public new string CssClass
    {
      get => base.CssClass;
      set => base.CssClass = value;
    }

    [VisualCategory("Validation")]
    [VisualProperty("Maximum Length:", 1)]
    [DefaultValue(256)]
    public int MaxLength
    {
      get => this.textbox.MaxLength;
      set => this.textbox.MaxLength = value;
    }
  }
}
