// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.BaseUserControl
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
  public abstract class BaseUserControl : UserControl, IResult
  {
    private NameValueCollection classAtributes = new NameValueCollection();
    private SimpleForm form;

    public virtual bool SetValidatorProperties(BaseValidator validator) => false;

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      WebUtil.ExecuteForAllControls((Control) this, (Action<Control>) (control =>
      {
        if (!(control is BaseValidator))
          return;
        ((BaseValidator) control).CssClass += string.Join(".", new string[2]
        {
          " fieldid",
          this.FieldID
        });
        ((BaseValidator) control).ValidationGroup = this.ValidationGroup;
      }));
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

    protected override void Render(HtmlTextWriter writer)
    {
      writer.Write("<div ");
      this.Attributes.Render(writer);
      writer.Write(" id='" + this.ClientID);
      writer.Write("'>");
      base.Render(writer);
      writer.Write("</div>");
    }

    public virtual string ControlName
    {
      get => this.classAtributes["name"];
      set => this.classAtributes["name"] = value;
    }

    public string CssClass
    {
      get => this.Attributes["class"];
      set => this.Attributes["class"] = value;
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

    public bool IsRequired { get; set; }

    public abstract ControlResult Result { get; set; }

    public string ValidationGroup
    {
      get => this.classAtributes["validationgroup"];
      set => this.classAtributes["validationgroup"] = value;
    }
  }
}
