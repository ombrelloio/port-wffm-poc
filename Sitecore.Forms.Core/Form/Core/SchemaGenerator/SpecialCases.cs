// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.SchemaGenerator.SpecialCases
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.Reflection;

namespace Sitecore.Form.Core.SchemaGenerator
{
  internal class SpecialCases
  {
    protected SpecialCases()
    {
    }

    public static bool IsContentPlaceHolderIDProperty(PropertyInfo pi)
    {
      bool flag = false;
      if (pi.Name == "ContentPlaceHolderID" && pi.DeclaringType.FullName == "System.Web.UI.WebControls.Content")
        flag = true;
      return flag;
    }

    public static bool IsDisallowedOnMobilePagesProperty(PropertyInfo pi)
    {
      bool flag = false;
      if (pi.Name == "SkinID" && pi.DeclaringType.FullName == "System.Web.UI.WebControls.WebControl")
        return true;
      if (pi.Name == "EnableTheming" && pi.DeclaringType.FullName == "System.Web.UI.WebControls.WebControl")
        flag = true;
      return flag;
    }

    public static bool IsStyleProperty(PropertyInfo pi)
    {
      bool flag = false;
      if (pi.Name == "Style" && pi.DeclaringType.FullName == "System.Web.UI.WebControls.WebControl")
        flag = true;
      return flag;
    }
  }
}
