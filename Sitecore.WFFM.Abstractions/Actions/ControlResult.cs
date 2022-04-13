// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Actions.ControlResult
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Sitecore.WFFM.Abstractions.Actions
{
  [XmlInclude(typeof (PostedFile))]
  [KnownType(typeof (PostedFile))]
  [SoapInclude(typeof (PostedFile))]
  [Serializable]
  public class ControlResult
  {
    public ControlResult(string fieldName, object value, string parameters)
      : this()
    {
      this.FieldName = fieldName;
      this.Parameters = parameters;
      this.Value = value;
    }

    public ControlResult(
      string fieldId,
      string fieldName,
      object value,
      string parameters,
      bool secure = false)
      : this()
    {
      this.FieldID = fieldId;
      this.FieldName = fieldName;
      this.Parameters = parameters;
      this.Value = value;
      this.AdaptForAnalyticsTag = true;
      this.Secure = secure;
    }

    public ControlResult()
    {
    }

    public string FieldID { get; set; }

    public string FieldName { get; set; }

    public string Parameters { get; set; }

    public object Value { get; set; }

    public string FieldType { get; set; }

    public bool Secure { get; set; }

    public bool AdaptForAnalyticsTag { get; set; }
  }
}
