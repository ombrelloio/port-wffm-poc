// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Dependencies.ReportDataProviderWrapper
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Shared;
using Sitecore.Xdb.Reporting;
using System.Reflection;

namespace Sitecore.WFFM.Analytics.Dependencies
{
  public class ReportDataProviderWrapper
  {
    private readonly IFactoryObjectsProvider factoryObjectsProvider;

    public ReportDataProviderWrapper(IFactoryObjectsProvider factoryObjectsProvider)
    {
      Assert.ArgumentNotNull((object) factoryObjectsProvider, nameof (factoryObjectsProvider));
      this.factoryObjectsProvider = factoryObjectsProvider;
    }

    public virtual ReportDataProviderBase GetReportDataProviderBase(
      bool assert = false)
    {
      try
      {
        return this.factoryObjectsProvider.CreateObject<ReportDataProviderBase>("reporting/dataProvider", assert);
      }
      catch (TargetInvocationException ex)
      {
        return (ReportDataProviderBase) null;
      }
    }
  }
}
