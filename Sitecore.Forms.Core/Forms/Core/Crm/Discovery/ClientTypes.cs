// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Discovery.ClientTypes
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Sitecore.Forms.Core.Crm.Discovery
{
  [Flags]
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2007/CrmDiscoveryService")]
  [Serializable]
  public enum ClientTypes
  {
    OutlookLaptop = 1,
    OutlookDesktop = 2,
    DataMigration = 4,
    OutlookConfiguration = 8,
    DataMigrationConfiguration = 16, // 0x00000010
  }
}
