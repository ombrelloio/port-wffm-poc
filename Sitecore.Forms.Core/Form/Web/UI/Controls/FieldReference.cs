// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.FieldReference
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Utility;
using Sitecore.Forms.Core.Data;
using Sitecore.Forms.Core.Rules;
using Sitecore.Web;
using Sitecore.WFFM.Abstractions.Data;
using System;
using System.Web.UI;

namespace Sitecore.Form.Web.UI.Controls
{
  [Dummy]
  [PersistChildren(true)]
  internal class FieldReference : Control
  {
    private Control innerControl;
    private Page page;

    public FieldReference(ID fieldID)
      : this(new FieldTypeItem(Sitecore.Context.Database.GetItem(fieldID)))
    {
    }

    public FieldReference(FieldTypeItem fieldItem)
      : this()
    {
      this.FieldTypeItem = fieldItem;
    }

    public FieldReference(FieldTypeItem fieldItem, Page contextPage)
      : this()
    {
      this.FieldTypeItem = fieldItem;
      this.page = contextPage;
    }

    protected FieldReference() => this.ReadQueryString = false;

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      Control innerControl = this.InnerControl;
    }

    public string FieldTypeID
    {
      get => ((object) ((CustomItemBase) this.FieldTypeItem).ID).ToString();
      set => this.FieldTypeItem = new FieldTypeItem(Sitecore.Context.Database.GetItem(value));
    }

    public FieldTypeItem FieldTypeItem { get; private set; }

    public bool DisableWebEditing { get; set; }

    public Control InnerControl
    {
      get
      {
        if (this.innerControl == null)
        {
          this.innerControl = FieldReflectionUtil.GetFieldInstance((IFieldTypeItem) this.FieldTypeItem, this.page);
          FieldTypeItem fieldTypeItem = this.FieldTypeItem;
          if (this.innerControl is IResult)
          {
            ((IResult) this.innerControl).FieldID = ((object) ((CustomItemBase) fieldTypeItem).ID).ToString();
            ((IResult) this.innerControl).ControlName = ((CustomItemBase) fieldTypeItem).Name;
          }
          if (FieldTypeItem.IsFieldItem(((CustomItemBase) fieldTypeItem).InnerItem))
          {
            fieldTypeItem = (FieldTypeItem) new FieldItem(((CustomItemBase) fieldTypeItem).InnerItem);
            ReflectionUtils.SetXmlProperties((object) this.innerControl, "<Title>" + FieldRenderer.Render(((CustomItemBase) fieldTypeItem).InnerItem, "Title", this.RenderingParameters, this.DisableWebEditing) + "</Title>", true);
            ReflectionUtils.SetXmlProperties((object) this.innerControl, fieldTypeItem.Parameters, true);
            ReflectionUtils.SetXmlProperties((object) this.innerControl, fieldTypeItem.LocalizedParameters, true);
          }
          if (this.ReadQueryString && this.innerControl is IResult)
          {
            string queryString = Sitecore.Web.WebUtil.GetQueryString(((CustomItemBase) fieldTypeItem).Name);
            if (!string.IsNullOrEmpty(queryString))
              ((IResult) this.innerControl).DefaultValue = queryString;
          }
          Rule.Run(((FieldItem) fieldTypeItem).Conditions, this.innerControl);
          this.innerControl.ID = "field_" + (object) ((CustomItemBase) fieldTypeItem).ID.ToShortID();
          this.Controls.Add(this.innerControl);
        }
        return this.innerControl;
      }
    }

    public bool IsHasTitle => !(this.InnerControl is IHasTitle) || !string.IsNullOrEmpty(((IHasTitle) this.InnerControl).Title);

    public bool ReadQueryString { get; set; }

    public string RenderingParameters { get; set; }
  }
}
