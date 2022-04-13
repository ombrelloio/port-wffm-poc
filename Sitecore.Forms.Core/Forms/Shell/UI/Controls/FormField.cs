// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.FormField
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Data;
using Sitecore.Resources;
using Sitecore.Web.UI;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Controls
{
  internal class FormField : Sitecore.Web.UI.HtmlControls.Control
  {
    public static readonly string DefautTypeValue = "{6353E864-D3AE-4FF9-88DD-60B0F779A44A}";
    public static readonly string KeyFieldDelete = "_field_deleted";
    public static readonly string KeyFieldID = "_field_id";
    public static readonly string KeyFieldLocProperties = "_field_properties_loc";
    public static readonly string KeyFieldName = "_field_name";
    public static readonly string KeyFieldProperties = "_field_properties";
    public static readonly string KeyFieldType = "_field_type";
    public static readonly string KeyFieldValidate = "_field_validate";
    public static readonly string KeyFieldTag = "_field_tag";
    public static readonly string KeyFieldIsSystem = "_field_issystem";
    private readonly string delete;
    private readonly string fieldID;
    private readonly string locproperties;
    private readonly string properties;
    private readonly string type;
    private readonly string validate;
    private readonly string tag;

    public FormField()
    {
    }

    public FormField(FieldDefinition field)
    {
      this.fieldID = field.FieldID;
      this.Name = field.Name;
      this.validate = field.IsValidate;
      this.tag = field.IsTag;
      this.type = string.IsNullOrEmpty(field.Type) ? FormField.DefautTypeValue : field.Type;
      this.properties = field.Properties;
      this.locproperties = field.LocProperties;
      this.delete = field.Deleted;
      this.EmptyName = field.EmptyName;
      this.Hide = false;
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
      if (string.IsNullOrEmpty(this.Name) || this.Name == FormField.EmptyFieldName)
      {
        this.Name = FormField.EmptyFieldName;
        str2 = this.Name;
        str1 = "color:silver";
      }
      Item obj = StaticSettings.ContextDatabase.GetItem(this.type);
      string str3 = string.Empty;
      if (((BaseItem) obj).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldTypeRequiredID].Value != "1")
        str3 = "disabled=\"1\"";
      output.Write(string.Format("<div class=\"{0}Scope\" id=\"{1}\" ", (object) this.Class, (object) ID));
      string attribute = Attributes["class"];
      Attributes.Remove("class");
      Attributes.Render(output);
      Attributes["class"] = attribute;
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat(" padding='0px' margin='0px' ");
      stringBuilder.AppendFormat(" onclick=\"javascript:return Sitecore.FormBuilder.selectControl(this,event,'{0}','PropertySettings',true)\"", (object) ID);
      stringBuilder.AppendFormat(" onmouseover=\"javascript:return Sitecore.FormBuilder.mouseMove(this,event,'{0}')\"", (object) ID);
      stringBuilder.AppendFormat(" onmouseout=\"javascript:return Sitecore.FormBuilder.mouseOver(this,event,'{0}')\" >", (object) ID);
      stringBuilder.Append("<table  width='100%' padding='0px' margin='0px' cellPadding='0' cellSpacing='0' ");
      stringBuilder.Append(" >");
      stringBuilder.AppendFormat("<tr class='{0}Row' >", (object) this.Class);
      stringBuilder.AppendFormat("<td class='{0}Name' >", (object) this.Class);
      stringBuilder.AppendFormat("<label class='scFbSmallLabel' for='{0}{1}'>{2}", (object) ID, (object) FormField.KeyFieldID, (object) DependenciesManager.ResourceManager.Localize("TITLE_CAPTION"));
      if (!string.IsNullOrEmpty(this.EmptyName))
      {
        this.Name = string.Format(FormField.LocEmptyFieldName, (object) this.EmptyName);
        str1 = "color:silver";
        str2 = this.Name;
      }
      stringBuilder.Append("</label>");
      stringBuilder.AppendFormat("<input id='{0}{1}' class='{2}NameInput'", (object) ID, (object) FormField.KeyFieldName, (object) this.Class);
      stringBuilder.AppendFormat(" value=\"{0}\" style=\"{1}\" title=\"{2}\"", (object) this.Name, (object) str1, (object) str2);
      stringBuilder.Append(" onchange=\"javascript:return Sitecore.FormBuilder.fieldChange(this,event)\"");
      stringBuilder.Append(" onkeyup=\"javascript:return Sitecore.FormBuilder.fieldChange(this,event)\"");
      stringBuilder.Append(" oncut=\"javascript:return Sitecore.FormBuilder.fieldChange(this,event)\"");
      stringBuilder.Append(" onpaste=\"javascript:return Sitecore.FormBuilder.fieldChange(this,event)\"");
      stringBuilder.AppendFormat(" onblur=\"javascript:return Sitecore.FormBuilder.blurInput(this,event,'{0}')\"", (object) ID);
      stringBuilder.AppendFormat(" onfocus=\"javascript:return Sitecore.FormBuilder.focusInput(this,event,'{0}','PropertySettings',true)\" />", (object) ID);
      stringBuilder.AppendFormat("<input id='{0}{1}' type='hidden' value=\"{2}\"/>", (object) ID, (object) FormField.KeyFieldID, (object) this.fieldID);
      stringBuilder.AppendFormat("<input id='{0}{1}' type='hidden' value=\"{2}\"/>", (object) ID, (object) FormField.KeyFieldTag, (object) this.tag);
      stringBuilder.AppendFormat("<textarea id='{0}{1}' rows='0' cols='0' readonly style='display:none'>{2}</textarea>", (object) ID, (object) FormField.KeyFieldProperties, (object) this.properties);
      stringBuilder.AppendFormat("<textarea id='{0}{1}' rows='0' cols='0' readonly style='display:none'>{2}</textarea>", (object) ID, (object) FormField.KeyFieldLocProperties, (object) this.locproperties);
      stringBuilder.AppendFormat("<input id ='{0}{1}' type='hidden' value=\"{2}\"/>", (object) ID, (object) FormField.KeyFieldDelete, (object) this.delete);
      stringBuilder.Append("</td>");
      stringBuilder.AppendFormat("<td class='{0}Type'>", (object) this.Class);
      stringBuilder.AppendFormat("<label class='scFbSmallLabel' margin='0px 0px 0px 0px' for='{0}{1}' style='padding:3px 0px 2px 2px'>{2}</label>", (object) ID, (object) FormField.KeyFieldType, (object) DependenciesManager.ResourceManager.Localize("TYPE_CAPTION"));
      stringBuilder.AppendFormat("<select id='{0}{1}' class='{2}TypeInput'", (object) ID, (object) FormField.KeyFieldType, (object) this.Class);
      stringBuilder.AppendFormat(" onclick=\"Sitecore.FormBuilder.selectControl(this,event,'{0}','PropertySettings',true);\"", (object) ID);
      stringBuilder.AppendFormat(" onchange=\"Sitecore.FormBuilder.onChangeType($('{0}_field_type', event))\"", (object) ID);
      stringBuilder.AppendFormat(">{0}</select>", (object) FormField.RenderFieldTypes(this.type));
      stringBuilder.Append("</td>");
      stringBuilder.AppendFormat("<td class='{0}Validate'>", (object) this.Class);
      stringBuilder.AppendFormat("<label class='scFbSmallLabel' for='{0}{1}'>{2}</label>", (object) ID, (object) FormField.KeyFieldValidate, (object) DependenciesManager.ResourceManager.Localize("REQUIRED_CAPTION"));
      stringBuilder.AppendFormat("<input id='{0}{1}' class='{2}ValidateInput' {3} type='checkbox' {4}", (object) ID, (object) FormField.KeyFieldValidate, (object) this.Class, (object) str3, this.validate == "1" || this.validate == "on" ? (object) " checked=\"checked\"" : (object) string.Empty);
      stringBuilder.Append(" onclick=\"javascript:return scForm.setModified(true);\" ");
      stringBuilder.AppendFormat(" onfocus=\"javascript:return Sitecore.FormBuilder.selectControl(this,event,'{0}','PropertySettings',true)\"/>", (object) ID);
      stringBuilder.Append("</td>");
      stringBuilder.AppendFormat("<td class='{0}DeleteButtonScorpe' align='right'>", (object) this.Class);
      stringBuilder.AppendFormat("<div id='{0}ButtonContainer' class='scFbButtonContainer'>", (object) ID);
      stringBuilder.AppendFormat("<img class='{0}DeleteButton' alt='' ", (object) this.Class);
      stringBuilder.AppendFormat(" onclick=\"Sitecore.FormBuilder.deleteField(this,event,'{0}', true)\" ", (object) ID);
      stringBuilder.AppendFormat(" src='{0}' />", (object) Themes.MapTheme(Images.GetThemedImageSource("Applications/16x16/delete2.png", (ImageDimension) 1), string.Empty, false));
      stringBuilder.Append("</div>");
      stringBuilder.Append("</td>");
      stringBuilder.Append("</tr>");
      stringBuilder.AppendFormat("<tr class='{0}Marker'>", (object) this.Class);
      stringBuilder.Append("<td colSpan='4' width='100%'>");
      stringBuilder.AppendFormat("<img class='{1}' id='{0}Marker' alt='' src='/sitecore/images/blank.gif' border='0'/>", (object) ID, Parent is FormSection ? (object) "scFbArrowField" : (object) "scFbArrowSection");
      stringBuilder.Append("</td>");
      stringBuilder.Append("</tr>");
      stringBuilder.Append("</table>");
      stringBuilder.Append("</div>");
      output.Write(stringBuilder.ToString());
    }

    private static string RenderFieldTypes(string selected)
    {
      Assert.ArgumentNotNull((object) selected, nameof (selected));
      Item obj = StaticSettings.ContextDatabase.GetItem(IDs.FormFieldsTypeRootID);
      Assert.IsNotNull((object) obj, typeof (Item), "Root of the fieds types \"" + (object) IDs.FormFieldsTypeRootID + "\" not found", new object[0]);
      StringBuilder stringBuilder = new StringBuilder();
      foreach (Item child1 in obj.Children)
      {
        if (child1.HasChildren)
        {
          stringBuilder.Append("<optgroup label=\"" + child1.DisplayName + "\">");
          foreach (Item child2 in child1.Children)
          {
            string strA = ((object) child2.ID).ToString();
            stringBuilder.AppendFormat("<option id=\"{0}\" value=\"{0}\" required=\"{1}\"", (object) strA, (object) ((BaseItem) child2).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldTypeRequiredID].Value);
            if (string.Compare(strA, selected, StringComparison.OrdinalIgnoreCase) == 0)
              stringBuilder.Append(" selected=\"selected\"");
            stringBuilder.Append(">" + child2.DisplayName + "</option>");
          }
          stringBuilder.Append("</optgroup>");
        }
      }
      return stringBuilder.ToString();
    }

    public bool Hide { get; set; }

    public string EmptyName { get; set; }

    public static string EmptyFieldName => DependenciesManager.ResourceManager.Localize("FIELD_NAME_EMPTY");

    public static string LocEmptyFieldName => DependenciesManager.ResourceManager.Localize("LOC_FIELD_NAME_EMPTY");

    public string DefaultName => string.Format(FormField.LocEmptyFieldName, (object) this.EmptyName);
  }
}
