// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Data.ActionDefinition
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Form.Core.ContentEditor.Data;
using Sitecore.Globalization;
using Sitecore.WFFM.Abstractions.Actions;
using System.Runtime.Serialization;

namespace Sitecore.Form.Core.Data
{
  [DataContract]
  public class ActionDefinition : IActionDefinition
  {
    public ActionDefinition(string actionid, string parameters)
      : this()
    {
      this.Paramaters = parameters;
      this.ActionID = actionid;
    }

    private ActionDefinition()
    {
    }

    [DataMember]
    public string ActionID { get; set; }

    [DataMember]
    public bool IsClient { get; set; }

    [DataMember]
    public string Paramaters { get; set; }

    [DataMember]
    public string UniqueKey { get; set; }

    public static implicit operator ListItemDefinition(ActionDefinition definition)
    {
      if (definition == null)
        return (ListItemDefinition) null;
      return new ListItemDefinition()
      {
        ItemID = definition.ActionID,
        Parameters = definition.Paramaters,
        Unicid = definition.UniqueKey
      };
    }

    public string GetFailureMessage(bool tryToGetDefaultValue, ID formID) => ((ListItemDefinition) this).GetFailedMessageForLanguage(Language.Current, tryToGetDefaultValue, formID);

    public string GetFailureMessage() => this.GetFailureMessage(true, ID.Null);

    public string GetTitle() => ((ListItemDefinition) this).GetTitle();
  }
}
