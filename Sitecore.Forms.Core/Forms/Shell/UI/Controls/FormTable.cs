// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.FormTable
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Utility;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.WFFM.Abstractions.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public class FormTable : System.Web.UI.WebControls.WebControl
    {
    public Dictionary<string, Sitecore.Web.UI.HtmlControls.Control> dictControls = new Dictionary<string, Sitecore.Web.UI.HtmlControls.Control>();

    public FormTable()
      : base(HtmlTextWriterTag.Div.ToString())
    {
    }

    protected override void OnLoad(EventArgs e)
    {
      if (Sitecore.Context.ClientPage.IsEvent)
        return;
      this.Reorganize(false, string.Empty);
    }

    public void Reorganize(bool upgrade, string id)
    {
      this.Controls.Clear();
      FormDefinition definition = FormDefinition.Parse(this.XmlDefinition);
      if (upgrade)
      {
        foreach (SectionDefinition section in definition.Sections)
        {
          if (string.IsNullOrEmpty(section.SectionID))
          {
            section.SectionID = id;
            break;
          }
        }
      }
      this.InitFormData(definition);
    }

    private void InitFormData(FormDefinition definition)
    {
      this.Attributes.Add("cellpadding", "0");
      this.Attributes.Add("cellspacing", "0");
      this.Attributes.Add("onselectstart", "return false;");
      this.ExpandSections(definition);
    }

    private System.Web.UI.Control GetEmptyScope() => (System.Web.UI.Control) this;

    private void ExpandSections(FormDefinition definition)
    {
      bool flag1 = true;
      bool flag2 = false;
      int num = 0;
      foreach (SectionDefinition section in definition.Sections)
      {
                Sitecore.Web.UI.HtmlControls.Control control = (Sitecore.Web.UI.HtmlControls.Control) null;
        if (!string.IsNullOrEmpty(section.SectionID) || definition.IsHasVisibleSection())
        {
          control = FormTable.AddSection(section);
          this.Controls.Add((System.Web.UI.Control) control);
          flag1 = false;
          ++num;
        }
        if (control == null)
          this.Controls.Add((System.Web.UI.Control) new ControlLiteral("<div id=\"FormBuilder_empty\" class=\"emptyScopeForFields\">"));
        foreach (FieldDefinition field in section.Fields)
        {
          FormTable.AddField((System.Web.UI.Control) (control ?? this.GetEmptyScope()), field);
          flag2 = true;
          flag1 = false;
        }
        if (control == null)
          this.Controls.Add((System.Web.UI.Control) new ControlLiteral("</div>"));
      }
      if (flag1)
      {
        this.Controls.Add((System.Web.UI.Control) FormDesignerUtils.GlobalButtons(this.ID));
      }
      else
      {
        if (!flag2 || num != 0)
          return;
        this.Controls.Add((System.Web.UI.Control) FormDesignerUtils.FieldButtons(this.ID));
      }
    }

    private static Sitecore.Web.UI.HtmlControls.Control AddSection(SectionDefinition section)
    {
      if (string.IsNullOrEmpty(section.SectionID))
      {
        section.SectionID = ((object) Sitecore.Data.ID.NewID.ToShortID()).ToString();
      }
      else
      {
        ID id;
        if (Sitecore.Data.ID.TryParse(section.SectionID, out id))
          section.SectionID = ((object) id.ToShortID()).ToString();
      }
      return FormDesignerUtils.GetSection(section);
    }

    private static void AddField(System.Web.UI.Control section, FieldDefinition field)
    {
      Assert.ArgumentNotNull((object) section, nameof (section));
      Assert.ArgumentNotNull((object) field, nameof (field));
      if (string.IsNullOrEmpty(field.ControlID))
        field.ControlID = ((object) Sitecore.Data.ID.NewID.ToShortID()).ToString();
      section.Controls.Add((System.Web.UI.Control) FormDesignerUtils.GetField(field.ControlID, field));
    }

    public Sitecore.Web.UI.HtmlControls.Control GetSection(string id)
    {
      foreach (System.Web.UI.Control control in this.Controls)
      {
        if (control.ID == id)
          return (Sitecore.Web.UI.HtmlControls.Control) control;
      }
      return (Sitecore.Web.UI.HtmlControls.Control) null;
    }

    public void UpdateBuilder()
    {
      NameValueCollection form = Sitecore.Context.ClientPage.ClientRequest.Form;
      FormDefinition formDefinition = new FormDefinition()
      {
        FormID = form["FormID"]
      };
      string str1 = form["Structure"];
      if (!string.IsNullOrEmpty(str1))
      {
        string[] strArray = str1.Split(',');
        SectionDefinition sectionDefinition = (SectionDefinition) null;
        foreach (string id in strArray)
        {
          if (!string.IsNullOrEmpty(id))
          {
            string emptyValue;
            string str2 = this.FormatName(id, FormSection.LocEmptySectionName, form[id + "_section_name"], out emptyValue);
            if (str2 != null)
            {
              string str3 = str2.Trim();
              sectionDefinition = new SectionDefinition()
              {
                ClientControlID = id,
                Name = str3 == FormSection.EmptySectionName ? string.Empty : str3,
                SectionID = form[id + FormSection.KeySectionId],
                Properties = form[id + FormSection.KeySectionProperties],
                LocProperties = form[id + FormSection.KeySectionLocProperties],
                Deleted = form[id + FormSection.KeySectionDeleted],
                Sortorder = (formDefinition.Sections.Count * 100).ToString(),
                EmptyName = emptyValue
              };
              if (string.IsNullOrEmpty(sectionDefinition.SectionID))
              {
                sectionDefinition.SectionID = id;
              }
              else
              {
                ShortID shortId;
                if (ShortID.TryParse(sectionDefinition.SectionID, out shortId))
                  sectionDefinition.SectionID = ((object) shortId.ToID()).ToString();
              }
              formDefinition.AddSection((ISectionDefinition) sectionDefinition);
            }
            else
            {
              if (sectionDefinition == null || id == "withoutsection")
              {
                sectionDefinition = new SectionDefinition();
                formDefinition.AddSection((ISectionDefinition) sectionDefinition);
                if (id == "withoutsection")
                  continue;
              }
              string str4 = this.FormatName(id, FormField.LocEmptyFieldName, form[id + FormField.KeyFieldName], out emptyValue).Trim();
              FieldDefinition fieldDefinition = new FieldDefinition()
              {
                ClientControlID = id,
                ControlID = id,
                Name = str4 == FormField.EmptyFieldName ? string.Empty : str4,
                Sortorder = (sectionDefinition.Fields.Count * 100).ToString(),
                FieldID = form[id + FormField.KeyFieldID],
                Properties = ParametersUtil.EncodeNodesText(form[id + FormField.KeyFieldProperties]),
                LocProperties = ParametersUtil.EncodeNodesText(form[id + FormField.KeyFieldLocProperties]),
                Type = form[id + FormField.KeyFieldType],
                IsValidate = !string.IsNullOrEmpty(form[id + FormField.KeyFieldValidate]) ? "1" : "0",
                IsTag = string.IsNullOrEmpty(form[id + FormField.KeyFieldTag]) || !(form[id + FormField.KeyFieldTag] == "1") ? "" : "1",
                Deleted = form[id + FormField.KeyFieldDelete],
                EmptyName = emptyValue
              };
              sectionDefinition.AddField((IFieldDefinition) fieldDefinition);
            }
          }
        }
      }
      this.XmlDefinition = formDefinition.ToXml();
    }

    private string FormatName(string id, string emptyMessage, string value, out string emptyValue)
    {
      emptyValue = string.Empty;
      ShortID shortId;
      if (ShortID.TryParse(id, out shortId))
      {
        Item obj = StaticSettings.ContextDatabase.GetItem(shortId.ToID());
        if (obj != null && string.Format(emptyMessage, (object) obj.Name) == value)
        {
          emptyValue = obj.Name;
          return string.Empty;
        }
      }
      return value;
    }

    private int FindFirstSectionPos(Sitecore.Web.UI.HtmlControls.Control control)
    {
      int index = 0;
      while (control.Controls.Count > index && !(control.Controls[index] is FormSection))
        ++index;
      return control.Controls.Count <= index ? -1 : index;
    }

    private int FindFirstFieldPos(Sitecore.Web.UI.HtmlControls.Control control)
    {
      int index = 0;
      while (control.Controls.Count > index && !(control.Controls[index] is FormField))
        ++index;
      return control.Controls.Count <= index ? -1 : index;
    }

    public string XmlDefinition
    {
      get => StringUtil.GetString(this.ViewState[nameof (XmlDefinition)]);
      set
      {
        Assert.ArgumentNotNull((object) value, nameof (value));
        this.ViewState[nameof (XmlDefinition)] = (object) value;
      }
    }
  }
}
