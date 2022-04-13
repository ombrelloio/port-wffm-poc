// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.TimeInfo
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
  public class TimeInfo
  {
    private CrmDateTime startField;
    private CrmDateTime endField;
    private TimeCode timeCodeField;
    private SubCode subCodeField;
    private Guid sourceIdField;
    private Guid calendarIdField;
    private int sourceTypeCodeField;
    private bool isActivityField;
    private int activityStatusCodeField;
    private double effortField;
    private string displayTextField;

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

    public TimeCode TimeCode
    {
      get => this.timeCodeField;
      set => this.timeCodeField = value;
    }

    public SubCode SubCode
    {
      get => this.subCodeField;
      set => this.subCodeField = value;
    }

    public Guid SourceId
    {
      get => this.sourceIdField;
      set => this.sourceIdField = value;
    }

    public Guid CalendarId
    {
      get => this.calendarIdField;
      set => this.calendarIdField = value;
    }

    public int SourceTypeCode
    {
      get => this.sourceTypeCodeField;
      set => this.sourceTypeCodeField = value;
    }

    public bool IsActivity
    {
      get => this.isActivityField;
      set => this.isActivityField = value;
    }

    public int ActivityStatusCode
    {
      get => this.activityStatusCodeField;
      set => this.activityStatusCodeField = value;
    }

    public double Effort
    {
      get => this.effortField;
      set => this.effortField = value;
    }

    public string DisplayText
    {
      get => this.displayTextField;
      set => this.displayTextField = value;
    }
  }
}
