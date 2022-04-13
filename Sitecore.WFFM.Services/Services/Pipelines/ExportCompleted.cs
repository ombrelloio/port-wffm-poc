// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Services.Pipelines.ExportCompleted
// Assembly: Sitecore.WFFM.Services, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A002B65D-6063-457D-AAC3-54DF69ADFBA9
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Services.dll

using Sitecore.IO;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;
using Sitecore.Web.UI.XamlSharp.Continuations;
using System;
using System.IO;
using System.Text;
using System.Web;

namespace Sitecore.WFFM.Services.Pipelines
{
    [Serializable]
    public class ExportCompleted : Command, ISupportsContinuation
    {
        public override void Execute(CommandContext context)
        {
            HttpContext.Current.Session["exportcontent"] = (object)FileUtil.ReadFromFile(context.Parameters["tempfile"]);
            HttpContext.Current.Session["filename"] = (object)context.Parameters["filename"];
            HttpContext.Current.Session["contentType"] = (object)context.Parameters["contentType"];
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("var iframe=document.createElement('iframe');");
            stringBuilder.Append("iframe.src='/sitecore modules/web/Web Forms for Marketers/export.aspx';");
            stringBuilder.Append("iframe.width='1';");
            stringBuilder.Append("iframe.height='1';");
            stringBuilder.Append("document.body.appendChild(iframe);");
            SheerResponse.Eval(stringBuilder.ToString());
            try
            {
                FileUtil.Delete(context.Parameters["tempfile"]);
            }
            catch (IOException ex)
            {
            }
        }
    }
}
