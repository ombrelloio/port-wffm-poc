// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Commands.SelectItemsBySource
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.Forms.Core.Data;
using Sitecore.Globalization;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace Sitecore.Forms.Core.Commands
{
  [Serializable]
  public class SelectItemsBySource : Command
  {
    public override void Execute(CommandContext context)
    {
      Assert.ArgumentNotNull((object) context.Parameters["value"], "value");
      Assert.ArgumentNotNullOrEmpty(context.Parameters["target"], "target");
      string queries = HttpUtility.UrlDecode(context.Parameters["value"]);
      if (queries.StartsWith(StaticSettings.SourceMarker))
        queries = new QuerySettings("root", queries.Substring(StaticSettings.SourceMarker.Length)).ToString();
      string str = context.Parameters["la"];
      if (!string.IsNullOrEmpty(str))
        str = "en";
      NameValueCollection collection1;
      using (new LanguageSwitcher(Language.Parse(str)))
        collection1 = QueryManager.Select(QuerySettings.ParseRange(queries));
      IEnumerable<string> collection2 = collection1.Select<string>((Func<string, string, string>) ((k, v) => string.Join(string.Empty, new string[6]
      {
        "<item>",
        k,
        "</item>",
        "<text>",
        (v ?? string.Empty).Replace("<", string.Empty).Replace(">", string.Empty),
        "</text>"
      })));
      SheerResponse.SetAttribute(context.Parameters["target"], "value", string.Join(string.Empty, new List<string>(collection2).ToArray()));
    }
  }
}
