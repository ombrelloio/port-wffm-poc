// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.ParametersUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using HtmlAgilityPack;
using Sitecore.Collections;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Forms.Core.Data;
using Sitecore.Globalization;
using Sitecore.Web;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Sitecore.Form.Core.Utility
{
  public class ParametersUtil
  {
    public static IEnumerable<string> XmlToStringArray(string xml) => string.IsNullOrEmpty(xml) ? (IEnumerable<string>) new List<string>() : ParametersUtil.XmlToStringArray(xml, false);

    public static IEnumerable<string> XmlToStringArray(string xml, bool applyUrlDecode)
    {
      HtmlDocument htmlDocument = new HtmlDocument();
      using (new ThreadCultureSwitcher(Language.Parse("en").CultureInfo))
        htmlDocument.LoadHtml(ParametersUtil.Decode(xml, applyUrlDecode));
      return (IEnumerable<string>) htmlDocument.DocumentNode.ChildNodes.Select<HtmlNode, string>((Func<HtmlNode, string>) (node => node.InnerHtml)).ToList<string>();
    }

    public static NameValueCollection XmlToNameValueCollection(string xml) => ParametersUtil.XmlToNameValueCollection(xml, false);

    public static NameValueCollection XmlToNameValueCollection(
      string xml,
      bool applyUrlDecode)
    {
      NameValueCollection nameValueCollection = new NameValueCollection();
      foreach (Pair<string, string> pair in ParametersUtil.XmlToPairArray(xml, applyUrlDecode))
        nameValueCollection[pair.Part1] = pair.Part2;
      return nameValueCollection;
    }

    public static IEnumerable<HtmlNode> XmlToHtmlNodeCollection(string xml) => ParametersUtil.XmlToHtmlNodeCollection(xml, false);

    public static IEnumerable<HtmlNode> XmlToHtmlNodeCollection(
      string xml,
      bool applyUrlDecode)
    {
      List<HtmlNode> htmlNodeList = new List<HtmlNode>();
      if (!string.IsNullOrEmpty(xml))
      {
        HtmlDocument htmlDocument = new HtmlDocument();
        using (new ThreadCultureSwitcher(Language.Parse("en").CultureInfo))
        {
          htmlDocument.LoadHtml(ParametersUtil.Decode(xml, applyUrlDecode));
          htmlNodeList.AddRange((IEnumerable<HtmlNode>) htmlDocument.DocumentNode.ChildNodes);
        }
      }
      return (IEnumerable<HtmlNode>) htmlNodeList;
    }

    public static string HtmlNodeCollectionToXml(IEnumerable<HtmlNode> nodes)
    {
      Assert.IsNotNull((object) nodes, nameof (nodes));
      NameValueCollection values = new NameValueCollection();
      foreach (HtmlNode node in nodes)
      {
        if (node != null)
          values[node.Name] = node.InnerHtml;
      }
      return ParametersUtil.NameValueCollectionToXml(values);
    }

    public static IEnumerable<Pair<string, string>> XmlToPairArray(string xml) => ParametersUtil.XmlToPairArray(xml, false);

    public static IEnumerable<Pair<string, string>> XmlToPairArray(
      params string[] xmls)
    {
      Dictionary<string, string> source = new Dictionary<string, string>();
      foreach (string xml in xmls)
      {
        foreach (Pair<string, string> pair in ParametersUtil.XmlToPairArray(xml))
        {
          if (source.ContainsKey(pair.Part1))
            source[pair.Part1] = pair.Part2;
          else
            source.Add(pair.Part1, pair.Part2);
        }
      }
      return source.Select<KeyValuePair<string, string>, Pair<string, string>>((Func<KeyValuePair<string, string>, Pair<string, string>>) (p => new Pair<string, string>(p.Key, p.Value)));
    }

    public static IEnumerable<Pair<string, string>> XmlToPairArray(
      string xml,
      bool applyUrlDecode)
    {
      List<Pair<string, string>> pairList = new List<Pair<string, string>>();
      if (!string.IsNullOrEmpty(xml))
      {
        HtmlDocument htmlDocument = new HtmlDocument();
        using (new ThreadCultureSwitcher(Language.Parse("en").CultureInfo))
          htmlDocument.LoadHtml(xml.StartsWith("<") ? xml : ParametersUtil.Decode(xml, applyUrlDecode));
        pairList.AddRange(htmlDocument.DocumentNode.ChildNodes.Select<HtmlNode, Pair<string, string>>((Func<HtmlNode, Pair<string, string>>) (node => new Pair<string, string>(node.Name, ParametersUtil.Decode(node.InnerHtml, applyUrlDecode)))));
      }
      return (IEnumerable<Pair<string, string>>) pairList;
    }

    public static Dictionary<string, string> XmlToDictionary(
      string xml,
      bool applyUrlDecode = false)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      if (!string.IsNullOrEmpty(xml))
      {
        HtmlDocument htmlDocument = new HtmlDocument();
        using (new ThreadCultureSwitcher(Language.Parse("en").CultureInfo))
          htmlDocument.LoadHtml(xml.StartsWith("<") ? xml : ParametersUtil.Decode(xml, applyUrlDecode));
        foreach (HtmlNode htmlNode in htmlDocument.DocumentNode.ChildNodes.Where<HtmlNode>((Func<HtmlNode, bool>) (x => x.Name != "#text")))
          dictionary.Add(htmlNode.Name.ToLower(), ParametersUtil.Decode(htmlNode.InnerHtml, applyUrlDecode));
      }
      return dictionary;
    }

    public static Dictionary<string, string> XmlToDictionaryWithOriginalNames(
      string xml,
      bool applyUrlDecode = false)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      if (!string.IsNullOrEmpty(xml))
      {
        HtmlDocument htmlDocument = new HtmlDocument();
        using (new ThreadCultureSwitcher(Language.Parse("en").CultureInfo))
          htmlDocument.LoadHtml(xml.StartsWith("<") ? xml : ParametersUtil.Decode(xml, applyUrlDecode));
        foreach (HtmlNode htmlNode in htmlDocument.DocumentNode.ChildNodes.Where<HtmlNode>((Func<HtmlNode, bool>) (x => x.Name != "#text")))
          dictionary.Add(htmlNode.OriginalName, htmlNode.InnerHtml);
      }
      return dictionary;
    }

    public static string ConcatXml(string a, string b) => ParametersUtil.PairArrayToXml(ParametersUtil.XmlToPairArray(a, b));

    public static IDictionary<string, string> ItemsValuesXmlToDictionaryList(string xml)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      string queries = HttpUtility.UrlDecode(xml);
      if (queries == null)
        return (IDictionary<string, string>) dictionary;
      return queries.StartsWith(StaticSettings.SourceMarker) ? (IDictionary<string, string>) Utils.GetItemsName(queries.Substring(StaticSettings.SourceMarker.Length)).ToDictionary<string, string, string>((Func<string, string>) (i => i), (Func<string, string>) (i => i)) : QueryManager.Select(QuerySettings.ParseRange(queries)).ToDictionary();
    }

    public static string StringArrayToXml(IEnumerable<string> values, string tagName) => ParametersUtil.StringArrayToXml(values, tagName, false);

    public static string StringArrayToXml(
      IEnumerable<string> values,
      string tagName,
      bool applyUrlEncode)
    {
      Assert.ArgumentNotNull((object) values, nameof (values));
      Assert.ArgumentNotNullOrEmpty(tagName, nameof (tagName));
      HtmlDocument doc = new HtmlDocument();
      foreach (string str in values)
        ParametersUtil.AddNode(doc, tagName, str ?? string.Empty, applyUrlEncode);
      return ParametersUtil.Encode(doc);
    }

    public static string NameValueCollectionToXml(NameValueCollection values) => ParametersUtil.NameValueCollectionToXml(values, false);

    public static string NameValueCollectionToXml(NameValueCollection values, bool applyUrlEncode)
    {
      Assert.ArgumentNotNull((object) values, nameof (values));
      HtmlDocument doc = new HtmlDocument();
      foreach (string allKey in values.AllKeys)
        ParametersUtil.AddNode(doc, allKey, values[allKey], applyUrlEncode);
      return ParametersUtil.Encode(doc);
    }

    public static string PairArrayToXml(IEnumerable<Pair<string, string>> values) => ParametersUtil.PairArrayToXml(values, false);

    public static string PairArrayToXml(
      IEnumerable<Pair<string, string>> values,
      bool applyUrlEncode,
      bool encodeNodeText = false,
      bool encodeDoc = true)
    {
      Assert.ArgumentNotNull((object) values, nameof (values));
      HtmlDocument doc = new HtmlDocument();
      foreach (Pair<string, string> pair in values)
      {
        Assert.ArgumentNotNullOrEmpty(pair.Part1, "tagName");
        string xml = pair.Part2 ?? string.Empty;
        ParametersUtil.AddNode(doc, pair.Part1, encodeNodeText ? ParametersUtil.Encode(xml) : xml, applyUrlEncode);
      }
      return !encodeDoc ? doc.DocumentNode.InnerHtml : ParametersUtil.Encode(doc);
    }

    internal static string Expand(string parameters) => ParametersUtil.Expand(parameters, false);

    internal static string Expand(string parameters, bool allowUrlDEcoding)
    {
      Assert.ArgumentNotNull((object) parameters, nameof (parameters));
      NameValueCollection collection = ParametersUtil.XmlToNameValueCollection(parameters, allowUrlDEcoding);
      collection.ForEach((Action<string, string>) ((k, v) =>
      {
        if (!SessionUtil.IsSessionKey(v))
          return;
        collection[k] = Sitecore.Web.WebUtil.GetSessionString(v, v);
      }));
      return ParametersUtil.NameValueCollectionToXml(collection);
    }

    public static string Escape(string value) => HttpUtility.HtmlEncode(HttpUtility.UrlEncode(value));

    public static string Unescape(string value) => HttpUtility.HtmlDecode(HttpUtility.UrlDecode(value));

    public static string EncodeNodesText(string xml) => string.IsNullOrEmpty(xml) ? xml : xml.Replace("[<&]", "[&lt;&amp;]");

    private static string Encode(HtmlDocument doc) => doc.DocumentNode != null && doc.DocumentNode.FirstChild != null ? ParametersUtil.Encode(doc.DocumentNode.InnerHtml) : string.Empty;

    private static string Encode(string xml) => HttpUtility.HtmlEncode(xml);

    private static string Decode(string xml, bool applyUrlDecode)
    {
      string html = HttpUtility.HtmlDecode(xml);
      if (!applyUrlDecode || string.IsNullOrEmpty(html))
        return html;
      HtmlDocument htmlDocument = new HtmlDocument();
      using (new ThreadCultureSwitcher(Language.Parse("en").CultureInfo))
        htmlDocument.LoadHtml(html);
      foreach (HtmlNode childNode in (IEnumerable<HtmlNode>) htmlDocument.DocumentNode.ChildNodes)
        childNode.InnerHtml = HttpUtility.UrlDecode(childNode.InnerHtml);
      return htmlDocument.DocumentNode.InnerHtml;
    }

    private static void AddNode(HtmlDocument doc, string tag, string value, bool applyUrlEncode)
    {
      HtmlNode element = doc.CreateElement(tag);
      element.InnerHtml = applyUrlEncode ? HttpUtility.UrlEncode(value) : value;
      doc.DocumentNode.AppendChild(element);
    }
  }
}
