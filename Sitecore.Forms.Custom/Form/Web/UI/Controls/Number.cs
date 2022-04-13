// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.Number
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
  public class Number : InputControl
  {
    private static readonly string baseCssClassName = "scfNumberBorder";
    private float minValue;
    private float maxValue;

    public Number(HtmlTextWriterTag tag)
      : base(tag)
    {
      this.maxValue = (float) int.MaxValue;
      this.minValue = 0.0f;
      this.CssClass = Number.baseCssClassName;
    }

    public Number()
      : this(HtmlTextWriterTag.Div)
    {
    }

    protected override void OnInit(EventArgs e)
    {
      this.textbox.CssClass = "scfNumberTextBox";
      this.help.CssClass = "scfNumberUsefulInfo";
      this.title.CssClass = "scfNumberLabel";
      this.generalPanel.CssClass = "scfNumberGeneralPanel";
      this.textbox.TextMode = TextBoxMode.SingleLine;
      this.Controls.AddAt(0, (Control) this.generalPanel);
      this.Controls.AddAt(0, (Control) this.title);
      this.generalPanel.Controls.AddAt(0, (Control) this.help);
      this.generalPanel.Controls.AddAt(0, (Control) this.textbox);
    }

    [VisualProperty("Maximum Value:", 2000)]
    [DefaultValue(2147483647)]
    [VisualCategory("Validation")]
    public string MaximumValue
    {
      get => (double) this.maxValue < 2147483648.0 && (double) this.maxValue % (double) (int) this.maxValue == 0.0 ? ((int) this.maxValue).ToString() : this.maxValue.ToString();
      set => this.maxValue = float.Parse(value);
    }

    [VisualProperty("Minimum Value:", 1000)]
    [DefaultValue(0)]
    [VisualCategory("Validation")]
    public string MinimumValue
    {
      get => (double) this.minValue % (double) (int) this.minValue == 0.0 ? ((int) this.minValue).ToString() : this.minValue.ToString();
      set => this.minValue = float.Parse(value);
    }

    [DefaultValue("scfNumberBorder")]
    [VisualProperty("CSS Class:", 600)]
    [VisualFieldType(typeof (CssClassField))]
    public new string CssClass
    {
      get => base.CssClass;
      set => base.CssClass = value;
    }
  }
}
