// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.FormSection
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Controls.Html;
using Sitecore.Form.Core.Visual;
using Sitecore.Forms.Core.Data;
using Sitecore.WFFM.Abstractions.Data;
using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  [ToolboxData("<div runat=\"server\"></div>")]
  [PersistChildren(true)]
  internal class FormSection : WebControl
  {
    private readonly IFieldItem[] fields;
    private readonly Item section;
    private bool showLegend = true;
    protected Div content;

    public FormSection(
      Item section,
      IFieldItem[] fields,
      string validationGroup,
      bool fastPreview)
      : base(HtmlTextWriterTag.Div)
    {
      this.section = section;
      this.fields = fields;
      this.ValidationGroup = validationGroup;
      this.IsFastPreview = fastPreview;
      this.IsFastPreview = false;
      this.RenderAsFieldSet = false;
      this.Information = string.Empty;
      this.ReadQueryString = false;
    }

    public FormSection(
      Item section,
      IFieldItem[] fields,
      bool renderAsFieldSet,
      string validationGroup,
      bool fastPreview)
      : this(section, fields, validationGroup, fastPreview)
    {
      this.RenderAsFieldSet = renderAsFieldSet;
      this.ReadQueryString = false;
    }

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      HtmlBaseControl htmlBaseControl;
      if (this.RenderAsFieldSet)
      {
        this.CssClass = string.IsNullOrEmpty(this.CssClass) ? "scfSectionBorder" : this.CssClass;
        Fieldset fieldset = new Fieldset();
        fieldset.Class = "scfSectionBorderAsFieldSet";
        htmlBaseControl = (HtmlBaseControl) fieldset;
      }
      else
      {
        if (string.IsNullOrEmpty(this.CssClass))
          this.CssClass = "scfSectionBorder";
        Div div = new Div();
        div.Class = this.CssClass;
        htmlBaseControl = (HtmlBaseControl) div;
      }
      foreach (string key in (IEnumerable) this.Attributes.Keys)
        htmlBaseControl.Attributes[key] = this.Attributes[key];
      htmlBaseControl.Style.Value = this.Style.Value;
      this.Controls.Add((Control) htmlBaseControl);
      if (this.showLegend && this.RenderAsFieldSet)
      {
        string str = FieldRenderer.Render(this.section, Sitecore.Form.Core.Configuration.FieldIDs.FieldTitleID, this.RenderingParameters, this.DisableWebEditing);
        if (!string.IsNullOrEmpty(str))
        {
          Legend legend1 = new Legend();
          legend1.Class = "scfSectionLegend";
          legend1.Title = str;
          Legend legend2 = legend1;
          htmlBaseControl.Controls.Add((Control) legend2);
        }
      }
      if (!string.IsNullOrEmpty(this.Information))
      {
        HtmlParagraph htmlParagraph1 = new HtmlParagraph();
        htmlParagraph1.Class = "scfSectionUsefulInfo";
        htmlParagraph1.Text = this.Information;
        HtmlParagraph htmlParagraph2 = htmlParagraph1;
        htmlBaseControl.Controls.Add((Control) htmlParagraph2);
      }
      Div div1 = new Div();
      div1.Class = "scfSectionContent";
      this.content = div1;
      htmlBaseControl.Controls.Add((Control) this.content);
      this.AddFields((Control) this.content);
    }

    protected override void OnPreRender(EventArgs e)
    {
      if (this.content.Controls.FirstOrDefault((Func<Control, bool>) (c => c is ControlReference && c.HasControls())) != null)
        return;
      this.Visible = false;
    }

    private void AddFields(Control content)
    {
      foreach (FieldItem field in this.fields)
      {
        if (field.Type != null && !string.IsNullOrEmpty(field.Title))
        {
          ControlReference controlReference = new ControlReference(field)
          {
            IsFastPreview = this.IsFastPreview,
            ValidationGroup = this.ValidationGroup,
            ReadQueryString = this.ReadQueryString,
            DisableWebEditing = this.DisableWebEditing,
            RenderingParameters = this.RenderingParameters
          };
          content.Controls.Add((Control) controlReference);
        }
      }
    }

    public bool DisableWebEditing { get; set; }

    [Browsable(false)]
    [VisualProperty("Help:", 500)]
    [VisualFieldType(typeof (TextAreaField))]
    [Localize]
    public string Information { get; set; }

    [Browsable(false)]
    [VisualProperty("Show Title:", 400)]
    [DefaultValue("Yes")]
    [VisualFieldType(typeof (BooleanField))]
    public string ShowLegend
    {
      set => this.showLegend = value == "Yes";
      get => this.showLegend.ToString();
    }

    [VisualProperty("CSS Class:", 600)]
    [VisualFieldType(typeof (CssClassField))]
    public new string CssClass
    {
      get => base.CssClass;
      set => base.CssClass = value;
    }

    [Browsable(false)]
    public bool RenderAsFieldSet { get; set; }

    [Browsable(false)]
    private string ValidationGroup
    {
      set => this.ViewState["summary"] = (object) value;
      get => this.ViewState["summary"] as string;
    }

    [Browsable(false)]
    public bool IsFastPreview { get; set; }

    [Browsable(false)]
    public bool ReadQueryString { get; set; }

    public string RenderingParameters { get; set; }
  }
}
