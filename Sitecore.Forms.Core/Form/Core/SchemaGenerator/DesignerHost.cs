// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.SchemaGenerator.DesignerHost
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Sitecore.Form.Core.SchemaGenerator
{
  public class DesignerHost : IDesignerHost, IServiceContainer, IServiceProvider
  {
    public event EventHandler Activated;

    private void InvokeActivated(EventArgs e)
    {
      EventHandler activated = this.Activated;
      if (activated == null)
        return;
      activated((object) this, e);
    }

    public event EventHandler Deactivated;

    private void InvokeDeactivated(EventArgs e)
    {
      EventHandler deactivated = this.Deactivated;
      if (deactivated == null)
        return;
      deactivated((object) this, e);
    }

    public event EventHandler LoadComplete;

    private void InvokeLoadComplete(EventArgs e)
    {
      EventHandler loadComplete = this.LoadComplete;
      if (loadComplete == null)
        return;
      loadComplete((object) this, e);
    }

    public event DesignerTransactionCloseEventHandler TransactionClosed;

    private void InvokeTransactionClosed(DesignerTransactionCloseEventArgs e)
    {
      DesignerTransactionCloseEventHandler transactionClosed = this.TransactionClosed;
      if (transactionClosed == null)
        return;
      transactionClosed((object) this, e);
    }

    public event DesignerTransactionCloseEventHandler TransactionClosing;

    private void InvokeTransactionClosing(DesignerTransactionCloseEventArgs e)
    {
      DesignerTransactionCloseEventHandler transactionClosing = this.TransactionClosing;
      if (transactionClosing == null)
        return;
      transactionClosing((object) this, e);
    }

    public event EventHandler TransactionOpened;

    private void InvokeTransactionOpened(EventArgs e)
    {
      EventHandler transactionOpened = this.TransactionOpened;
      if (transactionOpened == null)
        return;
      transactionOpened((object) this, e);
    }

    public event EventHandler TransactionOpening;

    private void InvokeTransactionOpening(EventArgs e)
    {
      EventHandler transactionOpening = this.TransactionOpening;
      if (transactionOpening == null)
        return;
      transactionOpening((object) this, e);
    }

    public void Activate() => throw new NotImplementedException();

    public IComponent CreateComponent(Type componentClass) => throw new NotImplementedException();

    public IComponent CreateComponent(Type componentClass, string name) => throw new NotImplementedException();

    public DesignerTransaction CreateTransaction() => throw new NotImplementedException();

    public DesignerTransaction CreateTransaction(string description) => throw new NotImplementedException();

    public void DestroyComponent(IComponent component) => throw new NotImplementedException();

    public IDesigner GetDesigner(IComponent component) => throw new NotImplementedException();

    public Type GetType(string typeName) => throw new NotImplementedException();

    public bool Loading => throw new NotImplementedException();

    public bool InTransaction => throw new NotImplementedException();

    public IContainer Container => throw new NotImplementedException();

    public IComponent RootComponent => throw new NotImplementedException();

    public string RootComponentClassName => throw new NotImplementedException();

    public string TransactionDescription => throw new NotImplementedException();

    public void AddService(Type serviceType, object serviceInstance) => throw new NotImplementedException();

    public void AddService(Type serviceType, object serviceInstance, bool promote) => throw new NotImplementedException();

    public void AddService(Type serviceType, ServiceCreatorCallback callback) => throw new NotImplementedException();

    public void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote) => throw new NotImplementedException();

    public void RemoveService(Type serviceType) => throw new NotImplementedException();

    public void RemoveService(Type serviceType, bool promote) => throw new NotImplementedException();

    object IServiceProvider.GetService(Type serviceType) => (object) null;
  }
}
