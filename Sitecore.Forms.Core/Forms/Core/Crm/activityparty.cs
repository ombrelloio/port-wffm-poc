// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.activityparty
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
  public class activityparty : BusinessEntity
  {
    private Lookup activityidField;
    private Key activitypartyidField;
    private string addressusedField;
    private CrmBoolean donotemailField;
    private CrmBoolean donotfaxField;
    private CrmBoolean donotphoneField;
    private CrmBoolean donotpostalmailField;
    private CrmFloat effortField;
    private string exchangeentryidField;
    private Picklist participationtypemaskField;
    private Lookup partyidField;
    private Lookup resourcespecidField;
    private CrmDateTime scheduledendField;
    private CrmDateTime scheduledstartField;

    public Lookup activityid
    {
      get => this.activityidField;
      set => this.activityidField = value;
    }

    public Key activitypartyid
    {
      get => this.activitypartyidField;
      set => this.activitypartyidField = value;
    }

    public string addressused
    {
      get => this.addressusedField;
      set => this.addressusedField = value;
    }

    public CrmBoolean donotemail
    {
      get => this.donotemailField;
      set => this.donotemailField = value;
    }

    public CrmBoolean donotfax
    {
      get => this.donotfaxField;
      set => this.donotfaxField = value;
    }

    public CrmBoolean donotphone
    {
      get => this.donotphoneField;
      set => this.donotphoneField = value;
    }

    public CrmBoolean donotpostalmail
    {
      get => this.donotpostalmailField;
      set => this.donotpostalmailField = value;
    }

    public CrmFloat effort
    {
      get => this.effortField;
      set => this.effortField = value;
    }

    public string exchangeentryid
    {
      get => this.exchangeentryidField;
      set => this.exchangeentryidField = value;
    }

    public Picklist participationtypemask
    {
      get => this.participationtypemaskField;
      set => this.participationtypemaskField = value;
    }

    public Lookup partyid
    {
      get => this.partyidField;
      set => this.partyidField = value;
    }

    public Lookup resourcespecid
    {
      get => this.resourcespecidField;
      set => this.resourcespecidField = value;
    }

    public CrmDateTime scheduledend
    {
      get => this.scheduledendField;
      set => this.scheduledendField = value;
    }

    public CrmDateTime scheduledstart
    {
      get => this.scheduledstartField;
      set => this.scheduledstartField = value;
    }
  }
}
