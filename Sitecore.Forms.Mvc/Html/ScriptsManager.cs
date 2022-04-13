// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Html.ScriptsManager
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using HtmlAgilityPack;
using Microsoft.Ajax.Utilities;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.WebPages;

namespace Sitecore.Forms.Mvc.Html
{
  public static class ScriptsManager
  {
    private const string ScriptsFilesKey = "WFFMScriptsKey";
    private static readonly Minifier Minifier = new Minifier();

    public static MvcHtmlString RenderScriptBlock(
      this HtmlHelper htmlHelper,
      Func<object, HelperResult> template)
    {
      Assert.ArgumentNotNull((object) template, nameof (template));
      return ScriptsManager.RenderBlock(template);
    }

    public static IHtmlString RenderScripts(
      this HtmlHelper htmlHelper,
      params string[] paths)
    {
      Assert.ArgumentNotNull((object) paths, nameof (paths));
      return htmlHelper.Render(paths);
    }

    public static MvcHtmlString RenderStyleBlock(
      this HtmlHelper htmlHelper,
      Func<object, HelperResult> template)
    {
      Assert.ArgumentNotNull((object) template, nameof (template));
      return ScriptsManager.RenderBlock(template, false);
    }

    public static IHtmlString RenderStyles(
      this HtmlHelper htmlHelper,
      params string[] paths)
    {
      Assert.ArgumentNotNull((object) paths, nameof (paths));
      return htmlHelper.Render(paths, false);
    }

    private static string Minify(string scriptBlock, bool script = true)
    {
      if (string.IsNullOrEmpty(scriptBlock))
        return scriptBlock;
      HtmlDocument htmlDocument = new HtmlDocument();
      using (new ThreadCultureSwitcher(Language.Parse("en").CultureInfo))
        htmlDocument.LoadHtml(scriptBlock);
      HtmlNode htmlNode = htmlDocument.DocumentNode.ChildNodes.FirstOrDefault<HtmlNode>();
      if (htmlNode == null)
        return scriptBlock;
      if (script)
      {
        CodeSettings codeSettings = new CodeSettings()
        {
          EvalTreatment = (EvalTreatment) 1
        };
        htmlNode.InnerHtml = ScriptsManager.Minifier.MinifyJavaScript(htmlNode.InnerHtml, codeSettings);
      }
      else
        htmlNode.InnerHtml = ScriptsManager.Minifier.MinifyStyleSheet(htmlNode.InnerHtml);
      return htmlDocument.DocumentNode.InnerHtml;
    }

    private static IHtmlString Render(
      this HtmlHelper htmlHelper,
      string[] paths,
      bool script = true)
    {
      Assert.ArgumentNotNull((object) paths, nameof (paths));
      StringBuilder res = new StringBuilder();
      EnumerableExtensions.Each<string>((IEnumerable<string>) paths, (Action<string>) (x => res.Append((object) htmlHelper.RenderFile(x, script))));
      return (IHtmlString) new MvcHtmlString(res.ToString());
    }

    private static MvcHtmlString RenderBlock(
      Func<object, HelperResult> template,
      bool scriptSource = true)
    {
      Assert.ArgumentNotNull((object) template, nameof (template));
      return new MvcHtmlString(ScriptsManager.Minify(template((object) null).ToHtmlString(), scriptSource));
    }

    private static IHtmlString RenderFile(
      this HtmlHelper htmlHelper,
      string path,
      bool script = true)
    {
      Assert.ArgumentNotNullOrEmpty(path, nameof (path));
      if (!(((ControllerContext) htmlHelper.ViewContext).HttpContext.Items[(object) "WFFMScriptsKey"] is HashSet<string> stringSet))
      {
        ((ControllerContext) htmlHelper.ViewContext).HttpContext.Items[(object) "WFFMScriptsKey"] = (object) new HashSet<string>()
        {
          path
        };
      }
      else
      {
        if (stringSet.Contains(path))
          return (IHtmlString) MvcHtmlString.Empty;
        stringSet.Add(path);
      }
      return !script ? Styles.Render(new string[1]
      {
        path
      }) : Scripts.Render(new string[1]{ path });
    }
  }
}
