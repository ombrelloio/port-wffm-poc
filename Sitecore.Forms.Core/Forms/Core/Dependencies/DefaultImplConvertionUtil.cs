// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Dependencies.DefaultImplConvertionUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Newtonsoft.Json;
using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Utils;
using System;

namespace Sitecore.Forms.Core.Dependencies
{
  [Serializable]
  public class DefaultImplConvertionUtil : IConvertionUtil
  {
    public string ConvertToJson(object obj)
    {
      Assert.ArgumentNotNull(obj, "Parameter obj is null");
      return JsonConvert.SerializeObject(obj);
    }
  }
}
