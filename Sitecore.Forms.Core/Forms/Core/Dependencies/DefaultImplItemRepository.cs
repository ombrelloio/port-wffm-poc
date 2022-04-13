// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Dependencies.DefaultImplItemRepository
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Data;
using Sitecore.Forms.Core.Data;
using Sitecore.Globalization;
using Sitecore.Marketing.Definitions.Campaigns;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Shared;
using System;

namespace Sitecore.Forms.Core.Dependencies
{
  [Serializable]
  public class DefaultImplItemRepository : IItemRepository
  {
    private readonly Database _contextDatabase;
    private readonly Database _masterDatabase;

    public DefaultImplItemRepository()
      : this(StaticSettings.ContextDatabase, StaticSettings.MasterDatabase)
    {
    }

    public DefaultImplItemRepository(Database contextDatabase)
      : this(contextDatabase, StaticSettings.MasterDatabase)
    {
    }

    public DefaultImplItemRepository(Database contextDatabase, Database masterDatabase)
    {
      Assert.IsNotNull((object) contextDatabase, nameof (contextDatabase));
      this._contextDatabase = contextDatabase;
      this._masterDatabase = masterDatabase;
    }

    public IActionItem CreateAction(string itemId)
    {
      Assert.ArgumentNotNullOrEmpty(itemId, nameof (itemId));
      ID id;
      return ID.TryParse(itemId, out id) ? this.CreateAction(id) : (IActionItem) null;
    }

    public IActionItem CreateAction(ID id)
    {
      Assert.ArgumentNotNull((object) id, nameof (id));
      Item obj = this._contextDatabase.GetItem(id);
      return obj != null && ActionItem.IsAction(obj) ? (IActionItem) new ActionItem(obj) : (IActionItem) null;
    }

    public IActionItem CreateAction(Item item)
    {
      Assert.ArgumentNotNull((object) item, nameof (item));
      return (IActionItem) new ActionItem(item);
    }

    public IFieldItem CreateFieldItem(Item item) => (IFieldItem) new FieldItem(item);

    public IFormItem CreateFormItem(Item item) => (IFormItem) new FormItem(item);

    public IFormItem CreateFormItem(ID formId)
    {
      Item innerItem = this.GetItem(formId);
      return innerItem == null ? (IFormItem) null : (IFormItem) new FormItem(innerItem);
    }

    public Item GetItem(ID id) => this.GetItem(this._contextDatabase, id);

    public Item GetItem(ID id, Language lang) => this.GetItem(this._contextDatabase, id, lang);

    public Item GetItem(string id) => this.GetItem(this._contextDatabase, ID.Parse(id));

    public Item GetItem(ItemUri uri) => Database.GetItem(uri);

    public Item GetItemFromMasterDatabase(ID id) => this.GetItem(this._masterDatabase, id);

    public ICampaignActivityDefinition GetCampaignItem(ID id)
    {
      Item obj = this.GetItem(id);
      return (ICampaignActivityDefinition) new CampaignActivityDefinition(obj.ID.Guid, obj.Name, obj.Language.CultureInfo, obj.Name, obj.Created, obj.Name);
    }

    public Item GetItemFromMasterDatabase(ID id, Language lang) => this.GetItem(this._masterDatabase, id, lang);

    public Item GetItemFromMasterDatabase(string id) => this.GetItem(this._masterDatabase, ID.Parse(id));

    public ISitecoreItem GetWrappedItem(ID id) => (ISitecoreItem) new SitecoreItemWrapper(this.GetItem(id));

    public ISitecoreItem GetWrappedItem(ID id, Language lang) => (ISitecoreItem) new SitecoreItemWrapper(this.GetItem(id, lang));

    public ISitecoreItem GetWrappedItem(string id) => (ISitecoreItem) new SitecoreItemWrapper(this.GetItem(id));

    public ISitecoreItem GetWrappedItem(ItemUri uri) => (ISitecoreItem) new SitecoreItemWrapper(this.GetItem(uri));

    private Item GetItem(Database db, ID id) => db.GetItem(id);

    private Item GetItem(Database db, ID id, Language lang) => db.GetItem(id, lang);
  }
}
