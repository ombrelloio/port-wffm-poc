// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Visual.RegexField
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Configuration;
using System;
using System.Web.UI;

namespace Sitecore.Form.Core.Visual
{
  public class RegexField : ValidationField
  {
    public RegexField()
      : base(HtmlTextWriterTag.Input.ToString())
    {
    }

    protected override void OnPreRender(object sender, EventArgs ev)
    {
      base.OnPreRender(sender, ev);
      this.Attributes["onblur"] = string.Format("Sitecore.PropertiesBuilder.onSaveRegexValue('{0}', '{1}')", (object) StaticSettings.PrefixId, (object) this.ID);
      this.Attributes["onchange"] = this.Attributes["onblur"];
      this.Attributes["onkeyup"] = this.Attributes["onblur"];
      this.Attributes["onpaste"] = this.Attributes["onblur"];
      this.Attributes["oncut"] = this.Attributes["onblur"];
      this.Attributes["disabled"] = "1";
    }
  }
}
