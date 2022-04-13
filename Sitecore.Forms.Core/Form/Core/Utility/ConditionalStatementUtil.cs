// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.ConditionalStatementUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Core.Data;
using Sitecore.WFFM.Abstractions.Data;
using System.Collections.Generic;

namespace Sitecore.Form.Core.Utility
{
  public class ConditionalStatementUtil
  {
    public static IEnumerable<ListField> GetConditionalItems(FormItem form)
    {
      Assert.ArgumentNotNull((object) form, nameof (form));
      List<ListField> listFieldList = new List<ListField>();
      foreach (IFieldItem field in form.Fields)
      {
        ListField listField = new ListField(field);
        if (!listField.IsEmpty)
          listFieldList.Add(listField);
      }
      return (IEnumerable<ListField>) listFieldList;
    }
  }
}
