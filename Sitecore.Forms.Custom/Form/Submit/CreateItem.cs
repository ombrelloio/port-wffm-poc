// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Submit.CreateItem
// Assembly: Sitecore.Forms.Custom, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E620A323-75A8-42D2-B304-7093FE781621
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Custom.dll

using Sitecore.Common;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Form.Core.Utility;
using Sitecore.Forms.Core.Data;
using Sitecore.SecurityModel;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using Sitecore.WFFM.Actions.Base;
using Sitecore.Workflows;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Sitecore.Form.Submit
{
  public class CreateItem : WffmSaveAction
  {
    private readonly IResourceManager resourceManager;

    public CreateItem()
      : this(DependenciesManager.ResourceManager)
    {
    }

    public CreateItem(IResourceManager resourceManager)
    {
      this.resourceManager = resourceManager;
      this.CheckSecurity = false;
    }

    public override void Execute(
      ID formId,
      AdaptedResultList adaptedFields,
      ActionCallContext actionCallContext = null,
      params object[] data)
    {
      SecurityDisabler securityDisabler = (SecurityDisabler) null;
      if (!this.CheckSecurity)
        securityDisabler = new SecurityDisabler();
      try
      {
        this.CreateItemByFields(formId, adaptedFields);
      }
      finally
      {
        ((Switcher<SecurityState, SecurityState>) securityDisabler)?.Dispose();
      }
    }

    protected virtual void CreateItemByFields(ID formid, AdaptedResultList fields)
    {
      if (StaticSettings.MasterDatabase == null)
        DependenciesManager.Logger.Warn("The Create Item action : the master database is unavailable", (object) this);
      TemplateItem template = StaticSettings.MasterDatabase.GetTemplate(this.Template);
      Error.AssertNotNull((object) template, string.Format(this.resourceManager.GetString("NOT_FOUND_TEMPLATE"), (object) this.Template));
      Item obj1 = StaticSettings.MasterDatabase.GetItem(this.Destination);
      Error.AssertNotNull((object) obj1, string.Format(this.resourceManager.GetString("NOT_FOUND_ITEM"), (object) this.Destination));
      using (new WorkflowContextStateSwitcher((WorkflowContextState) 2))
      {
        Item obj2 = ItemManager.CreateItem(((CustomItemBase) template).Name, obj1, ((CustomItemBase) template).ID);
        NameValueCollection nameValueCollection = StringUtil.ParseNameValueCollection(this.Mapping, '|', '=');
        obj2.Editing.BeginEdit();
        foreach (AdaptedControlResult field in fields)
        {
          if (nameValueCollection[field.FieldID] != null)
          {
            string str1 = nameValueCollection[field.FieldID];
            if (((BaseItem) obj2).Fields[str1] != null)
            {
              string str2 = string.Join("|", new List<string>(FieldReflectionUtil.GetAdaptedListValue(new FieldItem(StaticSettings.ContextDatabase.GetItem(field.FieldID)), field.Value, false)).ToArray());
              ((BaseItem) obj2).Fields[str1].Value = str2;
              if (str1 == ((object) FieldIDs.DisplayName).ToString())
                obj2.Name = Sitecore.Data.Items.ItemUtil.ProposeValidItemName(field.Value);
            }
            else
              DependenciesManager.Logger.Warn(string.Format("The Create Item action : the template does not contain field: {0}", (object) str1), (object) this);
          }
        }
        obj2.Editing.EndEdit();
      }
    }

    public bool CheckSecurity { get; set; }

    public string Mapping { get; set; }

    public string Destination { get; set; }

    public string Template { set; get; }
  }
}
