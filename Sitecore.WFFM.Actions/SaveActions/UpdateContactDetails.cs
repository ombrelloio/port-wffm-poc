// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Actions.SaveActions.UpdateContactDetails
// Assembly: Sitecore.WFFM.Actions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6607DF78-1371-42DC-8675-A986B5291018
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Actions.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Shared;
using Sitecore.WFFM.Actions.Base;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace Sitecore.WFFM.Actions.SaveActions
{
  [Required("IsXdbTrackerEnabled", true)]
  public class UpdateContactDetails : WffmSaveAction
  {
    private readonly IAnalyticsTracker analyticsTracker;
    private readonly IAuthentificationManager authentificationManager;
    private readonly ILogger logger;
    private readonly IFacetFactory facetFactory;

    public UpdateContactDetails(
      IAnalyticsTracker analyticsTracker,
      IAuthentificationManager authentificationManager,
      ILogger logger,
      IFacetFactory facetFactory)
    {
      Assert.IsNotNull((object) analyticsTracker, nameof (analyticsTracker));
      Assert.IsNotNull((object) authentificationManager, nameof (authentificationManager));
      Assert.IsNotNull((object) logger, nameof (logger));
      Assert.IsNotNull((object) facetFactory, nameof (facetFactory));
      this.analyticsTracker = analyticsTracker;
      this.authentificationManager = authentificationManager;
      this.logger = logger;
      this.facetFactory = facetFactory;
    }

    public string Mapping { get; set; }

    public override void Execute(
      ID formId,
      AdaptedResultList adaptedFields,
      ActionCallContext actionCallContext = null,
      params object[] data)
    {
      this.UpdateContact(adaptedFields);
    }

    protected virtual void UpdateContact(AdaptedResultList fields)
    {
      Assert.ArgumentNotNull((object) fields, "adaptedFields");
      Assert.IsNotNullOrEmpty(this.Mapping, "Empty mapping xml.");
      Assert.IsNotNull((object) this.analyticsTracker.CurrentContact, "Tracker.Current.Contact");
      if (!this.authentificationManager.IsActiveUserAuthenticated)
      {
        this.logger.Warn("[UPDATE CONTACT DETAILS Save action] User is not authenticated to edit contact details.", (object) this);
      }
      else
      {
        IEnumerable<FacetNode> mapping = this.ParseMapping(this.Mapping, fields);
        IContactFacetFactory contactFacetFactory = this.facetFactory.GetContactFacetFactory();
        foreach (FacetNode facetNode in mapping)
          contactFacetFactory.SetFacetValue(this.analyticsTracker.CurrentContact, facetNode.Key, facetNode.Path, facetNode.Value);
      }
    }

    public IEnumerable<FacetNode> ParseMapping(
      string mapping,
      AdaptedResultList adaptedFieldResultList)
    {
      Assert.ArgumentNotNullOrEmpty(mapping, nameof (mapping));
      Assert.ArgumentNotNull((object) adaptedFieldResultList, nameof (adaptedFieldResultList));
      return (IEnumerable<FacetNode>) ((object[]) new JavaScriptSerializer().Deserialize(mapping, typeof (object))).Cast<Dictionary<string, object>>().Select(item => new
      {
        item = item,
        itemValue = item["value"].ToString()
      }).Select(_param1 => new
      {
        TransparentIdentifier0 = _param1,
        itemId = !_param1.item.ContainsKey("id") || _param1.item["id"] == null ? "Preferred" : _param1.item["id"].ToString()
      }).Select(_param1 => new
      {
        TransparentIdentifier1 = _param1,
        value = adaptedFieldResultList.GetValueByFieldID(ID.Parse(_param1.TransparentIdentifier0.item["key"].ToString()))
      }).Where(_param1 => !string.IsNullOrEmpty(_param1.value)).Select(_param1 => new FacetNode(_param1.TransparentIdentifier1.itemId, _param1.TransparentIdentifier1.TransparentIdentifier0.itemValue, _param1.value)).ToList<FacetNode>();
    }
  }
}
