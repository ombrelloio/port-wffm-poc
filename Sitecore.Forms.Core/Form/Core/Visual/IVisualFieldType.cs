// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Visual.IVisualFieldType
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

namespace Sitecore.Form.Core.Visual
{
  public interface IVisualFieldType
  {
    string DefaultValue { get; set; }

    string EmptyValue { get; set; }

    string ID { get; set; }

    bool IsCacheable { get; }

    bool Localize { get; set; }

    ValidationType Validation { get; set; }

    string Render();
  }
}
