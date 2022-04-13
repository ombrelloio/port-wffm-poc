// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.TypeConverters.IsoDateTimeConverter
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Diagnostics;
using System.ComponentModel;
using System.Globalization;

namespace Sitecore.Forms.Mvc.TypeConverters
{
  public class IsoDateTimeConverter : TypeConverter
  {
    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      Assert.ArgumentNotNull(value, nameof (value));
      return (object) DateUtil.IsoDateToDateTime(value.ToString());
    }
  }
}
