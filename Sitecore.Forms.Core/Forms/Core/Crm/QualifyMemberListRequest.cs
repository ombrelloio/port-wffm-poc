// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.QualifyMemberListRequest
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Sitecore.Forms.Core.Crm
{
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2007/WebServices")]
  [DebuggerNonUserCode]
  [Serializable]
  public class QualifyMemberListRequest : Request
  {
    private Guid listIdField;
    private Guid[] membersIdField;
    private bool overrideorRemoveField;

    public Guid ListId
    {
      get => this.listIdField;
      set => this.listIdField = value;
    }

    public Guid[] MembersId
    {
      get => this.membersIdField;
      set => this.membersIdField = value;
    }

    public bool OverrideorRemove
    {
      get => this.overrideorRemoveField;
      set => this.overrideorRemoveField = value;
    }
  }
}
