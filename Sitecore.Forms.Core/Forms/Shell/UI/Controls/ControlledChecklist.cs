// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Shell.UI.Controls.ControlledChecklist
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Web.UI.Controls;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Forms.Shell.UI.Controls
{
  public class ControlledChecklist : GroupedChecklist
  {
    private const string ScriptPath = "/sitecore/shell/Applications/Modules/Web Forms for Marketers/script/ControlledChecklist.js";
    private readonly IResourceManager resourceManager;

    public ControlledChecklist()
      : this(DependenciesManager.ResourceManager)
    {
    }

    public ControlledChecklist(IResourceManager resourceManager)
    {
      Assert.IsNotNull((object) resourceManager, "Dependency resourceManager is null");
      this.resourceManager = resourceManager;
    }

    protected override void OnInit(EventArgs e)
    {
      ListItem listItem = this.Items.FindByValue("Always");
      if (listItem != null)
        this.Items.Remove(listItem);
      else
        listItem = new ListItem(this.resourceManager.Localize("ALWAYS"), "Always");
      this.Items.Insert(0, listItem);
    }

    protected override void OnPreRender(EventArgs e)
    {
      this.RegisterClientScripts();
      foreach (ListItem listItem in this.Items)
      {
        if (listItem.Value != "Always")
        {
          this.AttachClientHandler(listItem, "onclick", this.ClientSideOnNodeStateChanged);
          this.JoinEventHandler(listItem, "onclick", "Sitecore.Wfm.ControlledChecklist.updateState(this, 0)");
        }
      }
      ListItem listItem1 = this.Items.FindByValue("Always");
      if (listItem1 == null)
      {
        listItem1 = new ListItem(this.resourceManager.Localize("ALWAYS"), "Always");
        this.Items.Insert(0, listItem1);
      }
      this.AttachClientHandler(listItem1, "onclick", this.ClientSideOnNodeStateChanged);
      this.JoinEventHandler(listItem1, "onclick", "Sitecore.Wfm.ControlledChecklist.copyStateToAllSibling(this)");
      base.OnPreRender(e);
    }

    protected virtual void RegisterClientScripts() => this.Page.ClientScript.RegisterClientScriptInclude(nameof (ControlledChecklist), "/sitecore/shell/Applications/Modules/Web Forms for Marketers/script/ControlledChecklist.js");

    protected override void Render(HtmlTextWriter writer)
    {
      if (this.GetManagedSelectedValues().FirstOrDefault<string>((Func<string, bool>) (i => i == "Always")) != null)
        this.Select((Func<ListItem, bool>) (i => true));
      base.Render(writer);
    }

    public IEnumerable<string> GetManagedSelectedValues()
    {
      IEnumerable<string> source = this.GetSelectedItems().Select<ListItem, string>((Func<ListItem, string>) (i => i.Value));
      if (source.Contains<string>("Always"))
        return (IEnumerable<string>) new string[1]
        {
          "Always"
        };
      if (source.Any<string>())
        return source;
      return (IEnumerable<string>) new string[1]
      {
        "Never"
      };
    }

    private void JoinEventHandler(ListItem item, string clientEventName, string script)
    {
      Assert.ArgumentNotNullOrEmpty(clientEventName, nameof (clientEventName));
      item.Attributes[clientEventName] = string.Join(";", new string[2]
      {
        script,
        item.Attributes[clientEventName] ?? string.Empty
      });
    }

    private void AttachClientHandler(ListItem item, string clientEventName, string clientHandler)
    {
      if (string.IsNullOrEmpty(clientHandler))
        return;
      this.JoinEventHandler(item, clientEventName, string.Join(string.Empty, new string[2]
      {
        clientHandler,
        "($(this).up().up().up().up(), this )"
      }));
    }

    public string ClientSideOnNodeStateChanged { get; set; }

    public string SelectedTitle
    {
      get
      {
        if (this.GetManagedSelectedValues().FirstOrDefault<string>((Func<string, bool>) (v => v == "Always")) != null)
          return this.resourceManager.Localize("ALWAYS");
        string str = string.Join(", ", this.GetSelectedItems().Select<ListItem, string>((Func<ListItem, string>) (i => i.Text)).ToArray<string>());
        return string.IsNullOrEmpty(str) ? this.resourceManager.Localize("NEVER") : str;
      }
    }
  }
}
