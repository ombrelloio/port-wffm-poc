// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.ContentEditor.IGroupDefinition
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sitecore.WFFM.Abstractions.ContentEditor
{
  public interface IGroupDefinition
  {
    [XmlAttribute("id")]
    string ID { get; set; }

    [XmlIgnore]
    IEnumerable<IListItemDefinition> ListItems { get; set; }

    [XmlAttribute("displayName")]
    string DisplayName { get; set; }

    [XmlAttribute("onclick")]
    string OnClick { get; set; }

    bool IsEqual(IGroupDefinition definition);

    IListItemDefinition GetListItem(int index);

    IListItemDefinition GetListItem(string unicid);

    IEnumerable<IListItemDefinition> GetListItems(Sitecore.Data.ID actionID);

    void AddListItem(IListItemDefinition listItemDefinition);

    void RemoveListItem(int index);

    void RemoveListItem(IListItemDefinition listItemDefinition);

    void InsertListItem(int index, IListItemDefinition item);
  }
}
