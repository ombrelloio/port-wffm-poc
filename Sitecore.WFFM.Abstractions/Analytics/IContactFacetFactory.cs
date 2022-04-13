// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Analytics.IContactFacetFactory
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Analytics.Model.Framework;
using Sitecore.Analytics.Tracking;
using System;
using System.Collections.Generic;

namespace Sitecore.WFFM.Abstractions.Analytics
{
  public interface IContactFacetFactory
  {
    Dictionary<string, IFacet> ContactFacets { get; }

    void SetFacetValue(
      Contact contact,
      string key,
      string facetXpath,
      string facetValue,
      bool overwrite = true);

    IElement CreateElement(Type type);

    IEnumerable<IModelMember> GetFacetMembers(IElement facet);
  }
}
