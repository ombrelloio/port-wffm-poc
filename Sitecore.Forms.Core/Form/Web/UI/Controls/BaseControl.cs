// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.BaseControl
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Ascx.Controls;
using Sitecore.Form.Core.Utility;
using Sitecore.WFFM.Abstractions.Actions;
using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  [PersistChildren(false)]
  public abstract class BaseControl : WebControl, IResult
  {
    protected NameValueCollection classAtributes = new NameValueCollection();
    private SimpleForm form;

    protected BaseControl()
      : this(HtmlTextWriterTag.Div)
    {
    }

    protected BaseControl(HtmlTextWriterTag tag)
      : base(tag)
    {
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.CssClass = string.Join(" ", new string[2]
      {
        this.CssClass,
        NameValueCollectionUtil.GetString(this.classAtributes)
      });
    }

    public virtual string ControlName
    {
      get => this.classAtributes["name"] ?? string.Empty;
      set => this.classAtributes["name"] = value ?? string.Empty;
    }

    public virtual string DefaultValue
    {
      set
      {
      }
    }

    public virtual string FieldID
    {
      get => this.classAtributes["fieldid"];
      set => this.classAtributes["fieldid"] = value;
    }

    public SimpleForm Form
    {
      get
      {
        if (this.form == null)
          this.form = WebUtil.GetParent<SimpleForm>((Control) this);
        return this.form;
      }
    }

    public abstract ControlResult Result { get; set; }
  }
}
