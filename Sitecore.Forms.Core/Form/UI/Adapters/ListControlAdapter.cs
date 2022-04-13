// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.UI.Adapters.ListControlAdapter
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Client.Submit;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.Forms.Core.Data;
using Sitecore.Globalization;
using Sitecore.WFFM.Abstractions.Data;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Web;

namespace Sitecore.Form.UI.Adapters
{
  public class ListControlAdapter : Adapter
  {
    public override IEnumerable<string> AdaptToFriendlyListValues(
      IFieldItem field,
      string value,
      bool returnTexts)
    {
      IEnumerable<string> stringArray = ParametersUtil.XmlToStringArray(value);
      if (field != null & returnTexts)
      {
        Match match = Regex.Match(field.LocalizedParameters, "<items>([^<]*)</items>", RegexOptions.IgnoreCase);
        if (match.Success)
        {
          string queries = HttpUtility.UrlDecode(match.Result("$1"));
          if (queries.StartsWith(StaticSettings.SourceMarker))
            queries = new QuerySettings("root", queries.Substring(StaticSettings.SourceMarker.Length)).ToString();
          NameValueCollection nameValueCollection;
          using (new LanguageSwitcher(Context.Request == null || string.IsNullOrEmpty(Context.Request.QueryString["la"]) ? (!(Context.ContentLanguage != (Language) null) || !(((object) Context.ContentLanguage).ToString() != string.Empty) ? Context.Language : Context.ContentLanguage) : Language.Parse(Context.Request.QueryString["la"])))
            nameValueCollection = QueryManager.Select(QuerySettings.ParseRange(queries));
          List<string> stringList = new List<string>();
          foreach (string name in stringArray)
          {
            if (!string.IsNullOrEmpty(nameValueCollection[name]))
              stringList.Add(nameValueCollection[name]);
          }
          return (IEnumerable<string>) stringList;
        }
      }
      return stringArray;
    }

    public override string AdaptToFriendlyValue(IFieldItem field, string value) => string.Join(", ", new List<string>(this.AdaptToFriendlyListValues(field, value, true)).ToArray());
  }
}
