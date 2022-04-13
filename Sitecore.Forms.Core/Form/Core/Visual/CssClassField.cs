// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Visual.CssClassField
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Core.Visual
{
  public class CssClassField : ValidationField
  {
    private readonly IItemRepository itemRepository;

    public CssClassField()
      : this(DependenciesManager.ItemRepository)
    {
    }

    public CssClassField(IItemRepository itemRepository)
      : base(HtmlTextWriterTag.Select.ToString())
    {
      Assert.IsNotNull((object) itemRepository, nameof (itemRepository));
      this.itemRepository = itemRepository;
    }

    public override bool IsCacheable => false;

    protected override void OnPreRender(object sender, EventArgs ev)
    {
      this.Controls.Clear();
      base.OnPreRender(sender, ev);
      this.Controls.Add((Control) new Literal()
      {
        Text = string.Format("<option {0} value='{1}'>{1}</option>", this.DefaultValue == this.EmptyValue ? (object) "selected='selected'" : (object) string.Empty, (object) this.EmptyValue)
      });
      foreach (Item child in this.itemRepository.GetItem(IDs.CssClassRoot).Children)
        this.Controls.Add((Control) new Literal()
        {
          Text = string.Format("<option {0} value='{1}'>{2}</option>", this.DefaultValue == ((BaseItem) child).Fields[Sitecore.Form.Core.Configuration.FieldIDs.MetaDataListItemValue].Value ? (object) "selected='selected'" : (object) string.Empty, (object) ((BaseItem) child).Fields[Sitecore.Form.Core.Configuration.FieldIDs.MetaDataListItemValue].Value, (object) child.DisplayName)
        });
    }
  }
}
