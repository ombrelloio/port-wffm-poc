// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Analytics.IContactRepository
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Analytics.Tracking;
using System;

namespace Sitecore.WFFM.Abstractions.Analytics
{
  public interface IContactRepository
  {
    void SaveContact(Contact contact, bool release, TimeSpan? leaseDuration = null);
  }
}
