// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Applications.ContentEditor.GroupListField
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.UI.Controls;
using Sitecore.Shell.Applications.ContentEditor;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Xml;
using System;
using System.Collections.Specialized;
using System.Web.UI;
using System.Xml;

namespace Sitecore.Forms.Core.Applications.ContentEditor
{
  public class GroupListField : Sitecore.Web.UI.HtmlControls.Control, IContentField
  {
    private GroupListBuilder builder = new GroupListBuilder();
    private string source;

    public GroupListField() => this.Class = "scContentControlLayout";

    private void BuildGrid()
    {
      this.builder.Class = this.Class;
      this.builder.ID = ((Sitecore.Web.UI.HtmlControls.Control) this).ID;
      this.builder.ItemId = this.ItemID;
      this.builder.Value = this.Value;
      this.builder.Description = this.Description;
      this.builder.BuildGrid((Sitecore.Web.UI.HtmlControls.Control) this);
    }

    private XmlDocument GetDocument()
    {
      XmlDocument xmlDocument = new XmlDocument();
      string xml = StringUtil.GetString(((Component) this).ServerProperties["Value"]);
      if (xml.Length > 0)
      {
        xmlDocument.LoadXml(xml);
        return xmlDocument;
      }
      xmlDocument.LoadXml("<r/>");
      return xmlDocument;
    }

    private static string GetListValue(XmlDocument doc)
    {
      Assert.ArgumentNotNull((object) doc, nameof (doc));
      XmlNodeList xmlNodeList = doc.SelectNodes("/li/g");
      if (xmlNodeList != null && xmlNodeList.Count != 0)
      {
        foreach (XmlNode xmlNode in xmlNodeList)
        {
          if (xmlNode.ChildNodes.Count > 0 || XmlUtil.GetAttribute("li", xmlNode).Length > 0)
            return doc.OuterXml;
        }
      }
      return string.Empty;
    }

    public string GetValue() => this.Value;

    protected override void OnLoad(EventArgs e)
    {
      Assert.ArgumentNotNull((object) e, nameof (e));
      base.OnLoad(e);
      this.BuildGrid();
    }

    protected override void OnPreRender(EventArgs e)
    {
      Assert.ArgumentNotNull((object) e, nameof (e));
      // ISSUE: explicit non-virtual call
      base.OnPreRender(e);
      ((Component) this).ServerProperties["Value"] = ((Component) this).ServerProperties["Value"];
    }

    private void Refresh()
    {
      ((Sitecore.Web.UI.HtmlControls.Control) this).Controls.Clear();
      this.BuildGrid();
      Sitecore.Context.ClientPage.ClientResponse.SetOuterHtml(((Sitecore.Web.UI.HtmlControls.Control) this).ID, (Sitecore.Web.UI.HtmlControls.Control) this);
    }

    protected virtual void SetModified() => Sitecore.Context.ClientPage.Modified = true;

    public void SetValue(string value)
    {
      Assert.ArgumentNotNull((object) value, nameof (value));
      ((Component) this).ServerProperties["Value"] = (object) value;
    }

    public new string Value
    {
      get => ((Component) this).ServerProperties[nameof (Value)] as string;
      set => ((Component) this).ServerProperties[nameof (Value)] = (object) value;
    }

    public string Description { get; set; }

    public string ItemID
    {
      get => GetViewStateString(nameof (ItemID));
      set => SetViewStateString(nameof (ItemID), value);
    }

    public string ListItemClass
    {
      get => this.builder.ListItemClass;
      set => this.builder.ListItemClass = value;
    }

    public string GroupClass
    {
      get => this.builder.GroupClass;
      set => this.builder.GroupClass = value;
    }

    public string GroupEmptyMessage
    {
      get => this.builder.GroupEmptyMessage;
      set => this.builder.GroupEmptyMessage = value;
    }

    public string ListItemEmptyMessage
    {
      get => this.builder.ListItemsEmptyMessage;
      set => this.builder.ListItemsEmptyMessage = value;
    }

    public bool ReadOnlyMode
    {
      get => this.builder.ReadonlyMode;
      set => this.builder.ReadonlyMode = value;
    }

    public string TrackingXml
    {
      set => this.builder.TrackingXml = value;
    }

    public string Structure
    {
      set => this.builder.Structure = value;
    }

    public string Source
    {
      set
      {
        if (!string.IsNullOrEmpty(value))
        {
          NameValueCollection nameValues = StringUtil.GetNameValues(value.ToLower(), '=', '&');
          if (!string.IsNullOrEmpty(nameValues["root"]))
            this.builder.ActionRoot = nameValues["root"];
          if (!string.IsNullOrEmpty(nameValues["systemroot"]))
            this.builder.SystemRoot = nameValues["systemroot"];
        }
        this.source = value;
      }
      get => this.source;
    }

    public string OnClick
    {
      get => this.builder.GroupClick;
      set => this.builder.GroupClick = value;
    }
  }
}
