// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Discovery.ClientPatchInfo
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Sitecore.Forms.Core.Crm.Discovery
{
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2007/CrmDiscoveryService")]
  [DebuggerNonUserCode]
  [Serializable]
  public class ClientPatchInfo
  {
    private int depthField;
    private string descriptionField;
    private bool isMandatoryField;
    private string linkIdField;
    private Guid patchIdField;
    private string titleField;

    public int Depth
    {
      get => this.depthField;
      set => this.depthField = value;
    }

    public string Description
    {
      get => this.descriptionField;
      set => this.descriptionField = value;
    }

    public bool IsMandatory
    {
      get => this.isMandatoryField;
      set => this.isMandatoryField = value;
    }

    public string LinkId
    {
      get => this.linkIdField;
      set => this.linkIdField = value;
    }

    public Guid PatchId
    {
      get => this.patchIdField;
      set => this.patchIdField = value;
    }

    public string Title
    {
      get => this.titleField;
      set => this.titleField = value;
    }
  }
}
