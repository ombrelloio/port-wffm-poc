// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.Helpers.ContactFacetsHelper
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Analytics.Model.Framework;
using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.WFFM.Abstractions.Dependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Sitecore.Forms.Core.Data.Helpers
{
    //TODO: must be reworked
    public static class ContactFacetsHelper
  {
    public static IContactFacetFactory FacetFactory => DependenciesManager.FacetFactory.GetContactFacetFactory();

    public static string GetContactFacetsXmlTree()
    {
            
      XElement parent = new XElement((XName) "Top");
      ContactFacetsHelper.CreateNode(parent, "facets");
      parent.Add((object) new XElement((XName) "isFolder", (object) true));
      parent.Add((object) new XElement((XName) "expand", (object) true));
      foreach (KeyValuePair<string, IFacet> contactFacet in ContactFacetsHelper.FacetFactory.ContactFacets)
      {
        XElement node = new XElement((XName) "children");
        parent.Add((object) node);
        ContactFacetsHelper.ExpandElement(node, (IElement) contactFacet.Value, contactFacet.Key);
      }
      return DependenciesManager.ConvertionUtil.ConvertToJson((object) parent);
    }

    private static void CreateNode(XElement parent, string title, bool folder = true, string key = "")
    {
      Assert.ArgumentNotNullOrEmpty(title, nameof (title));
      parent.Add((object) new XElement((XName) nameof (title), (object) title));
      if (folder)
      {
        parent.Add((object) new XElement((XName) "isFolder", (object) true));
        parent.Add((object) new XElement((XName) "expand", (object) true));
      }
      if (string.IsNullOrEmpty(key.Trim()))
        return;
      parent.Add((object) new XElement((XName) nameof (key), (object) key.Trim()));
    }

    private static void ExpandElement(XElement node, IElement element, string title, string key = "")
    {
			//TODO: Must be reworked
			//Assert.ArgumentNotNull(node, "node");
			//Assert.ArgumentNotNull(element, "element");
			//if (string.IsNullOrEmpty(key.Trim()))
			//{
			//	key = title;
			//}
			//IEnumerable<IModelMember> facetMembers = FacetFactory.GetFacetMembers(element);
			//if (facetMembers == null)
			//{
			//	return;
			//}
			//if (!string.IsNullOrEmpty(title))
			//{
			//	CreateNode(node, title, folder: true, key);
			//}
			//foreach (IModelMember item in facetMembers)
			//{
			//	if (item is IModelAttributeMember && !string.Equals(item.get_Name(), "Preferred", StringComparison.OrdinalIgnoreCase))
			//	{
			//		XElement xElement = new XElement("children");
			//		node.Add(xElement);
			//		CreateNode(xElement, item.get_Name(), folder: false, key + "/" + item.get_Name());
			//	}
			//	else if (item is IModelElementMember)
			//	{
			//		IModelElementMember val = (IModelElementMember)(object)((item is IModelElementMember) ? item : null);
			//		XElement xElement2 = new XElement("children");
			//		node.Add(xElement2);
			//		ExpandElement(xElement2, val.get_Element(), ((IModelMember)val).get_Name(), key + "/" + ((IModelMember)val).get_Name());
			//	}
			//	else if (item is IModelDictionaryMember || item is IModelCollectionMember)
			//	{
			//		IModelDictionaryMember val2 = (IModelDictionaryMember)(object)((item is IModelDictionaryMember) ? item : null);
			//		Type type = ((val2 == null) ? ((object)((IModelCollectionMember)((item is IModelCollectionMember) ? item : null)).get_Elements()).GetType().GetGenericArguments().Single() : ((object)val2.get_Elements()).GetType().GetGenericArguments().Single());
			//		if (!(type == null))
			//		{
			//			ExpandElement(node, FacetFactory.CreateElement(type), string.Empty, key + "/Entries");
			//		}
			//	}
			//}
		}
  }
}
