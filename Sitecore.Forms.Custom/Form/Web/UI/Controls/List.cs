// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.List
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
  public class List : ListControl
  {
    private static readonly string baseCssClassName = "scfListBoxBorder";
    protected ListBox list = new ListBox();

    public List()
      : this(HtmlTextWriterTag.Div)
    {
    }

    private List(HtmlTextWriterTag tag)
      : base(tag)
    {
      this.CssClass = List.baseCssClassName;
      this.list.Rows = 4;
    }

    protected override void OnInit(EventArgs e)
    {
      this.KeepHiddenValue = false;
      base.OnInit(e);
      this.list.CssClass = "scfListBox";
      this.help.CssClass = "scfListBoxUsefulInfo";
      this.title.CssClass = "scfListBoxLabel";
      this.generalPanel.CssClass = "scfListBoxGeneralPanel";
      this.Controls.AddAt(0, (Control) this.generalPanel);
      this.Controls.AddAt(0, (Control) this.title);
      this.generalPanel.Controls.AddAt(0, (Control) this.help);
      this.generalPanel.Controls.AddAt(0, (Control) this.list);
    }

    [VisualProperty("Selection Mode:", 400)]
    [VisualCategory("List")]
    [DefaultValue("Single")]
    [VisualFieldType(typeof (SelectionModeField))]
    public string SelectionMode
    {
      set => this.list.SelectionMode = (ListSelectionMode) Enum.Parse(typeof (ListSelectionMode), value, true);
      get => this.list.SelectionMode.ToString();
    }

    [VisualProperty("Rows:", 450)]
    [DefaultValue(4)]
    public int Rows
    {
      set => this.list.Rows = value;
      get => this.list.Rows;
    }

    public override string ID
    {
      get => this.list.ID;
      set
      {
        this.title.ID = value + "text";
        this.list.ID = value;
        base.ID = value + "scope";
      }
    }

    protected override System.Web.UI.WebControls.ListControl InnerListControl => (System.Web.UI.WebControls.ListControl) this.list;

    [DefaultValue("scfListBoxBorder")]
    [VisualProperty("CSS Class:", 600)]
    [VisualFieldType(typeof (CssClassField))]
    public new string CssClass
    {
      get => base.CssClass;
      set => base.CssClass = value;
    }
  }
}
