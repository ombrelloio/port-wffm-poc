// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Metadata.CrmNullable
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Sitecore.Forms.Core.Crm.Metadata
{
  [XmlInclude(typeof (CrmOwnershipTypes))]
  [XmlInclude(typeof (CrmAttributeRequiredLevel))]
  [XmlInclude(typeof (CrmDisplayMasks))]
  [XmlInclude(typeof (CrmIntegerFormat))]
  [XmlInclude(typeof (CrmImeMode))]
  [XmlInclude(typeof (CrmStringFormat))]
  [XmlInclude(typeof (CrmDateTimeFormat))]
  [XmlInclude(typeof (CrmAssociatedMenuBehavior))]
  [XmlInclude(typeof (CrmAssociatedMenuGroup))]
  [XmlInclude(typeof (CrmCascadeType))]
  [XmlInclude(typeof (CrmPrivilegeType))]
  [XmlInclude(typeof (CrmAttributeType))]
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2007/WebServices")]
  [DebuggerNonUserCode]
  [Serializable]
  public abstract class CrmNullable
  {
    private bool isNullField;
    private bool isNullFieldSpecified;

    public bool IsNull
    {
      get => this.isNullField;
      set => this.isNullField = value;
    }

    [XmlIgnore]
    public bool IsNullSpecified
    {
      get => this.isNullFieldSpecified;
      set => this.isNullFieldSpecified = value;
    }
  }
}
