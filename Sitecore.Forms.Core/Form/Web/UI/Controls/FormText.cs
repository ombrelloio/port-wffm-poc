// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.FormText
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Web.UI;
using Sitecore.Web.UI.WebControls;
using Sitecore.Xml.Xsl;
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  public class FormText : FormControl
  {
    private readonly ID field;

    public FormText()
      : this((Item) null, (ID) FieldIDs.DisplayName)
    {
    }

    public FormText(Item item, ID field)
      : this(item, field, HtmlTextWriterTag.Div)
    {
    }

    public FormText(Item item, ID field, HtmlTextWriterTag tag)
      : base(tag)
    {
      this.Item = item;
      this.field = field;
    }

    public string GetFieldValue() => this.Item != null ? ((BaseItem) this.Item)[this.Field] : string.Empty;

    protected override void OnLoad(EventArgs e)
    {
      if (this.Item == null)
        return;
      var fieldRenderer = new Sitecore.Web.UI.WebControls.FieldRenderer();
      fieldRenderer.Item = this.Item;
      fieldRenderer.FieldName = ((BaseItem) this.Item).Fields[this.Field].Name;
      (fieldRenderer).Parameters = this.Parameters ?? string.Empty;
      fieldRenderer.DisableWebEditing = this.DisableWebEditing;
      RenderFieldResult renderFieldResult = fieldRenderer.RenderField();
      this.Controls.Add((Control) new Literal()
      {
        Text = ((object) renderFieldResult).ToString()
      });
    }

    [Browsable(false)]
    public Item Item { get; protected internal set; }

    [Browsable(false)]
    protected ID Field => this.field;

    public string Class
    {
      get => this.Attributes["class"];
      set => this.Attributes["class"] = value;
    }
  }
}
