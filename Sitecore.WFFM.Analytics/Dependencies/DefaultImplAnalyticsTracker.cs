// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Dependencies.DefaultImplAnalyticsTracker
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Analytics;
using Sitecore.Analytics.Core;
using Sitecore.Analytics.Data;
using Sitecore.Analytics.Model;
using Sitecore.Analytics.Tracking;
using Sitecore.Common;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Marketing.Definitions;
using Sitecore.Marketing.Definitions.Campaigns;
using Sitecore.Marketing.Definitions.Goals;
using Sitecore.Marketing.Definitions.PageEvents;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using Sitecore.WFFM.Analytics.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Sitecore.WFFM.Analytics.Dependencies
{
  public class DefaultImplAnalyticsTracker : IAnalyticsTracker
  {
    private readonly IItemRepository _itemRepository;
    private readonly IDefinitionManager<IGoalDefinition> _goalsDefinitionManager;
    private readonly IDefinitionManager<ICampaignActivityDefinition> _campaignsDefinitionManager;

    public DefaultImplAnalyticsTracker(
      IItemRepository itemRepository,
      IDefinitionManager<IGoalDefinition> goalsDefinitionManager,
      IDefinitionManager<ICampaignActivityDefinition> campaignsDefinitionManager)
    {
      Assert.ArgumentNotNull((object) itemRepository, nameof (itemRepository));
      Assert.IsNotNull((object) goalsDefinitionManager, nameof (goalsDefinitionManager));
      Assert.IsNotNull((object) campaignsDefinitionManager, nameof (campaignsDefinitionManager));
      this._itemRepository = itemRepository;
      this._goalsDefinitionManager = goalsDefinitionManager;
      this._campaignsDefinitionManager = campaignsDefinitionManager;
    }

    public ID SessionId => DependenciesManager.Logger.IsNull((object) this.CurrentContact, "CurrentContact") ? ID.Null : new ID(this.CurrentContact.ContactId);

    public DateTime BasePageTime
    {
      get => (DateTime) (Context.Items["SC_WFM_ANALYTICS_EVENT_BASE_TIME"] ?? (object) DateTime.UtcNow);
      set => Context.Items["SC_WFM_ANALYTICS_EVENT_BASE_TIME"] = (object) value;
    }

    public int EventCounter
    {
      get
      {
        int num = (int) (Context.Items["SC_WFM_ANALYTICS_EVENT_TICKS"] ?? (object) -1);
        int shortIntTicks;
        do
        {
          shortIntTicks = Sitecore.WFFM.Analytics.Core.DateUtil.GetShortIntTicks(DateTime.UtcNow - this.BasePageTime);
        }
        while (num == shortIntTicks);
        return shortIntTicks;
      }
    }

    public ITracker Current => Tracker.Current;

    public Contact CurrentContact => this.Current?.Contact;

    public ICurrentPageContext CurrentPage => this.Current?.CurrentPage;

    public Session CurrentSession => this.Current?.Session;

    public CurrentInteraction CurrentInteraction => this.Current?.Interaction;

    public int CurrentTrackerCurrentPageVisitPageIndex => ((IPage) this.Current.CurrentPage).VisitPageIndex;

    public IMarketingDefinitions DefinitionItems => Tracker.MarketingDefinitions;

    public bool IsRobot => this.CurrentContact != null && ContactClassification.IsRobot(this.CurrentContact.System.Classification);

    public void AddTag(string name, string value)
    {
      Assert.ArgumentNotNullOrEmpty(name, nameof (name));
      if (DependenciesManager.Logger.IsNull((object) this.CurrentContact, "CurrentContact"))
        return;
      this.CurrentContact.Tags.Add(name, value ?? string.Empty);
    }

    public string GetTag(string name)
    {
      Assert.ArgumentNotNullOrEmpty(name, nameof (name));
      return DependenciesManager.Logger.IsNull((object) this.CurrentContact, "CurrentContact") ? (string) null : this.CurrentContact.Tags[name];
    }

    public void RegisterFormDropouts()
    {
      if (DependenciesManager.Logger.IsNull((object) this.CurrentSession, "CurrentSession"))
        return;
      Session currentSession = this.CurrentSession;
      object obj;
      currentSession.CustomData.TryGetValue(Sitecore.WFFM.Abstractions.Analytics.Constants.SessionFormBeginTrack, out obj);
      if (!(obj is Dictionary<string, string> dictionary))
        return;
      foreach (KeyValuePair<string, string> keyValuePair in dictionary)
      {
        if (!string.IsNullOrEmpty(keyValuePair.Value))
        {
          string key = keyValuePair.Key;
          string s = keyValuePair.Value;
          int result;
          ID id;
          if (s != "-1" && int.TryParse(s, out result) && ID.TryParse(key, out id))
          {
            IPageContext currentContextPage = this.GetCurrentContextPage(new int?(result));
            if (currentContextPage != null)
              this.Register(currentContextPage, PageEventIds.FormDropout, Sitecore.WFFM.Abstractions.Analytics.Constants.FormDropoutEvent, string.Empty, string.Empty, id.Guid, false);
          }
        }
      }
      currentSession.CustomData.Remove(Sitecore.WFFM.Abstractions.Analytics.Constants.SessionFormBeginTrack);
    }

    public void TriggerCampaign(ICampaignActivityDefinition campaign)
    {
      Assert.ArgumentNotNull((object) campaign, nameof (campaign));
      this.GetCurrentContextPage()?.TriggerCampaign(this._itemRepository.GetCampaignItem(TypeExtensions.ToID(((IDefinition) campaign).Id)));
    }

    public void TriggerFieldEvent(ServerEvent serverEvent, bool onlyNew = true)
    {
      Assert.ArgumentNotNull((object) serverEvent, nameof (serverEvent));
      if ((this.GetCurrentContextPage()).PageEvents.Any<Sitecore.Analytics.Model.PageEventData>((Func<Sitecore.Analytics.Model.PageEventData, bool>) (pageEventData => this.AlreadyRegistered(pageEventData, serverEvent))))
        return;
      this.TriggerEvent(serverEvent);
    }

    public void TriggerEvent(ClientEvent clientEvent)
    {
      Assert.ArgumentNotNull((object) clientEvent, nameof (clientEvent));
      if (clientEvent.Value == "schidden")
        clientEvent.Value = "<schidden></schidden>";
      if (HttpContext.Current.Request.UrlReferrer == (Uri) null)
        return;
      Item obj1 = ID.IsID(clientEvent.FormID) ? Context.Database.GetItem(ID.Parse(clientEvent.FormID)) : Context.Database.GetItem(clientEvent.FormID);
      if (obj1 == null)
        return;
      if (clientEvent.Type == Sitecore.WFFM.Abstractions.Analytics.Constants.FieldCompletedEvent)
      {
        Item obj2 = Context.Database.GetItem(clientEvent.FieldID);
        if (obj2 != null)
        {
          IFieldItem fieldItem = this._itemRepository.CreateFieldItem(obj2);
          clientEvent.Data = fieldItem.FieldDisplayName;
          IFormItem formItem = this._itemRepository.CreateFormItem(obj1);
          if (fieldItem.IsSaveToStorage && formItem.IsSaveFormDataToStorage)
            clientEvent.Text = clientEvent.Value;
        }
      }
      IPageContext currentContextPage = this.GetCurrentContextPage(clientEvent.PageIndex);
      if (currentContextPage == null)
        return;
      ((IPage) currentContextPage).Url = new UrlData()
      {
        Path = HttpContext.Current.Request.UrlReferrer.AbsolutePath,
        QueryString = HttpContext.Current.Request.UrlReferrer.Query
      };
      if (clientEvent.PageID != null)
      {
        Item obj3 = ID.IsID(clientEvent.PageID) ? Context.Database.GetItem(ID.Parse(clientEvent.PageID)) : Context.Database.GetItem(clientEvent.PageID);
        if (Context.Item == null)
          Context.Item = obj3;
        if (obj3 != null)
          currentContextPage.SetItemProperties(obj3.ID.Guid, obj3.Language.Name, obj3.Version.Number);
      }
      this.CurrentPage.Cancel();
      this.Register(currentContextPage, (IEventBase) clientEvent);
    }

    public void TriggerGoal(Guid formId, Guid goalId, string data)
    {
      Assert.ArgumentNotNull((object) formId, nameof (formId));
      IPageContext currentContextPage = this.GetCurrentContextPage();
      if (currentContextPage == null)
        return;
      IGoalDefinition pageGoal = this.GetPageGoal(goalId);
      currentContextPage.RegisterGoal(pageGoal);
    }

    public Sitecore.Analytics.Model.PageEventData TriggerEvent(ServerEvent serverEvent)
    {
      Assert.ArgumentNotNull((object) serverEvent, nameof (serverEvent));
      if (serverEvent.Type == null)
        return null;
      if (Context.Database.GetItem(serverEvent.FormID) == null)
        return null;
      IPageContext currentContextPage = this.GetCurrentContextPage();
      return currentContextPage == null ? null : this.Register(currentContextPage, (IEventBase) serverEvent);
    }

    public Sitecore.Analytics.Model.PageEventData TriggerEvent(
      Guid eventId,
      string eventName,
      ID formId,
      string message,
      string action,
      string data)
    {
      return this.TriggerEvent(new ServerEvent()
      {
        FieldID = action,
        FormID = ((object) formId).ToString(),
        Type = eventName,
        Value = message,
        EventId = eventId,
        Data = data
      });
    }

    public Sitecore.Analytics.Model.PageEventData TriggerEvent(Sitecore.Analytics.Data.PageEventData pageEventData)
    {
      Assert.ArgumentNotNull((object) pageEventData, nameof (pageEventData));
      return this.GetCurrentContextPage()?.Register(pageEventData);
    }

    public Guid GetDataKey(string key)
    {
      Assert.ArgumentNotNullOrEmpty(key, nameof (key));
      ID id;
      return ID.TryParse(key, out id) ? id.Guid : Guid.Empty;
    }

    public IPageEventDefinition GetPageEvent(Guid id) => ((IEnumerable<IPageEventDefinition>) this.DefinitionItems.PageEvents).FirstOrDefault<IPageEventDefinition>((Func<IPageEventDefinition, bool>) (e => ((IDefinition) e).Id == id));

    public IGoalDefinition GetPageGoal(Guid id) => ((IEnumerable<IGoalDefinition>) this.DefinitionItems.Goals).FirstOrDefault<IGoalDefinition>((Func<IGoalDefinition, bool>) (e => ((IDefinition) e).Id == id));

    public IGoalDefinition GetPageGoal(string name) => ((IEnumerable<IGoalDefinition>) this.DefinitionItems.Goals).FirstOrDefault<IGoalDefinition>((Func<IGoalDefinition, bool>) (e => ((IDefinition) e).Name == name && ((object) e).GetType() == typeof (GoalDefinition)));

    public ICampaignActivityDefinition GetCampaign(Guid id) => this._campaignsDefinitionManager.Get(id, CultureInfo.InvariantCulture);

    public IGoalDefinition GetGoal(ID id) => this._goalsDefinitionManager.Get(id.ToGuid(), CultureInfo.InvariantCulture);

    public void Register(string fieldId, string eventId, string formId, string errorMessage)
    {
      Assert.ArgumentNotNullOrEmpty(fieldId, nameof (fieldId));
      Assert.ArgumentNotNullOrEmpty(eventId, nameof (eventId));
      Assert.ArgumentNotNullOrEmpty(formId, nameof (formId));
      Assert.ArgumentNotNullOrEmpty(errorMessage, nameof (errorMessage));
      this.TriggerEvent(new ServerEvent()
      {
        FieldID = fieldId,
        Type = eventId,
        FormID = formId,
        Value = errorMessage
      });
    }

    public void InitializeTracker()
    {
      if (!DependenciesManager.Settings.IsXdbTrackerEnabled || this.Current != null)
        return;
      Tracker.Initialize();
      if (this.Current == null)
        return;
      this.Current.Session.SetClassification(0, 0, false);
      this.Current.StartTracking();
    }

    private bool AlreadyRegistered(Sitecore.Analytics.Model.PageEventData pageEventData, ServerEvent serverEvent)
    {
      Assert.ArgumentNotNull((object) pageEventData, nameof (pageEventData));
      Assert.ArgumentNotNull((object) serverEvent, nameof (serverEvent));
      return !(pageEventData.Name != Sitecore.WFFM.Abstractions.Analytics.Constants.FieldCompletedEvent) && ((Entity) pageEventData).CustomValues.ContainsKey(Sitecore.WFFM.Abstractions.Analytics.Constants.FieldId) && ((Entity) pageEventData).CustomValues.ContainsKey(Sitecore.WFFM.Abstractions.Analytics.Constants.FieldValue) && (!(((Entity) pageEventData).CustomValues[Sitecore.WFFM.Abstractions.Analytics.Constants.FieldId].ToString() != serverEvent.FieldID) || !(((Entity) pageEventData).CustomValues[Sitecore.WFFM.Abstractions.Analytics.Constants.FieldValue].ToString() != serverEvent.Value));
    }

    private IPageContext GetCurrentContextPage(int? index = null)
    {
      if (this.Current == null && (Context.PageMode.IsPreview || Context.PageMode.IsExperienceEditorEditing))
        return (IPageContext) null;
      if (DependenciesManager.Logger.IsNull((object) this.CurrentSession, "CurrentSession"))
        return (IPageContext) null;
      if (this.CurrentSession.Interaction == null)
        return (IPageContext) null;
      return !index.HasValue || index.Value <= -1 ? this.CurrentSession.Interaction.PreviousPage ?? (IPageContext) this.CurrentPage : this.CurrentSession.Interaction.GetPage(index.Value);
    }

    private Sitecore.Analytics.Model.PageEventData Register(IPageContext page, IEventBase eventData)
    {
      Assert.ArgumentNotNull((object) page, nameof (page));
      Assert.ArgumentNotNull((object) eventData, nameof (eventData));
      Item obj = Context.Database.GetItem(eventData.Type);
      string name = obj != null ? obj.Name : eventData.Type;
      Guid dataKey = this.GetDataKey(eventData.FormID);
      return this.Register(page, eventData.EventId, name, eventData.Text, eventData.Data, dataKey);
    }

    private Sitecore.Analytics.Model.PageEventData Register(
      IPageContext page,
      Guid pageEventId,
      string name,
      string text,
      string data,
      Guid formId,
      bool registerFormBegin = true)
    {
      Assert.ArgumentNotNull((object) page, nameof (page));
      Assert.ArgumentNotNull((object) formId, nameof (formId));
      if (name == Sitecore.WFFM.Abstractions.Analytics.Constants.SubmitSuccessEvent && this.IsSuccessEventRegistered(formId))
        return null;
      if (registerFormBegin)
        this.RegisterFormBegin(page, formId, name == Sitecore.WFFM.Abstractions.Analytics.Constants.SubmitSuccessEvent);
            Sitecore.Analytics.Data.PageEventData pageEventData = new Sitecore.Analytics.Data.PageEventData(name ?? string.Empty, this.ResolveEventId(name, pageEventId))
      {
        Text = text ?? string.Empty,
        ItemId = formId,
        Data = data ?? string.Empty,
        DataKey = ((object) IDs.FormTemplateId).ToString()
      };
      return page.Register(pageEventData);
    }

    private bool IsSuccessEventRegistered(Guid formId)
    {
      Assert.ArgumentNotNull((object) formId, nameof (formId));
      return HttpContext.Current != null && (string) HttpContext.Current.Session[Sitecore.WFFM.Abstractions.Analytics.Constants.SessionFormBeginTrack + (object) formId] == "-1";
    }

    private void RegisterFormBegin(IPageContext page, Guid formId, bool submitSuccessForm)
    {
      Assert.ArgumentNotNull((object) page, nameof (page));
      Assert.ArgumentNotNull((object) formId, nameof (formId));
      if (DependenciesManager.Logger.IsNull((object) this.CurrentSession, "CurrentSession"))
        return;
      string key = formId.ToString();
      object obj;
      this.CurrentSession.CustomData.TryGetValue(Sitecore.WFFM.Abstractions.Analytics.Constants.SessionFormBeginTrack, out obj);
      if (!(obj is Dictionary<string, string> dictionary))
      {
        dictionary = new Dictionary<string, string>();
        this.CurrentSession.CustomData.Add(Sitecore.WFFM.Abstractions.Analytics.Constants.SessionFormBeginTrack, (object) dictionary);
      }
      if (submitSuccessForm)
        dictionary[key] = "-1";
      else if (!dictionary.TryGetValue(key, out string _))
      {
        dictionary.Add(key, ((IPage) page).VisitPageIndex.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        this.Register(page, PageEventIds.FormBegin, Sitecore.WFFM.Abstractions.Analytics.Constants.FormBeginEvent, string.Empty, string.Empty, formId);
      }
      else
      {
        if (!(dictionary[key] != "-1"))
          return;
        dictionary[key] = ((IPage) page).VisitPageIndex.ToString((IFormatProvider) CultureInfo.InvariantCulture);
      }
    }

    private Guid ResolveEventId(string eventName, Guid eventId)
    {
      if (!eventId.Equals(Guid.Empty))
        return eventId;
      if (eventName == Sitecore.WFFM.Abstractions.Analytics.Constants.FormBeginEvent)
        return PageEventIds.FormBegin;
      if (eventName == Sitecore.WFFM.Abstractions.Analytics.Constants.FormDropoutEvent)
        return PageEventIds.FormDropout;
      if (eventName == Sitecore.WFFM.Abstractions.Analytics.Constants.SubmitSuccessEvent)
        return PageEventIds.SubmitSuccessEvent;
      return eventName == Sitecore.WFFM.Abstractions.Analytics.Constants.FieldCompletedEvent ? PageEventIds.FieldCompleted : eventId;
    }
  }
}
