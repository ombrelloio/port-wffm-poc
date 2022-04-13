// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.RadioList
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Visual;
using Sitecore.Forms.Core.Data;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  [ValidationProperty("Value")]
  public class RadioList : ListControl
  {
    private static readonly string baseCssClassName = "scfRadioButtonListBorder";
    protected RadioButtonList buttonList = new RadioButtonList();

    public RadioList()
      : this(DependenciesManager.RequirementsChecker, HtmlTextWriterTag.Div)
    {
    }

    public RadioList(IRequirementsChecker requirementsChecker)
      : this(requirementsChecker, HtmlTextWriterTag.Div)
    {
    }

    public RadioList(IRequirementsChecker requirementsChecker, HtmlTextWriterTag tag)
      : this(requirementsChecker, tag, new ListFieldValueFormatter(DependenciesManager.Resolve<ISettings>()))
    {
    }

    public RadioList(
      IRequirementsChecker requirementsChecker,
      HtmlTextWriterTag tag,
      ListFieldValueFormatter listFieldValueFormatter)
      : base(requirementsChecker, tag, listFieldValueFormatter)
    {
      this.CssClass = RadioList.baseCssClassName;
      this.buttonList.RepeatColumns = 1;
    }

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.buttonList.CssClass = "scfRadioButtonList";
      this.help.CssClass = "scfRadioButtonListUsefulInfo";
      this.title.CssClass = "scfRadioButtonListLabel";
      this.generalPanel.CssClass = "scfRadioButtonListGeneralPanel";
      this.Controls.AddAt(0, (Control) this.generalPanel);
      this.Controls.AddAt(0, (Control) this.title);
      this.generalPanel.Controls.AddAt(0, (Control) this.help);
      this.generalPanel.Controls.AddAt(0, (Control) this.buttonList);
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.title.AssociatedControlID = (string) null;
    }

    [VisualProperty("Direction:", 300)]
    [VisualFieldType(typeof (DirectionField))]
    public string Direction
    {
      get => this.buttonList.RepeatDirection.ToString();
      set => this.buttonList.RepeatDirection = (RepeatDirection) Enum.Parse(typeof (RepeatDirection), value, true);
    }

    [VisualProperty("Columns:", 400)]
    [DefaultValue(1)]
    [VisualFieldType(typeof (EditField))]
    public string Columns
    {
      get => this.buttonList.RepeatColumns.ToString();
      set
      {
        int result;
        if (!int.TryParse(value, out result))
          result = 1;
        this.buttonList.RepeatColumns = result;
      }
    }

    public override string ID
    {
      get => base.ID;
      set
      {
        this.title.ID = value + "text";
        this.buttonList.ID = value + "scope";
        base.ID = value;
      }
    }

    public RepeatLayout RepeatLayout
    {
      get => this.buttonList.RepeatLayout;
      set => this.buttonList.RepeatLayout = value;
    }

    public override ControlResult Result => new ControlResult(this.ControlName, (object) (this.buttonList.SelectedValue ?? string.Empty), this.buttonList.SelectedItem != null ? this.buttonList.SelectedItem.Text : string.Empty);

    protected override System.Web.UI.WebControls.ListControl InnerListControl => (System.Web.UI.WebControls.ListControl) this.buttonList;

    [DefaultValue("scfRadioButtonListBorder")]
    [VisualProperty("CSS Class:", 600)]
    [VisualFieldType(typeof (CssClassField))]
    public new string CssClass
    {
      get => base.CssClass;
      set => base.CssClass = value;
    }

    public string Value
    {
      get
      {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (ListItem listItem in this.InnerListControl.Items)
        {
          if (listItem.Selected)
            stringBuilder.AppendFormat("<item>{0}</item>", (object) listItem.Value);
        }
        return stringBuilder.ToString();
      }
    }
  }
}
