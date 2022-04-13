// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Settings
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Configuration;

namespace Sitecore.Forms.Mvc
{
  public static class Settings
  {
    public static bool EnableBootstrapCssRendering => Sitecore.Configuration.Settings.GetBoolSetting("WFM.EnableBootstrapCssRendering", true);

    public static int LimitMultipleSubmits_IntervalInSeconds => Sitecore.Configuration.Settings.GetIntSetting("WFM.LimitMultipleSubmits.IntervalInSeconds", 30);
  }
}
