// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Attributes.SitecoreDefaultValueAttribute
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Utility;
using System;
using System.ComponentModel;

namespace Sitecore.Form.Core.Attributes
{
  public class SitecoreDefaultValueAttribute : DefaultValueAttribute
  {
    public SitecoreDefaultValueAttribute(Type type, string value)
      : base(type, value)
    {
    }

    public SitecoreDefaultValueAttribute(char value)
      : base(value)
    {
    }

    public SitecoreDefaultValueAttribute(byte value)
      : base(value)
    {
    }

    public SitecoreDefaultValueAttribute(short value)
      : base(value)
    {
    }

    public SitecoreDefaultValueAttribute(int value)
      : base(value)
    {
    }

    public SitecoreDefaultValueAttribute(long value)
      : base(value)
    {
    }

    public SitecoreDefaultValueAttribute(float value)
      : base(value)
    {
    }

    public SitecoreDefaultValueAttribute(double value)
      : base(value)
    {
    }

    public SitecoreDefaultValueAttribute(bool value)
      : base(value)
    {
    }

    public SitecoreDefaultValueAttribute(string value)
      : base(value)
    {
    }

    public SitecoreDefaultValueAttribute(object value)
      : base(value)
    {
    }

    public SitecoreDefaultValueAttribute(string sitecoreItemID, string sitecoreFieldID)
      : base(ItemUtil.GetItemValue(sitecoreItemID, sitecoreFieldID))
    {
    }
  }
}
