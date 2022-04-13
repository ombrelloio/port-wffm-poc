// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Commands.RedirectToEditForm
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Shell.Framework.Commands;
using System;
using System.Collections.Specialized;

namespace Sitecore.Forms.Core.Commands
{
  [Serializable]
  public class RedirectToEditForm : Command
  {
    public override void Execute(CommandContext context)
    {
      Assert.ArgumentNotNull((object) context, nameof (context));
      Error.AssertObject((object) context, nameof (context));
      if (context.Items.Length == 0)
        return;
      if (!Settings.OpenFormDesignerAsModalDialog)
      {
        string str = context.Parameters["url"] + "?" + context.Parameters["param"];
        if (string.IsNullOrEmpty(str))
          return;
        Context.ClientPage.ClientResponse.Eval("window.top.location.href='" + str + "';");
        Context.ClientPage.ClientResponse.DisableOutput();
      }
      else
      {
        NameValueCollection nameValues = StringUtil.GetNameValues(context.Parameters["param"].Trim('&'), '=', '&');
        CommandManager.GetCommand("forms:designer").Execute(new CommandContext(context.Items[0].Database.GetItem(nameValues["formid"])));
      }
    }
  }
}
