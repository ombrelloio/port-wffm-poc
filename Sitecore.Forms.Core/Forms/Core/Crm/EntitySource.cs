// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.EntitySource
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Sitecore.Forms.Core.Crm
{
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2007/CoreTypes")]
  [Serializable]
  public enum EntitySource
  {
    Account,
    Contact,
    Lead,
    All,
  }
}
