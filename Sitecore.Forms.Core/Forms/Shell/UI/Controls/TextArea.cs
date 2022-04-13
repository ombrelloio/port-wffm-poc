// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.TextArea
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Web.UI.HtmlControls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public class TextArea : XmlControl
  {
    protected override void OnLoad(EventArgs e)
    {
      // ISSUE: explicit non-virtual call
      base.OnLoad(e);
      if (!Sitecore.Context.ClientPage.IsEvent)
        (this).Controls.Add( new Sitecore.Web.UI.HtmlControls.Literal("<input id=\"" + (this).ID + "State\" type='hidden' value=\"" + ((WebControl) this).Attributes["value"] + "\">"));
      else
        ((WebControl) this).Attributes["value"] = Sitecore.Context.ClientPage.ClientRequest.Form[(this).ID + "State"];
    }

    protected override void DoRender(HtmlTextWriter output)
    {
      string attribute = ((WebControl) this).Attributes["value"];
      ((WebControl) this).Attributes.Remove("value");
      (this).RenderChildren(output);
      output.Write("<textarea id=\"" + (this).ID + "\"");
      ((WebControl) this).Attributes.Render(output);
      output.Write(" style='" + ((WebControl) this).Style.Value + "'>" + attribute);
      output.Write("</textarea>");
    }

    public string Value => ((WebControl) this).Attributes["value"];
  }
}
