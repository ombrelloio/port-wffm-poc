// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.FormControl
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  [PersistChildren(true)]
  public abstract class FormControl : WebControl
  {
    protected FormControl()
      : this(HtmlTextWriterTag.Div)
    {
    }

    protected FormControl(HtmlTextWriterTag tag)
      : base(tag)
    {
    }

    [Browsable(false)]
    public bool FastPreview { get; set; }

    [Browsable(false)]
    public bool DisableWebEditing { get; set; }

    [Browsable(false)]
    public string Parameters { get; set; }
  }
}
