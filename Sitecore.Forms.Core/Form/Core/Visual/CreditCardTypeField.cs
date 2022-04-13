// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Visual.CreditCardTypeField
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
  public class CreditCardTypeField : ValidationField
  {
    private readonly IItemRepository itemRepository;

    public CreditCardTypeField()
      : this(DependenciesManager.Resolve<IItemRepository>())
    {
    }

    public CreditCardTypeField(IItemRepository itemRepository)
      : base(HtmlTextWriterTag.Select.ToString())
    {
      Assert.IsNotNull((object) itemRepository, nameof (itemRepository));
      this.itemRepository = itemRepository;
    }

    protected override void OnPreRender(object sender, EventArgs ev)
    {
      base.OnPreRender(sender, ev);
      foreach (Item child in this.itemRepository.GetItem(IDs.CreditCardTypeRoot).Children)
      {
        string str = ((BaseItem) child).Fields[Sitecore.Form.Core.Configuration.FieldIDs.MetaDataListItemValue].Value;
        this.Controls.Add((Control) new Literal()
        {
          Text = string.Format("<option {0} value='{1}' title='{2}'>{3}</option>", str == this.DefaultValue ? (object) "selected='selected'" : (object) string.Empty, (object) str, (object) child.DisplayName, (object) child.DisplayName)
        });
      }
    }
  }
}
