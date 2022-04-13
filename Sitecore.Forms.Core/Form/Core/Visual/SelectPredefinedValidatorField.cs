// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Visual.SelectPredefinedValidatorField
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Form.Core.Configuration;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Visual
{
  public class SelectPredefinedValidatorField : ValidationField
  {
    public SelectPredefinedValidatorField()
      : base(HtmlTextWriterTag.Select.ToString())
    {
    }

    public override bool IsCacheable => false;

    protected override void OnPreRender(object sender, EventArgs ev)
    {
      this.Controls.Clear();
      base.OnPreRender(sender, ev);
      foreach (Item child in StaticSettings.ContextDatabase.GetItem(IDs.PredefinedValidatorsRoot).Children)
      {
        string str = ((object) child.ID.ToShortID()).ToString();
        this.Controls.Add((Control) new Literal()
        {
          Text = string.Format("<option {0} regex=\"{4}\" value=\"{1}\" title=\"{2}\">{3}</option>", this.DefaultValue == ((object) child.ID.ToShortID()).ToString() ? (object) "selected='selected'" : (object) string.Empty, (object) str, (object) child.DisplayName, (object) child.DisplayName, (object) HttpUtility.UrlEncode(((BaseItem) child).Fields[Sitecore.Form.Core.Configuration.FieldIDs.MetaDataListItemValue].Value))
        });
      }
      this.Attributes["onblur"] = string.Format("Sitecore.PropertiesBuilder.onSavePredefinedValidatorValue('{0}', '{1}')", (object) (StaticSettings.PrefixId + (this.Localize ? StaticSettings.PrefixLocalizeId : string.Empty)), (object) this.ID);
      this.Attributes["onchange"] = this.Attributes["onblur"];
      this.Attributes["onkeyup"] = this.Attributes["onblur"];
      this.Attributes["onpaste"] = this.Attributes["onblur"];
      this.Attributes["oncut"] = this.Attributes["onblur"];
      this.Attributes["class"] = "scFbPeValueProperty";
      this.Attributes["value"] = this.DefaultValue;
    }
  }
}
