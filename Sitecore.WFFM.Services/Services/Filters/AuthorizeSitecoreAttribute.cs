// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Services.Filters.AuthorizeSitecoreAttribute
// Assembly: Sitecore.WFFM.Services, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A002B65D-6063-457D-AAC3-54DF69ADFBA9
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Services.dll

using Sitecore.Diagnostics;
using Sitecore.Security.Accounts;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.WFFM.Services.Filters
{
  public class AuthorizeSitecoreAttribute : AuthorizeAttribute
  {
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
      Assert.ArgumentNotNull((object) httpContext, nameof (httpContext));
      User user = Context.User;
      return user.IsAuthenticated && user.IsAdministrator || base.AuthorizeCore(httpContext);
    }
  }
}
