// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.DistributeCampaignActivityRequest
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
  public class DistributeCampaignActivityRequest : Request
  {
    private Guid campaignActivityIdField;
    private bool propagateField;
    private BusinessEntity activityField;
    private Guid templateIdField;
    private PropagationOwnershipOptions ownershipOptionsField;
    private Moniker ownerField;
    private bool sendEmailField;

    public Guid CampaignActivityId
    {
      get => this.campaignActivityIdField;
      set => this.campaignActivityIdField = value;
    }

    public bool Propagate
    {
      get => this.propagateField;
      set => this.propagateField = value;
    }

    public BusinessEntity Activity
    {
      get => this.activityField;
      set => this.activityField = value;
    }

    public Guid TemplateId
    {
      get => this.templateIdField;
      set => this.templateIdField = value;
    }

    public PropagationOwnershipOptions OwnershipOptions
    {
      get => this.ownershipOptionsField;
      set => this.ownershipOptionsField = value;
    }

    public Moniker Owner
    {
      get => this.ownerField;
      set => this.ownerField = value;
    }

    public bool SendEmail
    {
      get => this.sendEmailField;
      set => this.sendEmailField = value;
    }
  }
}
