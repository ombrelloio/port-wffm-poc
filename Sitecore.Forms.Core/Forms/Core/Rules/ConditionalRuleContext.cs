// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Rules.ConditionalRuleContext
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Rules;
using System;
using System.Runtime.Serialization;
using System.Web.UI;

namespace Sitecore.Forms.Core.Rules
{
  [Serializable]
  public class ConditionalRuleContext : RuleContext, ISerializable
  {
    public Control Control { get; private set; }

    public object Model { get; set; }

    public ConditionalRuleContext(Control control)
    {
      Assert.ArgumentNotNull((object) control, nameof (control));
      this.Control = control;
    }

    public ConditionalRuleContext(object model) => this.Model = model;

    protected ConditionalRuleContext(SerializationInfo info, StreamingContext context)
    {
      Assert.ArgumentNotNull((object) info, nameof (info));
      this.Control = info.GetValue("_control", typeof (Control)) as Control;
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      Assert.ArgumentNotNull((object) info, nameof (info));
      info.AddValue("_control", (object) this.Control, typeof (Control));
    }
  }
}
