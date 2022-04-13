(function($j, scForm) {
  Sitecore.FormBuilder = new function() {
    var self = this;

    this.uniqueID = 0;
    this.active = null;
    this.type = null;
    this.hover = null;
    this.activeFilter = "progid:DXImageTransform.Microsoft.Gradient(GradientType=1, StartColorStr='#DFE6EC', EndColorStr='#ffffff');";
    this.activeColor = "#f0f3f7";
    this.activeBorder = "1px solid #B1B5BA";

    this.fieldEmpty = ''; //$('AddNewFieldText').value;
    this.sectionEmpty = ''; //$('AddNewSectionText').value;
    this.scopes = null;
    this.workPanel = null;
    this.initialized = false;
    this.form = null;

    this.dictionary = [];

    this.hideEditors = function() {
      $('PropertySettings').style.display = "none";
      if (self.scopes == null) {
        return;
      }

      self.scopes.each(function(element) {
        element.style.display = "none";
      });
    };

    this.queryState = function(scope) {
      return $(scope).style.display != "none";
    };

    this.updateStructure = function(isRefreshSortable) {
      Position.includeScrollOffsets = true;

      if (isRefreshSortable) {
        $j(".scFbTableSectionRow").sortable({
          axis: "y",
          dropOnEmpty: true,
          connectWith: [".scFbTableSectionRow"],
          update: self.onUpdateSorting
        });

        $j("#FormBuilder").sortable({
          axis: "y",
          update: self.onUpdateSorting
        });

        $j("#FormBuilder_empty").sortable({
          axis: "y",
          dropOnEmpty: true,
          connectWith: ["#FormBuilder_empty"],
          update: self.onUpdateSorting
        });
      }

      self.processStructure();
    };

    this.processStructure = function() {
      var structure = "";

      $j("#FormBuilder div.scFbTableSectionScope, #FormBuilder div.scFbTableFieldScope").each(function() {
        structure += "," + this.id;
      });

      $('Structure').value = structure;
    };

    this.save = function() {
      $j('#form').val(JSON.stringify(self.form));
    };

    this.loadModel = function() {
      self.form = $j.parseJSON($j('#form').val());
    };

    this.setField = function(field, properties) {
      self.form.fields.push($j.extend(properties, { id: field }));
    };

    this.getField = function(field) {
      var result = null;

      $j(self.form.fields).each(function() {
        if (this.id != null && this.id == field || this.id.v == field) {
          result = this;
          return false;
        }
      });

      return result;
    };

    this.getStructure = function(array, addroot) {
      var structure = "";

      array.each(function(element) {
        if (addroot) {
          structure += (structure != "" ? "," : "") + element.id;
        }

        var fields = $$('#' + element.id + ' div.scFbTableFieldScope');
        fields.each(function(field) {
          structure += (structure != "" ? "," : "") + field.id;
        });
      });

      return structure;
    };

    this.onUpdateSorting = function(element, moved) {
      self.updateStructure(false);
      scForm.setModified(true);

      if ($j(moved).hasClass("scFbTableFieldScope") || $j(moved).hasClass("scFbTableSectionScope")) {
        self.selectControl(moved, event, moved.id, 'PropertySettings', true);
      }
    };

    this.addField = function(sender, event, idDestination) {
      var destination = $j(idDestination);

      var idField = self.getUniqueID();
      var pos = !destination.hasClass('scFbTableSectionScope') ? (destination.children().length - 1) : $(idDestination + "Section").childNodes.length;

      scForm.postEvent(self, event, 'AddNewField("' + idDestination + '","' + idField + '","' + pos + '")');

      self.updateStructure(true);

      self.selectControl($(idField), event, idField, 'PropertySettings', true);

      $(idField + "_field_name").activate();
    };

    this.moveValue = function(source, destination, fieldName, iFrameName) {
      var target = $(destination);
      if (target != null) {
        if (target.tagName == "INPUT") {
          target.value = $(source).value;
          if (target.value == "") {
            target.value = "Submit";
          }
        } else {
          target.innerHTML = $(source).value;
        }
      } else {
        var frame = $(iFrameName);

        var query = frame.contentWindow.location.search;
        if (query.indexOf(destination) > -1) {
          var params = $A(query.split("&"));
          var field = false;
          var id = "";

          params.each(function(param) {
            var pair = param.split("=");

            if (pair[0] == "fieldname") {
              field = true;
            }

            if (pair[0] == "contentid") {
              id = pair[1];
            }

            if (field && id != "") {
              var ctl = frame.contentWindow.window.document.getElementById(id + "_content");
              if (ctl != null) {
                ctl.innerHTML = $(source).value;

                findFrame = true;

                throw $break;
              }
            }
          });
        }
      }
    };

    this.validateValue = function(source, destination, fieldName, iFrameName) {
      self.moveValue(source, destination, fieldName, iFrameName);

      scForm.postEvent(self, event, 'forms:validatetext(ctrl=' + source + ')');

      var validator = $(source + "Validate");
      if (validator != null) {
        var display = (validator.className === "scFbNotValide") ? "inline" : "none";
        $(source + "Fix").style.display = display;
      }
    };

    this.setRichText = function(id, text) {
      var element = $(id);
      element.value = text;
      self.executeKeyUp(element);
    };

    this.executeKeyUp = function(element) {
      if (element.onkeyup == null) {
        return;
      }

      var command = element.onkeyup.toString();
      command = command.substring(command.indexOf("{") + 1, command.indexOf("}"));
      ("<script>" + command + "</script>").evalScripts();
    };

    this.executeOnClick = function(element) {
      if (element.onclick == null) {
        return;
      }

      var command = element.onclick.toString();
      if (command.indexOf("return") > -1) {
        command = command.substring(command.indexOf("return") + 6, command.indexOf("}"));
      }

      ("<script>" + command + "</script>").evalScripts();
    };

    this.executeOnChange = function(element) {
      if (element.onchange == null) {
        return;
      }

      var command = element.onchange.toString();
      command = command.substring(command.indexOf("{") + 1, command.indexOf("}"));
      ("<script>" + command + "</script>").evalScripts();
    };

    this.addSection = function(sender, event, currentSectionId) {
      var idSection = self.getUniqueID();
      var current = $(currentSectionId);

      scForm.postEvent(self, event, 'AddNewSection("' + idSection + '","' + $A(current.parentNode.childNodes).indexOf(current) + '")');

      self.updateStructure(true);

      self.selectControl($(idSection), event, idSection, 'PropertySettings', true);

      $(idSection + "_section_name").activate();
    };

    this.upgradeToSection = function(sender, event) {
      if (self.active != null) {
        self.clearSelect(self.active);
      }

      var idSection = self.getUniqueID();

      scForm.postEvent(self, event, 'UpgradeToSection("' + "FormBuilder" + '","' + idSection + '")');

      self.updateStructure(true);
      self.selectControl($(idSection), event, idSection, 'PropertySettings', true);

      $$('#FormBuilder input.scFbTableSectionName').last().activate();
    };

    this.isAllFieldsDeleted = function(sectionId) {
      var isAllDeleted = true;

      $$('#' + sectionId + " div.scFbTableFieldScope").each(function(element) {
        if ($F(element.id + "_field_deleted") != "1") {
          isAllDeleted = false;
        }
      });

      return isAllDeleted;
    };

    this.showWarningEmptyForm = function() {
      scForm.postEvent(self, event, 'WarningEmptyForm');
      new Effect.Appear("FormBuilder", { duration: 0.3 });
    };

    this.deleteSection = function(sender, evt, id) {
      $(id + "_section_deleted").value = "1";

      var fields = $$('#' + id + ' div.scFbTableFieldScope');

      fields.each(function(element) {
        $(element.id + "_field_deleted").value = "1";
      });

      $(id).style.display = "none";

      scForm.setModified(true);

      self.updateFormState(id);
    };

    this.updateFormState = function(id) {
      if (isAllSectionsDeleted() && self.isAllFieldsDeleted("FormBuilder")) {
        self.showWarningEmptyForm();
        self.executeOnClick($('Welcome'));
      } else {
        self.selectSomething(id);
      }
    };

    this.selectSomething = function(deleted) {
      if (self.active == deleted) {
        var element = $(deleted);
        if (element != null) {
          var deleteMarker;

          var pointer = element;
          if (element.previousSibling != null) {
            do {
              pointer = pointer.previousSibling;
              deleteMarker = $(pointer.id + "_field_deleted") || $(pointer.id + "_section_deleted");
            } while (pointer.previousSibling != null && (deleteMarker != null && deleteMarker.value == "1"));
          } else {
            pointer = element.parentNode.parentNode;
          }

          if (pointer != null && pointer.style.display != "none" && pointer != element) {
            if ($j(pointer).hasClass("scFbTableSectionScope")) {
              pointer = $(pointer.id + "SectionRow");
            }

            if (pointer.onclick != null) {
              self.executeOnClick(pointer);
              return;
            }
          }
        }
      }

      selectPredefined();
    };

    this.deleteField = function(sender, evt, id) {
      $(id + "_field_deleted").value = "1";
      $(id).style.display = "none";
      scForm.setModified(true);

      self.updateFormState(id);
    };

    this.mouseMove = function(sender, evt, id) {
      if (!scForm) {
        return;
      }

      if (id != self.active) {
        if (typeof (id) == "object") {
          id = scForm.browser.getFrameElement(id).parentNode.id;
        }
        var element = $(id);

        if (element != null) {
          if (self.hover != null && self.hover != element) {
            self.mouseOver(self.hover, null, self.hover.id);
          }

          self.hover = element;
          element.style.border = "1px dashed #555555";
          self.displayButtons(id, "visible");
        }
      }

      scForm.browser.clearEvent(evt, true);
    };

    this.mouseOver = function(sender, evt, id) {
      if (!scForm) {
        return;
      }

      if (typeof (id) == "object") {
        id = scForm.browser.getFrameElement(id).parentNode.id;
      }

      var element = $(id);

      if (element != null) {
        if (id == self.active) {
          element.style.border = self.activeBorder;
        } else {
          element.style.border = "1px solid";
          element.style.borderColor = element.style.backgroundColor || "transparent";
          self.displayButtons(id, "hidden");
        }
      }
    };

    this.displayButtons = function(id, state) {
      var buttons = $(id + "ButtonContainer");
      if (buttons != null) {
        buttons.style.visibility = state;
      }
    };

    this.onClick = function(sender, evt, id, borderColor, backgroundColor) {
      var element = $(id);

      element.style.borderTop = "solid 1px " + (borderColor != null ? borderColor : self.activeColor);
      element.style.borderBottom = "solid 1px " + (borderColor != null ? borderColor : self.activeColor);

      element.style.backgroundColor = backgroundColor != null ? backgroundColor : self.activeColor;

      if (self.active != id) {
        self.setBackgroundColor($(self.active), "");
        self.active = id;
      }
    };

    this.focus = function(sender, evt, id, activeTab) {
      self.mouseOver(sender, evt, id);

      var oldActive = $(self.active);
      var newActive = $(id);

      if (oldActive == newActive) {
        return;
      }

      if (oldActive != null) {
        self.clearSelect(oldActive.id);
      }

      self.refreshActive(newActive, evt, activeTab);
    };

    this.refreshActive = function(newActive, evt) {
      if (newActive != null) {
        self.active = newActive.id;
        var fieldType = $(self.active + "_field_type");
        if (fieldType != null) {
          self.type = fieldType.selectedIndex;
        }

        self.select(self.active);

        $("Active").value = self.active;
      } else {
        self.active = null;
      }

      self.onSelectedChange(newActive, evt);
      self.updateAccordion();
    };

    this.select = function(id) {
      var element = $(id);
      if (element == null) {
        return;
      }

      element.style.backgroundColor = self.activeColor;
      element.style.border = self.activeBorder;
      $j(element).addClass('sc-section-selected');

      self.updateInnerFields(element, self.activeColor);

      var marker = $(id + "Marker");
      if (marker != null) {
        $(id + "Marker").style.visibility = "visible";
      }

      self.displayButtons(id, "visible");
    };

    this.updateInnerFields = function(current, color) {
      if (!$j(current).hasClass("scFbTableSectionScope")) {
        return;
      }

      var field = $$('#' + current.id + ' div.scFbTableFieldScope');

      field.each(function(element) {
        element.style.border = "1px solid";
        element.style.borderColor = color;
        element.style.backgroundColor = color;
      });
    };

    this.clearSelect = function(id) {
      var element = $(id);
      if (element == null) {
        return;
      }

      element.style.border = "1px solid transparent";
      element.style.backgroundColor = "transparent";

      $j(element).removeClass('sc-section-selected');

      var marker = $(id + "Marker");
      if (marker != null) {
        $(id + "Marker").style.visibility = "hidden";
      }

      self.updateInnerFields(element, "transparent");
      self.displayButtons(id, "hidden");
    };

    this.setBackgroundColor = function(element, color) {
      if (element == null) {
        return;
      }

      if ($j(element).hasClass("scFbTableSectionScope")) {
        $(element.id + "SectionRow").style.background = color;

        var field = $$('#' + element.id + ' div.scFbTableFieldScope');
        field.each(function(el) {
          el.style.border = "1px solid";
          el.style.borderColor = color;
          el.style.backgroundColor = "transparent";
        });
      } else {
        element.style.background = color;
      }
    };

    this.selectControl = function(sender, evt, id, tab, focus, focusedid) {
      if (self.active != id) {
        if (typeof (id) == "object") {
          id = scForm.browser.getFrameElement(id).parentNode.id;
          focusedid = id + focusedid;
          tab = tab + id;
        }

        self.hideEditors();

        if (tab != null) {
          if (tab == "SubmitTab") {
            if (Sitecore.PropertiesBuilder.UpdateTag) {
              Sitecore.PropertiesBuilder.UpdateTag = null;
              scForm.postEvent(self, evt, 'UpdateSubmit()');
            }
          }
          $j("#" + tab).show();
        }

        if (focus) {
          self.focus(sender, evt, id, tab);
        }

        if (tab != null) {
          $j("#" + tab).show();
        }

        scrollToElement($(id));

        if (focusedid != null && focusedid != '' && sender != null && sender.id != focusedid) {
          var focuselem = $(focusedid);
          focuselem.select();
        }
      }

      $j(window).trigger('resize');

      return true;
    };

    this.getIndexOfEditor = function(id) {
      var i = 0;
      this.scopes.each(function(element, index) {
        if (element.id == id) {
          i = index;
        }
      });

      return i;
    };

    this.focusInput = function(sender, evt, id, tab, focus) {
      var ctrl = $(id + "_field_name") || $(id + "_section_name");
      if (ctrl != null && ctrl.tagName == 'INPUT') {
        if ((ctrl.className == "scFbTableFieldNameInput" && (ctrl.value == $('AddNewFieldText').value || ctrl.value == ctrl.title)) ||
            (ctrl.className == "scFbTableSectionName" && (ctrl.value == $('AddNewSectionText').value || ctrl.value == ctrl.title))) {
          ctrl.value = "";
          ctrl.style.color = "";
        }
      }

      self.selectControl(sender, evt, id, tab, focus);
    };

    this.blurInput = function(sender, evt, id) {
      var ctrl = $(id + "_field_name") || $(id + "_section_name");
      if (ctrl == null || ctrl.tagName != 'INPUT' || ctrl.value != '') {
        return;
      }

      if (ctrl.className == "scFbTableFieldNameInput") {
        ctrl.value = ctrl.title || $('AddNewFieldText').value;
        ctrl.style.color = "silver";
      } else {
        if (ctrl.className == "scFbTableSectionName") {
          ctrl.value = ctrl.title || $('AddNewSectionText').value;
          ctrl.style.color = "silver";
        }
      }
    };

    this.onChangeType = function(sender, evt) {
      self.onChangeFieldType(sender, self.type, evt);
      self.updateAccordion();
    };

    this.updateAccordion = function() {
      if ($j("#Properties").hasClass("collapsible")) {
        $j("#Properties").collapsible('destroy').collapsible();
      } else {
        $j("#Properties").collapsible();
      }
    };

    this.setModified = function() {
      Sitecore.UI.ModifiedTracking.setModified(true);
    };

    this.fieldChange = function(sender, evt) {
      Sitecore.UI.ModifiedTracking.handleEvent(evt, false);
    };

    this.showLayout = function(edit, editorScope, designerScope, button, showCheck, state, defaultText) {
      if (state) {
        showCheck.value = "";

        designerScope.style.display = "none";

        self.executeKeyUp(edit);
        selectPredefined();

        button.className = "scRibbonToolbarLargeButton";

        $j(editorScope).hide();
      } else {
        showCheck.value = "1";
        edit.value = edit.value == "" ? defaultText : edit.value;

        designerScope.style.display = "block";

        self.executeKeyUp(edit);
        self.executeOnClick(designerScope);

        button.className = "scRibbonToolbarLargeButtonDown";
      }
    };

    this.layoutsHide = function() {
      var scopes = $$('#ContentTitle', '#ContentIntro', '#ContentFooter');
      var res = true;

      scopes.each(function(element) {
        res = (res && (element.style.display == "none"));
      });

      return res;
    };

    this.updateTitle = function() {
      self.showLayout($("TitleEdit"), $("ContentTitle"),
        $("TitleBorder"), $("TitleButton"), $("ShowTitle"),
        $("TitleBorder").style.display != "none", "Untitled Form");
    };

    this.updateIntro = function() {
      self.showLayout($("IntroHtml"), $("ContentIntro"),
        $("Intro"), $("IntroButton"), $("ShowIntro"),
        $("Intro").style.display != "none", "Set text for the introduction of the form");
    };

    this.updateFooter = function() {
      self.showLayout($("FooterHtml"), $("ContentFooter"),
        $("Footer"), $("FooterButton"), $("ShowFooter"),
        $("Footer").style.display != "none", "Set text for the footer of the form");
    };

    this.rise = function(message, id) {
      scForm.postEvent(self, event, message + '(target=' + id + ',value=' + escape($(id).value) + ')');
    };

    this.globalRise = function(message, id) {
      scForm.invoke(message + '(target=' + id + ',value=' + escape($(id).value) + ')');
    };

    this.getUniqueID = function() {
      self.uniqueID++;
      return "i" + self.uniqueID;
    };

    this.SaveData = function() {
      var active = $(self.active);
      var pos = -1;
      if ($j(active).hasClass("scFbTableSectionScope")) {
        pos = getElementPositionById(active.id, $$('FormBuilder .scFbTableSectionScope'));
      } else if ($j(active).hasClass("scFbTableFieldScope")) {
        pos = getElementPositionById(active.id, $$('FormBuilder .scFbTableFieldScope'));
      }

      self.clearSelect(self.active);

      Sitecore.App.invoke("forms:save");
      self.updateStructure(true);
      self.active = null;
      self.updateFormState(active);

      if ($j(active).hasClass("scFbTableSectionScope")) {
        active = $(getElementByPosition(pos, $$('FormBuilder .scFbTableSectionScope')) + "SectionRow");
      } else if ($j(active).hasClass("scFbTableFieldScope")) {
        active = $(getElementByPosition(pos, $$('FormBuilder .scFbTableFieldScope')));
      }

      if (active != null && active.style.display != "none" && active.onclick != null) {
        self.executeOnClick(active);
      } else {
        selectPredefined();
      }
    };

    /* sorting */

    this.getPosition = function(parentNode, node) {
      var pos = 0;
      for (var i in parentNode.childNodes) {
        if (parentNode.childNodes[i].id == node.id) {
          return pos;
        }

        ++pos;
      }

      return -1;
    };

    this.FuncForFieldOrSection = function(obj, delegate) {
      if (obj && obj.parentNode != null
          && ($j(obj).hasClass("scFbTableSectionScope") || $j(obj).hasClass("scFbTableFieldScope"))
          && delegate()) {
        self.select(obj.id);
      }
    };

    this.moveUp = function() {
      var ctrl = $(self.active);

      self.FuncForFieldOrSection(ctrl, function() {
        var parentNode = ctrl.parentNode;
        var pos = self.getPosition(parentNode, ctrl);
        if (pos > 0) {
          parentNode.removeChild(ctrl);
          parentNode.insertBefore(ctrl, parentNode.childNodes[pos - 1]);
          self.updateStructure(false);
          return true;
        }

        return false;
      });

      return false;
    };

    this.moveDown = function() {
      var ctrl = $(self.active);

      self.FuncForFieldOrSection(ctrl, function() {
        var parentNode = ctrl.parentNode;
        var pos = self.getPosition(parentNode, ctrl);
        if (pos < parentNode.childNodes.length - 1) {
          parentNode.removeChild(ctrl);
          if (pos == parentNode.childNodes.length - 1) {
            parentNode.appendChild(ctrl);
          } else {
            parentNode.insertBefore(ctrl, parentNode.childNodes[pos + 1]);
          }

          self.updateStructure(false);
          return true;
        }

        return false;
      });

      return false;
    };

    this.moveFirst = function() {
      var ctrl = $(self.active);

      self.FuncForFieldOrSection(ctrl, function() {
        var parentNode = ctrl.parentNode;

        if (parentNode.childNodes.length > 1) {
          parentNode.removeChild(ctrl);
          parentNode.insertBefore(ctrl, parentNode.childNodes[0]);
          self.updateStructure(false);
          return true;
        }

        return false;
      });

      return false;
    };

    this.moveLast = function() {
      var ctrl = $(self.active);

      self.FuncForFieldOrSection(ctrl, function() {
        var parentNode = ctrl.parentNode;
        if (parentNode.childNodes.length > 1) {
          parentNode.removeChild(ctrl);
          parentNode.appendChild(ctrl);
          self.updateStructure(false);
          return true;
        }

        return false;
      });

      return false;
    };

    /* events handler section */
    this.onSelectedChange = function(sender, evt) {};

    this.onChangeFieldType = function(sender, oldValue, evt) {};

    /* private functions */
    function load(sender, evt, stillExec) {
      self.loadModel();

      if (!self.initialized || stillExec == true) {
        self.scopes = $$("#ContentTitle", "#ContentIntro", "#ContentFooter", "#SubmitTab", "#PropertySettings");

        self.workPanel = $('WorkPanel');

        self.updateStructure(true);

        var caption = $("FormTitle").innerHTML;

        if (caption != null) {
          if (window.parent.scWin != null) {
            window.parent.scWin.setCaption(caption);
          }
        }

        if (window.top.Sitecore == null) {
          window.top.Sitecore = Sitecore;
        }

        window.top.Sitecore.FormBuilder = self;


        if ($("Welcome") != null) {
          self.selectControl(self, event, 'Welcome', 'PropertySettings', true);
        } else {
          selectPredefined();
        }

        setButtonState($("TitleButton"), $("ShowTitle"), $("TitleBorder"));
        setButtonState($("IntroButton"), $("ShowIntro"), $("Intro"));
        setButtonState($("FooterButton"), $("ShowFooter"), $("Footer"));

      }

      self.initialized = true;
      scForm.setModified(false);
    }

    function tryToWindowOnLoadedInIe() {
      var readyState = window.document.readyState;
      if (self.initialized || readyState == null) {
        return;
      }

      if (readyState != 'complete' && readyState != 'interactive') {
        setTimeout(tryToWindowOnLoadedInIe, 1000);
        return;
      }

      self.initialized = true;
      load(window, null, true);
    }

    function scrollToElement(element) {
      if (element.style.display != "none") {
        element.focus();
        if ($j(element).hasClass("scFbTableSectionScope")) {
          $(element.id + "_section_name").focus();
        } else if ($j(element).hasClass("scFbTableFieldScope")) {
          $(element.id + "_field_name").focus();
        }
      } else {
        selectPredefined();
      }
    }

    function selectPredefined() {
      if (!selectVisibleElement($$("#TitleBorder", "#Intro", "#Welcome"))) {
        if (!selectVisibleElement($$('#FormBuilder div.scFbTableSectionScope'), "SectionRow")) {
          if (!selectVisibleElement($$('#FormBuilder div.scFbTableFieldScope'))) {
            selectVisibleElement($$("#Footer", "#SubmitGrid"));
          }
        }
      }
    }

    function isAllSectionsDeleted() {
      var isAllDeleted = true;

      $$('#FormBuilder' + " div.scFbTableSectionScope").each(function(element) {
        if ($F(element.id + "_section_deleted") != "1") {
          isAllDeleted = false;
        }
      });

      return isAllDeleted;
    }

    function selectVisibleElement(array, postfix) {
      var isfound = false;
      if (array.size() > 0) {
        try {
          array.each(function(element) {
            if (element != null) {
              if (element.style.display != "none") {
                if (postfix != null && postfix != "") {
                  self.executeOnClick($(element.id + postfix));
                } else {
                  self.executeOnClick(element);
                }

                isfound = true;
                throw $break;
              }
            }
          });
        } catch (e) {
        }
      }

      return isfound;
    }

    function setButtonState(button, state, designElement) {
      if (designElement.style.display != "none") {
        button.className = "scRibbonToolbarLargeButtonDown";
        state.value = "1";
      } else {
        button.className = "scRibbonToolbarLargeButton";
        state.value = "0";
      }
    }

    function getElementPositionById(id, array) {
      var pos = 0;

      array.each(function(element) {
        if (element.display != "none") {
          if (element.id != id) {
            ++pos;
          } else {
            throw $break;
          }
        }
      });

      return pos;
    }

    function getElementByPosition(index, array) {
      var id = "";
      var pos = 0;

      array.each(function(element) {
        if (element.display != "none") {
          if (pos != index) {
            ++pos;
          } else {
            id = element.id;
            throw $break;
          }
        }
      });

      return id;
    }

    /* initialization */
    Sitecore.Dhtml.attachEvent(window, "onload", load);

    $j(function() {
      $j("#FormDesignerSplitter").splitter({
        type: "v",
        outline: true,
        minLeft: 350,
        sizeLeft: 250,
        minRight: 250,
        cookie: "FormDesignerSplitter",
        anchorToWindow: true,
      });
    });

    if (window.document.readyState != null) {
      setTimeout(tryToWindowOnLoadedInIe, 1000);
    }
  };

  $j.widget("sc.collapsible", {
    options: {
      header: '.sc-accordion-header-left'
    },

    _create: function() {
      var self = this,
          options = self.options;


      self.headers = self.element.find(options.header + ":visible");
      if (self.headers.length > 1) {
        self.headers
        .bind("mouseenter.collapsible", function() { $j(this).addClass("ui-state-hover"); })
        .bind("mouseleave.collapsible", function() { $j(this).removeClass("ui-state-hover"); })
        .bind("focus.collapsible", function() { $j(this).addClass("ui-state-focus"); })
        .bind("blur.collapsible", function() { $j(this).removeClass("ui-state-focus"); })
        .click(function() {
          var header = $j(this);
          header.next().slideToggle('slow', function() { self.element.trigger('collapsiblechange'); });
          return false;
        });
      }

      self.element.addClass("collapsible");
    },

    _setOption: function(key, value) {
      $j.Widget.prototype._setOption.apply(this, arguments);

      if (key == "toggle") {
        $j(this.headers[value]).trigger('click');
      }
    },

    destroy: function() {
      this.headers.unbind();
      this.element.removeClass("collapsible");
      return $j.Widget.prototype.destroy.call(this);
    }
  });
})(jQuery, scForm);