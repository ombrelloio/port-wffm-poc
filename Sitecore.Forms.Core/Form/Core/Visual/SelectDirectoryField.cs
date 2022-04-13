// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Visual.SelectDirectoryField
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Configuration;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Visual
{
  public class SelectDirectoryField : ValidationField
  {
    public SelectDirectoryField()
      : base(HtmlTextWriterTag.Span.ToString())
    {
    }

    protected override void OnPreRender(object sender, EventArgs ev)
    {
      base.OnPreRender(sender, ev);
      string str = this.Attributes["value"] ?? string.Empty;
      Literal literal = new Literal();
      string prefixId = StaticSettings.PrefixId;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("<input id='{0}' style='width:55%' readonly='true' type='input' value='{1}' ", (object) this.ID, (object) str);
      stringBuilder.AppendFormat(" onblur=\"Sitecore.PropertiesBuilder.onSaveValue('{0}', '{1}', '0')\"", (object) prefixId, (object) this.ID);
      stringBuilder.AppendFormat(" onchange=\"Sitecore.PropertiesBuilder.onSaveValue('{0}', '{1}', '0');\"", (object) prefixId, (object) this.ID);
      stringBuilder.AppendFormat(" onkeyup=\"Sitecore.PropertiesBuilder.onSaveValue('{0}', '{1}', '0')\"", (object) prefixId, (object) this.ID);
      stringBuilder.AppendFormat(" onpaste=\"Sitecore.PropertiesBuilder.onSaveValue('{0}', '{1}', '0')\"", (object) prefixId, (object) this.ID);
      stringBuilder.AppendFormat(" oncut=\"Sitecore.PropertiesBuilder.onSaveValue('{0}', '{1}', '0')\"", (object) prefixId, (object) this.ID);
      stringBuilder.Append(" />");
      literal.Text = stringBuilder.ToString();
      this.Controls.Add((Control) literal);
      this.Controls.Add((Control) new Literal()
      {
        Text = string.Format("<input style='width:10%;height:22px;margin:0px 0px 0px 2px;float:right' type='button' value='...' onclick=\"Sitecore.FormBuilder.rise('forms:selectdirectory','" + this.ID + "')\" />")
      });
      this.ID += "input";
    }
  }
}
