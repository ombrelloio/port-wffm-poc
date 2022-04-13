// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.FieldTypeItem
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Data;
using Sitecore.WFFM.Abstractions.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Sitecore.Forms.Core.Data
{
  public class FieldTypeItem : CustomItem, IFieldTypeItem
  {
    public FieldTypeItem(Item innerItem)
      : base(innerItem)
    {
    }

    public virtual string AssemblyName => ((BaseItem) ((CustomItemBase) this).InnerItem)[Sitecore.Form.Core.Configuration.FieldIDs.FieldTypeAssemblyID];

    public virtual string ClassName => ((BaseItem) ((CustomItemBase) this).InnerItem)[Sitecore.Form.Core.Configuration.FieldIDs.FieldTypeClassID];

    public virtual bool DenyTag => MainUtil.GetBool(((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.DenyTagID].Value, false);

    public virtual bool IsRequired => MainUtil.GetBool(((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldTypeRequiredID].Value, false);

    public virtual string LocalizedParameters => ((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldLocalizedParametersID].Value ?? string.Empty;

    public virtual string MVCClass => ((BaseItem) ((CustomItemBase) this).InnerItem)[Sitecore.Form.Core.Configuration.FieldIDs.MvcFieldId];

    public virtual string Parameters => ((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldParametersID].Value ?? string.Empty;

    public IEnumerable<ISubFieldItem> SubFields => (IEnumerable<ISubFieldItem>) ((IEnumerable<Item>) ((CustomItemBase) this).InnerItem.Children.ToArray()).Select<Item, SubFieldItem>((Func<Item, SubFieldItem>) (s => new SubFieldItem(s)));

    public virtual string UserControl => ((BaseItem) ((CustomItemBase) this).InnerItem)[Sitecore.Form.Core.Configuration.FieldIDs.FieldUserControlID];

    public virtual string[] Validators => ((BaseItem) ((CustomItemBase) this).InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldTypeValidation].Value.Split('|');

    public static bool IsFieldItem(Item item) => item.TemplateID == IDs.FieldTemplateID;

    [SpecialName]
    Item IFieldTypeItem.InnerItem => ((CustomItemBase) this).InnerItem;
  }
}
