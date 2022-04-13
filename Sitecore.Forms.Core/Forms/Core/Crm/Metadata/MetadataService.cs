// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.Metadata.MetadataService
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

namespace Sitecore.Forms.Core.Crm.Metadata
{
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [WebServiceBinding(Name = "MetadataServiceSoap", Namespace = "http://schemas.microsoft.com/crm/2007/WebServices")]
  [XmlInclude(typeof (CrmNullable))]
  public class MetadataService : SoapHttpClientProtocol
  {
    private CallerOriginToken callerOriginTokenValueField;
    private CrmAuthenticationToken crmAuthenticationTokenValueField;
    private SendOrPostCallback ExecuteOperationCompleted;

    public event ExecuteCompletedEventHandler ExecuteCompleted;

    public MetadataService() => this.Url = "http://sitecore.crm.dk.sitecore.net/MSCrmServices/2007/MetadataService.asmx";

    public CallerOriginToken CallerOriginTokenValue
    {
      get => this.callerOriginTokenValueField;
      set => this.callerOriginTokenValueField = value;
    }

    public CrmAuthenticationToken CrmAuthenticationTokenValue
    {
      get => this.crmAuthenticationTokenValueField;
      set => this.crmAuthenticationTokenValueField = value;
    }

    public IAsyncResult BeginExecute(
      MetadataServiceRequest Request,
      AsyncCallback callback,
      object asyncState)
    {
      return this.BeginInvoke("Execute", new object[1]
      {
        (object) Request
      }, callback, asyncState);
    }

    public new void CancelAsync(object userState) => base.CancelAsync(userState);

    public MetadataServiceResponse EndExecute(IAsyncResult asyncResult) => (MetadataServiceResponse) this.EndInvoke(asyncResult)[0];

    [SoapHeader("CallerOriginTokenValue")]
    [SoapHeader("CrmAuthenticationTokenValue")]
    [SoapDocumentMethod("http://schemas.microsoft.com/crm/2007/WebServices/Execute", ParameterStyle = SoapParameterStyle.Wrapped, RequestNamespace = "http://schemas.microsoft.com/crm/2007/WebServices", ResponseNamespace = "http://schemas.microsoft.com/crm/2007/WebServices", Use = SoapBindingUse.Literal)]
    [return: XmlElement("Response")]
    public MetadataServiceResponse Execute(MetadataServiceRequest Request) => (MetadataServiceResponse) this.Invoke(nameof (Execute), new object[1]
    {
      (object) Request
    })[0];

    public void ExecuteAsync(MetadataServiceRequest Request) => this.ExecuteAsync(Request, (object) null);

    public void ExecuteAsync(MetadataServiceRequest Request, object userState)
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
