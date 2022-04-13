// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.MultipleLineText
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Visual;
using Sitecore.WFFM.Abstractions.Actions;
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  public class MultipleLineText : SingleLineText
  {
    private static readonly string baseCssClassName = "scfMultipleLineTextBorder";

    public MultipleLineText(HtmlTextWriterTag tag)
      : base(tag)
    {
      this.MaxLength = 512;
      this.MinLength = 0;
      this.textbox.Rows = 4;
      this.CssClass = MultipleLineText.baseCssClassName;
    }

    public MultipleLineText()
      : this(HtmlTextWriterTag.Div)
    {
    }

    protected override void OnInit(EventArgs e)
    {
      this.textbox.CssClass = "scfMultipleLineTextBox";
      this.help.CssClass = "scfMultipleLineTextUsefulInfo";
      this.title.CssClass = "scfMultipleLineTextLabel";
      this.generalPanel.CssClass = "scfMultipleLineGeneralPanel";
      this.textbox.TextMode = TextBoxMode.MultiLine;
      this.Controls.AddAt(0, (Control) this.generalPanel);
      this.Controls.AddAt(0, (Control) this.title);
      this.generalPanel.Controls.AddAt(0, (Control) this.help);
      this.generalPanel.Controls.AddAt(0, (Control) this.textbox);
    }

    public override ControlResult Result => new ControlResult(this.ControlName, (object) this.textbox.Text, "multipleline");

    [VisualProperty("Rows:", 250)]
    [DefaultValue(4)]
    public int Rows
    {
      set => this.textbox.Rows = value;
      get => this.textbox.Rows;
    }

    [VisualProperty("Maximum Length:", 2000)]
    [DefaultValue(512)]
    [VisualCategory("Validation")]
    public new int MaxLength
    {
      set => base.MaxLength = value;
      get => base.MaxLength;
    }

    [DefaultValue("scfMultipleLineTextBorder")]
    [VisualProperty("CSS Class:", 600)]
    [VisualFieldType(typeof (CssClassField))]
    public new string CssClass
    {
      get => base.CssClass;
      set => base.CssClass = value;
    }
  }
}
