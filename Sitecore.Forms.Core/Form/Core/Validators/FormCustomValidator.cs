// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Validators.FormCustomValidator
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Form.Core.Utility;
using System;
using System.Collections.Specialized;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Validators
{
  public class FormCustomValidator : CustomValidator
  {
    protected NameValueCollection classAttributes = new NameValueCollection();

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.classAttributes = NameValueCollectionUtil.Concat(this.classAttributes, NameValueCollectionUtil.GetNameValues(this.CssClass));
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.CssClass = NameValueCollectionUtil.GetString(this.classAttributes, NameValueCollectionUtil.GetNameValues(this.CssClass));
    }
  }
}
