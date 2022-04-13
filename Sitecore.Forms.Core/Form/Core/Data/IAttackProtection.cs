// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Data.IAttackProtection
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Web.UI.Controls;
using System.ComponentModel;

namespace Sitecore.Form.Core.Data
{
  public interface IAttackProtection
  {
    [TypeConverter(typeof (ProtectionSchemaAdapter))]
    ProtectionSchema RobotDetection { get; set; }

    ProtectionType Type { get; }
  }
}
