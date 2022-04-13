// Decompiled with JetBrains decompiler
// Type: Sitecore.Globalization.AssemblyMetadata
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sitecore.Globalization
{
  internal class AssemblyMetadata
  {
    public static string[] GetStrings()
    {
      List<string> stringList = new List<string>();
      foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
      {
        IEnumerable<string> collection1 = ((IEnumerable<PropertyInfo>) type.GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>) (property => property.IsDefined(typeof (VisualPropertyAttribute), true))).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (property => ((VisualPropertyAttribute) property.GetCustomAttributes(typeof (VisualPropertyAttribute), true)[0]).DisplayName));
        stringList.AddRange(collection1);
        IEnumerable<string> collection2 = ((IEnumerable<PropertyInfo>) type.GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>) (property => property.IsDefined(typeof (VisualCategoryAttribute), true))).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (property => ((VisualCategoryAttribute) property.GetCustomAttributes(typeof (VisualCategoryAttribute), true)[0]).Category));
        stringList.AddRange(collection2);
      }
      return stringList.ToArray();
    }
  }
}
