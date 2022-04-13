// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.HtmlUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Sitecore.Form.Core.Utility
{
  public class HtmlUtil
  {
    public static readonly string AspCode = "(?<=&lt;%).*?(?=%&gt;)";
    public static readonly string AspTag = "&lt;%@.*?%&gt;|&lt;%|%&gt;";
    public static readonly string Attribute = "(=?\".*?\"|=?'.*?')|([\\w:-]+)";
    public static readonly string AttributeMatch = "(=?\".*?\"|=?'.*?')|([\\w:-]+)";
    public static readonly string Attributes = "(?<=&lt;(?!%)/?!?\\??[\\w:-]+).*?(?=(?<!%)/?&gt;)";
    public static readonly string Comment = "&lt;!--.*?--&gt;";
    public static readonly string Entity = "&amp;\\w+;";
    public static readonly string HtmlPattern = "((?<=&lt;script.*?(?<!/script)&gt;).+?(?=&lt;/script&gt;))|(&lt;!--.*?--&gt;)|(&lt;%@.*?%&gt;|&lt;%|%&gt;)|((?<=&lt;%).*?(?=%&gt;))|((?:&lt;/?!?\\??(?!%)|(?<!%)/?&gt;)+)|((?<=&lt;/?!?\\??(?!%))[\\w\\.:-]+(?=.*&gt;))|((?<=&lt;(?!%)/?!?\\??[\\w:-]+).*?(?=(?<!%)/?&gt;))|(&amp;\\w+;)";
    public static readonly string JavaScript = "(?<=&lt;script(?:\\s.*?)?&gt;).+?(?=&lt;/script&gt;)";
    public static readonly string TagDelimiter = "(?:&lt;/?!?\\??(?!%)|(?<!%)/?&gt;)+";
    public static readonly string TagName = "(?<=&lt;/?!?\\??(?!%))[\\w\\.:   -]+(?=.*&gt;)";
    public static readonly string XmlnsAttribute = "xmlns[:]\\w*[=].?[\"].*?[\"]";

    public static string ClearXmlns(string html) => !string.IsNullOrEmpty(html) ? new Regex(HtmlUtil.XmlnsAttribute, RegexOptions.IgnoreCase | RegexOptions.Singleline).Replace(html, string.Empty) : string.Empty;

    public static string EncodeHMTL(string html) => !string.IsNullOrEmpty(html) ? HttpUtility.HtmlEncode(html) : string.Empty;

    public static string EncodeTags(string source)
    {
      if (string.IsNullOrEmpty(source))
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder(source);
      stringBuilder.Replace("&", "&amp;");
      stringBuilder.Replace("<", "&lt;");
      stringBuilder.Replace(">", "&gt;");
      stringBuilder.Replace("\t", string.Empty.PadRight(4));
      return stringBuilder.ToString();
    }

    public static string HighlighteCode(string source)
    {
      source = HtmlUtil.EncodeTags(source);
      return HtmlUtil.PreFormat(Highlighter.Replace(source, new Regex(HtmlUtil.HtmlPattern, RegexOptions.IgnoreCase | RegexOptions.Singleline), new MatchEvaluator(HtmlUtil.Evaluator)));
    }

    public static string AttributeHighlighter(Match match)
    {
      if (match == null)
        return string.Empty;
      if (match.Groups[1].Success)
        return "<span style=\"color:#0000ff\">" + (object) match + "</span>";
      return match.Groups[2].Success ? "<span style=\"color:#ff0000\">" + (object) match + "</span>" : match.ToString();
    }

    public static string Evaluator(Match match)
    {
      if (match == null)
        return string.Empty;
      if (match.Groups[2].Success)
      {
        StringReader stringReader = new StringReader(match.ToString());
        StringBuilder stringBuilder = new StringBuilder();
        string str;
        while ((str = stringReader.ReadLine()) != null)
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append("\n");
          stringBuilder.Append("<span style=\"color:#000000\">");
          stringBuilder.Append(str);
          stringBuilder.Append("</span>");
        }
        return stringBuilder.ToString();
      }
      if (match.Groups[3].Success)
        return "<span style=\"background-color:yellow;color:#000000\">" + (object) match + "</span>";
      if (match.Groups[5].Success)
        return "<span style=\"color:#0000ff\">" + (object) match + "</span>";
      if (match.Groups[6].Success)
        return "<span style=\"color:#800000;\">" + (object) match + "</span>";
      if (match.Groups[7].Success)
        return Highlighter.Replace(match.ToString(), new Regex(HtmlUtil.Attribute, RegexOptions.IgnoreCase | RegexOptions.Singleline), new MatchEvaluator(HtmlUtil.AttributeHighlighter));
      return match.Groups[8].Success ? "<span style=\"color:#ff0000\">" + (object) match + "</span>" : match.ToString();
    }

    private static string PreFormat(string source)
    {
      StringReader stringReader = new StringReader(source);
      StringBuilder stringBuilder = new StringBuilder();
      string str;
      while ((str = stringReader.ReadLine()) != null)
      {
        stringBuilder.Append("<pre style=\"margin:0px\">");
        if (str.Length == 0)
          stringBuilder.Append("&nbsp;");
        else
          stringBuilder.Append(str);
        stringBuilder.Append("</pre>");
      }
      stringReader.Close();
      return stringBuilder.ToString();
    }
  }
}
