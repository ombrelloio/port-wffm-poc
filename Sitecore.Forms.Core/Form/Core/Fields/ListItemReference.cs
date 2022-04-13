// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Fields.ListItemReference
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Xml;
using System.Collections.Specialized;
using System.Xml;

namespace Sitecore.Form.Core.Fields
{
  public class ListItemReference
  {
    private ID id;
    private string parameters;
    private string displayName;

    public ListItemReference(XmlNode configNode)
    {
      Assert.ArgumentNotNull((object) configNode, nameof (configNode));
      this.ParseSettings(configNode);
    }

    private void ParseSettings(XmlNode node)
    {
      this.id = ListItemReference.ParseId(node);
      this.parameters = ListItemReference.ParseParameters(node);
      this.displayName = ListItemReference.ParseDisplayName(node);
    }

    private static ID ParseId(XmlNode configNode)
    {
      Assert.ArgumentNotNull((object) configNode, nameof (configNode));
      return MainUtil.GetID(XmlUtil.GetAttribute("id", configNode), ID.Null);
    }

    private static string ParseParameters(XmlNode configNode)
    {
      Assert.ArgumentNotNull((object) configNode, nameof (configNode));
      return XmlUtil.GetAttribute("par", configNode);
    }

    private static string ParseDisplayName(XmlNode configNode)
    {
      Assert.ArgumentNotNull((object) configNode, nameof (configNode));
      return XmlUtil.GetAttribute("displayName", configNode);
    }

    public string DisplayName => this.displayName;

    public ID ItemID => this.id;

    public NameValueCollection Parameters => StringUtil.GetNameValues(this.parameters, '=', '&');
  }
}
