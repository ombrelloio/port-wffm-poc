// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.IDUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sitecore.Form.Core.Utility
{
  public class IDUtil
  {
    public static readonly string guid = "(\\{){0,1}[0-9a-fA-F]{8}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{12}(\\}){0,1}";

    public static IEnumerable<string> GetGuids(string input)
    {
      List<string> stringList = new List<string>();
      if (!string.IsNullOrEmpty(input))
      {
        foreach (Match match in Regex.Matches(input, IDUtil.guid))
        {
          if (match.Success)
            stringList.Add(match.Value);
        }
      }
      return (IEnumerable<string>) stringList;
    }

    public static IEnumerable<ID> GetIDs(string input) => IDUtil.GetGuids(input).Select<string, ID>((Func<string, ID>) (s => new ID(s)));

    public static string RemoveBrackets(ID id)
    {
      Assert.ArgumentNotNull((object) id, nameof (id));
      return ((object) id).ToString().Trim('{', '}');
    }
  }
}
