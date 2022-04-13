// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.Label
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using System;

namespace Sitecore.Form.Web.UI.Controls
{
  public class Label : System.Web.UI.WebControls.Label
  {
    protected override void OnPreRender(EventArgs e)
    {
      if (string.IsNullOrEmpty(this.CssClass) && string.IsNullOrEmpty(this.Attributes["class"]))
        this.Attributes["class"] = "scfLabel";
      if (string.IsNullOrEmpty(this.Text) || this.Text.Trim().Length == 0)
      {
        this.AssociatedControlID = string.Empty;
        this.Style["display"] = "none";
      }
      base.OnPreRender(e);
    }
  }
}
