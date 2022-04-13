// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.Checkbox
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Visual;
using Sitecore.WFFM.Abstractions.Actions;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  [ValidationProperty("Value")]
  public class Checkbox : ValidateControl, IHasTitle
  {
    private static readonly string baseCssClassName = "scfCheckboxBorder";
    protected CheckBox checkbox = new CheckBox();
    protected Panel generalPanel;

    public Checkbox()
      : this(HtmlTextWriterTag.Div)
    {
    }

    protected Checkbox(HtmlTextWriterTag tag)
      : base(tag)
    {
      this.CssClass = Checkbox.baseCssClassName;
      this.generalPanel = new Panel();
    }

    public override void RenderControl(HtmlTextWriter writer) => this.DoRender(writer);

    protected virtual void DoRender(HtmlTextWriter writer) => base.RenderControl(writer);

    protected override void OnInit(EventArgs e)
    {
      this.checkbox.CssClass = "scfCheckbox";
      this.help.CssClass = "scfCheckboxUsefulInfo";
      this.generalPanel.CssClass = "scfCheckBoxListGeneralPanel";
      this.Controls.AddAt(0, (Control) this.generalPanel);
      this.generalPanel.Controls.AddAt(0, (Control) this.help);
      this.generalPanel.Controls.AddAt(0, (Control) this.checkbox);
      base.OnInit(e);
    }

    public override string ID
    {
      get => base.ID;
      set
      {
        this.checkbox.ID = value + "checkbox";
        base.ID = value;
      }
    }

    [VisualProperty("Checked:", 100)]
    [System.ComponentModel.DefaultValue("No")]
    [VisualFieldType(typeof (BooleanField))]
    public string Checked
    {
      set => this.checkbox.Checked = value == "Yes" || value == "1";
      get => this.checkbox.Checked.ToString();
    }

    public override string DefaultValue
    {
      set => this.Checked = value;
    }

    public override ControlResult Result
    {
      get => new ControlResult(this.ControlName, this.checkbox.Checked ? (object) "1" : (object) "0", (string) null);
      set => this.checkbox.Checked = (bool) value.Value;
    }

    public string Title
    {
      set => this.checkbox.Text = value;
      get => this.checkbox.Text;
    }

    [System.ComponentModel.DefaultValue("scfCheckboxBorder")]
    [VisualProperty("Css Class:", 600)]
    [VisualFieldType(typeof (CssClassField))]
    public new string CssClass
    {
      get => base.CssClass;
      set => base.CssClass = value;
    }

    protected override Control ValidatorContainer => (Control) this;

    protected override Control InnerValidatorContainer => (Control) this.generalPanel;

    public string Value => !this.checkbox.Checked ? string.Empty : "1";
  }
}
