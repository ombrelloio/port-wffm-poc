// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Models.FormModel
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Configuration;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Forms.Core.Data;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Links;
using Sitecore.Resources.Media;
using Sitecore.SecurityModel;
using Sitecore.Text;
using Sitecore.Web;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Sitecore.Forms.Mvc.Models
{
  public class FormModel : IFormModel, IModelEntity, ICloneable
  {
    public FormModel(Guid uniqueId, Sitecore.Data.Items.Item item)
      : this(uniqueId)
    {
      Assert.ArgumentNotNull((object) item, nameof (item));
      this.Item = (IFormItem) new FormItem(item);
      this.RedirectOnSuccess = this.Item.SuccessRedirect;
      this.IsValid = true;
      if (!this.RedirectOnSuccess)
        return;
      LinkField successPage = this.Item.SuccessPage;
      if (successPage == null)
        return;
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
          defaultUrlOptions.SiteResolving = Sitecore.Configuration.Settings.Rendering.SiteResolving;
          Sitecore.Data.Items.Item obj;
          using (new SecurityDisabler())
            obj = item.Database.Items[successPage.TargetID];
          urlString = new UrlString(LinkManager.GetItemUrl(obj, defaultUrlOptions));
        }
      }
      else
      {
        try
        {
          throw new NullReferenceException("Redirect item is null");
        }
        catch (NullReferenceException ex)
        {
          Log.Warn("[WFFM] The success page cannot be found", (Exception) ex, (object) this);
        }
      }
      if (urlString == null)
        return;
      string queryString = this.Item.SuccessPage.QueryString;
      if (!string.IsNullOrEmpty(queryString))
        urlString.Parameters.Add(WebUtil.ParseUrlParameters(queryString));
      this.SuccessRedirectUrl = ((object) urlString).ToString();
    }

    public FormModel(Guid uniqueId)
    {
      Assert.ArgumentCondition(uniqueId != Guid.Empty, nameof (uniqueId), "uniqueId is empty");
      this.UniqueId = uniqueId;
      this.Results = new List<ControlResult>();
      this.Failures = new List<ExecuteResult.Failure>();
    }

    public IFormItem Item { get; private set; }

    public List<ExecuteResult.Failure> Failures { get; set; }

    public string SuccessRedirectUrl { get; set; }

    public bool RedirectOnSuccess { get; set; }

    public DateTime RenderedTime { get; set; }

    public bool ReadQueryString { get; set; }

    public NameValueCollection QueryParameters { get; set; }

    public int EventCounter { get; set; }

    public List<ControlResult> Results { get; set; }

    public bool IsValid { get; set; }

    public Guid UniqueId { get; private set; }

    public object Clone()
    {
      FormModel formModel = (FormModel) this.MemberwiseClone();
      formModel.Results = new List<ControlResult>();
      formModel.Failures = new List<ExecuteResult.Failure>();
      return (object) formModel;
    }
  }
}
