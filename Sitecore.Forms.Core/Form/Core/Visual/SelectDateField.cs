// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Visual.SelectDateField
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Visual
{
  public class SelectDateField : ValidationField
  {
    public SelectDateField()
      : base(HtmlTextWriterTag.Div.ToString())
    {
    }

    protected override void OnPreRender(object sender, EventArgs ev)
    {
      base.OnPreRender(sender, ev);
      Sitecore.Form.Core.Utility.Utils.SetUserCulture();
      string str = this.Attributes["value"] ?? string.Empty;
      if (str.ToLower() == "today")
        str = DateUtil.ToIsoDate(DateTime.UtcNow);
      if (string.IsNullOrEmpty(str))
        str = DateUtil.ToIsoDate(DateTime.UtcNow.AddYears(1));
      Literal literal = new Literal();
      string prefixId = StaticSettings.PrefixId;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("<input id='{0}' style='width:89%' type='hidden' value='{1}' ", (object) this.ID, (object) str);
      stringBuilder.AppendFormat(" onblur=\"Sitecore.PropertiesBuilder.onSaveValue('{0}', '{1}', '0')\"", (object) prefixId, (object) this.ID);
      stringBuilder.AppendFormat(" onchange=\"Sitecore.PropertiesBuilder.onSaveDateValue('{0}', '{1}', '0');\"", (object) prefixId, (object) this.ID);
      stringBuilder.AppendFormat(" onkeyup=\"Sitecore.PropertiesBuilder.onSaveValue('{0}', '{1}', '0')\"", (object) prefixId, (object) this.ID);
      stringBuilder.AppendFormat(" onpaste=\"Sitecore.PropertiesBuilder.onSaveValue('{0}', '{1}', '0')\"", (object) prefixId, (object) this.ID);
      stringBuilder.AppendFormat(" oncut=\"Sitecore.PropertiesBuilder.onSaveValue('{0}', '{1}', '0')\"", (object) prefixId, (object) this.ID);
      stringBuilder.Append(" />");
      stringBuilder.AppendFormat("<input id='{0}' style='width:55%' readonly='true' type='edit' value='{1}' />", (object) (this.ID + "visible"), (object) DateUtil.IsoDateToDateTime(str).ToString(Sitecore.WFFM.Abstractions.Constants.Core.Constants.LongDateFormat));
      literal.Text = stringBuilder.ToString();
      this.Controls.Add((Control) literal);
      this.Controls.Add((Control) new Literal()
      {
        Text = string.Format("<input style='width:10%;height:22px;margin:0px 0px 0px 2px;float:right' type='button' value='...' onclick=\"Sitecore.FormBuilder.globalRise('forms:selectdate','{0}')\" />", (object) this.ID)
      });
      this.ID += "input";
    }
  }
}
