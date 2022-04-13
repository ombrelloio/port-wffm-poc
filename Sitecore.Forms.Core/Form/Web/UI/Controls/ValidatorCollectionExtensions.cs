// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.ValidatorCollectionExtensions
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Sitecore.Form.Web.UI.Controls
{
  public static class ValidatorCollectionExtensions
  {
    public static IValidator FirstOrDefault(
      this System.Web.UI.ValidatorCollection collection,
      Func<IValidator, bool> func)
    {
      if (collection != null && collection.Count > 0)
      {
        foreach (IValidator validator in collection)
        {
          if (validator != null && func(validator))
            return validator;
        }
      }
      return (IValidator) null;
    }

    public static void ForEach(this System.Web.UI.ValidatorCollection collection, Action<IValidator> action)
    {
      if (collection == null || collection.Count <= 0)
        return;
      foreach (IValidator validator in collection)
      {
        if (validator != null)
          action(validator);
      }
    }

    public static IEnumerable<IValidator> Where(
      this System.Web.UI.ValidatorCollection collection,
      Func<IValidator, bool> func)
    {
      List<IValidator> validatorList = new List<IValidator>();
      if (collection != null && collection.Count > 0)
      {
        foreach (IValidator validator in collection)
        {
          if (func(validator))
            validatorList.Add(validator);
        }
      }
      return (IEnumerable<IValidator>) validatorList;
    }
  }
}
