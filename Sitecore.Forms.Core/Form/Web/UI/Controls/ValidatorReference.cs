// Decompiled with JetBrains decompiler
// Type: Sitecore.Form.Web.UI.Controls.ValidatorReference
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Collections;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Attributes;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Utility;
using Sitecore.Forms.Core.Data;
using Sitecore.Reflection;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sitecore.Form.Web.UI.Controls
{
  [Dummy]
  [PersistChildren(true)]
  internal class ValidatorReference : WebControl
  {
    private static readonly string[] IgnoreProperties = new string[3]
    {
      "text",
      "tooltip",
      "ID"
    };
    private BaseValidator innerValidator;

    public ValidatorReference(ValidationItem validationItem, FieldTypeItem fieldItem)
    {
      this.ValidationItem = validationItem;
      this.FieldTypeItem = fieldItem;
    }

    protected ValidatorReference()
    {
    }

    public string ValidationGroup { get; set; }

    public FieldTypeItem FieldTypeItem { get; private set; }

    public string ValidationID
    {
      get => ((object) ((CustomItemBase) this.ValidationItem).ID).ToString();
      set => this.ValidationItem = new ValidationItem(Sitecore.Context.Database.GetItem(value));
    }

    public Control ControlToValidate { get; set; }

    public string LocParameters { get; set; }

    public string Parameters { get; set; }

    public string ControlTitle { get; set; }

    public BaseValidator InnerValidator
    {
      get
      {
        if (this.innerValidator == null)
        {
          this.innerValidator = (BaseValidator) ReflectionUtil.CreateObject(this.ValidationItem.Assembly, this.ValidationItem.Class, new object[0]);
          Assert.ArgumentNotNull((object) this.ControlToValidate, "control to validate");
          Assert.ArgumentNotNull((object) this.ValidationGroup, "validation group");
          this.innerValidator.ControlToValidate = this.ControlToValidate.ID;
          this.innerValidator.ID = this.ControlToValidate.ID + (object) ((CustomItemBase) this.ValidationItem).InnerItem.ID.ToShortID() + "_validator";
          FieldTypeItem fieldTypeItem = this.FieldTypeItem;
          this.SetDefaultProperties(this.ControlToValidate.GetType());
          ReflectionUtils.SetXmlProperties((object) this.innerValidator, ValidationItemAdapter.AdaptItem(this.ValidationItem), true);
          if (FieldTypeItem.IsFieldItem(((CustomItemBase) fieldTypeItem).InnerItem))
          {
            fieldTypeItem = (FieldTypeItem) new FieldItem(((CustomItemBase) fieldTypeItem).InnerItem);
            ReflectionUtils.SetXmlProperties((object) this.innerValidator, fieldTypeItem.Parameters, true);
            ReflectionUtils.SetXmlProperties((object) this.innerValidator, fieldTypeItem.LocalizedParameters, true, ValidatorReference.IgnoreProperties);
          }
          else
          {
            ReflectionUtils.SetXmlProperties((object) this.innerValidator, this.Parameters, true);
            ReflectionUtils.SetXmlProperties((object) this.innerValidator, this.LocParameters, true, ValidatorReference.IgnoreProperties);
          }
          this.innerValidator.CssClass = this.CssClass;
          this.innerValidator.ValidationGroup = this.ValidationGroup;
          NameValueCollection collection = new NameValueCollection();
          if (!string.IsNullOrEmpty(this.ValidationItem.TrackEvent))
            collection[Sitecore.WFFM.Abstractions.Analytics.Constants.TrackEvent] = this.ValidationItem.TrackEvent;
          collection[Sitecore.WFFM.Abstractions.Constants.Core.Constants.FieldId] = ((object) ((CustomItemBase) this.FieldTypeItem).ID).ToString();
          this.innerValidator.CssClass = string.Join(" ", new string[2]
          {
            this.innerValidator.CssClass,
            NameValueCollectionUtil.GetString(collection)
          });
          bool flag = false;
          if (this.ControlToValidate is BaseUserControl)
          {
            this.innerValidator.Attributes["inner"] = this.ValidationItem.IsInnerControl ? "1" : "0";
            flag = ((BaseUserControl) this.ControlToValidate).SetValidatorProperties(this.innerValidator);
            this.innerValidator.Attributes.Remove("inner");
          }
          if (!flag)
          {
            if (FieldTypeItem.IsFieldItem(((CustomItemBase) fieldTypeItem).InnerItem))
            {
              string fieldDisplayName = ((FieldItem) fieldTypeItem).FieldDisplayName;
              this.innerValidator.ErrorMessage = string.Format(this.innerValidator.ErrorMessage, (object) fieldDisplayName, (object) "{1}", (object) "{2}", (object) "{3}", (object) "{4}", (object) "{5}");
              this.innerValidator.Text = string.Format(this.innerValidator.Text, (object) fieldDisplayName, (object) "{1}", (object) "{2}", (object) "{3}", (object) "{4}", (object) "{5}");
              this.innerValidator.ToolTip = string.Format(this.innerValidator.ToolTip, (object) fieldDisplayName, (object) "{1}", (object) "{2}", (object) "{3}", (object) "{4}", (object) "{5}");
            }
            else
            {
              string str = this.ControlTitle;
              if (string.IsNullOrEmpty(this.ControlTitle))
                str = ((CustomItemBase) fieldTypeItem).Name;
              this.innerValidator.ErrorMessage = string.Format(this.innerValidator.ErrorMessage, (object) str, (object) "{1}", (object) "{2}", (object) "{3}", (object) "{4}", (object) "{5}");
              this.innerValidator.Text = string.Format(this.innerValidator.Text, (object) str, (object) "{1}", (object) "{2}", (object) "{3}", (object) "{4}", (object) "{5}");
              this.innerValidator.ToolTip = string.Format(this.innerValidator.ToolTip, (object) str, (object) "{1}", (object) "{2}", (object) "{3}", (object) "{4}", (object) "{5}");
            }
          }
          this.Controls.Add((Control) this.innerValidator);
        }
        return this.innerValidator;
      }
    }

    protected ValidationItem ValidationItem { get; set; }

    private void SetDefaultProperties(Type type) => ReflectionUtils.SetXmlProperties((object) this.innerValidator, (IEnumerable<Pair<string, string>>) ((IEnumerable<PropertyInfo>) type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)).Where<PropertyInfo>((Func<PropertyInfo, bool>) (property => property.GetGetMethod() != (MethodInfo) null)).Select(property => new
    {
      property = property,
      val = property.GetValue((object) this.ControlToValidate, (object[]) null)
    }).Where(_param1 => _param1.val != null).Select(_param1 => new Pair<string, string>(_param1.property.Name, _param1.val.ToString())).ToList<Pair<string, string>>(), true, ValidatorReference.IgnoreProperties);
  }
}
