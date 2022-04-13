// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.DateUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;

namespace Sitecore.Form.Core.Utility
{
  public class DateUtil
  {
    public static int GetShortIntTicks(TimeSpan dateTime) => dateTime.Days * 86400000 + dateTime.Hours * 3600000 + dateTime.Minutes * 60000 + dateTime.Seconds * 1000 + dateTime.Milliseconds;

    public static bool IsIsoDateTime(string datetime) => Sitecore.DateUtil.IsIsoDate(datetime);

    public static DateTime IsoDateTimeToDateTime(string iso) => Sitecore.DateUtil.IsoDateToDateTime(iso);

    public static string ConvertToUtc(string value) => !Sitecore.DateUtil.IsIsoDate(value) ? value : Sitecore.DateUtil.IsoDateToUtcIsoDate(value);
  }
}
