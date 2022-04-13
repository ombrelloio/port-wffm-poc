// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.Core.DateUtil
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using System;

namespace Sitecore.WFFM.Analytics.Core
{
  public class DateUtil
  {
    public static int GetShortIntTicks(TimeSpan dateTime) => dateTime.Days * 86400000 + dateTime.Hours * 3600000 + dateTime.Minutes * 60000 + dateTime.Seconds * 1000 + dateTime.Milliseconds;

    public static bool IsIsoDateTime(string datetime) => Sitecore.DateUtil.IsIsoDate(datetime);

    public static DateTime IsoDateTimeToDateTime(string iso) => Sitecore.DateUtil.IsoDateToDateTime(iso);

    public static string ConvertToUtc(string value) => !Sitecore.DateUtil.IsIsoDate(value) ? value : Sitecore.DateUtil.IsoDateToUtcIsoDate(value);
  }
}
