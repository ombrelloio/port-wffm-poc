Sitecore.PropertiesBuilder = new function() {
  var self = this;

  this.SourceMarker = "source:";
  this.editors = [];

  this.updateProperties = function(sender, oldValue, evt) {
    var properties = $($F("Active") + "_field_properties");
    var locproperties = $($F("Active") + "_field_properties_loc");

    var type = $($F("Active") + "_field_type");

    if (!evt) {
      evt = Event.fire(type, "click", "");
    }

    scForm.postEvent(this, evt, 'CompareTypes("' + type.id + '", "' + type.options[type.selectedIndex].id + '", "' + type.options[oldValue].id + '","' + encodeURIComponent(properties.value + locproperties.value) + '")');
  };

  this.changeType = function(res, id, newValue, oldValue) {
    if (res == "no") {

      var options = $A($(id).options);

      options.each(function(element) {
        if (element.id == oldValue) {
          $(id).selectedIndex = element.index;
          throw $break;
        }
      });

      return;
    }

    Sitecore.FormBuilder.type = $(id).selectedIndex;
    this.setActiveProperties($(id), null);
  };

  this.setActiveProperties = function(sender, evt) {
    var value = $F("Active");
    var type = $(value + "_field_type");

    if (type != null) {
      if ($(type.value).readAttribute("required") == "1") {
        $(value + "_field_validate").disabled = false;
      } else {
        $(value + "_field_validate").disabled = true;
        $(value + "_field_validate").checked = false;
      }
    } else {
      type = $(value + "_section_type");
    }

    if (!evt) {
      evt = Event.fire(value, "click", "");
    }

    if (type != null) {
      if (self.editors[type.value] == null) {
        scForm.postEvent(this, evt, 'LoadPropertyEditor("' + type.value + '", "' + value + '")');
        self.editors[type.value] = $("PropertySettings").innerHTML;

        $j(".v-splitter").trigger('change');

      } else {
        $("PropertySettings").innerHTML = self.editors[type.value];
      }

      var properties = $j("#" + value + "_field_properties");

      self.initState(sender, properties != null ? properties.val() : "", "");
      self.clearObsoleteProperties(properties, "pb_forms_");

      properties = $j("#" + value + "_field_properties_loc");
      self.initState(sender, properties != null ? properties.val() : "", "loc_");
      self.clearObsoleteProperties(properties, "pb_forms_loc_");

      $("PropertySettings").style.display = "block";

      if ($(value + "_field_tag") != null && $("denytag") == null) {
        var fieldSet = Builder.node('fieldset', { 'class': 'sc-accordion-header' }, [
          Builder.node('legend', { 'class': 'sc-accordion-header-left sc-tag' }, [
            Builder.node('span', { 'class': 'sc-accordion-header-center' }, [
              Builder.node('img', { 'class': 'sc-accordion-icon', 'border': '0', 'align': 'middle', 'src': '/~/icon/Applications/16x16/document_new.png', 'width': '16', 'height': '16' }),
              Builder.node('strong', {}, [Sitecore.FormBuilder.dictionary['analyticsLabel']]),
              Builder.node('div', { 'class': 'sc-accordion-header-right' })
            ])
          ]),
          Builder.node('div', {}, [
            Builder.node('div', { 'class': 'scFbText' }, [Sitecore.FormBuilder.dictionary['tagDescription']]),
            Builder.node('div', { 'class': 'scFbPeEntry' }, [
              Builder.node('label', { 'for': 'pb_forms_Tag', 'class': 'scFbPeLabel' }, [Sitecore.FormBuilder.dictionary['tagLabel']]),
              Builder.node('input', { 'type': 'checkbox', id: 'pb_forms_Tag' })
            ])
          ])
        ]);

        $("FieldProperties").appendChild(fieldSet);

        $j("<span class=''>&nbsp;</span>").appendTo("#FieldProperties .sc-tag");

        if ($(value + "_field_tag").getValue() == "1") {
          $("pb_forms_Tag").checked = $(value + "_field_tag").getValue();
        }

        Event.observe($("pb_forms_Tag"), "click", function(event) {
          var tag = Event.element(event);
          $($F("Active") + "_field_tag").value = tag.checked ? "1" : "";

          self.UpdateTag = true;
          Sitecore.UI.ModifiedTracking.setModified(true);
          scForm.setModified(true);
        });
      }

      self.addConditionRules(value);
    } else {
      if (self.editors[value] == null) {
        scForm.postEvent(this, evt, 'LoadPropertyEditor( "", "' + value + '")');

        self.editors[value] = $("PropertySettings").innerHTML;
      } else {
        $("PropertySettings").innerHTML = self.editors[value];
      }
    }
  };

  this.addConditionRules = function(field) {
    if (Sitecore.FormBuilder.form != null && Sitecore.FormBuilder.form.analytics) {
      var condition = this.get(field, "Conditions");

      var fieldSet = "<fieldset class='sc-accordion-header'><legend class='sc-accordion-header-left'><span class='sc-accordion-header-center'>"
        + "<img class='sc-accordion-icon' width='16' height='16' src='/~/icon/Applications/16x16/document_new.png' align='middle' border='0'/>"
        + "<strong>" + (Sitecore.FormBuilder.dictionary['conditionRulesLiteral'] || '') + "</strong><div class='sc-accordion-header-right'>&nbsp;</div></span></legend>"
        + "<div class='section-content scFbPeEntry'>"
        + '<button class="scButton edit-button" onclick="javascript:return scForm.postEvent(this,event, &quot;forms:editrule(id=' + field + ',cid=conditionsContains,rule=form)&quot;)">' + Sitecore.FormBuilder.dictionary['editButton'] + '</button>'
        + '<div class="condition-container" id="conditionsContains">'
        + (condition != null && condition != '' ? condition.t : ("<div class='no-conditions'>" + Sitecore.FormBuilder.dictionary['noConditions'] + "</div>"))
        + '</div>'
        + "</div></fieldset>";
      $j("#FieldProperties").append(fieldSet);

      $j("#FieldProperties").find('.scRule').each(function() {
        $j(this).html("<ul><li><span>" + $j(this).html() + "</span></ul></li>");
      });
    }
  };

  this.get = function(field, property) {
    var value = Sitecore.FormBuilder.getField(field);
    if (value != null && property != null && property != '') {
      return value[property] || '';
    }

    return '';
  };

  this.set = function(field, property, value, text) {
    value = Sitecore.FormBuilder.getField(field);
    if (property == null || property == '') {
      return;
    }

    if (value != null) {
      Sitecore.FormBuilder.setField(field, { property: { v: '', t: '' } });
      value = Sitecore.FormBuilder.getField(field);
    }

    if (value[property].t == null) {
      value[property] = value || '';
    } else {
      value[property].v = value || '';
      value[property].t = text || '';
    }
  };

  this.initState = function(sender, properties, prefix) {
    var array = Sitecore.Wfm.Parser.parseXMLToArray(properties);

    var i = 0;
    for (var element in array) {
      ++i;
      if (array[element] == null || array[element].wrap == null) {
        var ctrl = $("pb_forms_" + prefix + element);
        if (ctrl != null) {
          ctrl.value = array[element] || '';
          if (ctrl.tagName == "SELECT") {
            if (element == "SelectedValue") {
              self._setSelectedOptionByText(ctrl.options, array[element] || '');
            } else {
              self._selectSingleOptionByText(ctrl.options, array[element] || '');
            }
          }
          Sitecore.FormBuilder.executeOnChange(ctrl);
        }
      }
    }

    $$("input[type='hidden'][localUpdate]").each(function(el) {
      Sitecore.FormBuilder.executeOnChange(el);
    });
  };

  this.clearObsoleteProperties = function(properties, idPrefix) {
    var array = Sitecore.Wfm.Parser.parseXMLToArray(properties.value);
    var update = [];

    for (var element in array) {
      if (array[element] == null || array[element].wrap == null) {
        var ctrl = $(idPrefix + element);
        if (ctrl != null) {
          update[element] = array[element];
        }
      }
    }

    properties.value = Sitecore.Wfm.Parser.toXml(update);
  };

  this.setValue = function(element, value) {
    element.value = value;
    Sitecore.FormBuilder.executeOnChange(element);
  };

  this.setUnescapedValue = function(element, value) {
    this.setValue(element, value.unescapeHTML());
  };

  this.onSaveValueInfluenceList = function(prefix, propertyId, localize) {
    self.onSaveValue(prefix, propertyId, localize);
  };

  this.onSaveListValue = function(prefix, propertyId, localize) {
    self.onSaveValue(prefix, propertyId, localize, true);
    var element = $(propertyId);
    var itemsList = getItemsFormSource(element, element.value);
    var itemsAmount = itemsList ? itemsList.length : 0;
    $(propertyId + 'visible').value = itemsAmount + " " + getTranslatedText(element, "data-posttext", "#", itemsAmount);
    this._setSelectedValueForListTypes(prefix, itemsList, localize);
  };

  this.onSavePredefinedValidatorValue = function(prefix, propertyId) {
    var regexControl = $(prefix + "RegexPattern");
    var validatorControl = $(propertyId);
    if (regexControl != null) {
      var option = validatorControl.select('[value="' + validatorControl.getValue() + '"]');
      if (option.length > 0) {
        regexControl.value = unescape(option[0].readAttribute("regex"));
      }
    }

    if (regexControl != null) {
      regexControl.disabled = validatorControl.getValue() != 'A9F21BCD4C754A3486C7A2B3C19E04FF';

      if (regexControl.disabled == false) {
        var history = regexControl.readAttribute('history');
        if (history != null) {
          regexControl.value = history;
        }
      }
    }

    this.onSaveValue(prefix, propertyId, '0');
    this.onSaveValue(prefix, regexControl.id, '0');
  };

  this.onSaveRegexValue = function(prefix, propertyId) {
    var regexControl = $(propertyId);
    var validatorControl = $(prefix + "PredefinedValidator");

    if (regexControl != null) {
      if (regexControl.disabled == false) {
        regexControl.setAttribute('history', regexControl.getValue());
      }

      var customValidatorValue = 'A9F21BCD4C754A3486C7A2B3C19E04FF';
      regexControl.disabled = validatorControl.getValue() != customValidatorValue;
      if (validatorControl != null) {
        var option = validatorControl.select('[value="A9F21BCD4C754A3486C7A2B3C19E04FF"]');
        if (option[0] != null) option[0].writeAttribute("regex", regexControl.getValue());
      }
    }

    this.onSaveValue(prefix, propertyId, '0');
    this.onSaveValue(prefix, validatorControl.id, '0');
  };

  this.onSaveDateValue = function(prefix, propertyId) {
    self.onSaveValue(prefix, propertyId, false);
    var value = $(propertyId).value;
    scForm.invoke('forms:convertdate(target=' + propertyId + 'visible' + ',value=' + escape(value) + ')');
  };

  this.onSaveShowCAPTCHAValue = function(prefix, propertyId, newValue) {
    var value = $(propertyId).value;

    var schemaDom = null;
    do {
      if (value == '') {
        value = "<schema e='0' page='{FBF67C79-6FD7-454A-AB66-C93D872EA7A6}' placeholder='content'><session e='0' page='{96F047CC-61BB-4D97-BB13-6D3AA6491891}' placeholder='content'/><server e='0' page='{F6C596B7-5146-444C-B983-29B73CAB5657}' placeholder='content'/></schema>";
      }
      try {
        schemaDom = $j(value);
      } catch (e) {
        value = '';
      }
    } while (schemaDom == null);

    var schema = $j(schemaDom).find('schema');
    if (schema.length == 0) {
      schema = $j(schemaDom);
    }

    var enabled = schema.length == 1 ? schema : $j(schema[0]);
    if (newValue == null) {
      $('showAuto').checked = enabled.attr('e') == '1';
    } else {
      enabled.attr('e', $('showAuto').checked ? "1" : "0");
    }

    if (enabled.attr('e') == "1") {
      $('AttackProtection').disabled = false;
      $('IsRobot').style.color = "black";
      $('sessionLimit').style.color = "black";
      $('serverLimit').style.color = "black";
      disableEditLink($('EditLink'), false);
    } else {
      $('AttackProtection').disabled = true;
      $('IsRobot').style.color = "grey";
      $('sessionLimit').style.color = "grey";
      $('serverLimit').style.color = "grey";
      disableEditLink($('EditLink'), true);
    }

    var sessionNode = schema.length == 1 ? $j(schema).find('session') : $j(schema[1]);
    if (sessionNode.attr('e') == "1") {
      $j('#sessionLimit').css('display', 'block');
      $j('#sessionTimes').html(sessionNode.attr('s'));
      $j('#sessionMinutes').html(sessionNode.attr('m'));
    } else {
      $j('#sessionLimit').css('display', 'none');
    }

    var serverNode = schema.length == 1 ? $j(schema).find('server') : $j(schema[3]);
    if (serverNode.attr('e') == "1") {
      $j('#serverLimit').css('display', 'block');
      $j('#serverTimes').html(serverNode.attr('s'));
      $j('#serverMinutes').html(serverNode.attr('m'));
    } else {
      $j('#serverLimit').css('display', 'none');
    }

    $$('#AttackProtection img').each(function(element) {
      var url = $j(element).attr('link');
      if (($('AttackProtection').disabled && url.indexOf('_disabled16x16') > -1) ||
      (!$('AttackProtection').disabled && url.indexOf('_disabled16x16') == -1)) {
        $j(element).attr('link', $j(element).attr('linkd'));
        $j(element).attr('linkd', url);
        element.src = url;
      }
    });

    $(propertyId).value = $j("<div></div>").append(schema).html();
    this.onSaveValue(prefix, propertyId);
  };

  this._setSelectedValueForListTypes = function(prefix, list, localize) {
    var selected = $(prefix + "SelectedValue");
    if (selected == null) {
      return;
    }

    var mode = $(prefix + "SelectionMode");
    if (mode != null) {
      selected.multiple = (mode.getValue() == "Multiple") ? true : false;
    }

    while (selected.options.length > 0) {
      selected.remove(0);
    }

    var option = null;

    var empty = $(prefix + "EmptyChoice");
    if (empty != null && empty.getValue() == "Yes") {
      option = document.createElement('option');
      option.text = "";
      try {
        selected.add(option, null);
      } catch (ex) {
        selected.add(option);
      }
    }

    list.each(function(element) {
      option = document.createElement('option');
      option.value = element.value.text || element.value.textContent;
      option.text = element.text.text || element.text.textContent || option.value;
      try {
        selected.add(option, null);
      } catch (ex) {
        selected.add(option);
      }
    });

    var properties = getActiveProperties(localize);

    this._setSelectedOptionByText(selected.options, properties["SelectedValue"]);
  };

  this._selectSingleOptionByText = function(array, text) {
    for (var i = 0; i < array.length; ++i) {
      if (array[i].value == text) {
        array[i].selected = true;
      }
    }
  };

  this._setSelectedOptionByText = function(array, text) {
    var list = Sitecore.Wfm.Parser.parseXMLByTag(text || '', "item");

    list.each(function(element) {
      for (var i = 0; i < array.length; ++i) {
        if (array[i].value == element.value.text || array[i].value == element.value.textContent) {
          array[i].selected = true;
        }
      }
    });
  };

  this.onSaveValue = function(prefix, propertyId, localize, isEmptyValueNotValid) {
    var properties = getActiveProperties(localize);
    if (properties == null) {
      return;
    }

    var field = $($F("Active") + "_field_properties" + (localize == "1" ? "_loc" : ""));
    var parameter = $(propertyId);
    var name = propertyId.substring(prefix.length, propertyId.length);

    var value = "";
    if (parameter.tagName == "SELECT" && (propertyId == prefix + "SelectedValue")) {
      $A(parameter.options).each(function(element) {
        if (element.selected) {
          value += "<item>" + element.value + "</item>";
        }
      });
    } else {
      value = parameter.getValue();
      if (!value && isEmptyValueNotValid == true) {
        return;
      }
    }

    if (properties[name] != value && parameter.getAttribute("properties") != value) {
      properties[name] = value;
      field.value = Sitecore.Wfm.Parser.toXml(properties);
    }
  };

  function getActiveProperties(localize) {
    var active = $("Active");
    if (active != null) {
      var field = $(active.value + "_field_properties" + (localize == "1" ? "_loc" : ""));
      return Sitecore.Wfm.Parser.parseXMLToArray(field.value);
    }

    return null;
  }

  function disableEditLink(anchorObj, disable) {
    anchorObj = $(anchorObj);
    anchorObj.parentNode.disabled = disable;
    if (disable) {
      $j(anchorObj).unbind().css('color', 'grey');
    } else {
      $j(anchorObj).unbind().bind('click', function() {
        Sitecore.FormBuilder.rise('forms:openrobotdetection', "pb_forms_RobotDetection");
        return false;
      }).css('color', 'black');
    }
  }

  function getTranslatedText(element, attrName, splitterChar, itemsAmount) {
    var postfixTextAttr = element.getAttribute(attrName);
    var postfixTextParts = postfixTextAttr.split(splitterChar);
    var index = (postfixTextParts.length > 1 && itemsAmount !== 1) ? 1 : 0;
    return postfixTextParts[index];
  }

  function getItemsFormSource(ctrl, source) {
    var temp = Builder.node('input', { type: 'hidden', id: ctrl.id + "temp" });

    ctrl.parentNode.appendChild(temp);
    scForm.invoke("forms:selectitemsbysource" + '(target=' + temp.id + ',value=' + escape(source) + ',la=' + window.location.search.replace(/&amp;/g, '&').toQueryParams()['la'] + ')');

    temp = document.getElementById(temp.id);

    var list = Sitecore.Wfm.Parser.parseXMLByTag(decodeURIComponent(temp.value), "item", "text");
    ctrl.parentNode.removeChild(temp);

    return list;
  }

  Sitecore.UI.ModifiedTracking.track(true, Sitecore.App.getParentForm());

  Sitecore.FormBuilder.onSelectedChange = self.setActiveProperties;
  Sitecore.FormBuilder.onChangeFieldType = self.updateProperties;
};