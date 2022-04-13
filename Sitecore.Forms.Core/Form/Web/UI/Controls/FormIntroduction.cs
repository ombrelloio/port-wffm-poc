// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.FormIntroduction
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Form.Core.Configuration;
using System;
using System.Web.UI;

namespace Sitecore.Form.Web.UI.Controls
{
  [ToolboxData("<div runat=\"server\"></div>")]
  public class FormIntroduction : FormText
  {
    public FormIntroduction()
      : base((Item) null, Sitecore.Form.Core.Configuration.FieldIDs.FormIntroductionID, HtmlTextWriterTag.Div)
    {
    }

    public FormIntroduction(Item item)
      : base(item, Sitecore.Form.Core.Configuration.FieldIDs.FormIntroductionID, HtmlTextWriterTag.Div)
    {
    }

    protected override void OnLoad(EventArgs e)
    {
      this.Attributes["class"] = "scfIntroBorder";
      if (!(((BaseItem) this.Item).Fields[Sitecore.Form.Core.Configuration.FieldIDs.ShowFormIntroID].Value == "1"))
        return;
      base.OnLoad(e);
    }
  }
}
