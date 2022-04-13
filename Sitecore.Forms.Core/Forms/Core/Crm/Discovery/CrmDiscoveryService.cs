// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Discovery.CrmDiscoveryService
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Sitecore.Forms.Core.Crm.Discovery
{
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [WebServiceBinding(Name = "CrmDiscoveryServiceSoap", Namespace = "http://schemas.microsoft.com/crm/2007/CrmDiscoveryService")]
  [DebuggerNonUserCode]
  public class CrmDiscoveryService : SoapHttpClientProtocol
  {
    private SendOrPostCallback ExecuteOperationCompleted;

    public event ExecuteCompletedEventHandler ExecuteCompleted;

    public CrmDiscoveryService() => this.Url = "http://sitecore.test.dk.sitecore.net/MSCrmServices/2007/SPLA/CrmDiscoveryService.asmx";

    public IAsyncResult BeginExecute(
      Request Request,
      AsyncCallback callback,
      object asyncState)
    {
      return this.BeginInvoke("Execute", new object[1]
      {
        (object) Request
      }, callback, asyncState);
    }

    public new void CancelAsync(object userState) => base.CancelAsync(userState);

    public Response EndExecute(IAsyncResult asyncResult) => (Response) this.EndInvoke(asyncResult)[0];

    [SoapDocumentMethod("http://schemas.microsoft.com/crm/2007/CrmDiscoveryService/Execute", ParameterStyle = SoapParameterStyle.Wrapped, RequestNamespace = "http://schemas.microsoft.com/crm/2007/CrmDiscoveryService", ResponseNamespace = "http://schemas.microsoft.com/crm/2007/CrmDiscoveryService", Use = SoapBindingUse.Literal)]
    [return: XmlElement("Response")]
    public Response Execute(Request Request) => (Response) this.Invoke(nameof (Execute), new object[1]
    {
      (object) Request
    })[0];

    public void ExecuteAsync(Request Request) => this.ExecuteAsync(Request, (object) null);

    public void ExecuteAsync(Request Request, object userState)
    {
      if (this.ExecuteOperationCompleted == null)
        this.ExecuteOperationCompleted = new SendOrPostCallback(this.OnExecuteOperationCompleted);
      this.InvokeAsync("Execute", new object[1]
      {
        (object) Request
      }, this.ExecuteOperationCompleted, userState);
    }

    private void OnExecuteOperationCompleted(object arg)
    {
      if (this.ExecuteCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.ExecuteCompleted((object) this, new ExecuteCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }
  }
}
