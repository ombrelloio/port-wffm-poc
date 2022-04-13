// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Data.ISectionDefinition
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Xml.Serialization;
using System.Collections;
using System.Xml.Serialization;

namespace Sitecore.WFFM.Abstractions.Data
{
  public interface ISectionDefinition
  {
    [XmlAttribute("cci")]
    string ClientControlID { get; set; }

    [XmlAttribute("condition")]
    string Conditions { get; set; }

    [XmlAttribute("deleted")]
    string Deleted { get; set; }

    [XmlAttribute("emptyname")]
    string EmptyName { get; set; }

    [XmlElement("field", typeof (IFieldDefinition))]
    ArrayList Fields { get; set; }

    bool IsHasOnlyEmptyField { get; }

    [XmlAttribute("locproperties")]
    string LocProperties { get; set; }

    [XmlAttribute("name")]
    string Name { get; set; }

    [XmlAttribute("properties")]
    string Properties { get; set; }

    [XmlAttribute("id")]
    string SectionID { get; set; }

    [XmlAttribute("sortorder")]
    string Sortorder { get; set; }

    void AddField(IFieldDefinition fieldDefinition);

    Item CreateCorrespondingItem(Item parent, Language language);

    IFieldDefinition GetField(string id);

    Item UpdateSharedFields(Database database, Item item);

    string ToXml();

    string ToXml(string xsltFilename);

    XmlSerializable LoadXml(string xml);

    XmlSerializable LoadXml(string xml, string xsltFilename);

    void SaveAsXml(string filename);
  }
}
