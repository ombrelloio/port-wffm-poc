// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Services.Pipelines.ExportToXml.ExportFormDataToXml
// Assembly: Sitecore.WFFM.Services, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A002B65D-6063-457D-AAC3-54DF69ADFBA9
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Services.dll

using Sitecore.Diagnostics;
using Sitecore.Security.Accounts;
using Sitecore.Text;
using Sitecore.WFFM.Abstractions.Dependencies;
using System.Text.RegularExpressions;
using System.Xml;

namespace Sitecore.WFFM.Services.Pipelines.ExportToXml
{
  public class ExportFormDataToXml
  {
    public void Process(FormExportArgs args)
    {
      Context.Job?.Status.LogInfo(DependenciesManager.ResourceManager.Localize("EXPORTING_DATA"));
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.InnerXml = args.Packet.ToXml();
      string parameter = args.Parameters["contextUser"];
      Assert.IsNotNullOrEmpty(parameter, "contextUser");
      using (new UserSwitcher(parameter, true))
      {
        ListString listString = new ListString(Regex.Replace(DependenciesManager.FormRegistryUtil.GetExportRestriction(((object) args.Item.ID).ToString(), string.Empty), "{|}", string.Empty));
        XmlNodeList xmlNodeList1 = xmlDocument.SelectNodes("packet/formentry");
        Assert.IsNotNull((object) xmlNodeList1, "roots");
        foreach (string name in listString)
        {
          foreach (XmlNode xmlNode in xmlNodeList1)
          {
            Assert.IsNotNull((object) xmlNode.Attributes, "Attributes");
            XmlAttribute attribute = xmlNode.Attributes[name];
            if (attribute != null)
              xmlNode.Attributes.Remove(attribute);
            XmlNodeList xmlNodeList2 = xmlNode.SelectNodes(string.Format("field[@fieldid='{0}']", (object) name.ToLower()));
            Assert.IsNotNull((object) xmlNodeList2, "nodeList");
            foreach (XmlNode oldChild in xmlNodeList2)
              xmlNode.RemoveChild(oldChild);
          }
        }
        args.Result = xmlDocument.DocumentElement.OuterXml;
      }
    }
  }
}
