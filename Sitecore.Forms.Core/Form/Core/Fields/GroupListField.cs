// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Fields.GroupListField
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.ContentEditor.Data;
using Sitecore.Links;
using Sitecore.WFFM.Abstractions.ContentEditor;
using Sitecore.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Sitecore.Form.Core.Fields
{
  public class GroupListField : CustomField
  {
    private readonly XmlDocument data;

    public GroupListField(Field innerField)
      : base(innerField)
    {
      Assert.ArgumentNotNull((object) innerField, nameof (innerField));
      this.data = this.LoadData();
    }

    public static ID ExtractGroupID(XmlNode group)
    {
      Assert.ArgumentNotNull((object) group, nameof (group));
      string attribute = XmlUtil.GetAttribute("ID", group);
      return attribute.Length > 0 && ID.IsID(attribute) ? ID.Parse(attribute) : ID.Null;
    }

    public static ListItemReference[] ExtractReferences(XmlNode group)
    {
      Assert.IsNotNull((object) group, "group is null");
      XmlNodeList xmlNodeList = group.SelectNodes("li");
      Assert.IsNotNull((object) xmlNodeList, "Selected node is not exist");
      ListItemReference[] listItemReferenceArray = new ListItemReference[xmlNodeList.Count];
      for (int i = 0; i < xmlNodeList.Count; ++i)
        listItemReferenceArray[i] = new ListItemReference(xmlNodeList[i]);
      return listItemReferenceArray;
    }

    public XmlNode GetGroupNode(Item folder)
    {
      if (folder == null)
        return (XmlNode) null;
      Assert.IsNotNull((object) this.Data, "Data is null");
      Assert.IsNotNull((object) this.Data.DocumentElement, "Data.DocumentElement is null");
      return this.Data.DocumentElement.SelectSingleNode("g[@id='" + (object) folder.ID + "']");
    }

    public ID GetGroupID(Item group)
    {
      Assert.ArgumentNotNull((object) group, nameof (group));
      XmlNode groupNode = this.GetGroupNode(group);
      return groupNode != null ? GroupListField.ExtractGroupID(groupNode) : ID.Null;
    }

    public ListItemReference[] GetReferences(Item group)
    {
      Assert.ArgumentNotNull((object) group, nameof (group));
      XmlNode groupNode = this.GetGroupNode(group);
      return groupNode != null ? GroupListField.ExtractReferences(groupNode) : (ListItemReference[]) null;
    }

    private XmlDocument LoadData()
    {
      string str = this.Value;
      return !string.IsNullOrEmpty(str) ? XmlUtil.LoadXml(str) : XmlUtil.LoadXml("<li/>");
    }

    public override void Relink(ItemLink itemLink, Item newLink)
    {
      Assert.ArgumentNotNull((object) itemLink, nameof (itemLink));
      Assert.ArgumentNotNull((object) newLink, nameof (newLink));
      string xml = this.Value;
      if (string.IsNullOrEmpty(xml))
        return;
      ListDefinition listDefinition = ListDefinition.Parse(xml);
      if (listDefinition.Groups == null)
        return;
      string str1 = ((object) itemLink.TargetItemID).ToString();
      string str2 = ((object) newLink.ID).ToString();
      foreach (IGroupDefinition group in listDefinition.Groups)
      {
        if (group != null)
        {
          if (group.ID == str1)
            group.ID = str2;
          else if (group.ListItems != null)
          {
            foreach (IListItemDefinition listItem in group.ListItems)
            {
              if (listItem != null && listItem.ItemID == str1)
                listItem.ItemID = str2;
            }
          }
        }
      }
      this.InnerField.Item.Editing.BeginEdit();
      this.Value = listDefinition.ToXml();
      this.InnerField.Item.Editing.EndEdit();
    }

    public override void RemoveLink(ItemLink itemLink)
    {
      Assert.ArgumentNotNull((object) itemLink, nameof (itemLink));
      string xml = this.Value;
      if (string.IsNullOrEmpty(xml))
        return;
      ListDefinition listDefinition = ListDefinition.Parse(xml);
      if (listDefinition.Groups == null)
        return;
      string str = ((object) itemLink.TargetItemID).ToString();
      List<IGroupDefinition> list1 = listDefinition.Groups.ToList<IGroupDefinition>();
      foreach (IGroupDefinition group in listDefinition.Groups)
      {
        if (group != null)
        {
          if (group.ID == str)
            list1.Remove(group);
          else if (group.ListItems != null)
          {
            List<IListItemDefinition> list2 = group.ListItems.ToList<IListItemDefinition>();
            foreach (IListItemDefinition listItem in group.ListItems)
            {
              if (listItem != null && listItem.ItemID == str)
                list2.Remove(listItem);
            }
            group.ListItems = (IEnumerable<IListItemDefinition>) list2;
          }
        }
      }
      listDefinition.Groups = (IEnumerable<IGroupDefinition>) list1;
      this.InnerField.Item.Editing.BeginEdit();
      this.Value = listDefinition.ToXml();
      this.InnerField.Item.Editing.EndEdit();
    }

    public static implicit operator GroupListField(Field field) => field != null ? new GroupListField(field) : (GroupListField) null;

    public override void ValidateLinks(LinksValidationResult result)
    {
      Assert.ArgumentNotNull((object) result, nameof (result));
      string xml = this.Value;
      if (string.IsNullOrEmpty(xml))
        return;
      foreach (GroupDefinition group in ListDefinition.Parse(xml).Groups)
      {
        if (!string.IsNullOrEmpty(group.ID))
        {
          Item obj = this.InnerField.Database.GetItem(group.ID);
          if (obj != null)
            result.AddValidLink(obj, group.ID);
          else
            result.AddBrokenLink(group.ID);
        }
        if (group.ListItems != null)
        {
          foreach (ListItemDefinition listItem in group.ListItems)
          {
            Item obj = this.InnerField.Database.GetItem(listItem.ItemID);
            if (obj != null)
              result.AddValidLink(obj, listItem.ItemID);
            else
              result.AddBrokenLink(listItem.ItemID);
          }
        }
      }
    }

    public XmlDocument Data => this.data;
  }
}
