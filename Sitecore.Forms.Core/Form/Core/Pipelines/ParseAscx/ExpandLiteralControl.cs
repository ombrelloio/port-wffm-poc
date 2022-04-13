// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.ParseAscx.ExpandLiteralControl
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using HtmlAgilityPack;
using Sitecore.Form.Core.Pipeline.ParseAscx;
using Sitecore.Globalization;
using System.Text.RegularExpressions;
using System.Web;

namespace Sitecore.Form.Core.Pipelines.ParseAscx
{
  public class ExpandLiteralControl
  {
    public string LiteralControlRegex = "<cc[0-9]+:literalcontrol\\s.+>.*\\n.*</cc[0-9]+:literalcontrol>";
    public string SystemTagPrefix = "<%@\\sRegister.+Namespace=\"System.Web.UI\".+%>";

    public void Process(ParseAscxArgs args)
    {
      MatchCollection matchCollection = new Regex(this.SystemTagPrefix, RegexOptions.IgnoreCase | RegexOptions.Multiline).Matches(args.AscxContent);
      if (matchCollection.Count <= 0)
        return;
      string str = matchCollection[0].Value;
      int num = str.IndexOf("TagPrefix");
      if (num <= -1 || num + 11 >= str.Length)
        return;
      string tagPrefix = str.Substring(num + 11);
      tagPrefix = tagPrefix.Substring(0, tagPrefix.IndexOf("\""));
      if (string.IsNullOrEmpty(tagPrefix))
        return;
      Regex regex = new Regex(this.LiteralControlRegex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
      HtmlDocument doc = new HtmlDocument();
      args.AscxContent = regex.Replace(args.AscxContent, (MatchEvaluator) (match =>
      {
        if (match.Success)
        {
          using (new ThreadCultureSwitcher(Language.Parse("en").CultureInfo))
            doc.LoadHtml(match.Value);
          if (doc.DocumentNode != null && doc.DocumentNode.FirstChild != null)
          {
            HtmlNode firstChild = doc.DocumentNode.FirstChild;
            if (firstChild.Name.StartsWith(tagPrefix + ":"))
              return HttpUtility.HtmlDecode(firstChild.GetAttributeValue("text", match.Value));
          }
        }
        return match.Value;
      }));
    }
  }
}
