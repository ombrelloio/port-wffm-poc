// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Data.SubFieldItem
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Form.Core.Configuration;
using Sitecore.WFFM.Abstractions.Data;
using System.Runtime.CompilerServices;

namespace Sitecore.Form.Core.Data
{
  public class SubFieldItem : CustomItem, ISubFieldItem
  {
    public SubFieldItem(Item innerItem)
      : base(innerItem)
    {
    }

    public string DefaultTitle => ((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.SubFieldDefaultTitle].Value;

    public string Key
    {
      get
      {
        string str = ((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.SubFieldKeyTitle].Value;
        return string.IsNullOrEmpty(str) ? ((object) ((CustomItemBase) this).InnerItem.ID).ToString() : str;
      }
    }

    public string TitleProperty => ((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.SubFieldTitleProperty].Value;

    [SpecialName]
    Item ISubFieldItem.InnerItem => ((CustomItemBase) this).InnerItem;
  }
}
