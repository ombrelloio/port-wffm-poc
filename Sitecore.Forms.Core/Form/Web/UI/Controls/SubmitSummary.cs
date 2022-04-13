// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.SubmitSummary
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  public class SubmitSummary : WebControl
  {
    private string[] messages;

    public SubmitSummary()
      : base(HtmlTextWriterTag.Div)
    {
      this.ForeColor = Color.Red;
      this.messages = new string[0];
    }

    protected virtual string FormatMessage(string message) => Translate.Text(message ?? string.Empty);

    protected override void RenderChildren(HtmlTextWriter writer)
    {
      if (this.Messages.Length == 0)
        return;
      if (this.Messages.Length > 1)
      {
        writer.Write("<ul>");
        for (int index = 0; index < this.Messages.Length; ++index)
        {
          writer.Write("<li>");
          writer.Write(this.FormatMessage(this.Messages[index]));
          this.WriteBreakIfPresent(writer, "</li>");
        }
        this.WriteBreakIfPresent(writer, "</ul>");
      }
      else
        new Label()
        {
          Text = this.FormatMessage(this.Messages[0])
        }.RenderControl(writer);
    }

    [DefaultValue(typeof (Color), "Red")]
    public override Color ForeColor
    {
      get => base.ForeColor;
      set => base.ForeColor = value;
    }

    [Browsable(false)]
    public string[] Messages
    {
      get => this.messages;
      set
      {
        Assert.ArgumentNotNull((object) value, nameof (value));
        this.messages = ((IEnumerable<string>) value).Where<string>((Func<string, bool>) (s => !string.IsNullOrEmpty(s))).Distinct<string>().ToArray<string>();
      }
    }

    private void WriteBreakIfPresent(HtmlTextWriter writer, string text)
    {
      if (text == "b")
        writer.WriteBreak();
      else
        writer.Write(text);
    }
  }
}
