// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.DatabaseRepository
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Shared;

namespace Sitecore.Forms.Core.Data
{
  public class DatabaseRepository : IDatabaseRepository
  {
    public Database GetDatabase(string name)
    {
      Assert.ArgumentNotNull((object) name, nameof (name));
      return Factory.GetDatabase(name);
    }
  }
}
