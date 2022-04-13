// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Mvc.ViewModels.Fields.DropListField
// Assembly: Sitecore.Forms.Mvc, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1AD2DE3-BABE-4597-B643-AA577F98B8D6
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Mvc.dll

using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sitecore.Forms.Mvc.ViewModels.Fields
{
  public class DropListField : RadioListField
  {
    public DropListField() => this.EmptyChoise = true;

    public bool EmptyChoise { get; set; }

    public override void Initialize()
    {
      if (this.Parameters.ContainsKey("emptychoice"))
        this.EmptyChoise = string.Equals(this.Parameters["emptychoice"], "yes", StringComparison.OrdinalIgnoreCase);
      if (this.Items == null)
        this.Items = new List<SelectListItem>();
      if (this.EmptyChoise)
        this.Items.Insert(0, new SelectListItem()
        {
          Text = string.Empty,
          Value = string.Empty
        });
      base.Initialize();
    }
  }
}
