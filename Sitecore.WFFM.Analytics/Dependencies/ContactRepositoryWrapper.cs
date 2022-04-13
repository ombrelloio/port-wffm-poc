// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Dependencies.ContactRepositoryWrapper
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Analytics.Data;
using Sitecore.Analytics.Tracking;
using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Analytics;
using System;

namespace Sitecore.WFFM.Analytics.Dependencies
{
  public class ContactRepositoryWrapper : IContactRepository
  {
    private readonly ContactRepository _contactRepository;

    public ContactRepositoryWrapper(ContactRepository contactRepository)
    {
      Assert.IsNotNull((object) contactRepository, nameof (contactRepository));
      this._contactRepository = contactRepository;
    }

    public void SaveContact(Contact contact, bool release, TimeSpan? leaseDuration = null) => ((ContactRepositoryBase) this._contactRepository).SaveContact(contact);
  }
}
