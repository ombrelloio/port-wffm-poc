// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.RichTextBorder
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Text;
using Sitecore.Web.UI;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.XmlControls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public class RichTextBorder : Sitecore.Web.UI.XmlControls.XmlControl
  {
    protected Sitecore.Web.UI.HtmlControls.Literal html;
    protected Frame RichBorder;

    protected override void OnLoad(EventArgs e)
    {
      // ISSUE: explicit non-virtual call
      base.OnLoad(e);
      if (Sitecore.Context.ClientPage.IsEvent)
        return;
      this.RichBorder.XmlControl = "Forms.RichText.Html";
      UrlString urlString = new UrlString();
      string name = ((object) ShortID.NewId()).ToString();
      urlString.Append("contentid", name);
      urlString.Append("fieldname", this.FieldName);
      (this).Context.Session.Add(name, (object) this.Value);
      (this.RichBorder).Parameters = ((object) urlString).ToString();
      (this.RichBorder).ID = (this).ID + "_frame";
      (this.RichBorder).Class = "scFbFrame";
    }

    protected override void OnPreRender(EventArgs e)
    {
      Assert.ArgumentNotNull((object) e, nameof (e));
      // ISSUE: explicit non-virtual call
      base.OnPreRender(e);
      (this.RichBorder).Style.Value = (this).Style.Value;
      (this.RichBorder).Attributes.Add("width", "100%");
      (this.RichBorder).Attributes.Add("height", "0px");
      (this.RichBorder).Style.Add("border", "solid 0px");
      (this.RichBorder).Attributes.Remove("ondbclick");
      (this.RichBorder).Attributes.Remove("title");
    }

    public string Value
    {
      set => (this).ViewState["innerHtml"] = (object) value;
      get => (string) (this).ViewState["innerHtml"];
    }

    public string SourceID
    {
      get => (string) (this).ViewState[nameof (SourceID)];
      set => (this).ViewState[nameof (SourceID)] = (object) value;
    }

    public string FieldName
    {
      get => (string) (this).ViewState[nameof (FieldName)];
      set => (this).ViewState[nameof (FieldName)] = (object) value;
    }
  }
}
