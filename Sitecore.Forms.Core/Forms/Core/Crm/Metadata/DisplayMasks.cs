// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Metadata.DisplayMasks
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Sitecore.Forms.Core.Crm.Metadata
{
  [Flags]
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2007/WebServices")]
  [Serializable]
  public enum DisplayMasks
  {
    None = 1,
    PrimaryName = 2,
    ObjectTypeCode = 4,
    ValidForAdvancedFind = 8,
    ValidForForm = 16, // 0x00000010
    ValidForGrid = 32, // 0x00000020
    RequiredForForm = 64, // 0x00000040
    RequiredForGrid = 128, // 0x00000080
  }
}
