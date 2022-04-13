// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.FormTitle
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Form.Core.Configuration;
using System;
using System.Web.UI;

namespace Sitecore.Form.Web.UI.Controls
{
  [ToolboxData("<h1 runat=\"server\"></h1>")]
  public class FormTitle : FormText
  {
    private HtmlTextWriterTag tagKey = HtmlTextWriterTag.H1;

    public FormTitle()
      : this((Item) null)
    {
    }

    public FormTitle(Item item)
      : base(item, Sitecore.Form.Core.Configuration.FieldIDs.FormTitleID, HtmlTextWriterTag.H1)
    {
    }

    protected override void OnLoad(EventArgs e)
    {
      this.Class = "scfTitleBorder";
      if (this.Item == null)
        return;
      string str = ((BaseItem) this.Item).Fields[Sitecore.Form.Core.Configuration.FieldIDs.ShowFormTitleID].Value;
      if (!string.IsNullOrEmpty(str) && str == "1")
        base.OnLoad(e);
      else
        this.Visible = false;
    }

    protected override HtmlTextWriterTag TagKey => this.tagKey;

    public void SetTagKey(HtmlTextWriterTag tag) => this.tagKey = tag;
  }
}
