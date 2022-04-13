(function($scw, scBeacon) {
  if (typeof window.WffmFieldsData === 'undefined') {
    window.WffmFieldsData = {};
  }

  //WFFM AJAX forms widget
  //======================
  $scw.widget("wffm.wffmForm", {
    _create: function () {
      var form = $scw(this.element).removeData("validator").removeData("unobtrusiveValidation");

      var inline = $scw(form).hasClass("form-inline");
      var self = this;
      $scw(form).find(".form-group").addClass("has-feedback");

      $scw.validator.setDefaults({
        ignore: "",
        errorClass: 'has-error',
        validClass: 'has-success',
        highlight: function (element, errorClass, validClass) {
          if (inline) {
            self._popupHighlight(element, errorClass, validClass);
          } else {

            self._errorHighlight(element, errorClass, validClass);
          }
        },
        unhighlight: function (element, errorClass, validClass) {
          if (inline) {
            self._popupUnhighlight(element, errorClass, validClass);
          } else {
            self._errorUnhighlight(element, errorClass, validClass);
          }

        }
      });

      var defaultEmailValidation = $scw.validator.methods.email;

      $scw.validator.addMethod("email", function (value, element) {
        if (element["id"].indexOf("wffm") === 0) {
          return true;
        }

        return defaultEmailValidation.call(this, value, element);
      });

      $scw.validator.unobtrusive.parse(form);

      //remove validation for "hidden" elements
      var elements = $scw(form).find(".hidden").find(":input[data-val=true]");
      $scw.each(elements, function () {
        $scw(this).rules("remove");
      });

      var notExternalSite = !getFxmHost();


      var ajaxForm = $scw(form).attr("data-wffm-ajax") || !notExternalSite;
      if (ajaxForm) {

        $scw(form).submit(function (e) {
          if ($scw(this).valid()) {

            var headersList = {
              "X-RequestVerificationToken": $scw(form).find('[name=__RequestVerificationToken]').val(),
              "X-Requested-With": "XMLHttpRequest"
            };

            if (notExternalSite) {
              $scw.ajax({
                url: this.action,
                type: this.method,
                processData: false,
                contentType: false,
                headers: headersList,
                data: new FormData(form.get(0)),
                success: function (res) {
                  self.formSubmitSuccess(form, res);
                },
                error: function (xhr, status, exception) {
                  self.formSubmitError(form, xhr, status, exception);
                }
              });

            } else {

              var formIdInput = form.find("input[id=" + form.attr("id") + "_Id]");
              var formItemIdInput = form.find("input[id=" + form.attr("id") + "_FormItemId]");
              var hostUrl = getFxmHost() + "/form/process?" + formIdInput.attr("name") + "=" + formIdInput.attr("value") + "&"
                  + formItemIdInput.attr("name") + "=" + formItemIdInput.attr("value");
              $scw.ajax({
                url: hostUrl,
                type: 'POST',
                dataType: "json",
                processData: false,
                contentType: false,
                data: new FormData(form.get(0)),
                success: function (res) {
                  self.formSubmitSuccess(form, res);
                },
                error: function (xhr, status, exception) {
                  self.formSubmitError(form, xhr, status, exception);
                },
                xhrFields: {
                  withCredentials: true
                }
              });

            }
            e.preventDefault();
          }
        });
      }

      $scw(form).track();

      var datePickerIds = $scw(form).find(".datepicker");
      $scw.each(datePickerIds, function () {
        var fieldId = $scw(this).attr("id");
        var data = window.WffmFieldsData[fieldId];
        if (typeof data === "undefined") {
          data = {};
        }

        $scw(this).datepicker(data);
      });

      var dateIds = $scw(form).find(".wfmDatebox");
      $scw.each(dateIds, function () {
        var fieldId = $scw(this).attr("id");
        var data = window.WffmFieldsData[fieldId];
        if (typeof data === "undefined") {
          data = {};
        }

        $scw(this).datebox(data);
      });

    },

    formSubmitError: function(form, xhr, status, exception) {
      $scw(form).html(xhr.responseText);
    },

    formSubmitSuccess: function(form, res) {
      var $form = $scw(form);

      if (res.indexOf("<script") === 0) {
        $form.append(res);
      } else {
        $form.html(res);
      }

      $form.find(".field-validation-error").not(":empty").closest(".form-group").addClass("has-error");

      var token = $form.find("[name=__RequestVerificationToken]").val();
      $scw("[name=__RequestVerificationToken]").val(token);
    },

    _parse: function (form) {
      $scw(form).children().not(".hidden").find(":input[data-val=true]").each(function () {
        $scw.validator.unobtrusive.parseElement(this, true);
      });

      var info = window.unvalidationInfo(form);
      if (info) {
        info.attachValidation();
      }
    },

    _popupHighlight: function (element, errorClass, validClass) {

      var form = $scw(element).closest("form");
      var validator = form.data("validator");

      var errorMessage = validator.errorMap[element.name];
      $scw(element).closest(".form-group").popover({
        placement: "left",
        trigger: 'manual',
        content: errorMessage,
        template: "<div class=\"popover\"><div class=\"arrow\"></div><div class=\"popover-inner\"><div class=\"popover-content\"><p></p></div></div></div>"
      });
      $scw(element).closest(".form-group").popover("show");

      $scw(form).find("div.validation-summary-errors").find("ul").addClass("list-group").find("li").addClass("list-group-item list-group-item-danger");
    },

    _popupUnhighlight: function (element, errorClass, validClass) {
      $scw(element).closest(".form-group").popover("destroy");
    },

    _errorHighlight: function (element, errorClass, validClass) {
      var form = $scw(element).closest("form");
      $scw(element).closest(".form-group").addClass('has-error').removeClass(validClass);

      $scw(form).find("div.validation-summary-errors").find("ul").addClass("list-group").find("li").addClass("list-group-item list-group-item-danger");
    },

    _errorUnhighlight: function (element, errorClass, validClass) {
      var error = $scw(element).closest(".form-group").find("span.field-validation-error");
      if (error.length == 0) {
        $scw(element).closest(".form-group").removeClass('has-error').addClass(validClass);
      }
    },
  });

  //WFFM fields events track widget
  //===============================
  $scw.widget("wffm.track", {
    options: {
      formId: null,
      fieldId: null,
      fieldTitle: null,
      fieldValue: null,
      eventCount: null,
      rules: null
    },

    _create: function () {
      var self = this,
          options = this.options;
      options.eventCount = 0;
      var id = $scw(this.element).attr("id");
      options.formId = $scw(this.element).attr("data-wffm");
      this.element.find("input[type!='submit'][type!='password'], select, textarea").filter(":not([data-tracking='false'])").bind('focus', function (e) {
        self.onFocusField(e, this);
      }).bind('blur change', function (e) { self.onBlurField(e, this); });
    },

    _getElementName: function (element) {
      var fieldName = element.name;
      if (!this.endsWith(fieldName.toLowerCase(), "value")) {
        var searchPattern = "fields[";
        var index = fieldName.toLowerCase().indexOf(searchPattern);
        return fieldName.substring(0, index + searchPattern.length + 3) + "Value";
      }

      return fieldName;
    },



    _getElementValue: function (element) {
      var value = null;
      var checkboxListValue = [];
      if (element.type == "checkbox") {
        var fieldName = element.name;

        var form = $scw(element).closest("form");
        var selected = "selected";
        if (this.endsWith(fieldName.toLowerCase(), selected)) {
          var searchPattern = "items[";
          var index = fieldName.toLowerCase().indexOf(searchPattern);

          var pattern = fieldName.substring(0, index + searchPattern.length);
          var checkboxListSelected = form.find("input[name^='" + pattern + "']").not(":not(:checked)");
          $scw.each(checkboxListSelected, function () {
            var valueId = this.name.slice(0, -selected.length) + "Value";
            var valueElement = $scw(this).parent().find("input[name='" + valueId + "']");
            if (valueElement) {
              var v = valueElement.val();
              checkboxListValue.push(v);
            }
          });

          value = checkboxListValue;
        } else {
          var checkboxList = form.find("input[name='" + fieldName + "']");

          if (checkboxList.length > 1) {
            checkboxList = checkboxList.not(":not(:checked)");
            $scw.each(checkboxList, function () {
              checkboxListValue.push($scw(this).val());
            });

            value = checkboxListValue;
          } else {
            value = element.checked ? "1" : "0";
          }
        }
      } else {
        value = $scw(element).val();
      }

      return value;
    },

    onFocusField: function (e, element) {
      var fieldId = this._getElementName(element);
      var value = this._getElementValue(element);

      if (this.options.fieldId != fieldId) {
        this.options.fieldId = fieldId;
        this.options.fieldValue = value;
      }
    },

    onBlurField: function (e, element) {
      var form = $scw(element).closest("form");
      var validator = form.data("validator");

      var fieldId = this._getElementName(element);

      if (!this.endsWith(fieldId, "value")) {
        var owner = this._getOwner(form, fieldId);
        if (!owner) {
          return;
        }

        element = owner;
      }
      var value = this._getElementValue(element);
      if (this.options.fieldId != fieldId || (this.options.fieldId == fieldId && this.options.fieldValue != value)) {
        this.options.fieldId = fieldId;
        this.options.fieldValue = value;

        var id = element.id;
        var title = $scw("label[for='" + id + "']").text();
        if (!title) {
          title = $scw(this).attr("placeholder");
          if (!title) {
            title = id;
          }
        }
        title = title.replace(/(\r\n|\n|\r)/gm, " ").trim();
        if ($scw.isArray(value)) {
          value = value.join(',');
        }

        if (element.type == "password") {
          value = "schidden";
        }

        var clientEvent = this._getEvent(fieldId, title, "Field Completed", value.replace(/<schidden>.*<\/schidden>/, "schidden"));

        var validationevent = [];
        var valid = validator.element(element);
        if (validator && !valid) {
          validationevent = this._checkClientValidation(element, title, validator);
        }
        this._trackEvents($scw.merge([clientEvent], validationevent));
      }
    },

    endsWith: function (str, suffix) {
      return str.toLowerCase().indexOf(suffix.toLowerCase(), str.length - suffix.length) !== -1;
    },

    closestElement: function (element, cssClass) {
      var parent = $scw(element).parent();
      var search = parent.find("." + cssClass);
      if (!search[0] && !parent.hasClass("form-group")) {
        return this.closestElement(parent, cssClass);
      }
      return search;
    },

    _getOwner: function (form, elementId) {
      var targetId = elementId.slice(0, -(elementId.length - elementId.lastIndexOf('.') - 1)) + "Value";
      return form.find("input[name=\"" + targetId + "\"]")[0];
    },

    _checkClientValidation: function (element, title, validator) {
      var tracker = this;
      var events = [];

      $scw.each(validator.errorMap, function (key, value) {
        if (key == element.name) {
          var ruleName = tracker._selectKey(validator.settings.messages[key], value);

          var clientEvent = tracker._getEvent(key, title, "{844BBD40-91F6-42CE-8823-5EA4D089ECA2}", value);
          events.push(clientEvent);
        }
      });

      return events;
    },

    _selectKey: function (list, selectedValue) {
      var selectedKey;
      $scw.each(list, function (key, value) {
        if (selectedValue == value) {
          selectedKey = key;
          return false;
        }
      });
      return selectedKey;
    },

    _trackEvents: function (events) {
      if (!getFxmHost()) {
        $scw.ajax({
          type: 'POST',
          url: "/clientevent/process",
          data: JSON.stringify(events),
          dataType: 'json',
          contentType: 'application/json'
        });
        return;
      }

      var hostUrl = getFxmHost() + "/clientevent/process";

      $scw.ajax({
        url: hostUrl,
        data: JSON.stringify(events),
        type: "GET",
        dataType: "jsonp",
        jsonp: "callback"
      });

    },

    _getEvent: function (fieldid, title, type, value) {
      var options = this.options;
      ++options.eventCount;

      var fieldIdHidden = fieldid.slice(0, -5) + "Id";
      fieldid = $scw("input[name=\"" + fieldIdHidden + "\"]").val();

      return {
        'FieldID': fieldid,
        'Type': type,
        'Value': value,
        'FieldTitle': title,
        'FormID': options.formId,
        'Ticks': options.eventCount
      };
    },
  });

  //WFFM datebox field
  //===============================
  $scw.widget("wffm.datebox", {
    options: {
      dayId: null,
      monthId: null,
      yearId: null,
      empty: false
    },

    _create: function () {
      var self = this;
      var id = $scw(this.element).attr("id");
      var options = this.options;
      var elements = [$scw("#" + this.options.dayId), $scw("#" + this.options.monthId), $scw("#" + this.options.yearId)];
      $scw.each(elements, function () {
        $scw(this).bind("change", function (e) { self.updateDateField(e, this, id); });

        if (options.empty) {
          $scw(this).prop("selectedIndex", -1);
        }
      });
    },

    updateDateField: function (e, element, targetHiddenElementId) {
      var day = $scw("#" + this.options.dayId);
      var month = $scw("#" + this.options.monthId);
      var year = $scw("#" + this.options.yearId);

      if (month && $scw(month).val()) {
        if ($scw(day).find('option').length > 0 && $scw(year).find('option').length > 0) {
          var days = this.getDays(month.val(), year.val());

          if ($scw(day).find('option').length + 1 != days) {

            var selectedIndex = $scw(day)[0].selectedIndex;

            $scw(day).find("option").remove();

            var count = 31 - (31 - days);
            for (var i = 0; i < count; i++) {
              $scw(day).append($scw('<option/>', { value: i + 1, text: i + 1 }));
            }

            $scw(day).prop("selectedIndex", selectedIndex > count - 1 ? -1 : selectedIndex);
          }
        }
      }

      this.updateIsoDate(targetHiddenElementId);
    },

    getDays: function (month, year) {
      return new Date(year, month, 0).getDate();
    },

    nullOrEmpty: function (element) {
      return element && $scw(element).val();
    },

    updateIsoDate: function (targetHiddenElementId) {
      var day = $scw("#" + this.options.dayId).val();
      var month = $scw("#" + this.options.monthId).val();
      var year = $scw("#" + this.options.yearId).val();

      if (day == null || day == "" || month == null || month == "" || year == null || year == "") {
        var valueInput = $scw("#" + targetHiddenElementId);
        //if not empty
        if (valueInput.val()) {
          $scw("#" + targetHiddenElementId).val("").trigger("change");
        }
        return;
      }

      if (month.length == 1) {
        month = "0" + month;
      }
      if (day.length == 1) {
        day = "0" + day;
      }

      $scw("#" + targetHiddenElementId).val(year + month + day + "T000000").trigger("change");
    },
  });

  //WFFM jquery unobtrusive validation
  //==================================
  $scw.validator.addMethod("multiregex", function (value, element, params) {
    if (this.optional(element)) {
      return true;
    }

    return new RegExp(params.mpattern).test(value);
  });

  $scw.validator.unobtrusive.adapters.add("multiregex", ["pattern"], function (options) {
    var params = {
      mpattern: options.params.pattern
    };
    options.rules["multiregex"] = params;
    options.messages["multiregex"] = options.message;
  });

  $scw.validator.unobtrusive.adapters.addBool("ischecked", "required");

  function getFxmHost() {
    return (scBeacon && scBeacon.fxmHost) ? scBeacon.fxmHost : null;
  }
})(jQuery, window.SCBeacon);