// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.ErrorInfo
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
  public class ErrorInfo
  {
    private ResourceInfo[] resourceListField;
    private string errorCodeField;

    public ResourceInfo[] ResourceList
    {
      get => this.resourceListField;
      set => this.resourceListField = value;
    }

    public string ErrorCode
    {
      get => this.errorCodeField;
      set => this.errorCodeField = value;
    }
  }
}
