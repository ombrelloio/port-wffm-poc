// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Dependencies.DefaultImplFieldProvider
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Utility;
using Sitecore.WFFM.Abstractions.Data;
using Sitecore.WFFM.Abstractions.Shared;
using System;
using System.Collections.Generic;

namespace Sitecore.Forms.Core.Dependencies
{
  [Serializable]
  public class DefaultImplFieldProvider : IFieldProvider
  {
    private readonly IItemRepository itemRepository;
    private readonly ISettings settings;

    public DefaultImplFieldProvider(IItemRepository itemRepository, ISettings settings)
    {
      Assert.ArgumentNotNull((object) itemRepository, nameof (itemRepository));
      Assert.ArgumentNotNull((object) settings, nameof (settings));
      this.itemRepository = itemRepository;
      this.settings = settings;
    }

    public string GetAdaptedResult(ID fieldID, object value) => FieldReflectionUtil.GetAdaptedResult(fieldID, value);

    public string GetAdaptedValue(ID fieldId, string value) => FieldReflectionUtil.GetAdaptedValue(fieldId, value);

    public string GetAdaptedValue(string fieldId, string value) => FieldReflectionUtil.GetAdaptedValue(fieldId, value);

    public string GetAdaptedValue(IFieldItem fieldItem, string value) => FieldReflectionUtil.GetAdaptedValue(fieldItem, value);

    public string GetFieldDisplayName(string key)
    {
      Assert.ArgumentNotNullOrEmpty(key, key);
      Item obj1 = this.itemRepository.GetItem(key);
      if (obj1 != null && !this.settings.InsertIdToAnalytics)
        return this.itemRepository.CreateFieldItem(obj1).FieldDisplayName;
      string[] strArray = key.Split('/');
      if (strArray.Length == 2)
      {
        Item obj2 = this.itemRepository.GetItem(strArray[0] ?? string.Empty);
        if (obj2 != null && !string.IsNullOrEmpty(strArray[1]))
        {
          if (this.settings.InsertIdToAnalytics)
            return "<scparent>" + strArray[0] + "</scparent>" + strArray[1];
          IFieldItem fieldItem = this.itemRepository.CreateFieldItem(obj2);
          return "<scparent>" + fieldItem.FieldDisplayName + "</scparent>" + fieldItem.GetSubFieldTitle(strArray[1]);
        }
      }
      return key.Trim('{', '}');
    }

    public bool FieldCanListAdapt(IFieldItem field) => FieldReflectionUtil.FieldCanListAdapt(field);

    public IEnumerable<string> ListAdapt(IFieldItem field, string value) => FieldReflectionUtil.ListAdapt(field, value);
  }
}
