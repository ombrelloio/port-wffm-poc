// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Wrappers.RolesInRolesManagerWrapper
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Security.Accounts;
using System;
using System.Collections.Generic;

namespace Sitecore.Forms.Core.Wrappers
{
  [Serializable]
  public class RolesInRolesManagerWrapper : IRolesInRolesManager
  {
    public bool RolesInRolesSupported => RolesInRolesManager.RolesInRolesSupported;

    public void AddRoleToRole(Role memberRole, Role targetRole)
    {
      Assert.ArgumentNotNull((object) memberRole, nameof (memberRole));
      Assert.ArgumentNotNull((object) targetRole, nameof (targetRole));
      RolesInRolesManager.AddRoleToRole(memberRole, targetRole);
    }

    public void AddRoleToRoles(Role memberRole, IEnumerable<Role> targetRoles)
    {
      Assert.ArgumentNotNull((object) memberRole, nameof (memberRole));
      Assert.ArgumentNotNull((object) targetRoles, nameof (targetRoles));
      RolesInRolesManager.AddRoleToRoles(memberRole, targetRoles);
    }
  }
}
