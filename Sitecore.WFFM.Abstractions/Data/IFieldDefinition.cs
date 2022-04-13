// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Data.IFieldDefinition
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Xml.Serialization;
using System.Xml.Serialization;

namespace Sitecore.WFFM.Abstractions.Data
{
  public interface IFieldDefinition
  {
    bool Active { get; set; }

    [XmlAttribute("cci")]
    string ClientControlID { get; set; }

    [XmlAttribute("condition")]
    string Conditions { get; set; }

    [XmlAttribute("controlid")]
    string ControlID { get; set; }

    [XmlAttribute("deleted")]
    string Deleted { get; set; }

    [XmlAttribute("emptyname")]
    string EmptyName { get; set; }

    [XmlAttribute("id")]
    string FieldID { get; set; }

    [XmlAttribute("tag")]
    string IsTag { get; set; }

    [XmlAttribute("validate")]
    string IsValidate { get; set; }

    [XmlAttribute("locproperties")]
    string LocProperties { get; set; }

    [XmlAttribute("name")]
    string Name { get; set; }

    [XmlAttribute("properties")]
    string Properties { get; set; }

    string Sortorder { get; set; }

    [XmlAttribute("type")]
    string Type { get; set; }

    Item CreateCorrespondingItem(Item parent, Language language);

    void UpdateSharedFields(Item parent, Item field, Database database);

    string ToXml();

    string ToXml(string xsltFilename);

    XmlSerializable LoadXml(string xml);

    XmlSerializable LoadXml(string xml, string xsltFilename);

    void SaveAsXml(string filename);
  }
}
