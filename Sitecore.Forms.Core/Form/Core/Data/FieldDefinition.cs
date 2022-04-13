// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Data.FieldDefinition
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Globalization;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.Xml.Serialization;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Sitecore.Form.Core.Data
{
  [DebuggerDisplay("Type = {Type}", Name = "{Name}")]
  [XmlRoot("field")]
  public class FieldDefinition : XmlSerializable, IFieldDefinition
  {
    private string clientControlID;

    public FieldDefinition()
    {
      this.ControlID = string.Empty;
      this.Deleted = "0";
      this.FieldID = string.Empty;
      this.Type = string.Empty;
      this.Name = string.Empty;
      this.IsValidate = "0";
      this.IsTag = "0";
      this.Properties = string.Empty;
      this.LocProperties = string.Empty;
      this.Sortorder = string.Empty;
    }

    public bool Active { get; set; }

    [XmlAttribute("cci")]
    public string ClientControlID
    {
      get => this.clientControlID ?? this.ControlID;
      set => this.clientControlID = value;
    }

    [XmlAttribute("condition")]
    public string Conditions { get; set; }

    [XmlAttribute("controlid")]
    public string ControlID { get; set; }

    [XmlAttribute("deleted")]
    public string Deleted { get; set; }

    [XmlAttribute("emptyname")]
    public string EmptyName { get; set; }

    [XmlAttribute("id")]
    public string FieldID { get; set; }

    [XmlAttribute("tag")]
    public string IsTag { get; set; }

    [XmlAttribute("validate")]
    public string IsValidate { get; set; }

    [XmlAttribute("locproperties")]
    public string LocProperties { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("properties")]
    public string Properties { get; set; }

    public string Sortorder { get; set; }

    [XmlAttribute("type")]
    public string Type { get; set; }

    public Item CreateCorrespondingItem(Item parent, Language language)
    {
      Assert.ArgumentNotNull((object) parent, nameof (parent));
      Database database = parent.Database;
      Item field = database.GetItem(this.FieldID, language) ?? database.GetItem(this.CreateItem(parent).ID, language);
      bool flag = false;
      if (field == null)
      {
        flag = true;
        field = database.GetItem(this.CreateItem(parent).ID, language);
      }
      if (field != null)
      {
        field.Editing.BeginEdit();
        if (flag)
          ((BaseItem) field).Fields[(ID) FieldIDs.DisplayName].Value = this.Name;
        ((BaseItem) field).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldTitleID].Value = this.Name;
        ((BaseItem) field).Fields[(ID) FieldIDs.Sortorder].Value = this.Sortorder;
        ((BaseItem) field).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldParametersID].Value = this.Properties;
        ((BaseItem) field).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldLinkTypeID].Value = this.Type;
        ((BaseItem) field).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldRequiredID].Value = this.IsValidate;
        ((BaseItem) field).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldTagID].Value = this.IsTag;
        ((BaseItem) field).Fields[Sitecore.Form.Core.Configuration.FieldIDs.ConditionsFieldID].Value = this.Conditions;
        ((BaseItem) field).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldLocalizeParametersID].Value = this.LocProperties;
        field.Editing.EndEdit();
      }
      this.UpdateSharedFields(parent, field, database);
      return field;
    }

    public void UpdateSharedFields(Item parent, Item field, Database database)
    {
      Item obj1 = field ?? parent;
      Item obj2 = obj1 == null ? database.GetItem(this.FieldID) : obj1.Database.GetItem(this.FieldID);
      if (obj2 == null)
        return;
      if (parent != null && obj2.Parent.ID != parent.ID)
        ItemManager.MoveItem(obj2, parent);
      obj2.Editing.BeginEdit();
      ((BaseItem) obj2).Fields[(ID) FieldIDs.Sortorder].Value = this.Sortorder;
      ((BaseItem) obj2).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldParametersID].Value = this.Properties;
      ((BaseItem) obj2).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldLinkTypeID].Value = this.Type;
      ((BaseItem) obj2).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldRequiredID].Value = this.IsValidate;
      ((BaseItem) obj2).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldTagID].Value = this.IsTag;
      ((BaseItem) obj2).Fields[Sitecore.Form.Core.Configuration.FieldIDs.ConditionsFieldID].Value = this.Conditions;
      obj2.Editing.EndEdit();
    }

    private Item CreateItem(Item parent)
    {
      string str = !string.IsNullOrEmpty(this.Name) ? ItemUtil.ProposeValidItemName(this.Name) : "unknown field";
      if (string.IsNullOrEmpty(str))
        str = "unknown field";
      return ItemManager.CreateItem(str, parent, IDs.FieldTemplateID);
    }
  }
}
