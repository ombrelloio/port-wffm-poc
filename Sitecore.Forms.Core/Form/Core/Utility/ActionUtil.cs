// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Utility.ActionUtil
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Microsoft.Ajax.Utilities;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.ContentEditor.Data;
using Sitecore.Form.Core.Data;
using Sitecore.Forms.Core.Data;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Abstractions.ContentEditor;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Dependencies;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Linq;
using System.Web;

namespace Sitecore.Form.Core.Utility
{
    public class ActionUtil
    {
        public static string GetGlobalSaveActions(
          string globalSaveActionsXml,
          string updatedSaveActionsXml)
        {
            ListDefinition globalSaveActions = ListDefinition.Parse(globalSaveActionsXml);
            ListDefinition listDefinition = ListDefinition.Parse(updatedSaveActionsXml);
            Assert.IsNotNull((object)globalSaveActions, "globalSaveActions");
            foreach (var g in listDefinition.Groups)
            {
                foreach (var li in g.ListItems)
                {

                    if (ActionUtil.IsLocalized(li) || ActionUtil.ShouldRemoveLocalized(li))
                    {
                        IListItemDefinition listItem = ActionUtil.GetListItem(globalSaveActions, g.ID, li.Unicid);
                        li.Parameters = listItem?.Parameters ?? (string)null;
                    }
                    li.Parameters = ActionUtil.ClearLocalizedAttribute(li.Parameters);
                    li.Parameters = ActionUtil.SantizeParameters(li.Parameters);
                }
            }
            return listDefinition.ToXml();
        }

        public static string GetLocalizedSaveActions(string updatedSaveActionsXml)
        {
            ListDefinition listDefinition = ListDefinition.Parse(updatedSaveActionsXml);
            foreach (var g in listDefinition.Groups)
            {
                g.DisplayName = (string)null;
                g.OnClick = (string)null;
                g.ListItems = g.ListItems.Where<IListItemDefinition>((Func<IListItemDefinition, bool>)(li => ActionUtil.IsLocalized(li) && !ActionUtil.ShouldRemoveLocalized(li) && !string.IsNullOrEmpty(li.Parameters)));
                foreach (var li in g.ListItems)
                {
                    li.Parameters = ActionUtil.SantizeParameters(li.Parameters);
                };
            };
            return listDefinition.ToXml();
        }

        private static bool IsLocalized(IListItemDefinition li) => li?.Parameters?.Contains(HttpUtility.HtmlEncode("<localized>1</localized>")) ?? false;

        private static bool ShouldRemoveLocalized(IListItemDefinition li) => li?.Parameters?.Contains(HttpUtility.HtmlEncode("<deletelocalized>1</deletelocalized>")) ?? false;

        private static IListItemDefinition GetListItem(
          ListDefinition list,
          string groupId,
          string unicId)
        {
            // ISSUE: explicit non-virtual call
            return list?.Groups.FirstOrDefault((IGroupDefinition g) => g.ID == groupId)?.GetListItem(unicId) ?? null;
        }

        private static string ClearLocalizedAttribute(string parameters)
        {
            if (!string.IsNullOrEmpty(parameters))
            {
                parameters = parameters.Replace(HttpUtility.HtmlEncode("<localized/>"), string.Empty);
                parameters = parameters.Replace(HttpUtility.HtmlEncode("<localized></localized>"), string.Empty);
                parameters = parameters.Replace(HttpUtility.HtmlEncode("<localized>1</localized>"), string.Empty);
                parameters = parameters.Replace(HttpUtility.HtmlEncode("<localized>0</localized>"), string.Empty);
            }
            return parameters;
        }

        private static string SantizeParameters(string parameters)
        {
            if (!string.IsNullOrEmpty(parameters))
            {
                parameters = parameters.Replace(HttpUtility.HtmlEncode("<deletelocalized/>"), string.Empty);
                parameters = parameters.Replace(HttpUtility.HtmlEncode("<deletelocalized></deletelocalized>"), string.Empty);
                parameters = parameters.Replace(HttpUtility.HtmlEncode("<deletelocalized>1</deletelocalized>"), string.Empty);
                parameters = parameters.Replace(HttpUtility.HtmlEncode("<deletelocalized>0</deletelocalized>"), string.Empty);
            }
            return parameters;
        }

        public static string OverrideParameters(
          string globalSaveActionsXml,
          string localizedSaveActionsXml)
        {
            if (string.IsNullOrEmpty(globalSaveActionsXml))
                return (string)null;
            ListDefinition globalSaveActions = ListDefinition.Parse(globalSaveActionsXml);
            if (!string.IsNullOrEmpty(localizedSaveActionsXml))
                foreach (var g in ListDefinition.Parse(localizedSaveActionsXml).Groups)
                {
                    foreach (var li in g.ListItems)
                    {

                        {
                            if (string.IsNullOrEmpty(li.Parameters))
                                break;
                            IGroupDefinition groupDefinition = globalSaveActions.Groups.FirstOrDefault<IGroupDefinition>((Func<IGroupDefinition, bool>)(gg => gg.ID == g.ID));
                            IListItemDefinition listItemDefinition = groupDefinition != null ? groupDefinition.ListItems.FirstOrDefault<IListItemDefinition>((Func<IListItemDefinition, bool>)(liOverride => liOverride.ItemID == li.ItemID && liOverride.Unicid == li.Unicid)) : (IListItemDefinition)null;
                            if (listItemDefinition == null || string.IsNullOrEmpty(li.Parameters))
                                break;
                            listItemDefinition.Parameters = li.Parameters;
                        }
                    };
                }
            return globalSaveActions.ToXml();
        }

        public static ActionState GetActionState(
          IActionItem actionItem,
          string itemId,
          Tracking tracking,
          FormDefinition definition)
        {
            try
            {
                IAction action = DependenciesManager.FactoryObjectsProvider.CreateAction<IAction>(actionItem);
                if (action == null || !DependenciesManager.RequirementsChecker.CheckRequirements(action.GetType()))
                    return ActionState.Disabled;
                Item obj = DependenciesManager.Resolve<IItemRepository>().GetItem(itemId);
                return action.QueryState(new ActionQueryContext((IFormItem)(FormItem)obj, (ITracking)tracking, (IFormDefinition)definition));
            }
            catch (Exception ex)
            {
                DependenciesManager.Logger.Warn(Sitecore.StringExtensions.StringExtensions.FormatWith("Web Forms for Marketers: the '{0}' actionItem cannot be used", new object[1]
                {
          (object) itemId
                }) + ex.Message, (object)ex);
                return ActionState.Hidden;
            }
        }
    }
}
