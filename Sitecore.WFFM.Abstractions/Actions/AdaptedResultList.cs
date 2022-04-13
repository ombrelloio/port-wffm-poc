// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Actions.AdaptedResultList
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Sitecore.WFFM.Abstractions.Actions
{
  [Serializable]
  public class AdaptedResultList : IEnumerable<AdaptedControlResult>, IEnumerable
  {
    public static readonly AdaptedResultList Empty = new AdaptedResultList(new AdaptedControlResult[0]);
    private List<AdaptedControlResult> list;

    public AdaptedResultList(AdaptedControlResult[] array)
    {
      Assert.ArgumentNotNull((object) array, nameof (array));
      this.list = new List<AdaptedControlResult>((IEnumerable<AdaptedControlResult>) array);
    }

    public AdaptedResultList(List<AdaptedControlResult> list)
    {
      Assert.ArgumentNotNull((object) list, nameof (list));
      this.list = list;
    }

    public static implicit operator AdaptedResultList(
      List<AdaptedControlResult> list)
    {
      Assert.ArgumentNotNull((object) list, nameof (list));
      return new AdaptedResultList(list);
    }

    public bool IsTrueStatement(string condition)
    {
      if (string.IsNullOrEmpty(condition))
        return false;
      if (condition == "Always")
        return true;
      string str1 = condition;
      char[] chArray = new char[1]{ '|' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (str2.Length > 37)
        {
          int startIndex = 0;
          bool flag1 = str2.StartsWith("!");
          if (flag1)
            startIndex = 1;
          string id = str2.Substring(startIndex, 38);
          string str3 = str2.Substring(38 + startIndex, str2.Length - 38 - startIndex);
          AdaptedControlResult entryById = this.GetEntryByID(id);
          if (entryById != null)
          {
            string str4 = entryById.Value;
            if (str4 != null)
            {
              bool result;
              if (bool.TryParse(str4, out result))
                str4 = result ? "1" : "0";
              bool flag2 = str4.Contains("<item>" + str3 + "</item>") || str4.Contains("&lt;item&gt;" + str3 + "&lt;/item&gt;") || str4 == str3;
              if (flag1 == !flag2)
                return true;
            }
          }
        }
      }
      return false;
    }

    public string GetValueByFieldID(string id)
    {
      if (!string.IsNullOrEmpty(id))
      {
        AdaptedControlResult entryById = this.GetEntryByID(id);
        if (entryById != null)
          return entryById.Value ?? string.Empty;
      }
      return string.Empty;
    }

    public string GetValueByFieldID(ID id) => this.GetValueByFieldID(((object) id).ToString());

    public AdaptedControlResult GetEntryByID(string id) => string.IsNullOrEmpty(id) ? (AdaptedControlResult) null : this.list.Find((Predicate<AdaptedControlResult>) (s => s.FieldID == id));

    public AdaptedControlResult GetEntryByID(ID id)
    {
      Assert.ArgumentNotNull((object) id, nameof (id));
      return this.GetEntryByID(((object) id).ToString());
    }

    public AdaptedControlResult GetEntryByName(string name) => string.IsNullOrEmpty(name) ? (AdaptedControlResult) null : this.list.Find((Predicate<AdaptedControlResult>) (s => s.FieldName.ToLower() == name.ToLower()));

    public AdaptedControlResult GetEntry(string id, string name) => this.GetEntryByID(id) ?? this.GetEntryByName(name);

    public AdaptedControlResult GetEntry(ID id, string name)
    {
      Assert.ArgumentNotNull((object) id, nameof (id));
      return this.GetEntry(((object) id).ToString(), name);
    }

    IEnumerator<AdaptedControlResult> IEnumerable<AdaptedControlResult>.GetEnumerator() => (IEnumerator<AdaptedControlResult>) this.list.GetEnumerator();

    public IEnumerator GetEnumerator() => (IEnumerator) this.list.GetEnumerator();
  }
}
