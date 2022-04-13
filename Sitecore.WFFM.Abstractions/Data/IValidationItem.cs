// Decompiled with JetBrains decompiler
// Type: Sitecore.WFFM.Abstractions.Data.IValidationItem
// Assembly: Sitecore.WFFM.Abstractions, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 893E7AF0-E7C2-4A02-B47B-5687A98493FC
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.WFFM.Abstractions.dll

using Sitecore.Data;
using System.Web.UI.WebControls;

namespace Sitecore.WFFM.Abstractions.Data
{
  public interface IValidationItem
  {
    string Assembly { get; }

    string Class { get; }

    ValidatorDisplay Display { get; }

    bool EnableClientScript { get; }

    string ErrorMessage { get; }

    string GlobalParameters { get; }

    bool IsInnerControl { get; }

    string LocalizedParameters { get; }

    string Params { get; }

    string Text { get; }

    string TrackEvent { get; }

    string ValidationExpression { get; }

    Database Database { get; }

    string DisplayName { get; }

    string Icon { get; }

    ID ID { get; }

    Sitecore.Data.Items.Item InnerItem { get; }

    string Name { get; }

    string this[ID fieldID] { get; set; }

    string this[int fieldIndex] { get; }

    string this[string fieldName] { get; set; }

    void BeginEdit();

    void EndEdit();
  }
}
