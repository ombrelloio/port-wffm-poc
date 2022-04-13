// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.SchemaGenerator.SchemaGeneratorManager
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using HtmlAgilityPack;
using Sitecore.Diagnostics;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Layouts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.Design;

namespace Sitecore.Form.Core.SchemaGenerator
{
  public class SchemaGeneratorManager
  {
    private static readonly DesignerHost host = new DesignerHost();
    private static readonly IRegisterDirectiveService registerDirectiveService = (IRegisterDirectiveService) new RegisterDirectiveManager();

    public static string GetSchema(Control parent, string controlDirective, string[] scripts)
    {
      List<string> stringList = new List<string>();
      HtmlDocument doc = new HtmlDocument();
      stringList.Add(controlDirective);
      HtmlNode documentNode = doc.DocumentNode;
      SchemaGeneratorManager.BuildXmlTree((object) parent, documentNode, doc, (IList<string>) stringList, (object) parent);
      return SchemaGeneratorManager.AscxBuilder((IList<string>) stringList, documentNode, doc, scripts);
    }

    private static string AscxBuilder(
      IList<string> directives,
      HtmlNode root,
      HtmlDocument doc,
      string[] scripts)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (string directive in (IEnumerable<string>) directives)
      {
        stringBuilder.Append(directive);
        stringBuilder.Append("\n");
      }
      foreach (string script in scripts)
      {
        stringBuilder.AppendFormat("<link type=\"text/css\" rel=\"stylesheet\" href=\"{0}\"/>", (object) script);
        stringBuilder.Append("\n");
      }
      stringBuilder.Append(XHtml.Format(doc));
      return stringBuilder.ToString();
    }

    private static string GetDerectivesDefinition(object control) => SchemaGeneratorManager.registerDirectiveService.EnsureCustomControlRegisterDirective(control).GetHtml(true);

    private static void BuildXmlTree(
      object control,
      HtmlNode root,
      HtmlDocument doc,
      IList<string> directives,
      object parent)
    {
      HtmlNode htmlNode = (HtmlNode) null;
      if (!DesignAttributesUtil.IsDummy(control))
      {
        htmlNode = SchemaGeneratorManager.AddNode(control, root, doc, directives, parent);
        string derectivesDefinition = SchemaGeneratorManager.GetDerectivesDefinition(control);
        if (!directives.Contains(derectivesDefinition))
          directives.Add(derectivesDefinition);
      }
      if (!DesignAttributesUtil.IsPersistChildren(control) && !DesignAttributesUtil.IsParseChildren(control) || !(control is Control) || control is BaseUserControl || control is UserControl && !(control is SitecoreSimpleForm) && (!(control is UserControl) || !string.IsNullOrEmpty(((TemplateControl) control).AppRelativeVirtualPath)))
        return;
      foreach (Control control1 in ((Control) control).Controls)
        SchemaGeneratorManager.BuildXmlTree((object) control1, htmlNode ?? root, doc, directives, parent);
    }

    private static HtmlNode AddNode(
      object control,
      HtmlNode root,
      HtmlDocument doc,
      IList<string> directives,
      object parent)
    {
      SchemaInfo schemaInfo = SchemaInfo.Parse(SchemaGeneratorManager.GetToolboxValue(control));
      if (string.IsNullOrEmpty(schemaInfo.Tag))
        return (HtmlNode) null;
      HtmlNode htmlNode = !string.IsNullOrEmpty(schemaInfo.Prefix) ? doc.CreateElement(schemaInfo.Prefix + ":" + schemaInfo.Tag) : doc.CreateElement(schemaInfo.Tag);
      foreach (KeyValuePair<string, string> propertyAttribute in (IEnumerable<KeyValuePair<string, string>>) DesignAttributesUtil.GetPropertyAttributes(control, parent))
        schemaInfo.Attributes[propertyAttribute.Key] = propertyAttribute.Value;
      foreach (KeyValuePair<string, string> attribute1 in (IEnumerable<KeyValuePair<string, string>>) schemaInfo.Attributes)
      {
        HtmlAttribute attribute2 = doc.CreateAttribute(attribute1.Key);
        attribute2.Value = attribute1.Value;
        htmlNode.Attributes.Append(attribute2);
      }
      root.AppendChild(htmlNode);
      SchemaGeneratorManager.BuildInnerElements(control, htmlNode, doc, directives, parent);
      SchemaGeneratorManager.BuildInnerCollection(control, htmlNode, doc, directives, parent);
      return htmlNode;
    }

    private static void BuildInnerElements(
      object control,
      HtmlNode root,
      HtmlDocument doc,
      IList<string> directives,
      object parent)
    {
      foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) DesignAttributesUtil.GetPropertiesInnerElement(control))
      {
        if (keyValuePair.Value is IList)
        {
          foreach (object control1 in (IEnumerable) keyValuePair.Value)
            SchemaGeneratorManager.BuildXmlTree(control1, root, doc, directives, parent);
        }
        else
          root.InnerHtml = keyValuePair.Value.ToString();
      }
    }

    private static void BuildInnerCollection(
      object control,
      HtmlNode root,
      HtmlDocument doc,
      IList<string> directives,
      object parent)
    {
      foreach (KeyValuePair<string, object> propertiesInner in (IEnumerable<KeyValuePair<string, object>>) DesignAttributesUtil.GetPropertiesInnerCollection(control))
      {
        if (propertiesInner.Value is IList)
        {
          HtmlNode element = doc.CreateElement(propertiesInner.Key);
          root.AppendChild(element);
          foreach (object control1 in (IEnumerable) propertiesInner.Value)
            SchemaGeneratorManager.BuildXmlTree(control1, element, doc, directives, parent);
        }
      }
    }

    private static string GetToolboxValue(object control) => SchemaGeneratorManager.GetHTMLFromWebControlTool(new WebControlToolboxItem(control.GetType()), control);

    private static string GetHTMLFromWebControlTool(WebControlToolboxItem wcTool, object control)
    {
      Type toolType;
      try
      {
        toolType = wcTool.GetToolType((IDesignerHost) SchemaGeneratorManager.host);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex);
        return (string) null;
      }
      RegisterDirective registerDirective = SchemaGeneratorManager.registerDirectiveService.EnsureCustomControlRegisterDirective(control);
      if (registerDirective == (RegisterDirective) null)
      {
        Log.Error("Missing register directive", toolType);
        return string.Empty;
      }
      return string.Format((IFormatProvider) CultureInfo.InvariantCulture, wcTool.GetToolHtml((IDesignerHost) SchemaGeneratorManager.host), new object[1]
      {
        (object) registerDirective.TagPrefix
      }).Trim();
    }
  }
}
