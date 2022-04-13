// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Controllers.ClientEventController
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Forms.Mvc.Attributes;
using Sitecore.Mvc.Controllers;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Dependencies;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.Controllers
{
  public class ClientEventController : SitecoreController
  {
    [ParameterConverter("clientEvents")]
    public ActionResult Process(IList<ClientEvent> clientEvents)
    {
      DependenciesManager.AnalyticsTracker.InitializeTracker();
      foreach (ClientEvent clientEvent in (IEnumerable<ClientEvent>) clientEvents)
        DependenciesManager.AnalyticsTracker.TriggerEvent(clientEvent);
      return (ActionResult) null;
    }
  }
}
