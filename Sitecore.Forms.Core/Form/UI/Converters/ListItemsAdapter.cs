// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.UI.Converters.ListItemsAdapter
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Client.Submit;
using Sitecore.Form.Core.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Sitecore.Form.UI.Converters
{
  public class ListItemsAdapter : IListAdapter
  {
    public List<string> AdaptList(IList list)
    {
      Sitecore.Form.Web.UI.Controls.ListItemCollection source = list as Sitecore.Form.Web.UI.Controls.ListItemCollection;
      Assert.ArgumentNotNull((object) source, "items");
      return source.Cast<ListItem>().Select<ListItem, string>((Func<ListItem, string>) (item => item.Value)).ToList<string>();
    }

    public virtual IEnumerable<string> AdaptList(string value) => ParametersUtil.XmlToStringArray(value);
  }
}
