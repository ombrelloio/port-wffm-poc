// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.ContentEditor.Data.GroupDefinition
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
  [XmlRoot("g")]
  [Serializable]
  public class GroupDefinition : XmlSerializable, IGroupDefinition
  {
    private string id;
    private List<ListItemDefinition> items = new List<ListItemDefinition>();
    private string displayName;
    private string onclick;

    [XmlAttribute("id")]
    public string ID
    {
      get => this.id;
      set => this.id = value;
    }

    [XmlElement("li", typeof (ListItemDefinition))]
    public List<ListItemDefinition> ListItemsForSerialization
    {
      get => this.items;
      set => this.items = value;
    }

    [XmlIgnore]
    public IEnumerable<IListItemDefinition> ListItems
    {
      get => (IEnumerable<IListItemDefinition>) this.items;
      set => this.items = value.Cast<ListItemDefinition>().ToList<ListItemDefinition>();
    }

    [XmlAttribute("displayName")]
    public string DisplayName
    {
      get => this.displayName;
      set => this.displayName = value;
    }

    [XmlAttribute("onclick")]
    public string OnClick
    {
      get => this.onclick;
      set => this.onclick = value;
    }

    public bool IsEqual(IGroupDefinition definition)
    {
      if (definition == null)
        return false;
      if (this == definition)
        return true;
      if (!object.Equals((object) definition.ID, (object) this.id) || !object.Equals((object) definition.DisplayName, (object) this.displayName) || !object.Equals((object) definition.OnClick, (object) this.onclick) || this.ListItems.Count<IListItemDefinition>() != definition.ListItems.Count<IListItemDefinition>())
        return false;
      return !this.ListItems.Any<IListItemDefinition>() || !this.ListItems.Any<IListItemDefinition>((Func<IListItemDefinition, bool>) (g => definition.ListItems.Any<IListItemDefinition>((Func<IListItemDefinition, bool>) (d => !d.IsEqual(g)))));
    }

    public IListItemDefinition GetListItem(string unicid) => !string.IsNullOrEmpty(unicid) ? this.ListItems.FirstOrDefault<IListItemDefinition>((Func<IListItemDefinition, bool>) (li => li.Unicid == unicid)) : (IListItemDefinition) null;

    public IListItemDefinition GetListItem(int index)
    {
      Assert.ArgumentCondition(index >= 0, nameof (index), "Parameter index out of range");
      Assert.ArgumentCondition(index < this.items.Count, nameof (index), "Parameter index out of range");
      return (IListItemDefinition) this.items[index];
    }

    public void RemoveListItem(int index)
    {
      Assert.ArgumentCondition(index >= 0, nameof (index), "Parameter index out of range");
      Assert.ArgumentCondition(index < this.items.Count, nameof (index), "Parameter index out of range");
      this.items.RemoveAt(index);
    }

    public void RemoveListItem(IListItemDefinition listItemDefinition) => this.items.Remove((ListItemDefinition) listItemDefinition);

    public void InsertListItem(int index, IListItemDefinition item)
    {
      Assert.IsNotNull((object) item, "Parameter item is null");
      Assert.ArgumentCondition(index >= 0, nameof (index), "Parameter index out of range");
      Assert.ArgumentCondition(index <= this.items.Count, nameof (index), "Parameter index out of range");
      this.items.Insert(index, (ListItemDefinition) item);
    }

    public void AddListItem(IListItemDefinition listItemDefinition) => this.items.Add((ListItemDefinition) listItemDefinition);

    public IEnumerable<IListItemDefinition> GetListItems(Sitecore.Data.ID actionID)
    {
      Assert.ArgumentNotNull((object) actionID, "Parameter actionID is null");
      string action = ((object) actionID).ToString();
      return this.ListItems.Where<IListItemDefinition>((Func<IListItemDefinition, bool>) (s => s.ItemID == action));
    }
  }
}
