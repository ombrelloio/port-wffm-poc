// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Submit.FormRedirector
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Ascx.Controls;
using Sitecore.Form.Core.Utility;
using Sitecore.Forms.Core.Data.Serialization;
using Sitecore.Text;
using Sitecore.Web;
using System;

namespace Sitecore.Form.Core.Submit
{
  public class FormRedirector
  {
    public FormRedirector(string page, string placeholder, string backPage)
    {
      Assert.ArgumentNotNullOrEmpty(page, nameof (page));
      if (ID.IsID(page))
      {
        Item obj = Context.Database.GetItem(page);
        this.Page = obj == null ? page : Sitecore.Form.Core.Utility.ItemUtil.GetItemUrl(obj);
      }
      else
        this.Page = page;
      this.BackPage = backPage;
      this.Placeholder = placeholder;
      this.PageItem = Context.Item != null ? Context.Item.ID : (ID) null;
    }

    protected string Placeholder { get; set; }

    public string Page { get; private set; }

    public string BackPage { get; private set; }

    public ID PageItem { get; private set; }

    public void OnSubmit(object sender, EventArgs e)
    {
      SimpleForm form = (SimpleForm) sender;
      if (form == null)
        return;
      string key = ((object) ID.NewID.ToShortID()).ToString();
      FormState formState = new FormState(form);
      StorageUtil.SetValue(key, (object) formState);
      UrlString url = new UrlString(this.Page);
      url.Append(Sitecore.Web.WebUtil.GetQueryString());
      url.Append(SimpleForm.FormRedirectingFormIdKey, ((object) form.FormID).ToString());
      url.Append(SimpleForm.FormRedirectingHandlerKey, key);
      if (!string.IsNullOrEmpty(this.Placeholder))
        url.Append(SimpleForm.FormRedirectingPlaceholderKey, this.Placeholder);
      if (!ID.IsNullOrEmpty(this.PageItem))
        url.Append(SimpleForm.FormRedirectingPreviousPageItemKey, ((object) this.PageItem).ToString());
      else
        url.Remove(SimpleForm.FormRedirectingPreviousPageItemKey);
      if (!string.IsNullOrEmpty(this.BackPage))
      {
        url.Append(SimpleForm.FormRedirectingPreviousPageKey, this.BackPage);
      }
      else
      {
        url.Remove(SimpleForm.FormRedirectingPreviousPageKey);
        url.Remove(SimpleForm.FormRedirectingFormIdKey);
        url.Remove(SimpleForm.FormRedirectingPlaceholderKey);
        url.Remove(SimpleForm.FormRedirectingPreviousPageItemKey);
      }
      form.Page.Response.Clear();
      if (form.Page.Request.Browser.EcmaScriptVersion.Major > 0)
      {
        form.Page.Response.Write("<html><head>");
        form.Page.Response.Write("<script language='JavaScript' type='text/JavaScript'>document.location.replace('" + (object) url + "');</script>");
        form.Page.Response.Write("<noscript><meta http-equiv='refresh' content='0;URL=" + (object) url + "'></noscript>");
        form.Page.Response.Write("</head><body></body></html>");
      }
      else
      {
        form.Page.Response.StatusCode = 301;
        form.Page.Response.AddHeader("Location", ((object) url).ToString());
      }
      form.Page.Response.End();
    }
  }
}
