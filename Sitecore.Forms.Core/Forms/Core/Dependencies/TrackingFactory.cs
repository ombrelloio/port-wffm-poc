// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Dependencies.TrackingFactory
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Data;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Shared;
using System;

namespace Sitecore.Forms.Core.Dependencies
{
  [Serializable]
  public class TrackingFactory : ITrackingFactory
  {
    private readonly IDatabaseRepository databaseRepository;

    public TrackingFactory(IDatabaseRepository databaseRepository)
    {
      Assert.ArgumentNotNull((object) databaseRepository, nameof (databaseRepository));
      this.databaseRepository = databaseRepository;
    }

    public ITracking CreateFromXml(string xml, string databaseName)
    {
      Assert.ArgumentNotNull((object) xml, nameof (xml));
      Assert.ArgumentNotNull((object) databaseName, nameof (databaseName));
      return (ITracking) new Tracking(xml, this.databaseRepository.GetDatabase(databaseName));
    }
  }
}
