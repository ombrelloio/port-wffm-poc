// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.ListField
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.WFFM.Abstractions.Data;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Forms.Core.Data
{
  public class ListField
  {
    private List<Pair<string, string>> values;

    public ListField(IFieldItem field) => this.Field = field;

    public IFieldItem Field { get; private set; }

    public bool IsBidirectional => ItemUtil.IsFieldContainsValue(IDs.DefaultOptions, Sitecore.Form.Core.Configuration.FieldIDs.BidirectionalFields, ((object) this.Field.TypeID).ToString());

    public bool IsEmpty => this.Values.Count<Pair<string, string>>() == 0;

    public bool IsUnidirectional => ItemUtil.IsFieldContainsValue(IDs.DefaultOptions, Sitecore.Form.Core.Configuration.FieldIDs.UnidirectionalFields, ((object) this.Field.TypeID).ToString());

    public IEnumerable<Pair<string, string>> Values
    {
      get
      {
        if (this.values == null)
          this.LoadItems();
        return (IEnumerable<Pair<string, string>>) this.values;
      }
    }

    private bool IsCanBeCondition => this.IsUnidirectional || this.IsBidirectional;

    private void AddToList(
      List<Pair<string, string>> list,
      ID fieldId,
      string title,
      string value)
    {
      this.values.Add(new Pair<string, string>(title, Sitecore.StringExtensions.StringExtensions.FormatWith("{0}{1}", new object[2]
      {
        (object) this.Field.ID,
        (object) value
      })));
      if (!this.IsBidirectional)
        return;
      this.values.Add(new Pair<string, string>(title, Sitecore.StringExtensions.StringExtensions.FormatWith("!{0}{1}", new object[2]
      {
        (object) this.Field.ID,
        (object) value
      })));
    }

    private void LoadItems()
    {
      this.values = new List<Pair<string, string>>();
      List<string> stringList = FieldReflectionUtil.ListAdapt(this.Field);
      if ((stringList == null || stringList.Count == 0) && this.IsCanBeCondition)
        this.AddToList(this.values, this.Field.ID, this.Field.FieldDisplayName, "1");
      if (stringList == null)
        return;
      foreach (string title in stringList)
        this.AddToList(this.values, this.Field.ID, title, title);
    }
  }
}
