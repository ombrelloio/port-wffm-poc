// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Data.Tracking
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Analytics.Data;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Forms.Core.Dependencies;
using Sitecore.Marketing.Definitions;
using Sitecore.Marketing.Definitions.Campaigns;
using Sitecore.Marketing.Definitions.Events;
using Sitecore.Marketing.Definitions.Goals;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace Sitecore.Form.Core.Data
{
  public class Tracking : ITracking
  {
    private readonly IAnalyticsTracker _analyticsTracker;
    private readonly IDefinitionManager<ICampaignActivityDefinition> _campaignsDefinitionManager;
    private readonly IDefinitionManager<IGoalDefinition> _goalsDefinitionManager;
    private readonly IItemRepository _itemRepository;
    private readonly XElement _tracking;
    private Dictionary<Guid, PageEventData> _allevents;
    private IEnumerable<ICampaignActivityDefinition> _campaigns;
    private Item _goal;
    private IEnumerable<PageEventData> _goals;

    public Tracking(
      string xml,
      IItemRepository itemRepository,
      IAnalyticsTracker analyticsTracker,
      IDefinitionManager<IGoalDefinition> goalsDefinitionManager,
      IDefinitionManager<ICampaignActivityDefinition> campaignsDefinitionManager)
    {
      Assert.ArgumentNotNull((object) xml, nameof (xml));
      Assert.IsNotNull((object) itemRepository, nameof (itemRepository));
      Assert.IsNotNull((object) analyticsTracker, nameof (analyticsTracker));
      Assert.IsNotNull((object) goalsDefinitionManager, nameof (goalsDefinitionManager));
      Assert.IsNotNull((object) campaignsDefinitionManager, nameof (campaignsDefinitionManager));
      this._itemRepository = itemRepository;
      this._analyticsTracker = analyticsTracker;
      this._goalsDefinitionManager = goalsDefinitionManager;
      this._campaignsDefinitionManager = campaignsDefinitionManager;
      this._tracking = string.IsNullOrEmpty(xml) ? new XElement((XName) "tracking") : XDocument.Parse(xml).Root ?? new XElement((XName) "tracking");
    }

    public Tracking(string xml, Database database)
      : this(xml, (IItemRepository) new DefaultImplItemRepository(database), DependenciesManager.AnalyticsTracker, DependenciesManager.Resolve<IDefinitionManager<IGoalDefinition>>(DependenciesPaths.GoalsDefinitionManagerPath), DependenciesManager.Resolve<IDefinitionManager<ICampaignActivityDefinition>>(DependenciesPaths.CampaignsDefinitionManagerPath))
    {
      Assert.ArgumentNotNull((object) database, nameof (database));
      this.Database = database;
    }

    public IEnumerable<ICampaignActivityDefinition> Campaigns => this._campaigns ?? (this._campaigns = this.GetCampaigns());

    public IEnumerable<PageEventData> Goals => this._goals ?? (this._goals = this.GetGoals());

    public Dictionary<Guid, PageEventData> AllEvents => this._allevents ?? (this._allevents = this.GetEvents());

    public Item Goal => this._goal ?? (this._goal = this.GetGoal());

    public bool Ignore => MainUtil.GetBool(Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(this._tracking, "ignore"), false);

    public Database Database { get; }

    public bool IsDropoutTrackingEnabled => !this.Ignore && this.ContainsDropoutEvents();

    public void AddEvent(Guid id)
    {
      IEventDefinition eventItem = (IEventDefinition) this._analyticsTracker.GetPageEvent(id) ?? (IEventDefinition) this._analyticsTracker.GetPageGoal(id);
      if (eventItem == null || this._tracking.Descendants((XName) "event").Where<XElement>((Func<XElement, bool>) (s => Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(s, "name") == ((IDefinition) eventItem).Name)).Any<XElement>())
        return;
      this._tracking.Add((object) new XElement((XName) "event", new object[2]
      {
        (object) new XAttribute((XName) "name", (object) ((IDefinition) eventItem).Name),
        (object) new XAttribute((XName) nameof (id), (object) ((IDefinition) eventItem).Id.ToString())
      }));
    }

    public void RemoveAllEventsAndGoals() => this._tracking.Descendants((XName) "event").Remove<XElement>();

    public void SetCampaignEvent(Guid eventId)
    {
      this._tracking.Descendants((XName) "campaign").Remove<XElement>();
      ICampaignActivityDefinition campaign = this._analyticsTracker.GetCampaign(eventId);
      if (campaign == null)
        return;
      this._tracking.Add((object) new XElement((XName) "campaign", new object[2]
      {
        (object) new XAttribute((XName) "title", (object) ((IDefinition) campaign).Name),
        (object) new XAttribute((XName) "id", (object) eventId.ToString("B").ToUpperInvariant())
      }));
    }

    public void SetIgnore(bool isIgnore)
    {
      XAttribute xattribute = this._tracking.Attribute((XName) "ignore");
      if (xattribute != null)
      {
        if (!isIgnore)
          xattribute.Remove();
        else
          xattribute.Value = "1";
      }
      else
      {
        if (!isIgnore)
          return;
        this._tracking.Add((object) new XAttribute((XName) "ignore", (object) "1"));
      }
    }

    public override string ToString() => this._tracking.ToString();

    public void Update(bool enableAnalytics, bool enableDropout)
    {
      this.SetIgnore(!enableAnalytics);
      this.RemoveAllEventsAndGoals();
      if (!enableAnalytics)
        return;
      Item obj = this._itemRepository.GetItem(IDs.DefaultOptions);
      if (obj == null)
        return;
      string str1 = ((BaseItem) obj).Fields[Sitecore.Form.Core.Configuration.FieldIDs.AnalyticsEvents].Value;
      char[] chArray1 = new char[1]{ '|' };
      foreach (string str2 in str1.Split(chArray1))
      {
        ID id;
        if (ID.TryParse(str2, out id))
          this.AddEvent(id.Guid);
      }
      if (!enableDropout)
        return;
      string str3 = ((BaseItem) obj).Fields[Sitecore.Form.Core.Configuration.FieldIDs.DropoutEvents].Value;
      char[] chArray2 = new char[1]{ '|' };
      foreach (string str4 in str3.Split(chArray2))
      {
        ID id;
        if (ID.TryParse(str4, out id))
          this.AddEvent(id.Guid);
      }
    }

    public bool ContainsDropoutEvents()
    {
            Item item = _itemRepository.GetItem(IDs.DefaultOptions);
            if (item == null)
            {
                return false;
            }
            Guid[] @object = (from x in ((BaseItem)item).Fields[Sitecore.Form.Core.Configuration.FieldIDs.DropoutEvents].Value
                    .Split('|')
                              select new ID(x).Guid).ToArray();
            return ((IEnumerable<Guid>)AllEvents.Keys).Any((Func<Guid, bool>)((IEnumerable<Guid>)@object).Contains);
        }

    private Item GetGoal()
    {
      PageEventData pageEventData = this.Goals.FirstOrDefault<PageEventData>();
      return pageEventData == null ? (Item) null : this._itemRepository.GetItem(new ID(pageEventData.PageEventDefinitionId));
    }

    private Dictionary<Guid, PageEventData> GetEvents() => this._tracking.Elements((XName) "event").Select<XElement, PageEventData>((Func<XElement, PageEventData>) (e => new PageEventData(Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(e, "name"), new Guid(Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(e, "id")))
    {
      DataKey = ((object) IDs.FormTemplateID).ToString(),
      Data = Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(e, "name")
    })).ToDictionary<PageEventData, Guid, PageEventData>((Func<PageEventData, Guid>) (x => x.PageEventDefinitionId), (Func<PageEventData, PageEventData>) (y => y));

    private IEnumerable<PageEventData> GetGoals() => ((IEnumerable<KeyValuePair<Guid, PageEventData>>) this.AllEvents).Where<KeyValuePair<Guid, PageEventData>>((Func<KeyValuePair<Guid, PageEventData>, bool>) (x => this._goalsDefinitionManager.Get(x.Key, CultureInfo.InvariantCulture) != null)).Select<KeyValuePair<Guid, PageEventData>, PageEventData>((Func<KeyValuePair<Guid, PageEventData>, PageEventData>) (x => x.Value));

    private IEnumerable<ICampaignActivityDefinition> GetCampaigns() => this._campaigns = this._tracking.Elements((XName) "campaign").Select<XElement, XAttribute>((Func<XElement, XAttribute>) (e => e.Attribute((XName) "id"))).Where<XAttribute>((Func<XAttribute, bool>) (a => a != null)).Select<XAttribute, ICampaignActivityDefinition>((Func<XAttribute, ICampaignActivityDefinition>) (a => this._campaignsDefinitionManager.Get(new ID(a.Value).Guid, CultureInfo.InvariantCulture)));
  }
}
