// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Services.Pipelines.ExportToExcel.ExportFormDataToExcel
// Assembly: Sitecore.WFFM.Services, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A002B65D-6063-457D-AAC3-54DF69ADFBA9
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Services.dll

using Sitecore.Diagnostics;
using Sitecore.Security.Accounts;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Speak.ViewModel;
using System;
using System.Linq;
using System.Xml;

namespace Sitecore.WFFM.Services.Pipelines.ExportToExcel
{
  public class ExportFormDataToExcel
  {
    public void Process(FormExportArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      Context.Job?.Status.LogInfo(DependenciesManager.ResourceManager.Localize("EXPORTING_DATA"));
      string parameter = args.Parameters["contextUser"];
      Assert.IsNotNullOrEmpty(parameter, "contextUser");
      using (new UserSwitcher(parameter, true))
      {
        XmlDocument doc = new XmlDocument();
        XmlElement element1 = doc.CreateElement("ss:Workbook");
        XmlAttribute attribute1 = doc.CreateAttribute("xmlns");
        attribute1.Value = "urn:schemas-microsoft-com:office:spreadsheet";
        element1.Attributes.Append(attribute1);
        XmlAttribute attribute2 = doc.CreateAttribute("xmlns:o");
        attribute2.Value = "urn:schemas-microsoft-com:office:office";
        element1.Attributes.Append(attribute2);
        XmlAttribute attribute3 = doc.CreateAttribute("xmlns:x");
        attribute3.Value = "urn:schemas-microsoft-com:office:excel";
        element1.Attributes.Append(attribute3);
        XmlAttribute attribute4 = doc.CreateAttribute("xmlns:ss");
        attribute4.Value = "urn:schemas-microsoft-com:office:spreadsheet";
        element1.Attributes.Append(attribute4);
        XmlAttribute attribute5 = doc.CreateAttribute("xmlns:html");
        attribute5.Value = "http://www.w3.org/TR/REC-html40";
        element1.Attributes.Append(attribute5);
        doc.AppendChild((XmlNode) element1);
        XmlElement element2 = doc.CreateElement("Styles");
        element1.AppendChild((XmlNode) element2);
        XmlElement element3 = doc.CreateElement("Style");
        XmlAttribute attribute6 = doc.CreateAttribute("ss", "ID", "xmlns");
        attribute6.Value = "xBoldVerdana";
        element3.Attributes.Append(attribute6);
        element2.AppendChild((XmlNode) element3);
        XmlElement element4 = doc.CreateElement("Font");
        XmlAttribute attribute7 = doc.CreateAttribute("ss", "Bold", "xmlns");
        attribute7.Value = "1";
        element4.Attributes.Append(attribute7);
        XmlAttribute attribute8 = doc.CreateAttribute("ss", "FontName", "xmlns");
        attribute8.Value = "verdana";
        element4.Attributes.Append(attribute8);
        element3.AppendChild((XmlNode) element4);
        XmlElement element5 = doc.CreateElement("Style");
        XmlAttribute attribute9 = doc.CreateAttribute("ss", "ID", "xmlns");
        attribute9.Value = "xVerdana";
        element5.Attributes.Append(attribute9);
        element2.AppendChild((XmlNode) element5);
        XmlElement element6 = doc.CreateElement("Font");
        XmlAttribute attribute10 = doc.CreateAttribute("ss", "FontName", "xmlns");
        attribute10.Value = "verdana";
        element6.Attributes.Append(attribute10);
        element5.AppendChild((XmlNode) element6);
        XmlElement element7 = doc.CreateElement("Worksheet");
        XmlAttribute attribute11 = doc.CreateAttribute("ss", "Name", "xmlns");
        attribute11.Value = "Sheet1";
        element7.Attributes.Append(attribute11);
        element1.AppendChild((XmlNode) element7);
        XmlElement element8 = doc.CreateElement("Table");
        XmlAttribute attribute12 = doc.CreateAttribute("ss", "DefaultColumnWidth", "xmlns");
        attribute12.Value = "130";
        element8.Attributes.Append(attribute12);
        element7.AppendChild((XmlNode) element8);
        this.BuildHeader(doc, args.Item, element8);
        this.BuildBody(doc, args.Item, args.Packet, element8);
        XmlElement element9 = doc.CreateElement("WorksheetOptions");
        XmlElement element10 = doc.CreateElement("Selected");
        XmlElement element11 = doc.CreateElement("Panes");
        XmlElement element12 = doc.CreateElement("Pane");
        XmlElement element13 = doc.CreateElement("Number");
        element13.InnerText = "1";
        XmlElement element14 = doc.CreateElement("ActiveCol");
        element14.InnerText = "1";
        element12.AppendChild((XmlNode) element14);
        element12.AppendChild((XmlNode) element13);
        element11.AppendChild((XmlNode) element12);
        element9.AppendChild((XmlNode) element11);
        element9.AppendChild((XmlNode) element10);
        element7.AppendChild((XmlNode) element9);
        args.Result = "<?xml version=\"1.0\"?>" + doc.InnerXml.Replace("xmlns:ss=\"xmlns\"", "");
      }
    }

