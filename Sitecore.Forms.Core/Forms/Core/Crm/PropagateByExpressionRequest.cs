// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.PropagateByExpressionRequest
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
  public class PropagateByExpressionRequest : Request
  {
    private QueryBase queryExpressionField;
    private string friendlyNameField;
    private bool executeImmediatelyField;
    private BusinessEntity activityField;
    private Guid templateIdField;
    private PropagationOwnershipOptions ownershipOptionsField;
    private bool postWorkflowEventField;
    private Moniker ownerField;
    private bool sendEmailField;

    public QueryBase QueryExpression
    {
      get => this.queryExpressionField;
      set => this.queryExpressionField = value;
    }

    public string FriendlyName
    {
      get => this.friendlyNameField;
      set => this.friendlyNameField = value;
    }

    public bool ExecuteImmediately
    {
      get => this.executeImmediatelyField;
      set => this.executeImmediatelyField = value;
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

    public bool PostWorkflowEvent
    {
      get => this.postWorkflowEventField;
      set => this.postWorkflowEventField = value;
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
