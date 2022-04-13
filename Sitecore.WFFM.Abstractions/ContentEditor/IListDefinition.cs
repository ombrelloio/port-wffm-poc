// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.ContentEditor.IListDefinition
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sitecore.WFFM.Abstractions.ContentEditor
{
  public interface IListDefinition
  {
    [XmlIgnore]
    IEnumerable<IGroupDefinition> Groups { get; set; }

    bool IsEqual(IListDefinition definition);

    void AddGroup(IGroupDefinition groupDefinition);

    void RemoveGroup(IGroupDefinition groupDefinition);
  }
}
