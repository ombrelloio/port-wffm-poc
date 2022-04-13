// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Update.UpgradePostStep
// Assembly: Sitecore.WFFM.Update, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 141BB56E-B1B2-4D04-B15D-AD6BA1098ABC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Update.dll

using Sitecore.Install.Framework;
using Sitecore.WFFM.Update.Steps;
using System.Collections.Specialized;

namespace Sitecore.WFFM.Update
{
  public class UpgradePostStep : IPostStep
  {
    public void Run(ITaskOutput output, NameValueCollection metaData)
    {
      new AddEventIdToFormTrackingField().Process();
      new UpdateListItemsLocation().Process();
    }
  }
}
