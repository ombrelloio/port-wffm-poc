// Decompiled with JetBrains decompiler
// Type: Sitecore.Forms.Core.Data.FormItemSynchronizer
// Assembly: Sitecore.Forms.Core, Version=9.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1F92BA8-6EC8-4C65-ABE2-72A750000F37
// Assembly location: C:\Work\Assemblies\WFFM\Sitecore.Forms.Core.dll

using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Data;
using Sitecore.Form.Core.Utility;
using Sitecore.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace Sitecore.Forms.Core.Data
{
  internal class FormItemSynchronizer
  {
    private readonly Database database;
    private readonly FormDefinition definition;
    private readonly Language language;
    private Item formItem;

    public FormItemSynchronizer(Database database, Language language, FormDefinition definition)
    {
      Assert.ArgumentNotNull((object) database, nameof (database));
      Assert.ArgumentNotNull((object) language, nameof (language));
      Assert.ArgumentNotNull((object) definition, nameof (definition));
      this.database = database;
      this.language = language;
      this.definition = definition;
    }

    public Item Form
    {
      get
      {
        if (this.formItem == null && !string.IsNullOrEmpty(this.definition.FormID))
          this.formItem = this.database.GetItem(this.definition.FormID, this.language);
        return this.formItem;
      }
    }

    public static ID FindMatch(ID oldID, FormItem oldForm, FormItem newForm)
    {
      Assert.ArgumentNotNull((object) oldID, nameof (oldID));
      Assert.ArgumentNotNull((object) oldForm, nameof (oldForm));
      Assert.ArgumentNotNull((object) newForm, nameof (newForm));
      Item obj = oldForm.Database.GetItem(oldID);
      if (obj != null && obj.Paths.LongID.Contains(((object) oldForm.ID).ToString()))
      {
        if (obj.ParentID == oldForm.ID)
        {
          int num = oldForm.InnerItem.Children.IndexOf(obj);
          if (num > -1 && ((IEnumerable<Item>) newForm.InnerItem.Children).Count<Item>() > num)
            return newForm.InnerItem.Children[num].ID;
        }
        if (obj.Parent.ParentID == oldForm.ID)
        {
          int num1 = oldForm.InnerItem.Children.IndexOf(obj.Parent);
          int num2 = obj.Parent.Children.IndexOf(obj);
          if (num1 > -1 && num2 > -1 && ((IEnumerable<Item>) newForm.InnerItem.Children).Count<Item>() > num1 && ((IEnumerable<Item>) newForm.InnerItem.Children[num1].Children).Count<Item>() > num2)
            return newForm.InnerItem.Children[num1].Children[num2].ID;
        }
      }
      return ID.Null;
    }

    public static void UpdateIDReferences(FormItem oldForm, FormItem newForm)
    {
      Assert.ArgumentNotNull((object) oldForm, nameof (oldForm));
      Assert.ArgumentNotNull((object) newForm, nameof (newForm));
      newForm.SaveActions = FormItemSynchronizer.UpdateIDs(newForm.SaveActions, oldForm, newForm);
      newForm.CheckActions = FormItemSynchronizer.UpdateIDs(newForm.CheckActions, oldForm, newForm);
    }

    public void Synchronize()
    {
      foreach (SectionDefinition section in this.definition.Sections)
      {
        Item sectionItem = (Item) null;
        if (!this.DeleteSectionIsEmpty(section))
          sectionItem = this.UpdateSection(section);
        else if (!string.IsNullOrEmpty(section.SectionID))
          sectionItem = section.UpdateSharedFields(this.database, (Item) null);
        foreach (FieldDefinition field in section.Fields)
          this.SynchronizeField(sectionItem, field);
        if (sectionItem != null && !sectionItem.HasChildren)
          sectionItem.Delete();
      }
    }

    protected bool DeleteSectionIsEmpty(SectionDefinition section)
    {
      if (section != null)
      {
        bool flag = section.Deleted == "1";
        if (string.IsNullOrEmpty(section.Name))
        {
          if (section.IsHasOnlyEmptyField)
            section.Deleted = "1";
          else
            section.Name = string.Empty;
        }
        if (section.Deleted == "1")
        {
          Item obj = this.database.GetItem(section.SectionID, this.language);
          if (obj != null)
          {
            if (flag)
              obj.Delete();
            else
              Sitecore.Form.Core.Utility.Utils.RemoveVersionOrItem(obj);
          }
          return true;
        }
      }
      return false;
    }

    protected bool DeleteFieldIsEmpty(FieldDefinition field)
    {
      if (field != null)
      {
        Item obj = this.database.GetItem(field.FieldID, this.language);
        if (field.Deleted == "1")
        {
          obj?.Delete();
          return true;
        }
        if (string.IsNullOrEmpty(field.Name))
        {
          field.Deleted = "1";
          if (obj != null)
            Sitecore.Form.Core.Utility.Utils.RemoveVersionOrItem(obj);
          return true;
        }
      }
      return false;
    }

    protected void UpdateField(FieldDefinition field, Item sectionItem)
    {
      Assert.ArgumentNotNull((object) field, nameof (field));
      field.CreateCorrespondingItem(sectionItem ?? this.Form, this.language);
    }

    protected Item UpdateSection(SectionDefinition section) => section != null && this.Form != null && (!string.IsNullOrEmpty(section.SectionID) || this.definition.IsHasVisibleSection()) ? section.CreateCorrespondingItem(this.Form, this.language) : (Item) null;

    private static string UpdateIDs(string text, FormItem oldForm, FormItem newForm)
    {
      string input = text;
      if (!string.IsNullOrEmpty(input))
      {
        foreach (ID id in IDUtil.GetIDs(input))
        {
          ID match = FormItemSynchronizer.FindMatch(id, oldForm, newForm);
          if (!ID.IsNullOrEmpty(match))
            input = input.Replace(((object) id).ToString(), ((object) match).ToString());
        }
      }
      return input;
    }

    private void SynchronizeField(Item sectionItem, FieldDefinition field)
    {
      if (!this.DeleteFieldIsEmpty(field))
        this.UpdateField(field, sectionItem);
      else
        field.UpdateSharedFields(sectionItem, (Item) null, this.database);
    }
  }
}
