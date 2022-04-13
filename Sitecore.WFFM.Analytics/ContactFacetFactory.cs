// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Analytics.ContactFacetFactory
// Assembly: Sitecore.WFFM.Analytics, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C8CEE62-02B4-499C-A460-15C00E7B925E
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Analytics.dll

using Sitecore.Analytics.Model.Framework;
using Sitecore.Analytics.Tracking;
using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.WFFM.Abstractions.Analytics;
using Sitecore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace Sitecore.WFFM.Analytics
{
  public class ContactFacetFactory : IContactFacetFactory
    {
    private readonly Dictionary<Type, Type> _typeMap;

    public ContactFacetFactory()
    {
      string path = "model/entities/contact/facets";
      this._typeMap = this.LoadFacetsTypeMap("model/elements");
      this.ContactFacets = this.GetFacets(path);
    }

    public Dictionary<string, IFacet> ContactFacets { get; }

    public void SetFacetValue(
      Contact contact,
      string key,
      string facetXpath,
      string facetValue,
      bool overwrite = true)
    {
            //TODO: Must be reworked
      //Assert.ArgumentNotNull((object) contact, nameof (contact));
      //Assert.ArgumentNotNullOrEmpty(facetXpath, nameof (facetXpath));
      //Assert.ArgumentNotNull((object) facetValue, nameof (facetValue));
      //string key1 = facetXpath.Split('/')[0];
      //IFacet facet = contact.Facets[key1];
      //this.SetFacetMember((IElement) facet, this.GetFacetMembers((IElement) facet), key, facetXpath.Remove(0, key1.Length + 1), facetValue, overwrite);
      //PropertyInfo property = ((object) facet).GetType().GetProperty("Preferred");
      //if (!(property != (PropertyInfo) null) || !string.IsNullOrEmpty(property.GetValue((object) facet) as string))
      //  return;
      //property.SetValue((object) facet, (object) key);
    }

    public IElement CreateElement(Type type) => (IElement) Activator.CreateInstance(this._typeMap[type]);

    public IEnumerable<IModelMember> GetFacetMembers(IElement element)
    {
            ////TODO: Must be reworked
            return Array.Empty<IModelMember>();
      //      Assert.ArgumentNotNull((object) element, nameof (element));
      //PropertyInfo property = ((object) element).GetType().GetProperty("Members", BindingFlags.Instance | BindingFlags.NonPublic);
      //return !(property != (PropertyInfo) null) ? (IEnumerable<IModelMember>) null : (IEnumerable<IModelMember>) (property.GetValue((object) element) as IModelMemberCollection);
    }

    private Dictionary<Type, Type> LoadFacetsTypeMap(string path)
    {
            //TODO: Must be reworked
            return new Dictionary<Type, Type>();
      //      Assert.ArgumentNotNullOrEmpty(path, nameof (path));
      //XmlNode configNode = Factory.GetConfigNode(path);
      //Dictionary<Type, Type> dictionary = new Dictionary<Type, Type>();
      //if (configNode != null)
      //{
      //  foreach (XmlNode childNode in configNode.ChildNodes)
      //  {
      //    if (childNode.Name == "element")
      //    {
      //      Type type1 = Type.GetType(XmlUtil.GetAttribute("interface", childNode, true));
      //      Assert.IsNotNull((object) type1, "attribute");
      //      Type type2 = Type.GetType(XmlUtil.GetAttribute("implementation", childNode, true), true);
      //      dictionary.Add(type1, type2);
      //    }
      //  }
      //}
      //return dictionary;
    }

    private IEnumerable<KeyValuePair<string, Type>> LoadFacetMap(
      string path)
    {
            //TODO: Must be reworked
            return Array.Empty<KeyValuePair<string, Type>>();
      //      Assert.ArgumentNotNullOrEmpty(path, nameof (path));
      //XmlNode configNode = Factory.GetConfigNode(path);
      //Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
      //if (configNode != null)
      //{
      //  foreach (XmlNode childNode in configNode.ChildNodes)
      //  {
      //    if (childNode.Name == "facet")
      //    {
      //      string attribute = XmlUtil.GetAttribute("name", childNode, true);
      //      Type type = Type.GetType(XmlUtil.GetAttribute("contract", childNode, true), true);
      //      dictionary.Add(attribute, type);
      //    }
      //  }
      //}
      //return (IEnumerable<KeyValuePair<string, Type>>) dictionary;
    }

    private void SetFacetMember(
      IElement facet,
      IEnumerable<IModelMember> members,
      string key,
      string path,
      string value,
      bool overwrite = true)
    {
            //TODO: Must be reworked
      //      Assert.ArgumentNotNull((object) facet, nameof (members));
      //Assert.ArgumentNotNull((object) members, nameof (members));
      //Assert.ArgumentNotNullOrEmpty(path, nameof (path));
      //Assert.ArgumentNotNullOrEmpty(path, nameof (key));
      //Assert.ArgumentNotNull((object) value, nameof (value));
      //Type type = ((object) facet).GetType();
      //string memberName = path.Split('/')[0];
      //IModelMember imodelMember = members.FirstOrDefault<IModelMember>((Func<IModelMember, bool>) (x => x.Name == memberName));
      //Assert.IsNotNull((object) imodelMember, "memberFiled");
      //if (string.Equals(memberName, "Entries", StringComparison.OrdinalIgnoreCase))
      //{
      //  IElementDictionary<IElement> ielementDictionary = type.GetProperty("Entries", BindingFlags.Instance | BindingFlags.Public).GetValue((object) facet) as IElementDictionary<IElement>;
      //  Assert.IsNotNull((object) ielementDictionary, "Can't get facet entries.");
      //  IElement ielement;
      //  if (ielementDictionary.Keys.FirstOrDefault<string>((Func<string, bool>) (x => string.Equals(x, key, StringComparison.InvariantCultureIgnoreCase))) != null)
      //  {
      //    ielement = ielementDictionary[key];
      //  }
      //  else
      //  {
      //    object obj = ((object) ielementDictionary).GetType().GetMethod("Create", BindingFlags.Instance | BindingFlags.Public).Invoke((object) ielementDictionary, new object[1]
      //    {
      //      (object) key
      //    });
      //    Assert.IsNotNull(obj, "Can't create entry.");
      //    ielement = obj as IElement;
      //  }
      //  this.SetFacetMember(ielement, this.GetFacetMembers(ielement), key, path.Remove(0, memberName.Length + 1), value, overwrite);
      //}
      //if (imodelMember is IModelAttributeMember)
      //  this.SetAttribute(facet, imodelMember.Name, value, overwrite);
      //if (!(imodelMember is IModelElementMember))
      //  return;
      //object obj1 = type.GetProperty(memberName, BindingFlags.Instance | BindingFlags.Public).GetValue((object) facet);
      //Assert.IsNotNull(obj1, string.Format("Can't get facet element {0}.", (object) memberName));
      //IElement ielement1 = obj1 as IElement;
      //this.SetFacetMember(ielement1, this.GetFacetMembers(ielement1), key, path.Remove(0, memberName.Length + 1), value, overwrite);
    }

    private void SetAttribute(IElement element, string name, string value, bool overwrite)
    {
            //TODO: Must be reworked
      //      Assert.ArgumentNotNull((object) element, nameof (element));
      //Assert.ArgumentNotNullOrEmpty(name, nameof (name));
      //PropertyInfo property = ((object) element).GetType().GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
      //Assert.IsNotNull((object) property, "attribute");
      //if (property.GetValue((object) element) != null && !overwrite)
      //  return;
      //if (property.PropertyType.IsGenericType)
      //{
      //  DateTime dateTime = DateUtil.IsoDateToDateTime(value);
      //  Assert.IsNotNull((object) dateTime, "Date");
      //  property.SetValue((object) element, System.Convert.ChangeType((object) dateTime, ((IEnumerable<Type>) property.PropertyType.GetGenericArguments()).First<Type>()));
      //}
      //else
      //  property.SetValue((object) element, System.Convert.ChangeType((object) value, property.PropertyType));
    }

    private Dictionary<string, IFacet> GetFacets(string path)
    {
            //TODO: Must be reworked
            return new Dictionary<string, IFacet>();
      //      IEnumerable<KeyValuePair<string, Type>> keyValuePairs = this.LoadFacetMap(path);
      //Dictionary<string, IFacet> dictionary = new Dictionary<string, IFacet>();
      //foreach (KeyValuePair<string, Type> keyValuePair in keyValuePairs)
      //{
      //  string key = keyValuePair.Key;
      //  if (!dictionary.ContainsKey(key) && this._typeMap.ContainsKey(keyValuePair.Value))
      //  {
      //    IFacet instance = (IFacet) Activator.CreateInstance(this._typeMap[keyValuePair.Value]);
      //    if (instance != null)
      //      dictionary.Add(key, instance);
      //  }
      //}
      //return dictionary;
    }
  }
}
