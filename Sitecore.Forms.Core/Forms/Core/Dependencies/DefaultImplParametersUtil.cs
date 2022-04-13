// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Dependencies.DefaultImplParametersUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using HtmlAgilityPack;
using Sitecore.Form.Core.Utility;
using Sitecore.WFFM.Abstractions.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Sitecore.Forms.Core.Dependencies
{
  [Serializable]
  public class DefaultImplParametersUtil : IParametersUtil
  {
    public NameValueCollection XmlToNameValueCollection(string xml) => ParametersUtil.XmlToNameValueCollection(xml);

    public IEnumerable<HtmlNode> XmlToHtmlNodeCollection(string xml) => ParametersUtil.XmlToHtmlNodeCollection(xml);

    public string HtmlNodeCollectionToXml(IEnumerable<HtmlNode> nodes) => ParametersUtil.HtmlNodeCollectionToXml(nodes);

    public string NameValueCollectionToXml(NameValueCollection values) => ParametersUtil.NameValueCollectionToXml(values);
  }
}
