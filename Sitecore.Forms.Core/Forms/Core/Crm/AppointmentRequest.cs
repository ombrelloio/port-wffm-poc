// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.AppointmentRequest
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
  public class AppointmentRequest
  {
    private Guid serviceIdField;
    private int anchorOffsetField;
    private int userTimeZoneCodeField;
    private int recurrenceDurationField;
    private int recurrenceTimeZoneCodeField;
    private Sitecore.Forms.Core.Crm.AppointmentsToIgnore[] appointmentsToIgnoreField;
    private RequiredResource[] requiredResourcesField;
    private CrmDateTime searchWindowStartField;
    private CrmDateTime searchWindowEndField;
    private CrmDateTime searchRecurrenceStartField;
    private string searchRecurrenceRuleField;
    private int durationField;
    private ConstraintRelation[] constraintsField;
    private ObjectiveRelation[] objectivesField;
    private SearchDirection directionField;
    private int numberOfResultsField;
    private Guid[] sitesField;

    public Guid ServiceId
    {
      get => this.serviceIdField;
      set => this.serviceIdField = value;
    }

    public int AnchorOffset
    {
      get => this.anchorOffsetField;
      set => this.anchorOffsetField = value;
    }

    public int UserTimeZoneCode
    {
      get => this.userTimeZoneCodeField;
      set => this.userTimeZoneCodeField = value;
    }

    public int RecurrenceDuration
    {
      get => this.recurrenceDurationField;
      set => this.recurrenceDurationField = value;
    }

    public int RecurrenceTimeZoneCode
    {
      get => this.recurrenceTimeZoneCodeField;
      set => this.recurrenceTimeZoneCodeField = value;
    }

    public Sitecore.Forms.Core.Crm.AppointmentsToIgnore[] AppointmentsToIgnore
    {
      get => this.appointmentsToIgnoreField;
      set => this.appointmentsToIgnoreField = value;
    }

    public RequiredResource[] RequiredResources
    {
      get => this.requiredResourcesField;
      set => this.requiredResourcesField = value;
    }

    public CrmDateTime SearchWindowStart
    {
      get => this.searchWindowStartField;
      set => this.searchWindowStartField = value;
    }

    public CrmDateTime SearchWindowEnd
    {
      get => this.searchWindowEndField;
      set => this.searchWindowEndField = value;
    }

    public CrmDateTime SearchRecurrenceStart
    {
      get => this.searchRecurrenceStartField;
      set => this.searchRecurrenceStartField = value;
    }

    public string SearchRecurrenceRule
    {
      get => this.searchRecurrenceRuleField;
      set => this.searchRecurrenceRuleField = value;
    }

    public int Duration
    {
      get => this.durationField;
      set => this.durationField = value;
    }

    public ConstraintRelation[] Constraints
    {
      get => this.constraintsField;
      set => this.constraintsField = value;
    }

    public ObjectiveRelation[] Objectives
    {
      get => this.objectivesField;
      set => this.objectivesField = value;
    }

    public SearchDirection Direction
    {
      get => this.directionField;
      set => this.directionField = value;
    }

    public int NumberOfResults
    {
      get => this.numberOfResultsField;
      set => this.numberOfResultsField = value;
    }

    public Guid[] Sites
    {
      get => this.sitesField;
      set => this.sitesField = value;
    }
  }
}
