// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.SchemaGenerator.EventInfoInstance
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.ComponentModel;
using System.Reflection;

namespace Sitecore.Form.Core.SchemaGenerator
{
  internal class EventInfoInstance
  {
    private Type _baseType;
    protected EventInfo _eventInfo;
    protected string _name;
    protected bool browsable;
    protected string _category;

    public EventInfoInstance(EventInfo ei)
    {
      this.Init(ei, (Type) null);
      BrowsableAttribute customAttribute1 = (BrowsableAttribute) DesignAttributesUtil.GetCustomAttribute((MemberInfo) ei, typeof (BrowsableAttribute));
      this.browsable = customAttribute1 == null || customAttribute1.Browsable;
      CategoryAttribute customAttribute2 = (CategoryAttribute) DesignAttributesUtil.GetCustomAttribute((MemberInfo) ei, typeof (CategoryAttribute));
      if (customAttribute2 == null)
        return;
      this._category = customAttribute2.Category;
    }

    protected virtual void Init(EventInfo eventInfo, Type baseType)
    {
      this._eventInfo = eventInfo;
      this._name = "On" + eventInfo.Name;
      this._baseType = baseType;
    }

    public Type baseType => this._baseType;

    public EventInfo EventInfo => this._eventInfo;

    public string Name => this._name;

    public bool Browseable => this.browsable;

    public string Category => this._category;

    public string Value => this.Name;
  }
}
