// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.RichHTML
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.XmlControls;
using System;
using System.Web.UI;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public class RichHTML : XmlControl
  {
    protected Border content;

    protected override void OnLoad(EventArgs e)
    {
      // ISSUE: explicit non-virtual call
      base.OnLoad(e);
      if (!Sitecore.Context.ClientPage.IsEvent)
      {
        string name = WebUtil.GetQueryString("contentid") ?? ((object) ShortID.NewId()).ToString();
        (this).ID = name;
        this.content = new Border();
        (this.content).Class = "scFbFrameContent";
        (this.content).ID = (this).ID + "_content";
        if ((this).Context.Session[name] != null)
          (this.content).Controls.Add(new Literal((this).Context.Session[name] as string));
        (this).Controls.Add(this.content);
      }
      else
        this.content = (this).FindControl((this).ID + "_content") as Border;
    }
  }
}
