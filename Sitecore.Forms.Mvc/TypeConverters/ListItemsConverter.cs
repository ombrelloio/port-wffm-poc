// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.TypeConverters.ListItemsConverter
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Form.Core.Utility;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Sitecore.Forms.Mvc.TypeConverters
{
  public class ListItemsConverter : TypeConverter
  {
    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      string xml = value as string;
      if (string.IsNullOrEmpty(xml))
        return (object) null;
      return !string.IsNullOrEmpty(xml) ? (object) ParametersUtil.XmlToStringArray(xml).ToList<string>() : (object) new List<string>();
    }
  }
}