    private void BuildHeader(XmlDocument doc, IFormItem item, XmlElement root)
    {
      XmlElement element = doc.CreateElement("Row");
      string exportRestriction = DependenciesManager.FormRegistryUtil.GetExportRestriction(((object) item.ID).ToString(), string.Empty);
      if (exportRestriction.IndexOf("created", StringComparison.Ordinal) == -1)
      {
        XmlElement headerCell = this.CreateHeaderCell("String", "Created", doc);
        element.AppendChild((XmlNode) headerCell);
      }
      foreach (IFieldItem field in item.Fields)
      {
        if (exportRestriction.IndexOf(((object) field.ID).ToString(), StringComparison.Ordinal) == -1)
        {
          XmlElement headerCell = this.CreateHeaderCell("String", field.FieldDisplayName, doc);
          element.AppendChild((XmlNode) headerCell);
        }
      }
      root.AppendChild((XmlNode) element);
    }

    private XmlElement CreateHeaderCell(string sType, string sValue, XmlDocument doc)
    {
      XmlElement element1 = doc.CreateElement("Cell");
      XmlAttribute attribute1 = doc.CreateAttribute("ss", "StyleID", "xmlns");
      attribute1.Value = "xBoldVerdana";
      element1.Attributes.Append(attribute1);
      XmlElement element2 = doc.CreateElement("Data");
      XmlAttribute attribute2 = doc.CreateAttribute("ss", "Type", "xmlns");
      attribute2.Value = sType;
      element2.Attributes.Append(attribute2);
      element2.InnerText = sValue;
      element1.AppendChild((XmlNode) element2);
      return element1;
    }

    private void BuildBody(XmlDocument doc, IFormItem item, FormPacket packet, XmlElement root)
    {
      foreach (FormData entry in packet.Entries)
        root.AppendChild((XmlNode) this.BuildRow(entry, item, doc));
    }

    private XmlElement BuildRow(FormData entry, IFormItem item, XmlDocument xd)
    {
      XmlElement element = xd.CreateElement("Row");
      string exportRestriction = DependenciesManager.FormRegistryUtil.GetExportRestriction(((object) item.ID).ToString(), string.Empty);
      if (exportRestriction.IndexOf("created") == -1)
      {
        DateTime dateTime = entry.Timestamp;
        dateTime = dateTime.ToLocalTime();
        XmlElement cell = this.CreateCell("String", dateTime.ToString("G"), xd);
        element.AppendChild((XmlNode) cell);
      }
      foreach (IFieldItem field1 in item.Fields)
      {
        IFieldItem field = field1;
        if (exportRestriction.IndexOf(((object) field.ID).ToString(), StringComparison.Ordinal) == -1)
        {
          FieldData fieldData = entry.Fields.FirstOrDefault<FieldData>((Func<FieldData, bool>) (f => f.FieldId == field.ID.Guid));
          XmlElement cell = this.CreateCell("String", fieldData != null ? fieldData.Value : string.Empty, xd);
          element.AppendChild((XmlNode) cell);
        }
      }
      return element;
    }

    private XmlElement CreateCell(string sType, string sValue, XmlDocument doc)
    {
      XmlElement element1 = doc.CreateElement("Cell");
      XmlAttribute attribute1 = doc.CreateAttribute("ss", "StyleID", "xmlns");
      attribute1.Value = "xVerdana";
      element1.Attributes.Append(attribute1);
      XmlElement element2 = doc.CreateElement("Data");
      XmlAttribute attribute2 = doc.CreateAttribute("ss", "Type", "xmlns");
      attribute2.Value = sType;
      element2.Attributes.Append(attribute2);
      element2.InnerText = sValue;
      element1.AppendChild((XmlNode) element2);
      return element1;
    }
  }
}
