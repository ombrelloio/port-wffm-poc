// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Configuration.SecurityConfigurator
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Forms.Core.Wrappers;
using Sitecore.Security.AccessControl;
using Sitecore.Security.Accounts;
using System.Collections.Generic;

namespace Sitecore.Form.Core.Configuration
{
  public class SecurityConfigurator : ISecurityConfigurator
  {
    private readonly IRolesInRolesManager rolesManager;

    public SecurityConfigurator(IRolesInRolesManager rolesManager)
    {
      Assert.ArgumentNotNull((object) rolesManager, nameof (rolesManager));
      this.rolesManager = rolesManager;
    }

    public void SetupRoles()
    {
      if (!this.rolesManager.RolesInRolesSupported)
        return;
      Role memberRole1 = Role.FromName("sitecore\\Sitecore Client Forms Author");
      Role memberRole2 = Role.FromName("sitecore\\Sitecore Marketer Form Author");
      List<Role> roleList1 = new List<Role>();
      roleList1.Add(memberRole1);
      roleList1.Add(Role.FromName("sitecore\\Analytics Maintaining"));
      roleList1.Add(Role.FromName("sitecore\\Analytics Content Profiling"));
      roleList1.Add(Role.FromName("sitecore\\Analytics Reporting"));
      List<Role> roleList2 = roleList1;
      this.rolesManager.AddRoleToRole(memberRole1, Role.FromName("sitecore\\Designer"));
      this.rolesManager.AddRoleToRoles(memberRole2, (IEnumerable<Role>) roleList2);
    }

    public void GrantAccessToItems()
    {
      Item obj = Factory.GetDatabase("master").GetItem(Sitecore.WFFM.Abstractions.Constants.Core.Constants.RestrinctingPlaceholders);
      if (obj == null)
        return;
      AccessRuleCollection accessRules = obj.Security.GetAccessRules();
      Role role = Role.FromName("sitecore\\Sitecore Client Forms Author");
      accessRules.Helper.AddAccessPermission((Account) role, AccessRight.ItemWrite, (PropagationType) 1, (AccessPermission) 1);
      accessRules.Helper.AddAccessPermission((Account) role, AccessRight.ItemWrite, (PropagationType) 2, (AccessPermission) 1);
      accessRules.Helper.AddinheritancePermission((Account) role, AccessRight.Any, (PropagationType) 1, (InheritancePermission) 3);
      accessRules.Helper.AddinheritancePermission((Account) role, AccessRight.Any, (PropagationType) 2, (InheritancePermission) 3);
      obj.Security.SetAccessRules(accessRules);
    }
  }
}
