// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Speak.ViewModel.FormPacket
// Assembly: Sitecore.WFFM.Services, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A002B65D-6063-457D-AAC3-54DF69ADFBA9
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Services.dll

using Sitecore.WFFM.Abstractions.Analytics;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Sitecore.WFFM.Speak.ViewModel
{
  [XmlRoot("packet")]
  public class FormPacket
  {
    private List<FormData> entries;

    public FormPacket(IEnumerable<FormData> entries) => this.entries = new List<FormData>(entries);

    internal FormPacket()
    {
    }

    [XmlElement("formentry", typeof (FormData))]
    public List<FormData> Entries => this.entries;

    public string ToXml()
    {
      XDocument xdocument = new XDocument(new object[1]
      {
        (object) new XElement((XName) "packet")
      });
      foreach (FormData entry in this.Entries)
      {
        XName name1 = (XName) "formentry";
        object[] objArray = new object[5]
        {
          (object) new XAttribute((XName) "id", (object) entry.Id),
          (object) new XAttribute((XName) "formid", (object) entry.FormID),
          null,
          null,
          null
        };
        XName name2 = (XName) "created";
        DateTime dateTime = entry.Timestamp;
        dateTime = dateTime.ToLocalTime();
        string str = dateTime.ToString("G");
        objArray[2] = (object) new XAttribute(name2, (object) str);
        objArray[3] = (object) new XAttribute((XName) "interactionId", (object) entry.InteractionId);
        objArray[4] = (object) new XAttribute((XName) "contactId", (object) entry.InteractionId);
        XElement xelement = new XElement(name1, objArray);
        if (xdocument.Root != null)
          xdocument.Root.Add((object) xelement);
        foreach (FieldData field in entry.Fields)
          xelement.Add((object) new XElement((XName) "field", new object[6]
          {
            (object) new XAttribute((XName) "id", (object) field.Id),
            (object) new XAttribute((XName) "fieldid", (object) field.FieldId),
            (object) new XAttribute((XName) "formentryid", (object) entry.Id),
            (object) new XAttribute((XName) "name", (object) (field.FieldName ?? string.Empty)),
            (object) new XAttribute((XName) "value", (object) (field.Value ?? string.Empty)),
            (object) new XAttribute((XName) "params", (object) (field.Data ?? string.Empty))
          }));
      }
      return xdocument.ToString();
    }
  }
}
