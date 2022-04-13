// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.FormSection
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Utility;
using Sitecore.Resources;
using Sitecore.Web.UI;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Controls
{
  internal class FormSection : Sitecore.Web.UI.HtmlControls.Control
  {
    public static readonly string KeySectionDeleted = "_section_deleted";
    public static readonly string KeySectionId = "_section_id";
    public static readonly string KeySectionName = "_section_name";
    public static readonly string KeySectionProperties = "_field_properties";
    public static readonly string KeySectionLocProperties = "_field_properties_loc";
    public static readonly string KeySectionType = "_section_type";
    private readonly string delete;
    private readonly string param;
    private readonly string sectionID;
    private readonly string locparam;

    public FormSection()
    {
    }

    public FormSection(string id)
      : this()
    {
      this.sectionID = id;
      this.Name = string.Empty;
      this.delete = string.Empty;
      this.param = string.Empty;
      this.locparam = string.Empty;
      this.EmptyName = string.Empty;
    }

    public FormSection(SectionDefinition section)
      : this()
    {
      this.sectionID = section.SectionID;
      this.Name = section.Name;
      this.delete = section.Deleted;
      this.param = section.Properties;
      this.locparam = section.LocProperties;
      this.EmptyName = section.EmptyName;
    }

    public override void RenderControl(HtmlTextWriter writer) => DoRender(writer);

    protected override void DoRender(HtmlTextWriter output)
    {
      if (this.Hide || this.delete == "1")
        Style.Add("display", "none");
      else
        Style.Add("display", "block");
      string str1 = string.Empty;
      string str2 = string.Empty;
      if (string.IsNullOrEmpty(this.Name) || this.Name == FormSection.EmptySectionName)
      {
        this.Name = FormSection.EmptySectionName;
        str1 = " color:silver ";
        str2 = this.Name;
      }
      output.Write("<div class='scFbTableSectionScope' id=\"" + ID + "\" style='" + Style.Value + "' >");
      output.Write("<div border='0px' id=\"" + ID);
      output.Write("SectionRow\" class='scFbTableSectionHeader' padding=\"0px\" margin=\"0px\"");
      output.Write(" onmouseover=\"javascript:return Sitecore.FormBuilder.mouseMove(this,event,'" + ID + "')\"");
      output.Write(" onmouseout=\"javascript:return Sitecore.FormBuilder.mouseOver(this,event,'" + ID + "')\"");
      output.Write(" onclick=\"javascript:return Sitecore.FormBuilder.selectControl(this,event,'" + ID + "','PropertySettings',true)\" >");
      output.Write("<table  width='100%' padding='0px' margin='0px' cellPadding='0' cellSpacing='0'>");
      output.Write("<tr>");
      output.Write("<td class=\"scFbSectionNameInputScope\">");
      output.Write("<label class=\"scFbSmallLabel\" for=\"" + ID + FormSection.KeySectionName + "\"");
      output.Write("\tstyle=\"margin:0px 15px 0px 15px\">" + DependenciesManager.ResourceManager.Localize("TITLE_CAPTION"));
      if (!string.IsNullOrEmpty(this.EmptyName))
      {
        this.Name = this.DefaultName;
        str1 = " color:silver ";
        str2 = this.Name;
      }
      output.Write("</label>");
      output.Write("<input id=\"" + ID + FormSection.KeySectionName + "\"");
      output.Write(" class=\"" + this.Class + "Name\" value=\"" + this.Name + "\" style=\"" + str1 + "\"");
      output.Write(" title=\"" + str2 + "\"");
      output.Write(" onblur=\"javascript:return Sitecore.FormBuilder.blurInput(this,event,'" + ID + "')\"");
      output.Write(" onfocus=\"javascript:return Sitecore.FormBuilder.focusInput(this,event,'" + ID + "','PropertySettings',true)\" />");
      output.Write("<input id=\"" + ID + FormSection.KeySectionId + "\" type=\"hidden\" value=\"" + this.sectionID + "\" />");
      output.Write("<input id=\"" + ID + FormSection.KeySectionDeleted + "\" type=\"hidden\" value=\"" + this.delete + "\"/>");
      output.Write("<textarea id='{0}{1}' rows='0' cols='0' readonly style='display:none'>{2}</textarea>", (object) ID, (object) FormSection.KeySectionProperties, (object) this.param);
      output.Write("<textarea id='{0}{1}' rows='0' cols='0' readonly style='display:none'>{2}</textarea>", (object) ID, (object) FormSection.KeySectionLocProperties, (object) this.locparam);
      output.Write("<input id=\"" + ID + FormSection.KeySectionType + "\" type=\"hidden\" value=\"" + (object) IDs.SectionTypeID + "\"/>");
      output.Write("</td>");
      output.Write("<td class=\"scFbSectionButtonDeleteScope\">");
      output.Write("<div id=\"" + ID + "ButtonContainer\" class=\"scFbButtonContainerSection\">");
      output.Write("<img class=\"" + this.Class + "DeleteButton\" alt='' ");
      output.Write(Sitecore.StringExtensions.StringExtensions.FormatWith(" src='{0}'", new object[1]
      {
        (object) Images.GetThemedImageSource("Applications/16x16/delete2.png")
      }));
      output.Write(" onclick=\"javascript:return Sitecore.FormBuilder.deleteSection(this,event,'" + ID + "', true);\"/>");
      output.Write("</div>");
      output.Write("</td>");
      output.Write("</tr>");
      output.Write("</table>");
      output.Write("</div>");
      output.Write("<div border='0px' padding=\"0px\" margin=\"0px\" id=\"" + ID + "Section\" class=\"" + this.Class + "Row\" >");
      RenderChildren(output);
      output.Write("</div>");
      FormDesignerUtils.SectionButtons(ID).RenderControl(output);
      output.Write("<img class='scFbArrowSection' id=\"" + ID + "Marker\" alt='' src='/sitecore/images/blank.gif' border='0'/>");
      output.Write("</div>");
    }

    public bool Hide { set; get; }

    public string EmptyName { get; private set; }

    public static string EmptySectionName => DependenciesManager.ResourceManager.Localize("SECTION_NAME_EMPTY");

    public static string LocEmptySectionName => DependenciesManager.ResourceManager.Localize("LOC_SECTION_NAME_EMPTY");

    public string DefaultName => string.Format(FormSection.LocEmptySectionName, (object) this.EmptyName);
  }
}
