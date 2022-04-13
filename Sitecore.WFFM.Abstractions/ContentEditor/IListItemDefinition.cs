// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.ContentEditor.IListItemDefinition
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Data;
using Sitecore.Globalization;
using System.Xml.Serialization;

namespace Sitecore.WFFM.Abstractions.ContentEditor
{
  public interface IListItemDefinition
  {
    [XmlAttribute("id")]
    string ItemID { get; set; }

    [XmlElement("parameters")]
    string Parameters { get; set; }

    [XmlAttribute("unicid")]
    string Unicid { get; set; }

    string GetTitle();

    void SetFailedMessageForLanguage(string text, Language language);

    string GetFailedMessageForLanguage(Language language, string defaultValue);

    string GetFailedMessageForLanguage(Language language, bool tryGetDefaultValue, ID formID);

    bool IsEqual(IListItemDefinition definition);
  }
}
