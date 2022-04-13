// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Commands.ConvertDate
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Utility;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;
using System;

namespace Sitecore.Forms.Core.Commands
{
  [Serializable]
  public class ConvertDate : Command
  {
    public override void Execute(CommandContext context)
    {
            Sitecore.Form.Core.Utility.Utils.SetUserCulture();
      DateTime dateTime = DateUtil.IsoDateToDateTime(context.Parameters["value"]);
      SheerResponse.Eval("$('" + context.Parameters["target"] + "').value = '" + dateTime.ToString(Sitecore.WFFM.Abstractions.Constants.Core.Constants.LongDateFormat) + "'");
    }
  }
}
