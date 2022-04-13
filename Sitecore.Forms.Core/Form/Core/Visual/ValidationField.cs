// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Visual.ValidationField
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Configuration;
using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Visual
{
  public abstract class ValidationField : WebControl, IVisualFieldType
  {
    protected ValidationField(string tag)
      : base(tag)
    {
      this.Localize = false;
    }

    public ValidationType Validation { get; set; }

    public string DefaultValue { get; set; }

    public bool Localize { get; set; }

    public virtual bool IsCacheable => true;

    public string EmptyValue { get; set; }

    public virtual string Render()
    {
      this.OnPreRender((object) this, (EventArgs) null);
      HtmlTextWriter writer = new HtmlTextWriter((TextWriter) new StringWriter());
      this.RenderControl(writer);
      return writer.InnerWriter.ToString();
    }

    protected virtual void OnPreRender(object sender, EventArgs ev)
    {
      this.Attributes["onblur"] = string.Format("Sitecore.PropertiesBuilder.onSaveValue('{0}', '{1}', {2})", (object) (StaticSettings.PrefixId + (this.Localize ? StaticSettings.PrefixLocalizeId : string.Empty)), (object) this.ID, this.Localize ? (object) "1" : (object) "0");
      this.Attributes["onchange"] = this.Attributes["onblur"];
      this.Attributes["onkeyup"] = this.Attributes["onblur"];
      this.Attributes["onpaste"] = this.Attributes["onblur"];
      this.Attributes["oncut"] = this.Attributes["onblur"];
      this.Attributes["class"] = "scFbPeValueProperty";
      this.Attributes["value"] = this.DefaultValue;
      this.OnPreRender(ev);
    }
  }
}
