// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Update.Steps.AddEventIdToFormTrackingField
// Assembly: Sitecore.WFFM.Update, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 141BB56E-B1B2-4D04-B15D-AD6BA1098ABC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Update.dll

using Sitecore.Analytics;
using Sitecore.Common;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using Sitecore.Marketing.Definitions;
using Sitecore.Marketing.Definitions.Goals;
using Sitecore.Marketing.Definitions.PageEvents;
using System;
using System.Globalization;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Sitecore.WFFM.Update.Steps
{
  public class AddEventIdToFormTrackingField : IPostStepPartial
  {
    private static readonly ID FormTemplateId = new ID("{FFB1DA32-2764-47DB-83B0-95B843546A7E}");

    public void Process() => AddEventIdToFormTrackingField.UpdateFormsTrackingField();

    private static void UpdateFormsTrackingField()
    {
      foreach (Item selectItem in Factory.GetDatabase("master", false).SelectItems(string.Format("/sitecore/system/modules//*[@@templateid='{0}']", (object) AddEventIdToFormTrackingField.FormTemplateId)))
      {
        try
        {
          XDocument node = XDocument.Parse(((BaseItem) selectItem).Fields["__Tracking"].Value);
          foreach (XElement xpathSelectElement in node.XPathSelectElements("//tracking/event"))
          {
            string attributeValue1 = Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xpathSelectElement, "id");
            if (string.IsNullOrEmpty(attributeValue1))
            {
              string attributeValue2 = Sitecore.Extensions.XElementExtensions.XElementExtensions.GetAttributeValue(xpathSelectElement, "name");
              if (!string.IsNullOrEmpty(attributeValue2))
              {
                ID id = ID.Null;
                IGoalDefinition goal = AddEventIdToFormTrackingField.GetGoal(new ID(attributeValue1));
                if (goal != null)
                {
                  id = TypeExtensions.ToID(((IDefinition) goal).Id);
                }
                else
                {
                  IPageEventDefinition pageEvent = Tracker.MarketingDefinitions.PageEvents[attributeValue2];
                  if (pageEvent != null)
                    id = TypeExtensions.ToID(((IDefinition) pageEvent).Id);
                }
                if (id != ID.Null)
                  xpathSelectElement.Remove();
                else
                  xpathSelectElement.Add((object) new XAttribute((XName) "id", (object) id.Guid));
              }
            }
          }
          selectItem.Editing.BeginEdit();
          ((BaseItem) selectItem).Fields["__Tracking"].Value = node.ToString();
          selectItem.Editing.EndEdit();
        }
        catch (Exception ex)
        {
          Log.Error(ex.Message, ex);
        }
      }
    }

    private static IGoalDefinition GetGoal(ID id) => !(ServiceLocator.ServiceProvider.GetService(typeof (IDefinitionManager<IGoalDefinition>)) is IDefinitionManager<IGoalDefinition> service) ? (IGoalDefinition) null : service.Get(id.Guid, CultureInfo.InvariantCulture);
  }
}
