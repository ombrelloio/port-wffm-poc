// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.FormDesigner.OpenRobotDetection
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Utility;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web.UI.Sheer;
using System;
using System.Collections.Specialized;
using System.Web;

namespace Sitecore.Form.Core.FormDesigner
{
  [Serializable]
  public class OpenRobotDetection : Command
  {
    public override void Execute(CommandContext context) => Context.ClientPage.Start((object) this, "Run", new ClientPipelineArgs()
    {
      Parameters = {
        ["id"] = context.Parameters["target"],
        ["value"] = context.Parameters["value"]
      }
    });

    protected void Run(ClientPipelineArgs args)
    {
      Assert.ArgumentNotNull((object) args, nameof (args));
      if (args.IsPostBack)
      {
        if (!args.HasResult)
          return;
        NameValueCollection nameValueCollection = ParametersUtil.XmlToNameValueCollection(args.Result);
        SheerResponse.Eval("Sitecore.PropertiesBuilder.setValue($('" + args.Parameters["id"] + "'), '" + nameValueCollection["protectionschema"].Replace("\r\n", string.Empty) + "');");
      }
      else
      {
        UrlString url = new UrlString("/sitecore/shell/~/xaml/Sitecore.Forms.Shell.UI.Dialogs.EvilRobotDetection.aspx");
        string name = ((object) ID.NewID).ToString();
        NameValueCollection values = new NameValueCollection()
        {
          {
            "RobotDetection",
            args.Parameters["value"]
          }
        };
        HttpContext.Current.Session.Add(name, (object) ParametersUtil.NameValueCollectionToXml(values));
        url.Append("params", name);
        ModalDialog.Show(url);
        args.WaitForPostBack();
      }
    }
  }
}
