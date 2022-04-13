// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Data.IFormDefinition
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Xml.Serialization;
using System.Collections;
using System.Xml.Serialization;

namespace Sitecore.WFFM.Abstractions.Data
{
  public interface IFormDefinition
  {
    [XmlAttribute("displayname")]
    string DisplayName { get; set; }

    [XmlAttribute("id")]
    string FormID { get; set; }

    [XmlElement("section", typeof (ISectionDefinition))]
    ArrayList Sections { get; set; }

    void AddSection(ISectionDefinition sectionDefinition);

    ISectionDefinition GetSection(string id);

    bool IsHasVisibleSection();

    string ToXml();

    string ToXml(string xsltFilename);

    XmlSerializable LoadXml(string xml);

    XmlSerializable LoadXml(string xml, string xsltFilename);

    void SaveAsXml(string filename);
  }
}
