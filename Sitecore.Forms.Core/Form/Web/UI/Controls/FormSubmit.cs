// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.FormSubmit
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Form.Core.Configuration;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.ComponentModel;
using System.Web.UI;

namespace Sitecore.Form.Web.UI.Controls
{
  [ToolboxData("<div runat=\"server\"></div>")]
  [PersistChildren(true)]
  public class FormSubmit : FormText
  {
    protected readonly SubmitButton Button = new SubmitButton();

    public FormSubmit()
      : base((Item) null, Sitecore.Form.Core.Configuration.FieldIDs.FormSubmitID, HtmlTextWriterTag.Div)
    {
    }

    public FormSubmit(Item item)
      : base(item, Sitecore.Form.Core.Configuration.FieldIDs.FormSubmitID, HtmlTextWriterTag.Div)
    {
    }

    public override void RenderControl(HtmlTextWriter writer)
    {
      this.RenderBeginTag(writer);
      this.RenderChildren(writer);
      this.RenderEndTag(writer);
    }

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.Controls.Add((Control) this.Button);
    }

    protected override void OnLoad(EventArgs e)
    {
      string fieldValue = this.GetFieldValue();
      this.Button.Text = string.IsNullOrEmpty(fieldValue) ? DependenciesManager.ResourceManager.Localize("NO_BUTTON_NAME") : fieldValue;
      this.Button.CssClass = "scfSubmitButton";
      this.Button.ID = this.ID;
      this.Button.OnClientClick = "$scw.webform.lastSubmit = this.id;";
    }

    [Browsable(false)]
    public new string ID
    {
      get => this.Button.ID;
      set => this.Button.ID = value;
    }

    [Browsable(false)]
    public string ValidationGroup
    {
      get => this.Button.ValidationGroup;
      set => this.Button.ValidationGroup = value;
    }

    [Browsable(false)]
    public event EventHandler Click
    {
      add => this.Button.Click += value;
      remove => this.Button.Click -= value;
    }
  }
}
