// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Attributes.ListAdapterAttribute
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Client.Submit;
using Sitecore.Reflection;
using System;

namespace Sitecore.Form.Core.Attributes
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
  public class ListAdapterAttribute : Attribute
  {
    public ListAdapterAttribute(string propertyName, Type converter)
    {
      this.PropertyName = propertyName;
      this.Adapter = converter;
    }

    public Type Adapter { get; private set; }

    public string PropertyName { get; private set; }

    public IListAdapter GetInstance() => ReflectionUtil.CreateObject(this.Adapter) as IListAdapter;
  }
}
