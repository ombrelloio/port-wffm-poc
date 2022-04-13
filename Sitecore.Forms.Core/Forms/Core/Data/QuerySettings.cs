// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.QuerySettings
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using HtmlAgilityPack;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sitecore.Forms.Core.Data
{
  [Serializable]
  public class QuerySettings
  {
    private static readonly string defaultContextRoot = ((object) ItemIDs.RootID).ToString();
    private static string defaultFieldName = "__Item Name";
    private string textFieldName;
    private string valueFieldName;

    public QuerySettings(string queryType, string queryText)
      : this(queryType, queryText, (string) null, (string) null)
    {
    }

    public QuerySettings(
      string queryType,
      string queryText,
      string valueFieldName,
      string textFieldName)
    {
      Assert.ArgumentNotNullOrEmpty(queryType, nameof (queryType));
      Assert.ArgumentNotNull((object) queryText, nameof (queryText));
      this.QueryType = queryType;
      this.QueryText = queryText;
      this.ValueFieldName = valueFieldName;
      this.TextFieldName = textFieldName;
      this.ContextRoot = ((object) ItemIDs.RootID).ToString();
      this.LocalizedTexts = (IDictionary<string, string>) new Dictionary<string, string>();
    }

    public string ContextRoot { get; internal set; }

    public IDictionary<string, string> LocalizedTexts { get; private set; }

    public string QueryText { get; internal set; }

    public string QueryType { get; private set; }

    public bool ShowOnlyValue { get; internal set; }

    public string TextFieldName
    {
      get => this.textFieldName;
      set => this.textFieldName = string.IsNullOrEmpty(value) ? "__Item Name" : value;
    }

    public string ValueFieldName
    {
      get => this.valueFieldName;
      set => this.valueFieldName = string.IsNullOrEmpty(value) ? "__Item Name" : value;
    }

    public static IEnumerable<QuerySettings> ParseRange(string queries) => !string.IsNullOrEmpty(queries) ? ParametersUtil.XmlToHtmlNodeCollection(queries).Select<HtmlNode, QuerySettings>((Func<HtmlNode, QuerySettings>) (node => QuerySettings.ToQuerySettings(node))) : (IEnumerable<QuerySettings>) new List<QuerySettings>();

    public static string ToString(IEnumerable<QuerySettings> queries)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (QuerySettings query in queries)
        stringBuilder.Append(query.ToString());
      return stringBuilder.ToString();
    }

    public override string ToString()
    {
      HtmlDocument htmlDocument = new HtmlDocument();
      HtmlNode element1 = htmlDocument.CreateElement("query");
      HtmlNode element2 = htmlDocument.CreateElement("value");
      element2.InnerHtml = this.QueryText;
      element1.AppendChild(element2);
      foreach (KeyValuePair<string, string> localizedText in (IEnumerable<KeyValuePair<string, string>>) this.LocalizedTexts)
      {
        HtmlNode element3 = htmlDocument.CreateElement(localizedText.Key);
        element3.InnerHtml = localizedText.Value;
        element1.AppendChild(element3);
      }
      element1.Attributes.Append("t", this.QueryType);
      if (this.ValueFieldName != QuerySettings.defaultFieldName)
        element1.Attributes.Append("vf", this.ValueFieldName);
      if (this.TextFieldName != QuerySettings.defaultFieldName)
        element1.Attributes.Append("tf", this.TextFieldName);
      if (this.ContextRoot != QuerySettings.defaultContextRoot)
        element1.Attributes.Append("cr", this.ContextRoot);
      if (this.ShowOnlyValue)
        element1.Attributes.Append("sov", "true");
      return element1.OuterHtml;
    }

    private static QuerySettings ToQuerySettings(HtmlNode node)
    {
      QuerySettings query = new QuerySettings(node.GetAttributeValue("t", "default"), node.InnerHtml, node.GetAttributeValue("vf", QuerySettings.defaultFieldName), node.GetAttributeValue("tf", QuerySettings.defaultFieldName))
      {
        ContextRoot = node.GetAttributeValue("cr", QuerySettings.defaultContextRoot),
        ShowOnlyValue = node.GetAttributeValue("sov", false)
      };
      HtmlNode htmlNode = node.SelectSingleNode("value");
      query.QueryText = htmlNode == null ? node.InnerHtml : htmlNode.InnerHtml;
      node.ChildNodes.ForEach((Action<HtmlNode>) (a =>
      {
        if (!(a.Name != "value") || !(a.Name != "#text") || query.LocalizedTexts.ContainsKey(a.Name))
          return;
        query.LocalizedTexts.Add(a.Name, a.InnerHtml);
      }));
      return query;
    }
  }
}
