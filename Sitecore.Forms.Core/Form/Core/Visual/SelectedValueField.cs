// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Visual.SelectedValueField
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.Web.UI;

namespace Sitecore.Form.Core.Visual
{
  public class SelectedValueField : ValidationField
  {
    public SelectedValueField()
      : base(HtmlTextWriterTag.Select.ToString())
    {
    }

    protected override void OnPreRender(object sender, EventArgs ev)
    {
      base.OnPreRender(sender, ev);
      this.Attributes["size"] = "3";
      this.Attributes.Add("value", this.DefaultValue);
    }
  }
}
