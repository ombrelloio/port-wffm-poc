// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.DropList
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
  public class DropList : ListControl
  {
    private static readonly string baseCssClassName = "scfDropListBorder";
    protected DropDownList droplist = new DropDownList();
    private bool emptyLine = true;

    public DropList()
      : this(HtmlTextWriterTag.Div)
    {
    }

    private DropList(HtmlTextWriterTag tag)
      : base(tag)
    {
      this.CssClass = DropList.baseCssClassName;
    }

    protected override void InitItems(ListItemCollection items)
    {
      this.KeepHiddenValue = false;
      if (this.emptyLine)
      {
        if (items == null)
          items = new ListItemCollection();
        if (items.FindByText(string.Empty) == null)
          items.Insert(0, new ListItem(string.Empty));
      }
      base.InitItems(items);
    }

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.droplist.CssClass = "scfDropList";
      this.help.CssClass = "scfDropListUsefulInfo";
      this.title.CssClass = "scfDropListLabel";
      this.generalPanel.CssClass = "scfDropListGeneralPanel";
      this.Controls.AddAt(0, (Control) this.generalPanel);
      this.Controls.AddAt(0, (Control) this.title);
      this.generalPanel.Controls.AddAt(0, (Control) this.help);
      this.generalPanel.Controls.AddAt(0, (Control) this.droplist);
    }

    [VisualProperty("Empty Choice:", 300)]
    [DefaultValue("Yes")]
    [VisualCategory("List")]
    [VisualFieldType(typeof (EmptyChoiceField))]
    public string EmptyChoice
    {
      set => this.emptyLine = value == "Yes";
      get => this.emptyLine.ToString();
    }

    public override string ID
    {
      get => this.droplist.ID;
      set
      {
        this.title.ID = value + "text";
        this.droplist.ID = value;
        base.ID = value + "scope";
      }
    }

    public override ControlResult Result => new ControlResult(this.ControlName, (object) (this.InnerListControl.SelectedValue ?? string.Empty), this.InnerListControl.SelectedItem != null ? this.InnerListControl.SelectedItem.Text : string.Empty);

    protected override System.Web.UI.WebControls.ListControl InnerListControl => (System.Web.UI.WebControls.ListControl) this.droplist;

    [DefaultValue("scfDroplistBorder")]
    [VisualProperty("CSS Class:", 600)]
    [VisualFieldType(typeof (CssClassField))]
    public new string CssClass
    {
      get => base.CssClass;
      set => base.CssClass = value;
    }
  }
}
