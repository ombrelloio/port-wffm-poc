// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Core.Data.FormModel
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Newtonsoft.Json;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Sitecore.Form.Core.Data
{
  internal class FormModel
  {
    public FormModel()
    {
    }

    public FormModel(bool analytics) => this.Analytics = analytics;

    [JsonProperty("analytics")]
    [DefaultValue(false)]
    public bool Analytics { get; set; }

    [JsonProperty("fields")]
    [DefaultValue(null)]
    public Dictionary<string, Dictionary<string, string>>[] Fields { get; set; }

    public string Get(string fieldId, string property)
    {
      Dictionary<string, Dictionary<string, string>> dictionary = this.Get(fieldId);
      return dictionary != null && dictionary.ContainsKey(property) && dictionary[property].ContainsKey("v") ? dictionary[property]["v"] : string.Empty;
    }

    public Dictionary<string, Dictionary<string, string>> Get(string fieldId) => this.Fields != null ? ((IEnumerable<Dictionary<string, Dictionary<string, string>>>) this.Fields).FirstOrDefault<Dictionary<string, Dictionary<string, string>>>((Func<Dictionary<string, Dictionary<string, string>>, bool>) (f => f.Keys.Contains<string>("id") && f["id"].ContainsKey("v") && f["id"]["v"] == fieldId)) : new Dictionary<string, Dictionary<string, string>>();

    public void Set(string fieldId, string property, string value) => this.Set(fieldId, property, value, (string) null);

    public void Set(string fieldId, string property, string value, string text)
    {
      Assert.ArgumentNotNullOrEmpty(property, nameof (property));
      Dictionary<string, Dictionary<string, string>> dict = this.Get(fieldId);
      if (dict == null)
      {
        dict = new Dictionary<string, Dictionary<string, string>>();
        dict.Add("id", new Dictionary<string, string>()
        {
          {
            "v",
            fieldId
          }
        });
        this.Fields = ((IEnumerable<Dictionary<string, Dictionary<string, string>>>) this.Fields).Union<Dictionary<string, Dictionary<string, string>>>((IEnumerable<Dictionary<string, Dictionary<string, string>>>) new Dictionary<string, Dictionary<string, string>>[1]
        {
          dict
        }).ToArray<Dictionary<string, Dictionary<string, string>>>();
      }
      dict.Set<string, Dictionary<string, string>>(property, new Dictionary<string, string>()
      {
        {
          "v",
          value
        }
      });
      if (string.IsNullOrEmpty(text))
        return;
      dict[property].Add("t", text);
    }
  }
}
