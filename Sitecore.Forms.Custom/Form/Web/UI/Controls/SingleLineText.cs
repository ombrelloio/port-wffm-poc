// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.SingleLineText
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Visual;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof (IDesigner))]
  public class SingleLineText : RegexInputControl
  {
    private static readonly string baseCssClassName = "scfSingleLineTextBorder";

    public SingleLineText(HtmlTextWriterTag tag)
      : base(tag)
    {
      this.MaxLength = 256;
      this.MinLength = 0;
      this.CssClass = SingleLineText.baseCssClassName;
    }

    public SingleLineText()
      : this(HtmlTextWriterTag.Div)
    {
    }

    protected override void OnInit(EventArgs e)
    {
      this.textbox.CssClass = "scfSingleLineTextBox";
      this.help.CssClass = "scfSingleLineTextUsefulInfo";
      this.generalPanel.CssClass = "scfSingleLineGeneralPanel";
      this.title.CssClass = "scfSingleLineTextLabel";
      this.textbox.TextMode = TextBoxMode.SingleLine;
      this.Controls.AddAt(0, (Control) this.generalPanel);
      this.Controls.AddAt(0, (Control) this.title);
      this.generalPanel.Controls.AddAt(0, (Control) this.help);
      this.generalPanel.Controls.AddAt(0, (Control) this.textbox);
    }

    [VisualProperty("Maximum Length:", 2000)]
    [DefaultValue(256)]
    [VisualCategory("Validation")]
    public int MaxLength
    {
      set => this.textbox.MaxLength = value;
      get => this.textbox.MaxLength;
    }

    [VisualProperty("Minimum Length:", 1000)]
    [DefaultValue(0)]
    [VisualCategory("Validation")]
    public int MinLength { set; get; }

    [DefaultValue("scfSingleLineTextBorder")]
    [VisualProperty("CSS Class:", 600)]
    [VisualFieldType(typeof (CssClassField))]
    public new string CssClass
    {
      get => base.CssClass;
      set => base.CssClass = value;
    }
  }
}
