// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Services.FormRepository
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Forms.Mvc.Data.Wrappers;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.Forms.Mvc.Models;
using Sitecore.Web;
using System;
using System.Collections.Generic;
using System.Web;

namespace Sitecore.Forms.Mvc.Services
{
  public class FormRepository : IRepository<FormModel>
  {
    private readonly Dictionary<Guid, FormModel> models = new Dictionary<Guid, FormModel>();

    public FormRepository(IRenderingContext renderingContext)
    {
      Assert.ArgumentNotNull((object) renderingContext, nameof (renderingContext));
      this.RenderingContext = renderingContext;
    }

    public FormModel GetModel(Guid uniqueId)
    {
      if (uniqueId != Guid.Empty && this.models.ContainsKey(uniqueId))
        return (FormModel) this.models[uniqueId].Clone();
      Item obj = (Item) null;
      string dataSource = this.RenderingContext.Rendering.DataSource;
      if (!string.IsNullOrEmpty(dataSource))
      {
        try
        {
          obj = this.RenderingContext.Database.GetItem(dataSource);
        }
        catch
        {
        }
      }
      if (obj == null)
      {
        string parameter = this.RenderingContext.Rendering.Parameters[Constants.FormId];
        if (parameter != null)
          obj = this.RenderingContext.Database.GetItem(parameter);
      }
      Assert.IsNotNull((object) obj, "Form item is absent");
      FormModel formModel = new FormModel(uniqueId, obj)
      {
        ReadQueryString = MainUtil.GetBool(this.RenderingContext.Rendering.Parameters[Constants.ReadQueryString], false),
        QueryParameters = HttpUtility.ParseQueryString(WebUtil.GetQueryString())
      };
      this.models.Add(uniqueId, formModel);
      return formModel;
    }

    public FormModel GetModel() => this.GetModel(this.RenderingContext.Rendering.UniqueId);

    public IRenderingContext RenderingContext { get; private set; }
  }
}
