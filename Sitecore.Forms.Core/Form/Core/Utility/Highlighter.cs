// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.Highlighter
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Sitecore.Form.Core.Utility
{
  public class Highlighter
  {
    public static string HighlighterText(string source, string pattern, string color) => Highlighter.HighlighterText(source, new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline), ColorTranslator.FromHtml(color));

    public static string HighlighterText(string source, string pattern, Color color) => Highlighter.HighlighterText(source, new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline), color);

    public static string HighlighterText(
      string source,
      string pattern,
      RegexOptions options,
      Color color)
    {
      return Highlighter.HighlighterText(source, new Regex(pattern, options), color);
    }

    public static string Replace(string source, Regex regex, MatchEvaluator evaluator) => string.IsNullOrEmpty(source) ? string.Empty : regex.Replace(source, evaluator);

    public static string HighlighterText(string source, Regex regex, Color color)
    {
      Highlighter.MatchParser matchParser = new Highlighter.MatchParser(color);
      return Highlighter.Replace(source, regex, new MatchEvaluator(matchParser.MatchEval));
    }

    private class MatchParser
    {
      private Color color;

      public MatchParser(Color color) => this.color = color;

      public string MatchEval(Match match)
      {
        if (!match.Groups[0].Success)
          return match.ToString();
        StringReader stringReader = new StringReader(match.ToString());
        StringBuilder stringBuilder = new StringBuilder();
        string str;
        while ((str = stringReader.ReadLine()) != null)
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append("\n");
          stringBuilder.AppendFormat("<span style=\"color:{0}\">", (object) ColorTranslator.ToHtml(this.color));
          stringBuilder.Append(str);
          stringBuilder.Append("</span>");
        }
        return stringBuilder.ToString();
      }
    }
  }
}
