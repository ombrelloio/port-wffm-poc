// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.FieldViewModel
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using Sitecore.Data.Items;
using Sitecore.Forms.Mvc.Interfaces;
using Sitecore.WFFM.Abstractions.Data.Enums;
using System.Collections.Generic;
using System.Web.Routing;

namespace Sitecore.Forms.Mvc.ViewModels
{
  public class FieldViewModel : 
    IViewModel,
    IHasItem,
    IHasRegularExpression,
    IHasIsRequired,
    IStyleSettings
  {
    public FieldViewModel() => this.Parameters = new Dictionary<string, string>();

    public string FieldItemId { get; set; }

    public string FormId { get; set; }

    public virtual string Information { get; set; }

    public virtual string Title { get; set; }

    public virtual string Name { get; set; }

    public bool ShowInformation { get; set; }

    public bool ShowTitle { get; set; }

    public bool Visible { get; set; }

    public bool Tracking { get; set; }

    public Item Item { get; set; }

    public Dictionary<string, string> Parameters { get; set; }

    public FormType FormType { get; set; }

    public string LeftColumnStyle { get; set; }

    public string RightColumnStyle { get; set; }

    public bool IsRequired { get; set; }

    public string RegexPattern { get; set; }

    public RouteValueDictionary HtmlAttributes { get; set; }

    public virtual string CssClass { get; set; }

    public virtual void Initialize()
    {
    }
  }
}
