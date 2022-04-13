// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.ListItemCollection
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.Forms.Core.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  [Editor("System.Web.UI.Design.WebControls.ListItemsCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
  [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
  public sealed class ListItemCollection : IList, ICollection, IEnumerable
  {
    private readonly ArrayList listItems = new ArrayList();

    public ListItemCollection()
    {
    }

    public ListItemCollection(ICollection list) => this.listItems.AddRange(list);

    public void Clear() => this.listItems.Clear();

    public void CopyTo(Array array, int index) => this.listItems.CopyTo(array, index);

    public IEnumerator GetEnumerator() => this.listItems.GetEnumerator();

    public void RemoveAt(int index) => this.listItems.RemoveAt(index);

    int IList.Add(object item)
    {
      Assert.ArgumentNotNull(item, nameof (item));
      return this.listItems.Add((object) (ListItem) item);
    }

    bool IList.Contains(object item) => this.Contains((ListItem) item);

    int IList.IndexOf(object item) => this.IndexOf((ListItem) item);

    void IList.Insert(int index, object item)
    {
      Assert.ArgumentNotNull(item, nameof (item));
      this.Insert(index, (ListItem) item);
    }

    void IList.Remove(object item) => this.Remove((ListItem) item);

    public void Add(string item) => this.Add(new ListItem(item ?? string.Empty));

    public void Add(ListItem item)
    {
      Assert.ArgumentNotNull((object) item, nameof (item));
      this.listItems.Add((object) item);
    }

    public void AddRange(ListItem[] items)
    {
      Assert.ArgumentNotNull((object) items, nameof (items));
      foreach (ListItem listItem in items)
      {
        if (listItem != null)
          this.Add(listItem);
      }
    }

    public bool Contains(ListItem item) => this.listItems.Contains((object) item);

    public ListItem FindByText(string text)
    {
      int byTextInternal = this.FindByTextInternal(text, true);
      return byTextInternal != -1 ? (ListItem) this.listItems[byTextInternal] : (ListItem) null;
    }

    internal int FindByTextInternal(string text, bool includeDisabled)
    {
      int num = 0;
      foreach (ListItem listItem in this.listItems)
      {
        if (listItem.Text.Equals(text) && (includeDisabled || listItem.Enabled))
          return num;
        ++num;
      }
      return -1;
    }

    public ListItem FindByValue(string value)
    {
      int byValueInternal = this.FindByValueInternal(value, true);
      return byValueInternal != -1 ? (ListItem) this.listItems[byValueInternal] : (ListItem) null;
    }

    internal int FindByValueInternal(string value, bool includeDisabled)
    {
      int num = 0;
      foreach (ListItem listItem in this.listItems)
      {
        if (listItem.Value.Equals(value) && (includeDisabled || listItem.Enabled))
          return num;
        ++num;
      }
      return -1;
    }

    public int IndexOf(ListItem item) => this.listItems.IndexOf((object) item);

    public void Insert(int index, string item) => this.Insert(index, new ListItem(item ?? string.Empty));

    public void Insert(int index, ListItem item)
    {
      Assert.ArgumentNotNull((object) item, nameof (item));
      this.listItems.Insert(index, (object) item);
    }

    public void Remove(string item)
    {
      int index = this.IndexOf(new ListItem(item));
      if (index < 0)
        return;
      this.RemoveAt(index);
    }

    public void Remove(ListItem item)
    {
      int index = this.IndexOf(item);
      if (index < 0)
        return;
      this.RemoveAt(index);
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (ListItem listItem in this.listItems)
        stringBuilder.AppendFormat(", {0}", (object) listItem.Value);
      return stringBuilder.Length != 0 ? stringBuilder.ToString(2, stringBuilder.Length - 2) : string.Empty;
    }

    public static implicit operator ListItemCollection(string values)
    {
      ListItemCollection listItemCollection = new ListItemCollection();
      string queries = HttpUtility.UrlDecode(values);
      foreach (KeyValuePair<string, string> keyValuePair in !queries.StartsWith(StaticSettings.SourceMarker) ? (IEnumerable<KeyValuePair<string, string>>) QueryManager.Select(QuerySettings.ParseRange(queries)).ToDictionary() : (IEnumerable<KeyValuePair<string, string>>)Sitecore.Form.Core.Utility.Utils.GetItemsName(queries.Substring(StaticSettings.SourceMarker.Length)).ToDictionary<string, string, string>((Func<string, string>) (i => i), (Func<string, string>) (i => i)))
      {
        ListItem listItem = new ListItem(keyValuePair.Value, keyValuePair.Key);
        listItemCollection.Add(listItem);
      }
      return listItemCollection;
    }

    public ListItem[] ToArray()
    {
      List<ListItem> listItemList = new List<ListItem>();
      foreach (ListItem listItem in this.listItems)
        listItemList.Add(listItem);
      return listItemList.ToArray();
    }

    public int Capacity
    {
      get => this.listItems.Capacity;
      set => this.listItems.Capacity = value;
    }

    public ListItem this[int index] => (ListItem) this.listItems[index];

    public int Count => this.listItems.Count;

    public bool IsReadOnly => this.listItems.IsReadOnly;

    public bool IsSynchronized => this.listItems.IsSynchronized;

    public object SyncRoot => (object) this;

    bool IList.IsFixedSize => false;

    object IList.this[int index]
    {
      get => this.listItems[index];
      set => this.listItems[index] = value;
    }
  }
}
