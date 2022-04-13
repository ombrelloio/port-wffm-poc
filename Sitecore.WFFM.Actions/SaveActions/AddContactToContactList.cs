// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Actions.SaveActions.AddContactToContactList
// Assembly: Sitecore.WFFM.Actions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6607DF78-1371-42DC-8675-A986B5291018
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Actions.dll

using Sitecore.Analytics;
using Sitecore.Analytics.Model;
using Sitecore.Analytics.Tracking;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Shared;
using Sitecore.WFFM.Actions.Base;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.Configuration;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.WFFM.Actions.SaveActions
{
  [Required("IsXdbTrackerEnabled", true)]
  [Required("IsXdbEnabled", true)]
  public class AddContactToContactList : WffmSaveAction
  {
    private readonly IAnalyticsTracker analyticsTracker;
    private readonly IContactRepository contactRepository;

    public AddContactToContactList(
      IAnalyticsTracker analyticsTracker,
      IContactRepository contactRepository)
    {
      Assert.IsNotNull((object) analyticsTracker, nameof (analyticsTracker));
      Assert.IsNotNull((object) contactRepository, nameof (contactRepository));
      this.analyticsTracker = analyticsTracker;
      this.contactRepository = contactRepository;
    }

    public string ContactsLists { get; set; }

    public string ExecuteWhen { get; set; }

    public override void Execute(
      ID formId,
      AdaptedResultList adaptedFields,
      ActionCallContext actionCallContext = null,
      params object[] data)
    {
      Assert.ArgumentNotNull((object) adaptedFields, nameof (adaptedFields));
      Assert.IsNotNullOrEmpty(this.ContactsLists, "Empty contact list.");
      Assert.IsNotNull((object) this.analyticsTracker.CurrentContact, "Tracker.Current.Contact");
      if (!adaptedFields.IsTrueStatement(this.ExecuteWhen))
        return;
      List<string> list = ((IEnumerable<string>) this.ContactsLists.Split(',')).Select<string, string>((Func<string, string>) (x => ((object) ID.Parse(x)).ToString())).ToList<string>();
      if (Tracker.Current.Contact.IsNew && Factory.CreateObject("tracking/contactManager", true) is ContactManager contactManager)
      {
        Tracker.Current.Contact.ContactSaveMode = (ContactSaveMode) 2;
        contactManager.SaveContactToCollectionDb(Tracker.Current.Contact);
      }
      IdentifiedContactReference contactReference = new IdentifiedContactReference("xDB.Tracker", Tracker.Current.Contact.ContactId.ToString("N"));
      using (XConnectClient client = SitecoreXConnectClientConfiguration.GetClient("xconnect/clientconfig"))
      {
        try
        {
          Sitecore.XConnect.Contact contact = XConnectSynchronousExtensions.Get<Sitecore.XConnect.Contact>((IXdbContext) client, (IEntityReference<Sitecore.XConnect.Contact>) contactReference, (ExpandOptions) new ContactExpandOptions(new string[2]
          {
            "Personal",
            "ListSubscriptions"
          }));
          if (contact == null)
            return;
          foreach (string input in list)
            this.SetListSubscriptionsFacet(contact, (IXdbContext) client, Guid.Parse(input));
          XConnectSynchronousExtensions.Submit((IXdbContext) client);
        }
        catch (Exception ex)
        {
          Log.Error(ex.Message, ex, (object) this);
        }
      }
    }

    private void SetListSubscriptionsFacet(
      Sitecore.XConnect.Contact contact,
      IXdbContext client,
      Guid listId,
      Guid subscriptionSource = default (Guid))
    {
      Assert.ArgumentNotNull((object) contact, nameof (contact));
      Assert.ArgumentNotNull((object) client, nameof (client));
      Assert.ArgumentCondition(listId != Guid.Empty, nameof (listId), "List id cannot be an empty Guid.");
      ListSubscriptions listSubscriptions = CollectionModel.ListSubscriptions(contact) ?? new ListSubscriptions();
      if (listSubscriptions.Subscriptions == null)
        listSubscriptions.Subscriptions = new List<ContactListSubscription>();
      if (((IEnumerable<ContactListSubscription>) listSubscriptions.Subscriptions).Any<ContactListSubscription>((Func<ContactListSubscription, bool>) (x => x.ListDefinitionId == listId)))
        return;
      ContactListSubscription listSubscription = new ContactListSubscription(DateTime.Now, true, listId);
      if (subscriptionSource != Guid.Empty)
        listSubscription.SourceDefinitionId = new Guid?(subscriptionSource);
      listSubscriptions.Subscriptions.Add(listSubscription);
      CollectionModel.SetListSubscriptions(client, (IEntityReference<Sitecore.XConnect.Contact>) contact, listSubscriptions);
    }
  }
}
