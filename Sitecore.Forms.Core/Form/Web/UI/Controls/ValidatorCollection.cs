// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.ValidatorCollection
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using System;
using System.Collections;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;

namespace Sitecore.Form.Web.UI.Controls
{
  [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
  public sealed class ValidatorCollection : IList, ICollection, IEnumerable
  {
    private ArrayList listValidators = new ArrayList();

    public void Clear() => this.listValidators.Clear();

    public void CopyTo(Array array, int index) => this.listValidators.CopyTo(array, index);

    public IEnumerator GetEnumerator() => this.listValidators.GetEnumerator();

    public void RemoveAt(int index) => this.listValidators.RemoveAt(index);

    int IList.Add(object item) => this.listValidators.Add((object) (IValidator) item);

    bool IList.Contains(object item) => this.Contains((IValidator) item);

    int IList.IndexOf(object item) => this.IndexOf((IValidator) item);

    void IList.Insert(int index, object item) => this.Insert(index, (IValidator) item);

    void IList.Remove(object item) => this.Remove((IValidator) item);

    public void Add(IValidator validator) => this.listValidators.Add((object) validator);

    public void AddRange(IValidator[] validators)
    {
      Assert.ArgumentNotNull((object) validators, nameof (validators));
      foreach (IValidator validator in validators)
        this.Add(validator);
    }

    public bool Contains(IValidator validator) => this.listValidators.Contains((object) validator);

    public int IndexOf(IValidator validator) => this.listValidators.IndexOf((object) validator);

    public void Insert(int index, IValidator validator) => this.listValidators.Insert(index, (object) validator);

    public void Remove(IValidator validator)
    {
      int index = this.IndexOf(validator);
      if (index < 0)
        return;
      this.RemoveAt(index);
    }

    public int Capacity
    {
      get => this.listValidators.Capacity;
      set => this.listValidators.Capacity = value;
    }

    public IValidator this[int index] => (IValidator) this.listValidators[index];

    public int Count => this.listValidators.Count;

    public bool IsReadOnly => this.listValidators.IsReadOnly;

    public bool IsSynchronized => this.listValidators.IsSynchronized;

    public object SyncRoot => (object) this;

    bool IList.IsFixedSize => false;

    object IList.this[int index]
    {
      get => this.listValidators[index];
      set => this.listValidators[index] = (object) (IValidator) value;
    }
  }
}
