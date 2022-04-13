// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Actions.SystemActions.GoalAction
// Assembly: Sitecore.WFFM.Actions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6607DF78-1371-42DC-8675-A986B5291018
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Actions.dll

using Sitecore.Analytics.Data;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Shared;
using Sitecore.WFFM.Actions.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Sitecore.WFFM.Actions.SystemActions
{
  [Required("IsXdbTrackerEnabled", true)]
  public class GoalAction : WffmSystemAction
  {
    private readonly IItemRepository itemRepository;
    private readonly IFieldProvider fieldProvider;
    private readonly IAnalyticsTracker analyticsTracker;
    private readonly ISettings settings;
    private readonly IContextProvider contextProvider;
    private readonly ILogger logger;

    public GoalAction(
      IItemRepository itemRepository,
      IFieldProvider fieldProvider,
      IAnalyticsTracker analyticsTracker,
      ISettings settings,
      IContextProvider contextProvider,
      ILogger logger)
    {
      Assert.ArgumentNotNull((object) itemRepository, nameof (itemRepository));
      Assert.ArgumentNotNull((object) fieldProvider, nameof (fieldProvider));
      Assert.ArgumentNotNull((object) analyticsTracker, nameof (analyticsTracker));
      Assert.ArgumentNotNull((object) settings, nameof (settings));
      Assert.ArgumentNotNull((object) contextProvider, nameof (contextProvider));
      Assert.ArgumentNotNull((object) logger, nameof (logger));
      this.itemRepository = itemRepository;
      this.fieldProvider = fieldProvider;
      this.analyticsTracker = analyticsTracker;
      this.settings = settings;
      this.contextProvider = contextProvider;
      this.logger = logger;
    }

    public override void Execute(
      ID formId,
      AdaptedResultList adaptedFields,
      ActionCallContext actionCallContext = null,
      params object[] data)
    {
      IFormItem formItem = this.itemRepository.CreateFormItem(formId);
      if (formItem == null)
      {
        this.logger.Warn("Cannot create form item for id: " + (object) formId, (object) this);
      }
      else
      {
        this.TrackActionFailure(formId);
        ITracking tracking = formItem.Tracking;
        if (tracking != null)
        {
          foreach (PageEventData goal in tracking.Goals)
          {
            goal.Data = this.fieldProvider.GetFieldDisplayName(goal.PageEventDefinitionId.ToString());
            this.analyticsTracker.TriggerEvent(goal);
          }
        }
        this.TrackSubmitSuccessEvent(formId, (IEnumerable<AdaptedControlResult>) adaptedFields, formItem);
      }
    }

    private void TrackSubmitSuccessEvent(
      ID formId,
      IEnumerable<AdaptedControlResult> fields,
      IFormItem formItem)
    {
      if (this.analyticsTracker.Current == null && (this.settings.IsPreview || this.settings.IsPageEditorEditing))
        return;
      List<FieldData> fieldDataList = (List<FieldData>) null;
      if (formItem.IsSaveFormDataToStorage && fields != null)
        fieldDataList = fields.Where<AdaptedControlResult>((Func<AdaptedControlResult, bool>) (f => ((IEnumerable<IFieldItem>) formItem.Fields).FirstOrDefault<IFieldItem>((Func<IFieldItem, bool>) (itemField => ((object) itemField.ID).ToString() == f.FieldID && itemField.IsSaveToStorage)) != null)).Select<AdaptedControlResult, FieldData>((Func<AdaptedControlResult, FieldData>) (field =>
        {
          FieldData fieldData = new FieldData()
          {
            FieldId = new Guid(field.FieldID),
            Value = field.Secure ? string.Empty : field.Value,
            FieldName = field.FieldName
          };
          IFieldItem fieldItem = this.itemRepository.CreateFieldItem(this.itemRepository.GetItem(new ID(field.FieldID)));
          if (this.fieldProvider.FieldCanListAdapt(fieldItem))
            fieldData.Values = this.fieldProvider.ListAdapt(fieldItem, field.Secure ? string.Empty : field.Value).ToList<string>();
          return fieldData;
        })).ToList<FieldData>();
      string empty = string.Empty;
      if (fieldDataList != null)
      {
        StringBuilder sb = new StringBuilder();
        using (TextWriter textWriter = (TextWriter) new StringWriter(sb))
        {
          new XmlSerializer(fieldDataList.GetType()).Serialize(textWriter, (object) fieldDataList);
          empty = sb.ToString();
        }
      }
      this.analyticsTracker.TriggerEvent(PageEventIds.SubmitSuccessEvent, Sitecore.WFFM.Abstractions.Analytics.Constants.SubmitSuccessEvent, formId, string.Empty, ((object) formId).ToString(), empty);
    }

    private void TrackActionFailure(ID formID)
    {
      foreach (ExecuteResult.Failure formFailure in this.contextProvider.FormFailures)
      {
        string id = formFailure.FailedAction;
        ISitecoreItem sitecoreItem = (ISitecoreItem) null;
        if (!string.IsNullOrEmpty(id))
        {
          sitecoreItem = this.itemRepository.GetWrappedItem(id);
          if (sitecoreItem != null)
            id = sitecoreItem.Name + "[id=" + id + "]";
        }
        this.logger.Warn(Sitecore.StringExtensions.StringExtensions.FormatWith("The '{0}' save action failed: {1}", new object[2]
        {
          (object) id,
          (object) formFailure.ErrorMessage
        }), (object) formFailure.FailedAction);
        this.analyticsTracker.TriggerEvent(PageEventIds.FormSaveActionFailure, string.Empty, formID, string.Format("{0}: {1}", sitecoreItem != null ? (object) sitecoreItem.Name : (object) formFailure.FailedAction, string.IsNullOrEmpty(formFailure.ApiErrorMessage) ? (object) formFailure.ErrorMessage : (object) formFailure.ApiErrorMessage), formFailure.FailedAction, string.Empty);
      }
    }

    public override ActionState QueryState(ActionQueryContext queryContext)
    {
      Assert.ArgumentNotNull((object) queryContext, nameof (queryContext));
      return queryContext.Tracking == null || !queryContext.Form.IsAnalyticsEnabled ? ActionState.Hidden : base.QueryState(queryContext);
    }
  }
}
