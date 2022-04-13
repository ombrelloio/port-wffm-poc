﻿// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.AppointmentProposal
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
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2006/Scheduling")]
  [DebuggerNonUserCode]
  [Serializable]
  public class AppointmentProposal
  {
    private CrmDateTime startField;
    private CrmDateTime endField;
    private Guid siteIdField;
    private string siteNameField;
    private ProposalParty[] proposalPartiesField;

    public CrmDateTime Start
    {
      get => this.startField;
      set => this.startField = value;
    }

    public CrmDateTime End
    {
      get => this.endField;
      set => this.endField = value;
    }

    public Guid SiteId
    {
      get => this.siteIdField;
      set => this.siteIdField = value;
    }

    public string SiteName
    {
      get => this.siteNameField;
      set => this.siteNameField = value;
    }

    public ProposalParty[] ProposalParties
    {
      get => this.proposalPartiesField;
      set => this.proposalPartiesField = value;
    }
  }
}
