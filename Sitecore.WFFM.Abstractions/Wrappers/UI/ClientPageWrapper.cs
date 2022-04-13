// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Wrappers.UI.ClientPageWrapper
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Diagnostics;
using Sitecore.Web.UI.Sheer;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;

namespace Sitecore.WFFM.Abstractions.Wrappers.UI
{
  [DependencyPath("wffm/wrappers/clientPageWrapper")]
  [Serializable]
  public class ClientPageWrapper
  {
    public ClientPageWrapper(ClientResponseWrapper clientResponse)
    {
      Assert.ArgumentNotNull((object) clientResponse, nameof (clientResponse));
      this.ClientResponse = clientResponse;
    }

    public ClientResponseWrapper ClientResponse { get; private set; }

    public virtual void Start(object obj, string methodName, ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull(obj, nameof (obj));
      Assert.ArgumentNotNullOrEmpty(methodName, nameof (methodName));
      Context.ClientPage.Start(obj, methodName, args);
    }
  }
}
