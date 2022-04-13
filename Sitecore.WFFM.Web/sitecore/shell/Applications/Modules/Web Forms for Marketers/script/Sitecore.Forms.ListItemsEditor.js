(function() {
  Sitecore.Wfm = Sitecore.Wfm || {};

  Sitecore.Wfm.ListEditor = new function() {
    var self = this;

    var tableLength = null;

    this.dictionary = [];

    this.onChangeField = function(sender, event, fieldName) {
      var popupMenu = Sitecore.Wfm.PopupMenu;

      var popupArgs = popupMenu.args; 
      if (popupArgs != null) {
        popupArgs.value = fieldName;
        if (popupArgs.id != 'ctl00_ctl05_ctl00_ctl00_ctl05_ItemTextField') {
          $('ValueFieldLiteral').innerHTML = "[" + fieldName + "]";
        } else {
          $('TextFieldLiteral').innerHTML = "[" + fieldName + "]";
        }

        popupMenu.args = null;
      }

      ListItems.callback(
        "0&queriessessionkey=" + $('ctl00_ctl05_ctl00_ctl00_ctl05_QueryKeyHolder').value + "&querytype=" + $('ctl00_ctl05_ctl00_ctl00_ctl05_SetItemsMode').getValue() +
        '&ctl00_ctl05_ctl00_ctl00_ctl05_ItemValueField=' + $('ctl00_ctl05_ctl00_ctl00_ctl05_ItemValueField').value +
        '&ctl00_ctl05_ctl00_ctl00_ctl05_ItemTextField=' + $('ctl00_ctl05_ctl00_ctl00_ctl05_ItemTextField').value +
        '&XPathQueryEdit=' + $('XPathQueryEdit').value +
        '&SitecoreQueryEdit=' + $('SitecoreQueryEdit').value +
        '&FastQueryEdit=' + $('FastQueryEdit').value
      );

      scForm.browser.closePopups();
      var activePopup = popupMenu.activePopup;
      if (activePopup != null && activePopup.parentNode != null) {
        $(activePopup).remove();
      }
    };

    this.onAddNewItem = function(sender, event) {
      var grid = $('ManualSettingsGrid');

      if (tableLength == null) {
        tableLength = grid.rows.length;
      } else {
        tableLength += 1;
      }

      var position = getPosition(Event.element(event));
      var row = grid.insertRow(position + 1);

      var itemValue = row.insertCell(0);
      itemValue.innerHTML = '<input value="' + $('ctl00_ctl05_ctl00_ctl00_ctl05_EmptyValueListItemHolder').value + '" style="width: 100%;" id="wfmListItem_v' + tableLength + '" class="scWfmListItemEdit scWfmEmpty" itemtype="value" onfocus="javascript:return Sitecore.Wfm.ListEditor.onListItemFocus(this, event)" onblur="javascript:return Sitecore.Wfm.ListEditor.onListItemBlur(this, event)"/>';

      var itemText = row.insertCell(1);
      itemText.innerHTML = '<input value="' + $('ctl00_ctl05_ctl00_ctl00_ctl05_EmptyTextListItemHolder').value + '" style="margin: 0px 0px 0px 5px; width: 100%;" id="wfmListItem_t' + tableLength + '" class="scWfmListItemEdit scWfmEmpty" itemtype="text" onfocus="javascript:return Sitecore.Wfm.ListEditor.onListItemFocus(this, event)" onblur="javascript:return Sitecore.Wfm.ListEditor.onListItemBlur(this, event)"/>';

      if ($('ctl00_ctl05_ctl00_ctl00_ctl05_ShowOnlyValue').value == 1) {
        itemText.style.display = 'none';
      }

      var addButton = row.insertCell(2);
      addButton.innerHTML = grid.rows[1].cells[2].innerHTML;

      var removeButton = row.insertCell(3);
      removeButton.innerHTML = grid.rows[1].cells[3].innerHTML;
    };

    this.onRemoveItem = function(sender, event) {
      var grid = $('ManualSettingsGrid');

      if (grid.rows.length > 2) {
        grid.deleteRow(getPosition(Event.element(event)));
        return;
      }

      grid.select('input').each(function(element) {
        var type = element.readAttribute('itemtype');
        if ((type === 'value' && element.value !== document.querySelector("[id$='EmptyValueListItemHolder']").value)
            || (type === 'text' && element.value != document.querySelector("[id$='EmptyTextListItemHolder']").value)) {
          element.value = '';
        }
      });
    };

    this.onListItemFocus = function(sender) {
      var element = $(sender);
      var itemtype = element.readAttribute('itemtype');
      if (itemtype == 'value' && element.value == document.querySelector("[id$='EmptyValueListItemHolder']").value) {
        element.value = '';
        element.removeClassName('scWfmEmpty');
        return;
      }

      if (itemtype == 'text') {
        var valueElement = element.up().previous().down().value;
        var emptyText = document.querySelector("[id$='EmptyTextListItemHolder']").value;

        if (element.value == emptyText.replace('{0}', '') || element.value == emptyText.replace('{0}', '"' + valueElement + '"')) {
          element.value = '';
          element.removeClassName('scWfmEmpty');
        }
      }
    };

    this.onListItemBlur = function(sender) {
      var element = $(sender);
      var itemtype = element.readAttribute('itemtype');
      if (element.value != '') {
        return;
      }

      var emptyValueText = document.querySelector("[id$='EmptyValueListItemHolder']").value;

      if (itemtype == 'value') {
        element.value = emptyValueText;
        element.addClassName('scWfmEmpty');
        return;
      }

      var valueElement = element.up().previous().down().value;
      var emptyTextText = document.querySelector("[id$='EmptyTextListItemHolder']").value;

      if (valueElement == emptyValueText) {
        element.value = emptyTextText.replace('{0}', '');
        element.addClassName('scWfmEmpty');
      } else {
        element.value = emptyTextText.replace('{0}', '"' + valueElement + '"');
        element.addClassName('scWfmEmpty');
      }
    };

    this.showText = function() {
      showTextColumn();
      return false;
    };

    this.hideText = function() {
      hideTextColumn();
      return false;
    };

    this.listItemsOnUpdate = function() {
      var cols = ListItems.get_table().get_columns();
      if (document.querySelector("[id$='ShowOnlyValue']").value == 1) {
        return;
      }

      if (cols[0].Width > 232) {
        cols[0].Width = 232;
        cols[1].Width = 231;
      }

      ListItems.render();
    };

    this.controlValuesLock = function() {
      if ($('LockValueImage').style.display != 'none') {
        if (confirm(self.dictionary['unlockValues']) == true) {
          $('UnLockValueImage').style.display = '';
          $('LockValueImage').style.display = 'none';

          $$('#ManualSettingsGrid  input[itemtype="value"]').each(function(element) {
            element.enable();
            element.removeClassName("disabled");
            element.up().next().next().next().down().down().show();
            element.up().next().next().next().down().down().next().hide();
          });
        }
      } else {
        $('UnLockValueImage').style.display = 'none';
        $('LockValueImage').style.display = '';

        $$('#ManualSettingsGrid  input[itemtype="value"]').each(function(element) {
          if (element.value != document.querySelector("[id$='EmptyValueListItemHolder']").value) {
            element.disable();
            element.addClassName("disabled");
            element.up().next().next().next().down().down().hide();
            element.up().next().next().next().down().down().next().show();
          }
        });
      }

      return false;
    };
  };

  function getPosition(element) {
    return $(element).up('tr').rowIndex;
  }

  function showTextColumn() {
    document.querySelector("[id$='ShowOnlyValue']").value = 0;

    $$('#ManualSettingsGrid  input[itemtype="text"]').each(function(element) { element.up().show(); });

    document.querySelector("[id$='EnterDifferentTextLink']").hide();
    document.querySelector("[id$='UseOnlyValueTextLink']").show();

    document.querySelector("[id$='ValueLockLink']").up().morph('width:45%');
    $('TextCaptionGrid').up().show();
  }

  function hideTextColumn() {
    document.querySelector("[id$='ShowOnlyValue']").value = 1;

    document.querySelector("[id$='ValueLockLink']").up().morph('width:90%', {
      afterFinish: function() {
        $$('#ManualSettingsGrid  input[itemtype="text"]').each(function(element) { element.up().hide(); });
        $('TextCaptionGrid').up().hide();
        document.querySelector("[id$='EnterDifferentTextLink']").show();
        document.querySelector("[id$='UseOnlyValueTextLink']").hide();
      }
    });
  }
})();