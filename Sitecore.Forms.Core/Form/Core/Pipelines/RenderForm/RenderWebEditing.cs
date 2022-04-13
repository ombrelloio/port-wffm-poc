// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.RenderForm.RenderWebEditing
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Globalization;
using Sitecore.Resources;
using Sitecore.Shell;
using Sitecore.Sites;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI;
using Sitecore.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Sitecore.Form.Core.Pipelines.RenderForm
{
  public class RenderWebEditing
  {
    private static Language GetLanguage()
    {
      string languageName = RenderWebEditing.GetLanguageName();
      Language language;
      return !string.IsNullOrEmpty(languageName) && Language.TryParse(languageName, out language) ? language : Context.Language;
    }

    private static string GetLanguageName() => WebUtil.GetCookieValue("shell#lang", Sitecore.Configuration.Settings.DefaultLanguage);

    public void Process(RenderFormArgs args)
    {
      Assert.IsNotNull((object) args, nameof (args));
      Assert.IsNotNull((object) args.Item, "args.item");
      if (args.DisableWebEdit)
        return;
      SiteContext site = Context.Site;
      if (site == null || site.DisplayMode != DisplayMode.Edit || !(WebUtil.GetQueryString("sc_ce") != "1") || Context.PageDesigner.IsDesigning || !(WebUtil.GetQueryString("sc_webedit") != "0") || !(WebUtil.GetQueryString("sc_duration") != "temporary") || !Context.PageMode.IsExperienceEditorEditing || !Context.IsAdministrator && args.Item.Locking.IsLocked() && !args.Item.Locking.HasLock() || !args.Item.Access.CanWrite() || !args.Item.Access.CanWriteLanguage() || args.Item.Appearance.ReadOnly || args.Item.Appearance.Hidden && !UserOptions.View.ShowHiddenItems)
        return;
      HtmlTextWriter htmlTextWriter = new HtmlTextWriter((TextWriter) new StringWriter());
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("fld_{0}", (object) args.Item.ID.ToShortID());
      stringBuilder.AppendFormat("_{0}", (object) args.Item.ID.ToShortID());
      stringBuilder.AppendFormat("_{0}", (object) args.Item.Language);
      stringBuilder.AppendFormat("_{0}", (object) args.Item.Version);
      stringBuilder.AppendFormat("_{0}", (object) MainUtil.GetSequencer());
      string str = stringBuilder.ToString();
      htmlTextWriter.Write("<input id=\"" + str + "\" name=\"" + str + "\" type=\"hidden\" value=\"" + args.Item.Name + "\" />");
      htmlTextWriter.Write("<span id=\"" + str + "_edit\" class=\"scWebEditInput\"");
      htmlTextWriter.Write(">");
      htmlTextWriter.Write(args.Result.FirstPart);
      args.Result.FirstPart = htmlTextWriter.InnerWriter.ToString();
      args.Result.LastPart += "</span>";
    }

    private static void RenderFrameBar(
      TextWriter output,
      RenderFormArgs args,
      IEnumerable<WebEditButton> buttons,
      string controlID)
    {
      Assert.ArgumentNotNull((object) output, nameof (output));
      Assert.ArgumentNotNull((object) args, nameof (args));
      Assert.ArgumentNotNull((object) buttons, nameof (buttons));
      using (new LanguageSwitcher(RenderWebEditing.GetLanguage()))
      {
        string url;
        if (UserOptions.WebEdit.UsePopupContentEditor)
          url = "javascript:Sitecore.WebEdit.postRequest(\"webedit:edit(id=" + (object) args.Item.ID + ",language=" + (object) args.Item.Language + ",version=" + (object) args.Item.Version + ")\")";
        else
          url = ((object) new UrlString(WebUtil.GetRawUrl())
          {
            ["sc_ce"] = "1",
            ["sc_ce_uri"] = HttpUtility.UrlEncode(((object) Context.Item.Uri).ToString())
          }).ToString();
        RenderWebEditing.RenderButtons(output, args.Item, buttons, url, controlID);
      }
    }

    private static void RenderButtons(
      TextWriter output,
      Item item,
      IEnumerable<WebEditButton> buttons,
      string url,
      string controlID)
    {
      Assert.ArgumentNotNull((object) output, nameof (output));
      Assert.ArgumentNotNull((object) item, "field");
      Assert.ArgumentNotNull((object) buttons, nameof (buttons));
      Assert.ArgumentNotNull((object) url, nameof (url));
      foreach (WebEditButton button in buttons)
      {
        if (button.IsDivider)
        {
          RenderWebEditing.RenderDivider(output);
        }
        else
        {
          string click = button.Click.Replace("$URL", url).Replace("$ItemID", ((object) item.ID).ToString()).Replace("$Language", ((object) item.Language).ToString()).Replace("$Version", ((object) item.Version).ToString()).Replace("$FieldID", ((object) item.ID).ToString()).Replace("$ControlID", ((object) Context.Item.ID).ToString()).Replace("$MessageParameters", "itemid=" + (object) item.ID + ",language=" + (object) item.Language + ",version=" + (object) item.Version + ",fieldid=" + (object) item.ID + ",controlid=" + controlID).Replace("$JavascriptParameters", "\"" + (object) item.ID + "\",\"" + (object) item.Language + "\",\"" + (object) item.Version + "\",\"" + (object) item.ID + "\",\"" + controlID + "\"");
          RenderWebEditing.RenderButton(output, button.Header, button.Icon, click, button.Tooltip, button.Type);
        }
      }
    }

    private static void RenderButton(
      TextWriter output,
      string header,
      string icon,
      string click,
      string tooltip,
      string type)
    {
      Assert.ArgumentNotNull((object) output, nameof (output));
      Assert.ArgumentNotNull((object) header, nameof (header));
      Assert.ArgumentNotNull((object) icon, nameof (icon));
      Assert.ArgumentNotNull((object) click, nameof (click));
      Assert.ArgumentNotNull((object) tooltip, nameof (tooltip));
      click = click.StartsWith("javascript:") ? StringUtil.EscapeJavascriptString(StringUtil.EscapeQuote(click), false) : "javascript:window.location.href='" + click + "'; return false";
      Tag tag = new Tag("span");
      tag.Add("onclick", click);
      tag.Add("onmouseover", "javascript:this.className='scWebEditFrameButtonHover'");
      tag.Add("onmouseout", "javascript:this.className='scWebEditFrameButton'");
      tag.Add("class", "scWebEditFrameButton");
      if (!string.IsNullOrEmpty(tooltip))
        tag.Add("title", tooltip);
      tag.Add(nameof (type), type ?? "own");
      output.Write(tag.Start());
      output.Write(((object) new ImageBuilder()
      {
        Src = Images.GetThemedImageSource(icon, (ImageDimension) 1),
        Alt = Sitecore.Form.Core.Configuration.Translate.SystemText(tooltip),
        Class = "scWebEditFrameButtonIcon"
      }).ToString());
      if (!string.IsNullOrEmpty(header))
      {
        output.Write(' ');
        output.Write(Sitecore.Form.Core.Configuration.Translate.SystemText(header));
      }
      output.Write(tag.End());
    }

    private static void RenderDivider(TextWriter output)
    {
      Assert.ArgumentNotNull((object) output, nameof (output));
      output.Write("<div class=\"scWebEditFrameDivider\">");
      output.Write(Images.GetSpacer(1, 1));
      output.Write("</div>");
    }
  }
}
