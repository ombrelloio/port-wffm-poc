// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.AddContactToContactListPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using ComponentArt.Web.UI;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Utility;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.Forms.Shell.UI.Controls;
using Sitecore.Marketing.Definitions;
using Sitecore.Marketing.Definitions.ContactLists;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public class AddContactToContactListPage : EditorBase
  {
    private const string ContactsListKey = "ContactsLists";
    private readonly IResourceManager resourceManager;
    private IQueryable<DefinitionResult<IContactListDefinition>> _contactList;
    protected ComboBox ConditionCombobox;
    protected ControlledChecklist ConditionList;
    private readonly Sitecore.Web.UI.HtmlControls.Literal ConditionText;
    private readonly Sitecore.Web.UI.HtmlControls.Literal ContactListHeader;
    protected CheckBoxList ContactListsBox;

    public AddContactToContactListPage()
      : this(DependenciesManager.ResourceManager)
    {
    }

    public AddContactToContactListPage(IResourceManager resourceManager)
    {
      Assert.IsNotNull((object) resourceManager, nameof (resourceManager));
      this.resourceManager = resourceManager;
    }

    public string RestrictedFieldTypes => Sitecore.Web.WebUtil.GetQueryString(nameof (RestrictedFieldTypes), "{1F09D460-200C-4C94-9673-488667FF75D1}|{1AD5CA6E-8A92-49F0-889C-D082F2849FBD}|{7FB270BE-FEFC-49C3-8CB4-947878C099E5}");

    public IQueryable<DefinitionResult<IContactListDefinition>> ContactsLists
    {
      get
      {
        IDefinitionManager<IContactListDefinition> service = (IDefinitionManager<IContactListDefinition>) ServiceLocator.ServiceProvider.GetService(typeof (IDefinitionManager<IContactListDefinition>));
        if (service == null)
          return (IQueryable<DefinitionResult<IContactListDefinition>>) null;
        IQueryable<DefinitionResult<IContactListDefinition>> queryable = ((IEnumerable<DefinitionResult<IContactListDefinition>>) service.GetAll<IContactListDefinition>(CultureInfo.InvariantCulture, new RetrievalParameters<IContactListDefinition, IContactListDefinition>((Expression<Func<IContactListDefinition, IContactListDefinition>>) null, 1, int.MaxValue, true, false), false)).AsQueryable<DefinitionResult<IContactListDefinition>>();
        return this._contactList ?? (this._contactList = queryable);
      }
    }

    protected override void OnInit(EventArgs e)
    {
      this.ConditionList.AddRange(ConditionalStatementUtil.GetConditionalItems(this.CurrentForm));
      this.ConditionList.SelectRange(this.GetValueByKey("ExecuteWhen", "Always"));
      this.ConditionCombobox.Text = this.ConditionList.SelectedTitle;
      base.OnInit(e);
    }

    protected override void OnLoad(EventArgs e)
    {
            base.OnLoad(e);
            if (Sitecore.Context.ClientPage.IsEvent)
            {
                return;
            }
            string valueByKey = GetValueByKey("ContactsLists");
            List<string> selected = ((valueByKey == null) ? new List<string>() : valueByKey.Split(',').ToList());
            foreach (System.Web.UI.WebControls.ListItem item in ContactsLists.Select((DefinitionResult<IContactListDefinition> contactList) => new System.Web.UI.WebControls.ListItem
            {
                Value = ((object)((IDefinition)contactList.Data).Id).ToString(),
                Text = ((IDefinition)contactList.Data).Alias,
                Selected = selected.Contains(((object)((IDefinition)contactList.Data).Id).ToString())
            }))
            {
                ContactListsBox.Items.Add(item);
            }
        }

    protected override void Localize()
    {
      this.Header = this.resourceManager.Localize("ADD_CONTACT_TO_CONTACT_LIST");
      this.Text = this.resourceManager.Localize("SELECT_THE_CONTACT_LIST");
      this.ContactListHeader.Text = this.resourceManager.Localize("CONTACT_LISTS");
      this.ConditionText.Text = this.resourceManager.Localize("CONDITION");
    }

    protected override void SaveValues()
    {
      base.SaveValues();
      this.SetValue("ContactsLists", string.Join(",", (IEnumerable<string>) this.ContactListsBox.Items.Where((Func<System.Web.UI.WebControls.ListItem, bool>) (x => x.Selected)).Select<System.Web.UI.WebControls.ListItem, string>((Func<System.Web.UI.WebControls.ListItem, string>) (x => x.Value)).ToList<string>()));
      this.SetValue("ExecuteWhen", string.Join("|", this.ConditionList.GetManagedSelectedValues().ToArray<string>()));
    }
  }
}
