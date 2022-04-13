// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Actions.SystemActions.TagAction
// Assembly: Sitecore.WFFM.Actions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6607DF78-1371-42DC-8675-A986B5291018
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Actions.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Shared;
using Sitecore.WFFM.Actions.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.WFFM.Actions.SystemActions
{
  [Required("IsXdbTrackerEnabled", true)]
  public class TagAction : WffmSystemAction
  {
    private readonly IItemRepository itemRepository;
    private readonly IFieldProvider fieldProvider;
    private readonly IAnalyticsTracker analyticsTracker;
    private readonly ISettings settings;

    public TagAction(
      IItemRepository itemRepository,
      IFieldProvider fieldProvider,
      IAnalyticsTracker analyticsTracker,
      ISettings settings)
    {
      Assert.ArgumentNotNull((object) itemRepository, nameof (itemRepository));
      Assert.ArgumentNotNull((object) fieldProvider, nameof (fieldProvider));
      Assert.ArgumentNotNull((object) analyticsTracker, nameof (analyticsTracker));
      Assert.ArgumentNotNull((object) settings, nameof (settings));
      this.itemRepository = itemRepository;
      this.fieldProvider = fieldProvider;
      this.analyticsTracker = analyticsTracker;
      this.settings = settings;
    }

    public override void Execute(
      ID formid,
      AdaptedResultList adaptedFields,
      ActionCallContext actionCallContext = null,
      params object[] data)
    {
      IFormItem formItem = this.itemRepository.CreateFormItem(formid);
      if (formItem == null || this.QueryState(new ActionQueryContext(formItem)) == ActionState.Hidden || adaptedFields == null)
        return;
      foreach (AdaptedControlResult adaptedField in adaptedFields)
      {
        IFieldItem fieldItem = this.itemRepository.CreateFieldItem(this.itemRepository.GetItem(adaptedField.FieldID));
        if (fieldItem.IsTag)
        {
          string str = adaptedField.AdaptForAnalyticsTag ? this.fieldProvider.GetAdaptedValue(fieldItem, adaptedField.Value) : adaptedField.Value;
          this.analyticsTracker.AddTag(fieldItem.Name, str);
        }
      }
    }

    public override ActionState QueryState(ActionQueryContext queryContext)
    {
      if (queryContext.Tracking != null && (!this.settings.IsXdbTrackerEnabled || queryContext.Tracking.Ignore) || queryContext.Tracking == null && !queryContext.Form.IsAnalyticsEnabled)
        return ActionState.Hidden;
      if (queryContext.Structure != null)
      {
        if (!queryContext.Structure.ToXml().Contains("tag=\"1\""))
          return ActionState.Hidden;
      }
      else if (((IEnumerable<IFieldItem>) queryContext.Form.Fields).FirstOrDefault<IFieldItem>((Func<IFieldItem, bool>) (f => f.IsTag)) == null)
        return ActionState.Hidden;
      return base.QueryState(queryContext);
    }
  }
}
