// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.AccessRights
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Sitecore.Forms.Core.Crm
{
  [Flags]
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2006/CoreTypes")]
  [Serializable]
  public enum AccessRights
  {
    ReadAccess = 1,
    WriteAccess = 2,
    AppendAccess = 4,
    AppendToAccess = 8,
    CreateAccess = 16, // 0x00000010
    DeleteAccess = 32, // 0x00000020
    ShareAccess = 64, // 0x00000040
    AssignAccess = 128, // 0x00000080
  }
}
