// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Utils.IParametersUtil
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using HtmlAgilityPack;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Sitecore.WFFM.Abstractions.Utils
{
  public interface IParametersUtil
  {
    NameValueCollection XmlToNameValueCollection(string xml);

    IEnumerable<HtmlNode> XmlToHtmlNodeCollection(string xml);

    string HtmlNodeCollectionToXml(IEnumerable<HtmlNode> nodes);

    string NameValueCollectionToXml(NameValueCollection values);
  }
}
