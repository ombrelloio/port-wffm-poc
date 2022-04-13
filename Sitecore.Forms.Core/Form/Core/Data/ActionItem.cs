// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Data.ActionItem
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Sitecore.Form.Core.Data
{
  public class ActionItem : CustomItemBase, IActionItem
  {
    public ActionItem(Sitecore.Data.Items.Item item)
      : this(DependenciesManager.FactoryObjectsProvider, item)
    {
    }

    public ActionItem(IFactoryObjectsProvider factoryObjectsProvider, Sitecore.Data.Items.Item item)
      : base(item)
    {
      Assert.IsNotNull((object) factoryObjectsProvider, nameof (factoryObjectsProvider));
      this.ActionInstance = factoryObjectsProvider.CreateAction<IAction>(this.FactoryObjectName, this.Assembly, this.Class);
    }

    public ActionType ActionType => this.ActionInstance == null ? ActionType.Undefined : this.ActionInstance.ActionType;

    public IAction ActionInstance { get; private set; }

    public string Assembly => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldTypeAssemblyID].Value;

    public string Class => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldTypeClassID].Value;

    public string FactoryObjectName => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.FieldTypeFactoryObjectNameID].Value;

    public string Description => this.InnerItem.Help.Text;

    public string Editor => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.ActionEditorID].Value;

    public string GlobalParameters => string.Join(string.Empty, new string[2]
    {
      this.Parameters,
      this.LocalizedParameters
    });

    public bool IsClientAction => MainUtil.GetBool(((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.IsClientAction].Value, false);

    public string LocalizedParameters => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.ActionLocalizedParametersID].Value;

    public string Parameters => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.ActionParametersID].Value;

    public string QueryString => ((BaseItem) this.InnerItem).Fields[Sitecore.Form.Core.Configuration.FieldIDs.ActionEditorQueryString].Value;

    public string Tooltip => this.InnerItem.Help.ToolTip;

    public string this[ID fieldId] => ((BaseItem) this.InnerItem)[fieldId];

    public static bool IsAction(Sitecore.Data.Items.Item item)
    {
      Assert.ArgumentNotNull((object) item, nameof (item));
      return (item.TemplateID == FormIDs.ActionTemplateID) || ((IEnumerable<TemplateItem>) item.Template.BaseTemplates).FirstOrDefault<TemplateItem>((Func<TemplateItem, bool>) (t => (((CustomItemBase) t).ID == FormIDs.ActionTemplateID))) != null;
    }

    [SpecialName]
    Sitecore.Data.Items.Item IActionItem.InnerItem => this.InnerItem;
  }
}
