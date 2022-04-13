// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Configuration.TranslationImporter
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.IO;
using Sitecore.Jobs;
using Sitecore.Shell.Applications.Globalization.ImportLanguage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Sitecore.Form.Core.Configuration
{
  public class TranslationImporter : ITranslationImporter
  {
    public void AddTranslations()
    {
      string path = StringUtil.Combine((object) FileUtil.MapPath(Sitecore.Configuration.Settings.TempFolderPath), (object) "WFFM", "\\");
      Database database = Database.GetDatabase("core");
      Assert.IsNotNull((object) database, "Can't find core database");
      List<string> list = ((IEnumerable<Language>) LanguageManager.GetLanguages(database)).Select<Language, string>((Func<Language, string>) (lang => lang.Name)).ToList<string>();
      foreach (string file in Directory.GetFiles(path))
      {
        var job = JobManager.Start(new DefaultJobOptions("ImportLanguage", "ImportLanguage", "shell", (object) new ImportLanguageForm.Importer("core", file, list), "Import")
        {
          ContextUser = Context.User
        });
        while (!job.IsDone)
          Thread.Sleep(500);
      }
    }
  }
}
