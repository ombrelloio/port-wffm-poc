// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Crm.CrmService
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

namespace Sitecore.Forms.Core.Crm
{
  [GeneratedCode("wsdl", "2.0.50727.3038")]
  [DebuggerStepThrough]
  [DesignerCategory("code")]
  [WebServiceBinding(Name = "CrmServiceSoap", Namespace = "http://schemas.microsoft.com/crm/2007/WebServices")]
  [XmlInclude(typeof (CrmReference))]
  [XmlInclude(typeof (Property[]))]
  [XmlInclude(typeof (activityparty[]))]
  public class CrmService : SoapHttpClientProtocol
  {
    private CrmAuthenticationToken crmAuthenticationTokenValueField;
    private CallerOriginToken callerOriginTokenValueField;
    private CorrelationToken correlationTokenValueField;
    private SendOrPostCallback ExecuteOperationCompleted;
    private SendOrPostCallback FetchOperationCompleted;
    private SendOrPostCallback CreateOperationCompleted;
    private SendOrPostCallback DeleteOperationCompleted;
    private SendOrPostCallback RetrieveOperationCompleted;
    private SendOrPostCallback RetrieveMultipleOperationCompleted;
    private SendOrPostCallback UpdateOperationCompleted;

    public CrmAuthenticationToken CrmAuthenticationTokenValue
    {
      get => this.crmAuthenticationTokenValueField;
      set => this.crmAuthenticationTokenValueField = value;
    }

    public CallerOriginToken CallerOriginTokenValue
    {
      get => this.callerOriginTokenValueField;
      set => this.callerOriginTokenValueField = value;
    }

    public CorrelationToken CorrelationTokenValue
    {
      get => this.correlationTokenValueField;
      set => this.correlationTokenValueField = value;
    }

    public event ExecuteCompletedEventHandler ExecuteCompleted;

    public event FetchCompletedEventHandler FetchCompleted;

    public event CreateCompletedEventHandler CreateCompleted;

    public event DeleteCompletedEventHandler DeleteCompleted;

    public event RetrieveCompletedEventHandler RetrieveCompleted;

    public event RetrieveMultipleCompletedEventHandler RetrieveMultipleCompleted;

    public event UpdateCompletedEventHandler UpdateCompleted;

    [SoapHeader("CorrelationTokenValue")]
    [SoapHeader("CallerOriginTokenValue")]
    [SoapHeader("CrmAuthenticationTokenValue")]
    [SoapDocumentMethod("http://schemas.microsoft.com/crm/2007/WebServices/Execute", ParameterStyle = SoapParameterStyle.Wrapped, RequestNamespace = "http://schemas.microsoft.com/crm/2007/WebServices", ResponseNamespace = "http://schemas.microsoft.com/crm/2007/WebServices", Use = SoapBindingUse.Literal)]
    [return: XmlElement("Response")]
    public Response Execute(Request Request) => (Response) this.Invoke(nameof (Execute), new object[1]
    {
      (object) Request
    })[0];

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

    public Response EndExecute(IAsyncResult asyncResult) => (Response) this.EndInvoke(asyncResult)[0];

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

    [SoapHeader("CorrelationTokenValue")]
    [SoapHeader("CallerOriginTokenValue")]
    [SoapHeader("CrmAuthenticationTokenValue")]
    [SoapDocumentMethod("http://schemas.microsoft.com/crm/2007/WebServices/Fetch", ParameterStyle = SoapParameterStyle.Wrapped, RequestNamespace = "http://schemas.microsoft.com/crm/2007/WebServices", ResponseNamespace = "http://schemas.microsoft.com/crm/2007/WebServices", Use = SoapBindingUse.Literal)]
    public string Fetch(string fetchXml) => (string) this.Invoke(nameof (Fetch), new object[1]
    {
      (object) fetchXml
    })[0];

    public IAsyncResult BeginFetch(
      string fetchXml,
      AsyncCallback callback,
      object asyncState)
    {
      return this.BeginInvoke("Fetch", new object[1]
      {
        (object) fetchXml
      }, callback, asyncState);
    }

    public string EndFetch(IAsyncResult asyncResult) => (string) this.EndInvoke(asyncResult)[0];

    public void FetchAsync(string fetchXml) => this.FetchAsync(fetchXml, (object) null);

    public void FetchAsync(string fetchXml, object userState)
    {
      if (this.FetchOperationCompleted == null)
        this.FetchOperationCompleted = new SendOrPostCallback(this.OnFetchOperationCompleted);
      this.InvokeAsync("Fetch", new object[1]
      {
        (object) fetchXml
      }, this.FetchOperationCompleted, userState);
    }

