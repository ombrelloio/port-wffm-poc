// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.TemplateMenu
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Form.Core.Configuration;
using Sitecore.Web.UI;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public class TemplateMenu : Sitecore.Web.UI.HtmlControls.Control
  {
    public EventHandler Change;

    public TemplateMenu()
    {
    }

    public TemplateMenu(string template)
      : this()
    {
      this.TemplateID = template;
      Attributes["class"] = "scfEntry";
    }

    public string TemplateID
    {
      get => GetViewStateString("templateID");
      set => SetViewStateString("templateID", value);
    }

    public string ShowStandardField
    {
      get => GetViewStateString("StandardField");
      set => SetViewStateString("StandardField", value);
    }

    public string TemplateFieldName
    {
      get => GetViewStateString("templatefield");
      set => SetViewStateString("templatefield", value);
    }

    public string TemplateFieldID
    {
      get => GetViewStateString("templatefieldid");
      set => SetViewStateString("templatefieldid", value);
    }

    public string FieldName
    {
      get => GetViewStateString("field");
      set => SetViewStateString("field", value);
    }

    public string FieldID
    {
      get => GetViewStateString("fieldid");
      set => SetViewStateString("fieldid", value);
    }

    protected override void DoRender(HtmlTextWriter output)
    {
      output.Write("<div" + this.ControlAttributes + ">");
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("<span class=\"scfFieldName\">{0}:</span>", (object) this.FieldName);
      stringBuilder.AppendFormat("<select class=\"scfAborder\" id='select_{0}'", (object) ID);
      stringBuilder.AppendFormat(" onchange=\"scForm.postEvent(this,event,'{0}.ChangeTemplateField(&quot;' + this.value + '&quot;)')\" >", (object) ID);
      stringBuilder.Append("<option class='scfNotDefined'");
      if (this.TemplateFieldID == null || this.TemplateFieldID == ((object) Sitecore.Data.ID.Null).ToString())
        stringBuilder.Append(" selected='selected'");
      stringBuilder.AppendFormat("value='{0}'>{1}</option>", (object)Sitecore.Data.ID.Null, (object) DependenciesManager.ResourceManager.Localize("NOD_DEFINED"));
      output.Write(stringBuilder.ToString());
      if (!string.IsNullOrEmpty(this.TemplateID))
        this.TemplateContent(StaticSettings.ContextDatabase.GetTemplate(this.TemplateID), output);
      output.Write("</select>");
      output.Write("</div>");
    }

    internal void Redraw()
    {
      HtmlTextWriter htmlTextWriter = new HtmlTextWriter((TextWriter) new StringWriter(new StringBuilder()));
      DoRender(htmlTextWriter);
      SheerResponse.SetOuterHtml(ID, htmlTextWriter.InnerWriter.ToString());
    }

    private void ChangeTemplateField(string value) => this.TemplateFieldID = value;

    private void TemplateContent(TemplateItem template, HtmlTextWriter writer)
    {
      if (template == null)
        return;
      this.RenderTemplatePart(template, writer);
      this.RenderTemplates(template, writer);
    }

    private void RenderTemplates(TemplateItem template, HtmlTextWriter writer)
    {
      if (template == null)
        return;
      foreach (TemplateItem baseTemplate in template.BaseTemplates)
      {
        if (baseTemplate.ID != TemplateIDs.StandardTemplate || this.ShowStandardField == "1")
        {
          this.RenderTemplatePart(baseTemplate, writer);
          this.RenderTemplates(baseTemplate, writer);
        }
      }
    }

    private void RenderTemplatePart(TemplateItem template, HtmlTextWriter writer)
    {
      foreach (TemplateSectionItem section in template.GetSections())
      {
        writer.Write("<optgroup  class=\"scEditorHeaderNavigatorSection\" label=\"" + ((CustomItemBase) section).DisplayName + "\">");
        foreach (TemplateFieldItem field in section.GetFields())
        {
          string str = ((object) ((CustomItemBase) field).ID).ToString();
          writer.Write("<option id=\"" + str + "\" value=\"" + str + "\"");
          writer.Write(" class=\"scEditorHeaderNavigatorField\" ");
          if (str == this.TemplateFieldID)
            writer.Write(" selected=\"selected\"");
          writer.Write(">" + ((CustomItemBase) field).DisplayName + "</option>");
        }
        writer.Write("</optgroup>");
      }
    }
  }
}
