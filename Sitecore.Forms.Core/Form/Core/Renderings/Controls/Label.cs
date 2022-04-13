// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Renderings.Controls.Label
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Renderings.Controls
{
  public class Label : Sitecore.Web.UI.HtmlControls.Label
  {
    protected override void DoRender(HtmlTextWriter output)
    {
      if (string.IsNullOrEmpty(((HeaderedItemsControl) this).Header))
        (this).Visible = false;
      if (!(this).Visible)
        return;
      base.DoRender(output);
    }

    public new string For
    {
      get => ((WebControl) this).Attributes["for"];
      set => ((WebControl) this).Attributes["for"] = value;
    }
  }
}
