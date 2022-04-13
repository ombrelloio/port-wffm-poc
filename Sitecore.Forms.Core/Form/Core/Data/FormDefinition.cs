// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Data.FormDefinition
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Forms.Core.Data;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.Xml.Serialization;
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;

namespace Sitecore.Form.Core.Data
{
  [DebuggerDisplay("Sections = {Sections != null ? Sections.Count : 0}")]
  [XmlRoot("sitecore")]
  public class FormDefinition : XmlSerializable, IFormDefinition
  {
    private ArrayList sections;

    [XmlAttribute("displayname")]
    public string DisplayName { get; set; }

    [XmlAttribute("id")]
    public string FormID { get; set; }

    [XmlElement("section", typeof (SectionDefinition))]
    public ArrayList Sections
    {
      get
      {
        if (this.sections == null)
          this.sections = new ArrayList();
        return this.sections;
      }
      set => this.sections = value;
    }

    public static FormDefinition Parse(FormItem formItem)
    {
      Assert.ArgumentNotNull((object) formItem, nameof (formItem));
      FormDefinition form = new FormDefinition();
      form.FormID = ((object) formItem.ID).ToString();
      form.DisplayName = formItem.DisplayName;
      foreach (Item section in formItem.Sections)
        FormDefinition.LoadSection(form, formItem, section);
      return form;
    }

    public static FormDefinition Parse(string xml)
    {
      try
      {
        return XmlSerializable.LoadXml(xml, typeof (FormDefinition)) as FormDefinition;
      }
      catch
      {
      }
      return XmlSerializable.LoadXml("<sitecore/>", typeof (FormDefinition)) as FormDefinition;
    }

    public void AddSection(ISectionDefinition sectionDefinition)
    {
      if (this.Sections == null)
        this.Sections = new ArrayList();
      this.Sections.Add((object) sectionDefinition);
    }

    public ISectionDefinition GetSection(string id)
    {
      foreach (SectionDefinition section in this.Sections)
      {
        if (section.SectionID == id)
          return (ISectionDefinition) section;
      }
      SectionDefinition sectionDefinition = new SectionDefinition();
      sectionDefinition.SectionID = id;
      this.Sections.Add((object) sectionDefinition);
      return (ISectionDefinition) sectionDefinition;
    }

    public bool IsHasVisibleSection() => this.Sections.ToArray().Cast<SectionDefinition>().Where<SectionDefinition>((Func<SectionDefinition, bool>) (s => s.Deleted != "1")).Count<SectionDefinition>() > 1;

    private static void LoadField(SectionDefinition section, FieldItem field)
    {
      Assert.ArgumentNotNull((object) section, nameof (section));
      Assert.ArgumentNotNull((object) field, nameof (field));
      FieldDefinition fieldDefinition = new FieldDefinition()
      {
        FieldID = ((object) ((CustomItemBase) field).ID).ToString(),
        Name = field.Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldTitleID].Value,
        ControlID = ((object) ((CustomItemBase) field).ID.ToShortID()).ToString(),
        Conditions = field.Fields[Sitecore.Form.Core.Configuration.FieldIDs.ConditionsFieldID].Value
      };
      if (string.IsNullOrEmpty(fieldDefinition.Name))
        fieldDefinition.EmptyName = ((CustomItemBase) field).Name;
      string str = field.Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldLinkTypeID].Value;
      Item obj = ((CustomItemBase) field).Database.GetItem(str);
      if (obj != null)
      {
        fieldDefinition.Type = ((object) obj.ID).ToString();
        fieldDefinition.Properties = field.Parameters;
        fieldDefinition.LocProperties = field.LocalizedParameters;
      }
      fieldDefinition.IsValidate = field.Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldRequiredID].Value;
      fieldDefinition.IsTag = field.IsTag ? "1" : "0";
      section.AddField((IFieldDefinition) fieldDefinition);
    }

    private static void LoadSection(FormDefinition form, FormItem formItem, Item section)
    {
      Assert.ArgumentNotNull((object) form, nameof (form));
      Assert.ArgumentNotNull((object) formItem, nameof (formItem));
      Assert.ArgumentNotNull((object) section, nameof (section));
      SectionDefinition section1 = new SectionDefinition();
      if (section.ID != formItem.ID)
      {
        section1.SectionID = ((object) section.ID.ToShortID()).ToString();
        section1.Name = ((BaseItem) section).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldTitleID].Value;
        section1.Properties = ((BaseItem) section).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldParametersID].Value;
        section1.LocProperties = ((BaseItem) section).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldLocalizeParametersID].Value;
        section1.Conditions = ((BaseItem) section).Fields[Sitecore.Form.Core.Configuration.FieldIDs.ConditionsFieldID].Value;
        if (string.IsNullOrEmpty(section1.Name))
          section1.EmptyName = section.Name;
      }
      else
      {
        section1.SectionID = string.Empty;
        section1.Name = string.Empty;
      }
      form.AddSection((ISectionDefinition) section1);
      foreach (FieldItem field in formItem[section1.SectionID])
        FormDefinition.LoadField(section1, field);
    }
  }
}
