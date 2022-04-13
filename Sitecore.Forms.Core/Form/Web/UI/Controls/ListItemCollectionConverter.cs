// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.ListItemCollectionConverter
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.ComponentModel;
using System.Globalization;

namespace Sitecore.Form.Web.UI.Controls
{
  public class ListItemCollectionConverter : TypeConverter
  {
    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      return value is string ? (object) (ListItemCollection) (value as string) : value;
    }
  }
}
