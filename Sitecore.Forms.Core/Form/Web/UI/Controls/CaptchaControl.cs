// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.CaptchaControl
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using HtmlAgilityPack;
using Sitecore.Globalization;
using Sitecore.Layouts;
using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Sitecore.Form.Web.UI.Controls
{
  public class CaptchaControl : MSCaptcha.CaptchaControl
  {
    private readonly HtmlGenericControl uniqueId;

    public CaptchaControl() => this.uniqueId = new HtmlGenericControl("input");

    protected override void OnInit(EventArgs eventArgs)
    {
      base.OnInit(eventArgs);
      this.uniqueId.Attributes["type"] = "hidden";
      this.uniqueId.ID = this.ID + "uniqueId";
      this.uniqueId.Attributes["name"] = this.uniqueId.UniqueID;
      this.LoadControlState((object) null);
    }

    protected override void OnLoad(EventArgs eventArgs)
    {
      base.OnLoad(eventArgs);
      this.uniqueId.Attributes["value"] = (string) this.SaveControlState();
    }

    protected override void LoadControlState(object state)
    {
      if (state == null)
        state = (object) (this.uniqueId.Attributes["value"] ?? this.Page.Request.Form[this.uniqueId.UniqueID]);
      base.LoadControlState(state);
    }

    protected override void OnPreRender(EventArgs eventArgs)
    {
      this.Controls.Add((Control) this.uniqueId);
      base.OnPreRender(eventArgs);
    }

    protected override void Render(HtmlTextWriter output)
    {
      HtmlTextWriter htmlTextWriter = new HtmlTextWriter((TextWriter) new StringWriter());
      this.RenderChildren(htmlTextWriter);
      base.Render(htmlTextWriter);
      string html = htmlTextWriter.InnerWriter.ToString();
      HtmlDocument htmlDocument = new HtmlDocument();
      using (new ThreadCultureSwitcher(Language.Parse("en").CultureInfo))
        htmlDocument.LoadHtml(html);
      HtmlNode childNode = htmlDocument.DocumentNode.ChildNodes[1].ChildNodes[0];
      childNode.Attributes.Append("alt", "Captcha");
      childNode.Attributes.Remove("border");
      output.Write(XHtml.Format(htmlDocument));
    }
  }
}
