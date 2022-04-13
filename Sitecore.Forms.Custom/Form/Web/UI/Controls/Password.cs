// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.Password
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Visual;
using Sitecore.WFFM.Abstractions.Actions;
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  public class Password : SingleLineText
  {
    private static readonly string baseCssClassName = "scfPasswordBorder";

    public Password(HtmlTextWriterTag tag)
      : base(tag)
    {
      this.MaxLength = 256;
      this.MinLength = 0;
      this.CssClass = Password.baseCssClassName;
    }

    public Password()
      : this(HtmlTextWriterTag.Div)
    {
    }

    protected override void OnInit(EventArgs e)
    {
      this.textbox.CssClass = "scfPasswordTextBox";
      this.help.CssClass = "scfPasswordUsefulInfo";
      this.generalPanel.CssClass = "scfPasswordGeneralPanel";
      this.title.CssClass = "scfPasswordLabel";
      this.textbox.TextMode = TextBoxMode.Password;
      this.Controls.AddAt(0, (Control) this.generalPanel);
      this.Controls.AddAt(0, (Control) this.title);
      this.generalPanel.Controls.AddAt(0, (Control) this.help);
      this.generalPanel.Controls.AddAt(0, (Control) this.textbox);
    }

    [DefaultValue("scfPasswordBorder")]
    [VisualProperty("CSS Class:", 600)]
    [VisualFieldType(typeof (CssClassField))]
    public new string CssClass
    {
      get => base.CssClass;
      set => base.CssClass = value;
    }

    public override ControlResult Result => new ControlResult(this.Title, (object) this.textbox.Text, Sitecore.StringExtensions.StringExtensions.FormatWith("secure:<schidden>{0}</schidden>", new object[1]
    {
      (object) this.textbox.Text
    }))
    {
      Secure = true
    };
  }
}
