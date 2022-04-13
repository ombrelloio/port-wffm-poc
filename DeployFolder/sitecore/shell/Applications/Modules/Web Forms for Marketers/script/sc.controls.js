if (!window.$scw) {
  window.$scw = jQuery.noConflict(true);
}

$scw(function() {
  $scw.widget("wffm.droptreemap", {
    options: {
      listData: null,
      treeData: null,
      selected: {},
      change: null,
      container: null,
      mappedKeysHeader: null,
      fieldsHeader: null,
      addFieldTitle: null,
      addBtn: null
    },

    _updateSelected: function(key, value, id) {
      if (value != null) {
        this.options.selected[key] = { "key": key, "value": value, "id": id };
      }
    },

    _getId: function() {
      return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
      });
    },

    _index: function(selectedKey) {
      for (var i = 0; i < this.options.listData.length; i++) {
        if (this.options.listData[i].id === selectedKey) {
          return i;
        }
      }

      return -1;
    },

    _createPair: function(svalue, tvalue, pairkey) {
      var self = this,
        options = this.options;

      var pairs = $scw(".droptreemap-pair").length;
      if (pairs >= self.options.listData.length) {
        return;
      }

      var container = $scw('<div class="droptreemap-pair"/>');

      var selected = [];

      if (typeof self.options.selected !== "undefined") {
        for (var k in self.options.selected) {
          selected.push(self.options.selected[k].key);
        }
      }

      var keys = $scw.grep(options.listData, function(element) {
        return $scw.inArray(element.id, selected) === -1 || element.id === svalue;
      });

      self.options.addBtn.css("display", pairs + 1 === self.options.listData.length ? "none" : "block");

      var pair = container.droptreepair({
        treeData: options.treeData,
        listData: keys,
        selectValue: svalue,
        treeValue: tvalue,
        key: pairkey,
        change: function(key, value, id) {
          self._updateSelected(key, value, id);
          if (options.change) {
            options.change.call(self, JSON.stringify(self._removeEmpty(options.selected)));
          }
        }
      });

      self.options.container.append(container);


      var deleteBtn = $scw('<a class="droptree-img-btn" href="#"><img src="/sitecore/shell/applications/Modules/Web Forms for Marketers/images/delete2.png" title="Remove"></a>');
      deleteBtn.on("click", function() {
        deleteBtn.parent().remove();


        self.options.addBtn.css("display", "block");
        delete self.options.selected[svalue];

        var text = $scw(deleteBtn.parent()).find(".droptree-select-container select").find(":selected").text();

        //update sibilings                
        var option = $scw("<option></option>").attr("value", svalue).text(text);
        option.attr("selected", false);

        $scw(".droptree-select-container select").append(option);

        if (options.change) {
          options.change.call(self, JSON.stringify(self._removeEmpty(options.selected)));
        }
      });

      pair.append(deleteBtn);
    },

    _removeEmpty: function(list) {
      var selected = [];
      if (!list) {
        return selected;
      }

      for (var key in list) {
        if (list[key].value != null && list[key].key != null) {
          selected.push(list[key]);
        }
      }
      return selected;
    },

    _createAddButton: function() {
      var self = this;
      var title = "Add Field";
      if (self.options.addFieldTitle !== null) {
        title = self.options.addFieldTitle;
      }
      var addBtn = $scw('<a href="#"><img src="/sitecore/shell/applications/Modules/Web Forms for Marketers/images/element_add.png" title="' + title + '"><span>' + title + '</span></img></a>');
      addBtn.on("click", function() {
        self._createPair();
      });

      self.options.addBtn = addBtn;
    },

    _createHeaders: function() {
      var self = this;

      if (self.options.fieldsHeader != null && self.options.mappedKeysHeader != null) {
        var container = $scw('<div class="droptreemap-header" />');
        var fields = $scw('<div class="droptreemap-header-title" ><span class="droptreemap-header-field" >' + self.options.fieldsHeader + '</span></div>');
        var keys = $scw('<div class="droptreemap-header-title" ><span class="droptreemap-header-key">' + self.options.mappedKeysHeader + '</span></div>');

        container.append(fields);
        container.append(keys);

        self.element.prepend(container);
      }
    },

    _create: function() {
      var self = this,
        options = this.options;

      var container = $scw('<div class="droptreemap-container" />');
      container.uniqueId();

      options.container = container;
      self.element.append(container);

      self._createHeaders();


      self._createAddButton();

      //workaround to remove incorrect json value
      var newSelected = {};

      if (Object.keys(options.selected).length > 0) {
        for (var key in self.options.selected) {
          var obj = {
            "key": self.options.selected[key].key,
            "value": self.options.selected[key].value,
            "id": self.options.selected[key].id
          };

          if ((obj.key == null) || (obj.key == "undefined")) {
            continue;
          }

          newSelected[self.options.selected[key].key] = obj;
        }

        self.options.selected = newSelected;
        for (var key1 in self.options.selected) {
          self._createPair(self.options.selected[key1].key, self.options.selected[key1].value, self.options.selected[key1].id);
        }
      } else {
        self._createPair();
      }

      self.element.append(self.options.addBtn);
    }
  });

  $scw.widget("wffm.droptreepair", {
    options: {
      listData: null,
      treeData: null,
      key: null,
      selectList: null,
      selectClass: null,
      selectValue: null,
      selectIndex: null,
      keyInput: null,
      tree: null,
      treeValue: null,
      change: null,
      id: null
    },

    _createSelect: function() {
      var self = this,
        options = this.options;

      if (!options.className) {
        options.className = "droptree-pair-select";
      }

      var container = $scw('<div class="droptree-select-container" style="float:left;margin-right:10px;display:inline;" />');
      container.uniqueId();

      var selectList = $scw('<select class="' + options.selectClass + '"/>');
      selectList.uniqueId();

      selectList.change(function() {
        options.selectValue = $scw(this).find(":selected").val();
        if (options.change) {
          options.change.call(self, options.selectValue, options.treeValue, options.key);
        }
      });

      self._populateList(selectList, options.listData);
      options.selectList = selectList;

      container.append(selectList);

      $scw(".droptree-select-container select option[value='" + selectList.val() + "']").remove();

      self.element.append(container);
    },

    _createKey: function() {
      var self = this,
        options = this.options;

      var keyDiv = $scw('<div class="droptree-key-container" style="float:right;margin-right:10px;display:inline;display: none" />');
      keyDiv.uniqueId();
      options.keyInput = $scw('<input type="text"  style="display: none />');
      if (options.key) {
        $(options.keyInput).val(options.key);
      }

      options.keyInput.change(function() {
        var text = $scw(this).val().trim();
        if (/\S/.test(text)) {
          options.key = text;
          if (options.change) {
            options.change.call(self, options.selectValue, options.treeValue, options.key);
          }
        }
      });

      keyDiv.append(options.keyInput);

      self.element.append(keyDiv);

      if (options.treeValue) {
        options.keyInput.toggle(options.treeValue.toLowerCase().indexOf("/entries/") >= 0);
      }
    },

    _populateList: function(element, fields) {
      if (!element || !fields) {
        return;
      }

      var self = this,
        options = this.options;

      for (var i = 0; i < fields.length; i++) {
        var option = $scw("<option></option>").attr("value", fields[i].id).text(fields[i].title);

        if (this.options.selectValue && this.options.selectValue == fields[i].id) {
          option.attr("selected", true);
        }

        if (this.options.selectIndex === i) {
          this.options.selectValue = fields[i].id;
          option.attr("selected", true);
        }

        element.append(option);
      }

      if (options.selectValue === null) {
        options.selectValue = element.val();
      }

      options.change.call(self, options.selectValue, options.treeValue, options.key);
    },

    _createTree: function() {
      var self = this,
        options = this.options;

      var container = $scw('<div class="droptree-tree-container" style="display:inline;position:relative;float:left;overflow:hidden;"/>');
      container.uniqueId();

      container.droptree({
        data: options.treeData,
        value: options.treeValue,
        change: function(key) {
          options.treeValue = key;
          options.keyInput.toggle(key.toLowerCase().indexOf("/entries/") >= 0);
          if (options.change) {
            options.change.call(this, options.selectValue, options.treeValue, options.key);
          }
        }
      });

      options.tree = container;
      self.element.append(container);
    },

    _create: function() {

      var self = this;

      self._createSelect();
      self._createTree();
      self._createKey();
    }
  });

  $scw.widget("wffm.droptree", {
    options: {
      data: null,
      value: null,
      change: null,
      input: null
    },

    _createTree: function() {
      var self = this,
         options = this.options;

      var input = $scw('<input type="text" class="droptree-tree-value" style="cursor:pointer;"/>');
      input.uniqueId();
      input.val(options.value);
      options.input = input;

      var tree = $scw('<div class="droptree-tree" style=" display: none;left:0;"/>');

      self.element.append(input);
      self.element.append(tree);

      tree.dynatree({
        checkbox: false,
        selectMode: 1,
        children: options.data,

        onQuerySelect: function(select, node) {
          return !node.data.isFolder;
        },

        onSelect: function(select, node) {
          options.value = node.data.key;
          options.input.val(node.data.title);
          if (options.change) {
            options.change.call(self, options.value);
          }
        },

        onClick: function(node) {

          if (!node.data.isFolder) {
            node.toggleSelect();
            tree.slideToggle();
          }
        },

        cookieId: "dynatree-Cb4",
        idPrefix: "dynatree-Cb4-"
      });

      input.click(function() {
        $scw("div.droptree-tree").slideUp();
        tree.slideToggle();
      });
    },

    _create: function() {
      this._createTree();
    }
  });
});