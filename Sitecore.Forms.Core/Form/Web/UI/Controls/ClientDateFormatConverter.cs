// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.ClientDateFormatConverter
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Form.Core.Configuration;

namespace Sitecore.Form.Web.UI.Controls
{
  public class ClientDateFormatConverter
  {
    public static object ConvertToClientFormat(string value)
    {
      if (value != null)
      {
        Item obj = StaticSettings.ContextDatabase.GetItem(IDs.DateFormatsRoot);
        if (obj != null)
        {
          Item child = obj.Children[value];
          if (child != null)
          {
            string str = ((BaseItem) child)["Value"];
            if (!string.IsNullOrEmpty(str))
              return (object) str;
          }
        }
      }
      return (object) value;
    }
  }
}
