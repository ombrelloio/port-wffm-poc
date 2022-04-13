// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.ConstraintRelation
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
  public class ConstraintRelation
  {
    private Guid objectIdField;
    private string constraintTypeField;
    private string constraintsField;

    public Guid ObjectId
    {
      get => this.objectIdField;
      set => this.objectIdField = value;
    }

    public string ConstraintType
    {
      get => this.constraintTypeField;
      set => this.constraintTypeField = value;
    }

    public string Constraints
    {
      get => this.constraintsField;
      set => this.constraintsField = value;
    }
  }
}
