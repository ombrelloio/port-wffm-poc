// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Wrappers.IRolesInRolesManager
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Security.Accounts;
using Sitecore.WFFM.Abstractions.Dependencies;
using System.Collections.Generic;

namespace Sitecore.Forms.Core.Wrappers
{
  [DependencyPath("wffm/wrappers/rolesInRolesManager")]
  public interface IRolesInRolesManager
  {
    bool RolesInRolesSupported { get; }

    void AddRoleToRole(Role memberRole, Role targetRole);

    void AddRoleToRoles(Role memberRole, IEnumerable<Role> targetRoles);
  }
}
