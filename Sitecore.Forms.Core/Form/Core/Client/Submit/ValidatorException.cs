// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Client.Submit.ValidatorException
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.Runtime.Serialization;
using System.Web.Services.Protocols;
using System.Xml;

namespace Sitecore.Form.Core.Client.Submit
{
  [Serializable]
  public class ValidatorException : SoapException
  {
    public ValidatorException(string message)
      : base(message, new XmlQualifiedName("wfm"))
    {
    }

    public ValidatorException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    public ValidatorException(string message, Exception innerException)
      : base(message, new XmlQualifiedName("wfm"), innerException)
    {
    }
  }
}
