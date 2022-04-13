// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.UI.Controls.VSplitter
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Shell.Controls.Splitters;
using Sitecore.Web.UI;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.WebControls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.UI.Controls
{
  public class VSplitter : VSplitterXmlControl
  {
    public VSplitter()
    {
    }

    public VSplitter(Sitecore.Web.UI.HtmlControls.Image image) => this.Image = image;

    protected override void OnLoad(EventArgs e)
    {
      Assert.ArgumentNotNull((object) e, nameof (e));
      if (Sitecore.Context.ClientPage.IsEvent)
        return;
      (this.Image).Attributes["onmousedown"] = "scVSplit.mouseDown(this, event, '" + ID + "', '" + this.Target + "')";
      (this.Image).Attributes["onmousemove"] = "scVSplit.mouseMove(this, event, '" + ID + "')";
      (this.Image).Attributes["onmouseup"] = "scVSplit.mouseUp(this, event, '" + ID + "', '" + this.Target + "');";
      (this.Image).Width = UIUtil.IsIE() ? Unit.Pixel(4) : Unit.Pixel(6);
      string str = Registry.GetString("/Current_User/VSplitters/" + ID);
      if (string.IsNullOrEmpty(str))
        return;
      string[] strArray = str.Split(',');
      if (!(Parent is GridPanel parent))
        return;
      int num = (parent).Controls.IndexOf(this);
      if (num <= 0 || num >= (parent).Controls.Count - 1)
        return;
      (parent).SetExtensibleProperty(this, "Height", "100%");
      (parent).SetExtensibleProperty(this, "Width", UIUtil.IsIE() ? "4" : "6");
      if (string.Compare(this.Target, "left", StringComparison.InvariantCultureIgnoreCase) == 0)
        (parent).SetExtensibleProperty((this).Parent.Controls[num - 1], "Width", strArray[0]);
      if (string.Compare(this.Target, "right", StringComparison.InvariantCultureIgnoreCase) != 0)
        return;
      (parent).SetExtensibleProperty((this).Parent.Controls[num + 1], "Width", strArray[1]);
    }

    protected override void Render(HtmlTextWriter output) => (this.Image).RenderControl(output);
  }
}