    private void OnFetchOperationCompleted(object arg)
    {
      if (this.FetchCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.FetchCompleted((object) this, new FetchCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    [SoapHeader("CorrelationTokenValue")]
    [SoapHeader("CallerOriginTokenValue")]
    [SoapHeader("CrmAuthenticationTokenValue")]
    [SoapDocumentMethod("http://schemas.microsoft.com/crm/2007/WebServices/Create", ParameterStyle = SoapParameterStyle.Wrapped, RequestNamespace = "http://schemas.microsoft.com/crm/2007/WebServices", ResponseNamespace = "http://schemas.microsoft.com/crm/2007/WebServices", Use = SoapBindingUse.Literal)]
    public Guid Create(BusinessEntity entity) => (Guid) this.Invoke(nameof (Create), new object[1]
    {
      (object) entity
    })[0];

    public IAsyncResult BeginCreate(
      BusinessEntity entity,
      AsyncCallback callback,
      object asyncState)
    {
      return this.BeginInvoke("Create", new object[1]
      {
        (object) entity
      }, callback, asyncState);
    }

    public Guid EndCreate(IAsyncResult asyncResult) => (Guid) this.EndInvoke(asyncResult)[0];

    public void CreateAsync(BusinessEntity entity) => this.CreateAsync(entity, (object) null);

    public void CreateAsync(BusinessEntity entity, object userState)
    {
      if (this.CreateOperationCompleted == null)
        this.CreateOperationCompleted = new SendOrPostCallback(this.OnCreateOperationCompleted);
      this.InvokeAsync("Create", new object[1]
      {
        (object) entity
      }, this.CreateOperationCompleted, userState);
    }

    private void OnCreateOperationCompleted(object arg)
    {
      if (this.CreateCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.CreateCompleted((object) this, new CreateCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    [SoapHeader("CorrelationTokenValue")]
    [SoapHeader("CallerOriginTokenValue")]
    [SoapHeader("CrmAuthenticationTokenValue")]
    [SoapDocumentMethod("http://schemas.microsoft.com/crm/2007/WebServices/Delete", ParameterStyle = SoapParameterStyle.Wrapped, RequestNamespace = "http://schemas.microsoft.com/crm/2007/WebServices", ResponseNamespace = "http://schemas.microsoft.com/crm/2007/WebServices", Use = SoapBindingUse.Literal)]
    public void Delete(string entityName, Guid id) => this.Invoke(nameof (Delete), new object[2]
    {
      (object) entityName,
      (object) id
    });

    public IAsyncResult BeginDelete(
      string entityName,
      Guid id,
      AsyncCallback callback,
      object asyncState)
    {
      return this.BeginInvoke("Delete", new object[2]
      {
        (object) entityName,
        (object) id
      }, callback, asyncState);
    }

    public void EndDelete(IAsyncResult asyncResult) => this.EndInvoke(asyncResult);

    public void DeleteAsync(string entityName, Guid id) => this.DeleteAsync(entityName, id, (object) null);

    public void DeleteAsync(string entityName, Guid id, object userState)
    {
      if (this.DeleteOperationCompleted == null)
        this.DeleteOperationCompleted = new SendOrPostCallback(this.OnDeleteOperationCompleted);
      this.InvokeAsync("Delete", new object[2]
      {
        (object) entityName,
        (object) id
      }, this.DeleteOperationCompleted, userState);
    }

    private void OnDeleteOperationCompleted(object arg)
    {
      if (this.DeleteCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.DeleteCompleted((object) this, new AsyncCompletedEventArgs(completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    [SoapHeader("CorrelationTokenValue")]
    [SoapHeader("CallerOriginTokenValue")]
    [SoapHeader("CrmAuthenticationTokenValue")]
    [SoapDocumentMethod("http://schemas.microsoft.com/crm/2007/WebServices/Retrieve", ParameterStyle = SoapParameterStyle.Wrapped, RequestNamespace = "http://schemas.microsoft.com/crm/2007/WebServices", ResponseNamespace = "http://schemas.microsoft.com/crm/2007/WebServices", Use = SoapBindingUse.Literal)]
    public BusinessEntity Retrieve(
      string entityName,
      Guid id,
      ColumnSetBase columnSet)
    {
      return (BusinessEntity) this.Invoke(nameof (Retrieve), new object[3]
      {
        (object) entityName,
        (object) id,
        (object) columnSet
      })[0];
    }

    public IAsyncResult BeginRetrieve(
      string entityName,
      Guid id,
      ColumnSetBase columnSet,
      AsyncCallback callback,
      object asyncState)
    {
      return this.BeginInvoke("Retrieve", new object[3]
      {
        (object) entityName,
        (object) id,
        (object) columnSet
      }, callback, asyncState);
    }

    public BusinessEntity EndRetrieve(IAsyncResult asyncResult) => (BusinessEntity) this.EndInvoke(asyncResult)[0];

    public void RetrieveAsync(string entityName, Guid id, ColumnSetBase columnSet) => this.RetrieveAsync(entityName, id, columnSet, (object) null);

    public void RetrieveAsync(
      string entityName,
      Guid id,
      ColumnSetBase columnSet,
      object userState)
    {
      if (this.RetrieveOperationCompleted == null)
        this.RetrieveOperationCompleted = new SendOrPostCallback(this.OnRetrieveOperationCompleted);
      this.InvokeAsync("Retrieve", new object[3]
      {
        (object) entityName,
        (object) id,
        (object) columnSet
      }, this.RetrieveOperationCompleted, userState);
    }

    private void OnRetrieveOperationCompleted(object arg)
    {
      if (this.RetrieveCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.RetrieveCompleted((object) this, new RetrieveCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    [SoapHeader("CorrelationTokenValue")]
    [SoapHeader("CallerOriginTokenValue")]
    [SoapHeader("CrmAuthenticationTokenValue")]
    [SoapDocumentMethod("http://schemas.microsoft.com/crm/2007/WebServices/RetrieveMultiple", ParameterStyle = SoapParameterStyle.Wrapped, RequestNamespace = "http://schemas.microsoft.com/crm/2007/WebServices", ResponseNamespace = "http://schemas.microsoft.com/crm/2007/WebServices", Use = SoapBindingUse.Literal)]
    public BusinessEntityCollection RetrieveMultiple(QueryBase query) => (BusinessEntityCollection) this.Invoke(nameof (RetrieveMultiple), new object[1]
    {
      (object) query
    })[0];

    public IAsyncResult BeginRetrieveMultiple(
      QueryBase query,
      AsyncCallback callback,
      object asyncState)
    {
      return this.BeginInvoke("RetrieveMultiple", new object[1]
      {
        (object) query
      }, callback, asyncState);
    }

    public BusinessEntityCollection EndRetrieveMultiple(
      IAsyncResult asyncResult)
    {
      return (BusinessEntityCollection) this.EndInvoke(asyncResult)[0];
    }

    public void RetrieveMultipleAsync(QueryBase query) => this.RetrieveMultipleAsync(query, (object) null);

    public void RetrieveMultipleAsync(QueryBase query, object userState)
    {
      if (this.RetrieveMultipleOperationCompleted == null)
        this.RetrieveMultipleOperationCompleted = new SendOrPostCallback(this.OnRetrieveMultipleOperationCompleted);
      this.InvokeAsync("RetrieveMultiple", new object[1]
      {
        (object) query
      }, this.RetrieveMultipleOperationCompleted, userState);
    }

    private void OnRetrieveMultipleOperationCompleted(object arg)
    {
      if (this.RetrieveMultipleCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.RetrieveMultipleCompleted((object) this, new RetrieveMultipleCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    [SoapHeader("CorrelationTokenValue")]
    [SoapHeader("CallerOriginTokenValue")]
    [SoapHeader("CrmAuthenticationTokenValue")]
    [SoapDocumentMethod("http://schemas.microsoft.com/crm/2007/WebServices/Update", ParameterStyle = SoapParameterStyle.Wrapped, RequestNamespace = "http://schemas.microsoft.com/crm/2007/WebServices", ResponseNamespace = "http://schemas.microsoft.com/crm/2007/WebServices", Use = SoapBindingUse.Literal)]
    public void Update(BusinessEntity entity) => this.Invoke(nameof (Update), new object[1]
    {
      (object) entity
    });

    public IAsyncResult BeginUpdate(
      BusinessEntity entity,
      AsyncCallback callback,
      object asyncState)
    {
      return this.BeginInvoke("Update", new object[1]
      {
        (object) entity
      }, callback, asyncState);
    }

    public void EndUpdate(IAsyncResult asyncResult) => this.EndInvoke(asyncResult);

    public void UpdateAsync(BusinessEntity entity) => this.UpdateAsync(entity, (object) null);

    public void UpdateAsync(BusinessEntity entity, object userState)
    {
      if (this.UpdateOperationCompleted == null)
        this.UpdateOperationCompleted = new SendOrPostCallback(this.OnUpdateOperationCompleted);
      this.InvokeAsync("Update", new object[1]
      {
        (object) entity
      }, this.UpdateOperationCompleted, userState);
    }

    private void OnUpdateOperationCompleted(object arg)
    {
      if (this.UpdateCompleted == null)
        return;
      InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
      this.UpdateCompleted((object) this, new AsyncCompletedEventArgs(completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
    }

    public new void CancelAsync(object userState) => base.CancelAsync(userState);
  }
}
