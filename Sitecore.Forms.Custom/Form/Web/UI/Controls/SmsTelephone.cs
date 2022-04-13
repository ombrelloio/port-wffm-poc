// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.SmsTelephone
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Visual;
using System;
using System.ComponentModel;
using System.Web.UI;

namespace Sitecore.Form.Web.UI.Controls
{
  public class SmsTelephone : Telephone
  {
    private static readonly string baseCssClassName = "scfSmsTelephoneBorder";

    public SmsTelephone()
      : this(HtmlTextWriterTag.Div)
    {
    }

    public SmsTelephone(HtmlTextWriterTag tag)
      : base(tag)
    {
      this.CssClass = SmsTelephone.baseCssClassName;
    }

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.textbox.CssClass = "scfSmsTelephoneTextBox";
      this.help.CssClass = "scfSmsTelephoneUsefulInfo";
      this.title.CssClass = "scfSmsTelephoneLabel";
      this.generalPanel.CssClass = "scfSmsTelephoneGeneralPanel";
    }

    [DefaultValue("scfSmsTelephoneBorder")]
    [VisualProperty("CSS Class:", 600)]
    [VisualFieldType(typeof (CssClassField))]
    public new string CssClass
    {
      get => base.CssClass;
      set => base.CssClass = value;
    }
  }
}
