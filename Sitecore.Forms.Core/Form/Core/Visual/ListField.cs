// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Visual.ListField
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Visual
{
  public class ListField : ValidationField
  {
    private readonly IResourceManager resourceManager;

    public ListField()
      : this(DependenciesManager.ResourceManager)
    {
    }

    public ListField(IResourceManager resourceManager)
      : this(HtmlTextWriterTag.Span.ToString(), resourceManager)
    {
    }

    public ListField(string tag, IResourceManager resourceManager)
      : base(tag)
    {
      Assert.IsNotNull((object) resourceManager, nameof (resourceManager));
      this.resourceManager = resourceManager;
    }

    protected override void OnPreRender(object sender, EventArgs ev)
    {
      base.OnPreRender(sender, ev);
      string str1 = this.Attributes["value"] ?? string.Empty;
      Literal literal = new Literal();
      string str2 = this.Localize ? "1" : "0";
      string str3 = StaticSettings.PrefixId + (this.Localize ? StaticSettings.PrefixLocalizeId : string.Empty);
      StringBuilder stringBuilder = new StringBuilder();
      string str4 = this.resourceManager.Localize("ITEM_LOW_CASE") + Sitecore.WFFM.Abstractions.Constants.Core.Constants.SinglePluralDelimiter + this.resourceManager.Localize("ITEMS_LOW_CASE");
      stringBuilder.AppendFormat("<input id='{0}' Items='1' type='hidden' value='{1}' data-posttext='{2}'", (object) this.ID, (object) str1, (object) str4);
      stringBuilder.AppendFormat(" localUpdate='1' onblur=\"Sitecore.PropertiesBuilder.onSaveValue('{0}', '{1}', '{2}')\"", (object) str3, (object) this.ID, (object) str2);
      stringBuilder.AppendFormat(" onchange=\"Sitecore.PropertiesBuilder.onSaveListValue('{0}', '{1}', '{2}');\"", (object) str3, (object) this.ID, (object) str2);
      stringBuilder.AppendFormat(" onkeyup=\"Sitecore.PropertiesBuilder.onSaveValue('{0}', '{1}', '{2}')\"", (object) str3, (object) this.ID, (object) str2);
      stringBuilder.AppendFormat(" onpaste=\"Sitecore.PropertiesBuilder.onSaveValue('{0}', '{1}', '{2}')\"", (object) str3, (object) this.ID, (object) str2);
      stringBuilder.AppendFormat(" oncut=\"Sitecore.PropertiesBuilder.onSaveValue('{0}', '{1}', '{2}')\"", (object) str3, (object) this.ID, (object) str2);
      stringBuilder.Append(" />");
      stringBuilder.AppendFormat("<input class='list-items-value' id='{0}' style='width:55%' readonly='true' type='edit' value='0 Items' />", (object) (this.ID + "visible"));
      literal.Text = stringBuilder.ToString();
      this.Controls.Add((Control) literal);
      this.Controls.Add((Control) new Literal()
      {
        Text = string.Format("<input style='width:10%;height:22px;margin:0px 0px 0px 2px;float:right' type='button' value='...' onclick=\"Sitecore.FormBuilder.rise('list:edit','" + this.ID + "')\" />")
      });
      this.ID += "input";
    }
  }
}
