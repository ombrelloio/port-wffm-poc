// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.ContentEditor.Data.ListDefinition
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.ContentEditor;
using Sitecore.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Sitecore.Form.Core.ContentEditor.Data
{
  [XmlRoot("li")]
  [Serializable]
  public class ListDefinition : XmlSerializable, IListDefinition
  {
    private List<GroupDefinition> groups = new List<GroupDefinition>();

    public static ListDefinition Parse(string xml)
    {
      Assert.IsNotNullOrEmpty(xml, "Parameter xml is null or empty");
      return XmlSerializable.LoadXml(xml, typeof (ListDefinition)) as ListDefinition;
    }

    public bool IsEqual(IListDefinition definition)
    {
      if (definition == null)
        return false;
      if (this == definition)
        return true;
      if (this.Groups.Count<IGroupDefinition>() != definition.Groups.Count<IGroupDefinition>())
        return false;
      return !this.Groups.Any<IGroupDefinition>() || !this.Groups.Any<IGroupDefinition>((Func<IGroupDefinition, bool>) (g => definition.Groups.Any<IGroupDefinition>((Func<IGroupDefinition, bool>) (d => !d.IsEqual(g)))));
    }

    [XmlElement("g", typeof (GroupDefinition))]
    public List<GroupDefinition> GroupsForSerialization
    {
      get => this.groups;
      set
      {
        Assert.IsNotNull((object) value, "Should be List<GroupDefinition> object");
        this.groups = value;
      }
    }

    [XmlIgnore]
    public IEnumerable<IGroupDefinition> Groups
    {
      get => (IEnumerable<IGroupDefinition>) this.groups;
      set
      {
        Assert.IsNotNull((object) value, "Should be IEnumerable<GroupDefinition> object");
        this.groups = value.Cast<GroupDefinition>().ToList<GroupDefinition>();
      }
    }

    public void AddGroup(IGroupDefinition groupDefinition)
    {
      Assert.IsNotNull((object) groupDefinition, "Argument groupDefinition is null");
      this.groups.Add((GroupDefinition) groupDefinition);
    }

    public void RemoveGroup(IGroupDefinition groupDefinition)
    {
      Assert.IsNotNull((object) groupDefinition, "Argument groupDefinition is null");
      this.groups.Remove((GroupDefinition) groupDefinition);
    }
  }
}
