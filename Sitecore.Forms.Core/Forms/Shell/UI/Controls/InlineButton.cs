// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.InlineButton
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Resources;
using Sitecore.Web.UI.HtmlControls;
using System;
using System.Collections.Specialized;
using System.Text;
using System.Web.UI;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public class InlineButton : Sitecore.Web.UI.HtmlControls.Control
  {
    private string onClick = string.Empty;
    private string text = string.Empty;
    private string icon = string.Empty;
    private string size = string.Empty;

    public InlineButton()
    {
    }

    public InlineButton(string name, string icon, string click, string size)
    {
      this.text = name;
      this.icon = icon;
      this.onClick = click;
      this.size = size;
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (this.text != null)
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat("onclick={0}", (object) this.onClick);
        stringBuilder.AppendFormat("|icon={0}", (object) this.icon);
        stringBuilder.AppendFormat("|text={0}", (object) this.text);
        stringBuilder.AppendFormat("|size={0}", (object) this.size);
        Controls.Add(new Literal("<input ID=\"" + ID + "buttonstate\" Type=\"hidden\" value=\"" + (object) stringBuilder + "\"/>"));
      }
      else
      {
        if (!Sitecore.Context.ClientPage.IsEvent || Sitecore.Context.ClientPage.ClientRequest.Form[ID + "buttonstate"] == null)
          return;
        NameValueCollection nameValues = StringUtil.GetNameValues(Sitecore.Context.ClientPage.ClientRequest.Form[ID + "buttonstate"]);
        this.onClick = nameValues["onclick"];
        this.icon = nameValues["icon"];
        this.text = nameValues["text"];
        this.size = nameValues["size"];
      }
    }

    protected override void DoRender(HtmlTextWriter output)
    {
      output.Write("<a href=\"#\" id=\"" + ID + "\" onclick=\"" + this.onClick + "\" class=\"scFbRollOver\" RollOver=\"true\">");
      RenderChildren(output);
      output.Write(Sitecore.StringExtensions.StringExtensions.FormatWith("<img class=\"scFbInlineImage\" width=\"{0}\" height=\"{0}\" style=\"background-image:url({1});filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src='{1}', sizingMethod='scale');\" alt=\"\" src=\"/sitecore/images/blank.gif\" align=\"absMiddle\" border=\"0\" />", new object[2]
      {
        (object) this.size,
        (object) Images.GetThemedImageSource(this.icon)
      }));
      output.Write(this.text);
      output.Write("</a>");
    }
  }
}
