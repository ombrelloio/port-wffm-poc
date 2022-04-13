// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Attributes.AdapterAttribute
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Client.Submit;
using Sitecore.Reflection;
using System;

namespace Sitecore.Form.Core.Attributes
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
  public class AdapterAttribute : Attribute
  {
    public AdapterAttribute(Type adapterType) => this.AdapterType = adapterType;

    public Type AdapterType { get; set; }

    public Adapter GetInstance() => ReflectionUtil.CreateObject(this.AdapterType) as Adapter;
  }
}
