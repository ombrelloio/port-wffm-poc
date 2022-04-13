// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Metadata.SecurityPrivilegeMetadata
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
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2007/WebServices")]
  [DebuggerNonUserCode]
  [Serializable]
  public class SecurityPrivilegeMetadata
  {
    private bool canBeBasicField;
    private bool canBeDeepField;
    private bool canBeGlobalField;
    private bool canBeLocalField;
    private string nameField;
    private Key privilegeIdField;
    private CrmPrivilegeType privilegeTypeField;

    public bool CanBeBasic
    {
      get => this.canBeBasicField;
      set => this.canBeBasicField = value;
    }

    public bool CanBeDeep
    {
      get => this.canBeDeepField;
      set => this.canBeDeepField = value;
    }

    public bool CanBeGlobal
    {
      get => this.canBeGlobalField;
      set => this.canBeGlobalField = value;
    }

    public bool CanBeLocal
    {
      get => this.canBeLocalField;
      set => this.canBeLocalField = value;
    }

    public string Name
    {
      get => this.nameField;
      set => this.nameField = value;
    }

    public Key PrivilegeId
    {
      get => this.privilegeIdField;
      set => this.privilegeIdField = value;
    }

    public CrmPrivilegeType PrivilegeType
    {
      get => this.privilegeTypeField;
      set => this.privilegeTypeField = value;
    }
  }
}
