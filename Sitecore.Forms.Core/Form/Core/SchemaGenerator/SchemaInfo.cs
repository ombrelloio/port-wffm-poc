// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.SchemaGenerator.SchemaInfo
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace Sitecore.Form.Core.SchemaGenerator
{
  public class SchemaInfo
  {
    private Dictionary<string, string> attributes = new Dictionary<string, string>();

    public SchemaInfo(string prefix, string tag)
    {
      this.Prefix = prefix;
      this.Tag = tag;
    }

    public IDictionary<string, string> Attributes => (IDictionary<string, string>) this.attributes;

    public string Prefix { get; private set; }

    public string Tag { get; set; }

    public static SchemaInfo Parse(string html)
    {
      int count = html.IndexOf(":");
      int num1 = Math.Min(html.IndexOf("\""), html.IndexOf("'"));
      string prefix = string.Empty;
      if (count > -1 && (num1 == -1 || count < num1 && num1 != -1))
      {
        prefix = html.Substring(1, count - 1);
        html = html.Remove(1, count);
        int num2 = html.IndexOf("</");
        if (num2 != -1)
          html = html.Remove(num2 + 2, count);
      }
      HtmlNode element = new HtmlDocument().CreateElement(prefix + ":control");
      element.InnerHtml = html;
      HtmlNode childNode = element.ChildNodes[0];
      SchemaInfo schemaInfo = new SchemaInfo(prefix, childNode.Name);
      foreach (HtmlAttribute attribute in (IEnumerable<HtmlAttribute>) childNode.Attributes)
        schemaInfo.Attributes.Add(attribute.Name.ToLower(), attribute.Value);
      return schemaInfo;
    }
  }
}
