// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Pipelines.FormSubmit.SaveFailedArgs
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.WFFM.Abstractions.Actions;
using System;

namespace Sitecore.Form.Core.Pipelines.FormSubmit
{
  public class SaveFailedArgs : FormSubmitFailedArgs
  {
    public SaveFailedArgs(ID formID, AdaptedResultList fields, string actionFailed, Exception ex)
      : this(formID, fields, ID.Parse(actionFailed), ex)
    {
    }

    public SaveFailedArgs(ID formID, AdaptedResultList fields, ID actionFailed, Exception ex)
      : base(formID, fields, actionFailed, ex)
    {
    }
  }
}
