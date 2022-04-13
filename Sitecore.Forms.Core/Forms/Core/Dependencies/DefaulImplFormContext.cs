// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Dependencies.DefaulImplFormContext
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Wrappers;
using System;
using System.Collections.Generic;

namespace Sitecore.Forms.Core.Dependencies
{
  [Serializable]
  public class DefaulImplFormContext : IFormContext
  {
    private readonly ISitecoreContextWrapper sitecoreContextWrapper;

    public DefaulImplFormContext(ISitecoreContextWrapper sitecoreContextWrapper)
    {
      Assert.ArgumentNotNull((object) sitecoreContextWrapper, nameof (sitecoreContextWrapper));
      this.sitecoreContextWrapper = sitecoreContextWrapper;
    }

    public List<ExecuteResult.Failure> Failures
    {
      get => (List<ExecuteResult.Failure>) this.sitecoreContextWrapper.GetItem("scwfm_failures");
      set => this.sitecoreContextWrapper.SetItem("scwfm_failures", (object) value);
    }
  }
}
