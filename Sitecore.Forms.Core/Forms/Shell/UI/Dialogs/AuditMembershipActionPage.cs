// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Dialogs.AuditMembershipActionPage
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Data.Templates;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Configuration;
using Sitecore.Forms.Core.Data;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Dialogs
{
  public abstract class AuditMembershipActionPage : MemberhipActionPage
  {
    private readonly IResourceManager resourceManager;
    private readonly IItemRepository itemRepository;
    protected DropDownList AuditField;
    protected Groupbox AuditGroupbox;
    protected Sitecore.Web.UI.HtmlControls.Literal SaveAuditLiteral;

    public AuditMembershipActionPage()
      : this(DependenciesManager.ResourceManager, DependenciesManager.Resolve<IItemRepository>())
    {
    }

    public AuditMembershipActionPage(
      IResourceManager resourceManager,
      IItemRepository itemRepository)
    {
      Assert.IsNotNull((object) resourceManager, "Dependency resourceManager is null");
      Assert.IsNotNull((object) itemRepository, nameof (itemRepository));
      this.resourceManager = resourceManager;
      this.itemRepository = itemRepository;
    }

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.FillAudit(this.AuditField, this.GetValueByKey("AuditField"));
    }

    protected override void Localize()
    {
      base.Localize();
      ((HeaderedItemsControl) this.AuditGroupbox).Header = this.resourceManager.Localize("AUDIT");
      this.SaveAuditLiteral.Text = this.resourceManager.Localize("SAVE_AUDIT_INFORMATION_TO");
    }

    protected void FillAudit(DropDownList auditList, string defaultValue)
    {
      Assert.ArgumentNotNull((object) auditList, nameof (auditList));
      auditList.Items.Clear();
      System.Web.UI.WebControls.ListItem listItem1 = new System.Web.UI.WebControls.ListItem(this.resourceManager.Localize("DO_NOT_SAVE"), "NoAudit");
      auditList.Items.Add(listItem1);
      Item innerItem = this.itemRepository.GetItem(this.CurrentID);
      if (innerItem == null)
        return;
      foreach (Sitecore.Data.Templates.TemplateField field in TemplateManager.GetTemplate(StaticSettings.CoreDatabase.GetItem(new FormItem(innerItem).ProfileItem).TemplateID, StaticSettings.CoreDatabase).GetFields(false))
      {
        if (Settings.AuditAllowedTypes.Contains(string.Join(string.Empty, new string[3]
        {
          "|",
          field.Type,
          "|"
        })))
        {
          System.Web.UI.WebControls.ListItem listItem2 = new System.Web.UI.WebControls.ListItem(field.Name, field.Name);
          if (defaultValue == field.Name)
            listItem2.Selected = true;
          auditList.Items.Add(listItem2);
        }
      }
    }

    protected override void SaveValues()
    {
      base.SaveValues();
      this.SetValue("AuditField", this.AuditField.SelectedValue);
    }
  }
}
