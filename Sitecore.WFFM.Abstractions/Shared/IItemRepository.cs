// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Shared.IItemRepository
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Marketing.Definitions.Campaigns;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Dependencies;

namespace Sitecore.WFFM.Abstractions.Shared
{
  [DependencyPath("wffm/itemRepository")]
  public interface IItemRepository
  {
    IActionItem CreateAction(string itemId);

    IActionItem CreateAction(ID id);

    IActionItem CreateAction(Item item);

    IFieldItem CreateFieldItem(Item item);

    IFormItem CreateFormItem(Item item);

    IFormItem CreateFormItem(ID formId);

    Item GetItem(ID id);

    Item GetItem(ID id, Language lang);

    Item GetItem(string id);

    Item GetItem(ItemUri uri);

    Item GetItemFromMasterDatabase(ID id);

    ICampaignActivityDefinition GetCampaignItem(ID id);

    Item GetItemFromMasterDatabase(ID id, Language lang);

    Item GetItemFromMasterDatabase(string id);

    ISitecoreItem GetWrappedItem(ID id);

    ISitecoreItem GetWrappedItem(ID id, Language lang);

    ISitecoreItem GetWrappedItem(string id);

    ISitecoreItem GetWrappedItem(ItemUri uri);
  }
}
