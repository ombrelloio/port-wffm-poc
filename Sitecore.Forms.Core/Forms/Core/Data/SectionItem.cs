// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.SectionItem
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Form.Core.Configuration;
using System.Collections.Generic;

namespace Sitecore.Forms.Core.Data
{
  public class SectionItem : CustomItem
  {
    public SectionItem(Item innerItem)
      : base(innerItem)
    {
    }

    public string Title => ((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldTitleID].Value;

    public string LocalizedParameters => ((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldLocalizedParametersID].Value;

    public string Parameters => ((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldParametersID].Value;

    public string Conditions => ((BaseItem) ((CustomItemBase) this).InnerItem)[Sitecore.Form.Core.Configuration.FieldIDs.ConditionsFieldID];

    public FieldItem[] Fields
    {
      get
      {
        List<FieldItem> fieldItemList = new List<FieldItem>();
        foreach (Item child in ((CustomItemBase) this).InnerItem.Children)
        {
          if (child.TemplateID == IDs.FieldTemplateID)
            fieldItemList.Add(new FieldItem(child));
        }
        return fieldItemList.ToArray();
      }
    }
  }
}
