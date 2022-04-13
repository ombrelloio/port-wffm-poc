// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Configuration.ISecurityConfigurator
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.WFFM.Abstractions.Dependencies;

namespace Sitecore.Form.Core.Configuration
{
  [DependencyPath("wffm/securityConfigurator")]
  public interface ISecurityConfigurator
  {
    void GrantAccessToItems();

    void SetupRoles();
  }
}
