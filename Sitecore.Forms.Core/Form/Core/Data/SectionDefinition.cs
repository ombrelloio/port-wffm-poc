// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Data.SectionDefinition
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Form.Core.Configuration;
using Sitecore.Globalization;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.Xml.Serialization;
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;

namespace Sitecore.Form.Core.Data
{
  [XmlRoot("section")]
  [DebuggerDisplay("Fields = {Fields != null ? Fields.Count : 0}", Name = "{Name}")]
  public class SectionDefinition : XmlSerializable, ISectionDefinition
  {
    private ArrayList fields;
    private string sectionID;
    private string clientControlID;

    public SectionDefinition()
    {
      this.Properties = string.Empty;
      this.LocProperties = string.Empty;
      this.EmptyName = string.Empty;
    }

    [XmlAttribute("cci")]
    public string ClientControlID
    {
      get => this.clientControlID ?? this.SectionID;
      set => this.clientControlID = value;
    }

    [XmlAttribute("condition")]
    public string Conditions { get; set; }

    [XmlAttribute("deleted")]
    public string Deleted { get; set; }

    [XmlAttribute("emptyname")]
    public string EmptyName { get; set; }

    [XmlElement("field", typeof (FieldDefinition))]
    public ArrayList Fields
    {
      get => this.fields ?? (this.fields = new ArrayList());
      set => this.fields = value;
    }

    public bool IsHasOnlyEmptyField
    {
      get
      {
        bool flag = true;
        foreach (FieldDefinition field in this.Fields)
        {
          if (!string.IsNullOrEmpty(field.Name) && field.Deleted != "1")
          {
            flag = false;
            break;
          }
        }
        return flag;
      }
    }

    [XmlAttribute("locproperties")]
    public string LocProperties { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("properties")]
    public string Properties { get; set; }

    [XmlAttribute("id")]
    public string SectionID
    {
      get => this.sectionID ?? string.Empty;
      set => this.sectionID = value;
    }

    [XmlAttribute("sortorder")]
    public string Sortorder { get; set; }

    public static SectionDefinition Parse(string xml)
    {
      try
      {
        return (SectionDefinition) XmlSerializable.LoadXml(xml, typeof (SectionDefinition));
      }
      catch
      {
        return new SectionDefinition();
      }
    }

    public void AddField(IFieldDefinition fieldDefinition)
    {
      if (this.Fields == null)
        this.Fields = new ArrayList();
      this.Fields.Add((object) fieldDefinition);
    }

    public Item CreateCorrespondingItem(Item parent, Language language)
    {
      Database database = parent.Database;
      bool flag = false;
      if (this.fields == null || this.fields.Count <= 0)
        return (Item) null;
      Item obj = database.GetItem(this.sectionID, language);
      if (obj != null)
      {
        if ((obj.Parent.ID!=parent.ID))
          ItemManager.MoveItem(obj, parent);
      }
      else
      {
        flag = true;
        obj = database.GetItem(this.CreateItem(parent).ID, language);
      }
      if (obj != null)
      {
        obj.Editing.BeginEdit();
        if (flag && !string.IsNullOrEmpty(this.Name))
          obj.Name = ItemUtil.ProposeValidItemName(this.Name);
        ((BaseItem) obj).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldTitleID].Value = this.Name;
        ((BaseItem) obj).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldLocalizeParametersID].Value = this.LocProperties;
        obj.Editing.EndEdit();
      }
      this.UpdateSharedFields(parent.Database, obj);
      return obj;
    }

    public IFieldDefinition GetField(string id) => (IFieldDefinition) this.Fields.OfType<FieldDefinition>().FirstOrDefault<FieldDefinition>((Func<FieldDefinition, bool>) (definition => definition.FieldID == id));

    public Item UpdateSharedFields(Database database, Item item)
    {
      Item obj = item ?? database.GetItem(this.SectionID);
      if (obj != null)
      {
        obj.Editing.BeginEdit();
        ((BaseItem) obj).Fields[(ID) FieldIDs.Sortorder].Value = this.Sortorder;
        ((BaseItem) obj).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldParametersID].Value = this.Properties;
        ((BaseItem) obj).Fields[Sitecore.Form.Core.Configuration.FieldIDs.ConditionsFieldID].Value = this.Conditions;
        obj.Editing.EndEdit();
      }
      return obj;
    }

    private Item CreateItem(Item parent)
    {
      string str = !string.IsNullOrEmpty(this.Name) ? ItemUtil.ProposeValidItemName(this.Name) : "unknown section";
      if (string.IsNullOrEmpty(str))
        str = "unknown section";
      return ItemManager.CreateItem(str, parent, IDs.SectionTemplateID);
    }
  }
}
