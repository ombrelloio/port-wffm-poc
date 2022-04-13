// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Property
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
  [XmlInclude(typeof (UniqueIdentifierProperty))]
  [XmlInclude(typeof (StringProperty))]
  [XmlInclude(typeof (StatusProperty))]
  [XmlInclude(typeof (StateProperty))]
  [XmlInclude(typeof (PicklistProperty))]
  [XmlInclude(typeof (OwnerProperty))]
  [XmlInclude(typeof (LookupProperty))]
  [XmlInclude(typeof (KeyProperty))]
  [XmlInclude(typeof (EntityNameReferenceProperty))]
  [XmlInclude(typeof (DynamicEntityArrayProperty))]
  [XmlInclude(typeof (CustomerProperty))]
  [XmlInclude(typeof (CrmNumberProperty))]
  [XmlInclude(typeof (CrmMoneyProperty))]
  [XmlInclude(typeof (CrmFloatProperty))]
  [XmlInclude(typeof (CrmDecimalProperty))]
  [XmlInclude(typeof (CrmDateTimeProperty))]
  [XmlInclude(typeof (CrmBooleanProperty))]
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [XmlType(Namespace = "http://schemas.microsoft.com/crm/2006/WebServices")]
  [DebuggerNonUserCode]
  [Serializable]
  public abstract class Property
  {
    private string nameField;

    [XmlAttribute]
    public string Name
    {
      get => this.nameField;
      set => this.nameField = value;
    }
  }
}
