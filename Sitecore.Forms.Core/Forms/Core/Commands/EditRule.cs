// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Commands.EditRule
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Web;
using Sitecore.Forms.Core.Rules;
using Sitecore.Shell.Applications.Dialogs.RulesEditor;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Specialized;
using System.Xml.Linq;

namespace Sitecore.Forms.Core.Commands
{
  [Serializable]
  public class EditRule : Command
  {
    public override void Execute(CommandContext context)
    {
      string parameter = context.Parameters["rule"];
      FormModel t = (FormModel) null;
      if (!Json.Instance.TryDeserializeObject<FormModel>(WebUtil.GetFormValue(parameter), out t))
        return;
      Context.ClientPage.Start((object) this, "Run", new ClientPipelineArgs(new NameValueCollection()
      {
        ["rule"] = parameter,
        ["form"] = WebUtil.GetFormValue(parameter),
        ["id"] = context.Parameters["id"],
        ["cid"] = context.Parameters["cid"],
        ["ruletext"] = t.Get(context.Parameters["id"], "Conditions")
      }));
    }

    protected void Run(ClientPipelineArgs args)
    {
      if (!args.IsPostBack)
      {
        RulesEditorOptions rulesEditorOptions = new RulesEditorOptions()
        {
          IncludeCommon = true,
          RulesPath = "/sitecore/system/Settings/Rules/Web Form for Marketers",
          AllowMultiple = true,
          HideActions = false
        };
        XElement xelement = XElement.Parse(Sitecore.StringExtensions.StringExtensions.FormatWith(RuleRenderer.DefualtCondition, new object[1]
        {
          (object) ID.NewID
        }));
        string parameter = args.Parameters["ruletext"];
        if (!string.IsNullOrEmpty(parameter))
        {
          try
          {
            xelement = XElement.Parse(parameter);
          }
          catch
          {
          }
        }
        rulesEditorOptions.Value = xelement.ToString();
        SheerResponse.ShowModalDialog(((object) rulesEditorOptions.ToUrlString()).ToString(), "800px", "600px", string.Empty, true);
        args.WaitForPostBack();
      }
      else
      {
        if (!args.HasResult)
          return;
        string text = RuleRenderer.Render(args.Result);
        string parameter = args.Parameters["rule"];
        FormModel t = (FormModel) null;
        Json.Instance.TryDeserializeObject<FormModel>(args.Parameters["form"], out t);
        t.Set(args.Parameters["id"], "Conditions", args.Result, text);
        string json = DependenciesManager.ConvertionUtil.ConvertToJson((object) t);
        SheerResponse.SetAttribute(parameter, "value", json);
        SheerResponse.SetInnerHtml(args.Parameters["cid"], string.IsNullOrEmpty(text) ? "<div class='no-conditions'>" + DependenciesManager.ResourceManager.Localize("THERE_IS_NO_CONDITIONS_FOR_THIS_ELEMENT") + "</div>" : text);
        SheerResponse.Eval("Sitecore.FormBuilder.loadModel();");
        SheerResponse.Eval("$j('#FieldProperties').find('.scRule').each(function () {$j(this).html('<ul><li><span>' + $j(this).html() + '</span></ul></li>');}); ");
      }
    }
  }
}
