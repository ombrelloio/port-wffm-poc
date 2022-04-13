// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Wrappers.UI.ClientResponseWrapper
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;

namespace Sitecore.WFFM.Abstractions.Wrappers.UI
{
  [DependencyPath("wffm/wrappers/clientResponseWrapper")]
  [Serializable]
  public class ClientResponseWrapper
  {
    public virtual void Eval(string script)
    {
      Assert.ArgumentNotNullOrEmpty(script, nameof (script));
      Context.ClientPage.ClientResponse.Eval(script);
    }
  }
}
