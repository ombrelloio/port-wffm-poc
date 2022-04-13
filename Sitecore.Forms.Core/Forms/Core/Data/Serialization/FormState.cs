// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.Serialization.FormState
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Diagnostics;
using Sitecore.Form.Core.Ascx.Controls;
using Sitecore.WFFM.Abstractions.Actions;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Sitecore.Forms.Core.Data.Serialization
{
  [Serializable]
  public class FormState
  {
    public FormState(SimpleForm form)
    {
      Assert.ArgumentNotNull((object) form, nameof (form));
      this.ID = form.ID;
      this.ControlResults = form.GetChildState();
      this.ValidationState = new List<ValidatorState>();
      foreach (IValidator validator in form.Page.Validators)
        this.ValidationState.Add(new ValidatorState(validator));
      this.FormItemId = form.FormID;
      this.IsSubmitted = false;
    }

    public string ID { get; set; }

    public Sitecore.Data.ID FormItemId { get; set; }

    public List<ControlResult> ControlResults { get; set; }

    public List<ValidatorState> ValidationState { get; set; }

    public bool IsSubmitted { get; set; }
  }
}
