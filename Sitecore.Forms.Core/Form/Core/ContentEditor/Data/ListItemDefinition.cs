// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.ContentEditor.Data.ListItemDefinition
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using HtmlAgilityPack;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Globalization;
using Sitecore.WFFM.Abstractions.ContentEditor;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Serialization;

namespace Sitecore.Form.Core.ContentEditor.Data
{
  [XmlRoot("li")]
  [Serializable]
  public class ListItemDefinition : XmlSerializable, IListItemDefinition
  {
    [XmlAttribute("id")]
    public string ItemID { get; set; }

    [XmlElement("parameters")]
    public string Parameters { get; set; }

    [XmlAttribute("unicid")]
    public string Unicid { get; set; }

    public string GetTitle()
    {
      if (!string.IsNullOrEmpty(this.ItemID))
      {
        Item obj = DependenciesManager.ItemRepository.GetItem(this.ItemID);
        if (obj != null)
        {
          string subTitle = this.GetSubTitle();
          return string.Join(string.Empty, new string[3]
          {
            obj.DisplayName,
            !string.IsNullOrEmpty(subTitle) ? ": " : string.Empty,
            DependenciesManager.TranslationProvider.SystemText(subTitle ?? string.Empty)
          });
        }
      }
      return string.Empty;
    }

    private string GetSubTitle()
    {
      string nameValue = DependenciesManager.ParametersUtil.XmlToNameValueCollection(this.Parameters)[Sitecore.WFFM.Abstractions.Constants.Core.Constants.ActionSubTitle];
      if (string.IsNullOrEmpty(nameValue))
        return string.Empty;
      ID id;
      if (ID.TryParse(nameValue, out id))
      {
        Item obj = DependenciesManager.ItemRepository.GetItem(id);
        if (obj != null)
          return obj.DisplayName;
      }
      return nameValue;
    }

    public void SetFailedMessageForLanguage(string text, Language language)
    {
      Assert.ArgumentNotNull((object) language, nameof (language));
      NameValueCollection nameValueCollection = DependenciesManager.ParametersUtil.XmlToNameValueCollection(this.Parameters);
      string str1 = nameValueCollection["__messages"];
      string str2 = text;
      Item obj = DependenciesManager.ItemRepository.GetItem(FormIDs.SubmitErrorId, language);
      if (obj != null && str2 == ((BaseItem) obj)["Value"])
        str2 = string.Empty;
      string xml = "<messages></messages>";
      if (!string.IsNullOrEmpty(str1))
        xml = str1;
      HtmlNode htmlNode1 = DependenciesManager.ParametersUtil.XmlToHtmlNodeCollection(xml).First<HtmlNode>();
      HtmlNode htmlNode2 = htmlNode1.SelectSingleNode(language.Name.ToLower());
      if (htmlNode2 == null)
      {
        htmlNode2 = HtmlNode.CreateNode(Sitecore.StringExtensions.StringExtensions.FormatWith("<{0}/>", new object[1]
        {
          (object) language.Name.ToLower()
        }));
        if (!string.IsNullOrEmpty(str2))
          htmlNode1.AppendChild(htmlNode2);
      }
      else if (string.IsNullOrEmpty(str2))
        htmlNode1.RemoveChild(htmlNode2);
      htmlNode2.InnerHtml = str2 ?? string.Empty;
      if (!string.IsNullOrEmpty(htmlNode1.InnerHtml))
        nameValueCollection["__messages"] = DependenciesManager.ParametersUtil.HtmlNodeCollectionToXml((IEnumerable<HtmlNode>) new HtmlNode[1]
        {
          htmlNode1
        });
      else
        nameValueCollection.Remove("__messages");
      this.Parameters = DependenciesManager.ParametersUtil.NameValueCollectionToXml(nameValueCollection);
    }

    public string GetFailedMessageForLanguage(Language language, string defaultValue)
    {
      Assert.IsNotNull((object) defaultValue, "Argument defaultValue is null");
      Assert.IsNotNull((object) language, "Argument language is null");
      string nameValue = DependenciesManager.ParametersUtil.XmlToNameValueCollection(this.Parameters)["__messages"];
      if (!string.IsNullOrEmpty(nameValue))
      {
        HtmlNode htmlNode1 = DependenciesManager.ParametersUtil.XmlToHtmlNodeCollection(nameValue).FirstOrDefault<HtmlNode>();
        if (htmlNode1 != null)
        {
          HtmlNode htmlNode2 = htmlNode1.SelectSingleNode(language.Name.ToLower());
          if (htmlNode2 != null && !string.IsNullOrEmpty(htmlNode2.InnerHtml))
            return htmlNode2.InnerHtml;
        }
      }
      return defaultValue;
    }

    public string GetFailedMessageForLanguage(
      Language language,
      bool tryGetDefaultValue,
      ID formID)
    {
      Assert.ArgumentNotNull((object) formID, nameof (formID));
      string defaultValue = string.Empty;
      if (tryGetDefaultValue)
      {
        Item obj1 = DependenciesManager.ItemRepository.GetItem(formID);
        if (obj1 != null)
        {
          string str = ((BaseItem) obj1)[FormIDs.SaveActionFailedMessage];
          if (!string.IsNullOrEmpty(str))
            defaultValue = str;
        }
        else
        {
          Item obj2 = DependenciesManager.ItemRepository.GetItem(FormIDs.SubmitErrorId, language);
          if (obj2 != null)
            defaultValue = ((BaseItem) obj2)["Value"];
        }
      }
      return this.GetFailedMessageForLanguage(language, defaultValue);
    }

    public bool IsEqual(IListItemDefinition definition)
    {
      if (definition == null)
        return false;
      if (this == definition)
        return true;
      return object.Equals((object) definition.ItemID, (object) this.ItemID) && object.Equals((object) definition.Parameters, (object) this.Parameters) && object.Equals((object) definition.Unicid, (object) this.Unicid);
    }
  }
}
