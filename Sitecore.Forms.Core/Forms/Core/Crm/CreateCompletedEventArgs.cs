// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.CreateCompletedEventArgs
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

namespace Sitecore.Forms.Core.Crm
{
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [DebuggerNonUserCode]
  public class CreateCompletedEventArgs : AsyncCompletedEventArgs
  {
    private object[] results;

    internal CreateCompletedEventArgs(
      object[] results,
      Exception exception,
      bool cancelled,
      object userState)
      : base(exception, cancelled, userState)
    {
      this.results = results;
    }

    public Guid Result
    {
      get
      {
        this.RaiseExceptionIfNecessary();
        return (Guid) this.results[0];
      }
    }
  }
}
