// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.WffmActionEvent
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Form.Core.Data;
using Sitecore.WFFM.Abstractions.Actions;
using System;
using System.Runtime.Serialization;

namespace Sitecore.Form.Core
{
  [DataContract]
  [KnownType(typeof (ActionDefinition))]
  public class WffmActionEvent
  {
    [DataMember]
    public ID FormID { get; set; }

    [DataMember]
    public ControlResult[] Fields { get; set; }

    [DataMember]
    public IActionDefinition[] Actions { get; set; }

    [DataMember]
    public Guid SessionIDGuid { get; set; }

    [DataMember]
    public string UserName { get; set; }

    [DataMember]
    public string Password { get; set; }

    [DataMember]
    public string CMInstanceName { get; set; }
  }
}
