// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.Data.Wrappers.WrappedRenderingContext
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Form.Core.Configuration;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Forms.Mvc.Data.Wrappers
{
  public class WrappedRenderingContext : IRenderingContext
  {
    public IRendering Rendering => RenderingContext.CurrentOrNull != null ? (IRendering) new WrappedRendering(RenderingContext.CurrentOrNull.Rendering) : (IRendering) new WrappedRendering(this.TryToGetRendering());

    public IDatabase Database => RenderingContext.CurrentOrNull != null ? (IDatabase) new WrappedDatabase(RenderingContext.CurrentOrNull.PageContext.Database) : (IDatabase) new WrappedDatabase(Context.Database);

    private Sitecore.Mvc.Presentation.Rendering TryToGetRendering()
    {
      string name1 = ((IEnumerable<string>) HttpContext.Current.Request.QueryString.AllKeys).FirstOrDefault<string>((Func<string, bool>) (x => x != null && x.EndsWith("." + Constants.FormItemId) && x.StartsWith("wffm")));
      if (name1 != null)
      {
        Guid guid = Guid.Parse(HttpContext.Current.Request.QueryString[name1]);
        if (guid != Guid.Empty)
        {
          Item obj = this.Database.GetItem(new ID(guid));
          if (obj != null)
          {
            Sitecore.Mvc.Presentation.Rendering rendering = new Sitecore.Mvc.Presentation.Rendering();
            rendering.RenderingItem = (RenderingItem)(obj);
            string name2 = ((IEnumerable<string>) HttpContext.Current.Request.QueryString.AllKeys).FirstOrDefault<string>((Func<string, bool>) (x => x != null && x.EndsWith("." + Constants.Id) && x.StartsWith("wffm")));
            rendering.UniqueId = Guid.Parse(string.IsNullOrEmpty(name2) ? name1.Replace("wffm", "").Replace("." + Constants.FormItemId, "") : HttpContext.Current.Request.QueryString[name2]);
            rendering.Parameters[Constants.FormId] = ((object) obj.ID).ToString();
            rendering.RenderingItem = (RenderingItem)(this.Database.GetItem(IDs.FormMvcInterpreterID));
            return rendering;
          }
        }
      }
      return (Sitecore.Mvc.Presentation.Rendering) null;
    }
  }
}
