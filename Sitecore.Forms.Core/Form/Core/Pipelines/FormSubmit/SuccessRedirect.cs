// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.FormSubmit.SuccessRedirect
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Configuration;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Pipelines;
using Sitecore.Resources.Media;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;

namespace Sitecore.Form.Core.Pipelines.FormSubmit
{
  public class SuccessRedirect : ClientPipelineArgs
  {
    public void Process(SubmitSuccessArgs args)
    {
      Assert.IsNotNull((object) args, nameof (args));
      if (args.Form != null)
      {
        if (!args.Form.SuccessRedirect)
          return;
        ((PipelineArgs) args).AbortPipeline();
        LinkField successPage = args.Form.SuccessPage;
        UrlString urlString = (UrlString) null;
        if (successPage.LinkType == "external")
          urlString = new UrlString(successPage.Url);
        else if (successPage.TargetItem != null)
        {
          string linkType = successPage.LinkType;
          if (!(linkType == "internal"))
          {
            if (linkType == "media")
              urlString = new UrlString(MediaManager.GetMediaUrl(new MediaItem(successPage.TargetItem)));
          }
          else
          {
            UrlOptions defaultUrlOptions = LinkManager.GetDefaultUrlOptions();
            defaultUrlOptions.SiteResolving = Settings.Rendering.SiteResolving;
            urlString = new UrlString(LinkManager.GetItemUrl(successPage.TargetItem, defaultUrlOptions));
          }
        }
        else
          DependenciesManager.Logger.Warn(string.Format("Web Forms for Marketers : Success page for the form does not exist. Form ID: '{0}'.", (object) args.Form.ID), new object());
        if (urlString == null)
          return;
        string attribute = ((XmlField) successPage).GetAttribute("querystring");
        if (!string.IsNullOrEmpty(attribute))
          urlString.Parameters.Add(WebUtil.ParseUrlParameters(attribute));
        WebUtil.Redirect(((object) urlString).ToString(), false);
      }
      else
      {
        Uri result;
        if (!Uri.TryCreate(args.Result, UriKind.RelativeOrAbsolute, out result))
          return;
        try
        {
          string pathAndQuery = result.PathAndQuery;
        }
        catch (Exception ex)
        {
          return;
        }
        ((PipelineArgs) args).AbortPipeline();
        WebUtil.Redirect(args.Result, false);
      }
    }
  }
}
