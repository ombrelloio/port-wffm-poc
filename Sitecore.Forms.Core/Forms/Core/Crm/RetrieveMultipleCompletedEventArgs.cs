// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.RetrieveMultipleCompletedEventArgs
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
  public class RetrieveMultipleCompletedEventArgs : AsyncCompletedEventArgs
  {
    private object[] results;

    internal RetrieveMultipleCompletedEventArgs(
      object[] results,
      Exception exception,
      bool cancelled,
      object userState)
      : base(exception, cancelled, userState)
    {
      this.results = results;
    }

    public BusinessEntityCollection Result
    {
      get
      {
        this.RaiseExceptionIfNecessary();
        return (BusinessEntityCollection) this.results[0];
      }
    }
  }
}
