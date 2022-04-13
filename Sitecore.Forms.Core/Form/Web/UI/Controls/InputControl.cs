// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.InputControl
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Attributes;
using Sitecore.WFFM.Abstractions.Actions;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  public abstract class InputControl : ValidateControl, IHasTitle
  {
    protected Panel generalPanel = new Panel();
    protected TextBox textbox = new TextBox();
    protected Label title = new Label();

    protected InputControl()
    {
    }

    protected InputControl(HtmlTextWriterTag tag)
      : base(tag)
    {
    }

    [Bindable(true)]
    [Description("Title")]
    public string Title
    {
      get => this.title.Text;
      set => this.title.Text = value;
    }

    public override string DefaultValue
    {
      set => this.Text = value;
    }

    public override string ID
    {
      get => this.textbox.ID;
      set
      {
        this.title.ID = value + "_text";
        this.textbox.ID = value;
        base.ID = value + "_scope";
        this.title.AssociatedControlID = this.textbox.ID;
      }
    }

    public override ControlResult Result
    {
      get => new ControlResult(this.ControlName, (object) this.textbox.Text, (string) null);
      set => this.textbox.Text = value.Value.ToString();
    }

    [VisualProperty("Default Value:", 100)]
    [System.ComponentModel.DefaultValue("")]
    [Localize]
    public virtual string Text
    {
      get => this.textbox.Text;
      set => this.textbox.Text = value;
    }

    protected override Control InnerValidatorContainer => (Control) this.generalPanel;

    protected override Control ValidatorContainer => (Control) this;

    public override void RenderControl(HtmlTextWriter writer) => this.DoRender(writer);

    protected virtual void DoRender(HtmlTextWriter writer) => base.RenderControl(writer);
  }
}
