// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Configuration.Installation
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Forms.Core.Wrappers;
using Sitecore.Install.Framework;
using Sitecore.WFFM.Abstractions.Dependencies;
using System.Collections.Specialized;
using System.Threading;

namespace Sitecore.Form.Core.Configuration
{
  public class Installation : IPostStep
  {
    private readonly ISecurityConfigurator securityConfigurator;
    private readonly ITranslationImporter translationImporter;
    private readonly IJobContext jobContext;

    public Installation()
      : this(DependenciesManager.Resolve<ISecurityConfigurator>(), DependenciesManager.Resolve<ITranslationImporter>(), DependenciesManager.Resolve<IJobContext>())
    {
    }

    public Installation(
      ISecurityConfigurator securityConfigurator,
      ITranslationImporter translationImporter,
      IJobContext jobContext)
    {
      Assert.ArgumentNotNull((object) securityConfigurator, nameof (securityConfigurator));
      Assert.ArgumentNotNull((object) translationImporter, nameof (translationImporter));
      Assert.ArgumentNotNull((object) jobContext, nameof (jobContext));
      this.securityConfigurator = securityConfigurator;
      this.translationImporter = translationImporter;
      this.jobContext = jobContext;
    }

    public void Run(ITaskOutput output, NameValueCollection metaData)
    {
      this.securityConfigurator.SetupRoles();
      this.securityConfigurator.GrantAccessToItems();
      output.Execute((ThreadStart) (() => Context.ClientPage.SendMessage((object) this, "forms:selectplaceholders")));
    }
  }
}
