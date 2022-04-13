// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.Telephone
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Visual;
using Sitecore.WFFM.Abstractions.Actions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  public class Telephone : InputControl
  {
    private static readonly string baseCssClassName = "scfTelephoneBorder";

    public Telephone()
      : this(HtmlTextWriterTag.Div)
    {
    }

    public Telephone(HtmlTextWriterTag tag)
      : base(tag)
    {
      this.CssClass = Telephone.baseCssClassName;
    }

    protected override void OnInit(EventArgs e)
    {
      this.textbox.CssClass = "scfTelephoneTextBox";
      this.help.CssClass = "scfTelephoneUsefulInfo";
      this.title.CssClass = "scfTelephoneLabel";
      this.generalPanel.CssClass = "scfTelephoneGeneralPanel";
      this.textbox.TextMode = TextBoxMode.SingleLine;
      this.Controls.AddAt(0, (Control) this.generalPanel);
      this.Controls.AddAt(0, (Control) this.title);
      this.generalPanel.Controls.AddAt(0, (Control) this.help);
      this.generalPanel.Controls.AddAt(0, (Control) this.textbox);
    }

    public override ControlResult Result
    {
      get
      {
        string str1 = new string(this.textbox.Text.Where<char>((Func<char, bool>) (c => char.IsDigit(c))).ToArray<char>());
        string controlName = this.ControlName;
        string str2 = str1;
        string parameters;
        if (!string.IsNullOrEmpty(this.textbox.Text))
          parameters = string.Join(string.Empty, new string[3]
          {
            "<scfriendly>",
            this.textbox.Text,
            "</scfriendly>"
          });
        else
          parameters = (string) null;
        return new ControlResult(controlName, (object) str2, parameters);
      }
      set
      {
      }
    }

    [DefaultValue("scfTelephoneBorder")]
    [VisualProperty("CSS Class:", 600)]
    [VisualFieldType(typeof (CssClassField))]
    public new string CssClass
    {
      get => base.CssClass;
      set => base.CssClass = value;
    }
  }
}
