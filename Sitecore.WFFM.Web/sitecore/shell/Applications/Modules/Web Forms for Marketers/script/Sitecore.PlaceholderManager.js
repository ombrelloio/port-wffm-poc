window.Sitecore = window.Sitecore || {};
window.Sitecore.Wfm = window.Sitecore.Wfm || {};

Sitecore.Wfm.PlaceholderManager = new function() {
  var self = this;

  this.load = function() {
    if ($('ActiveUrl') != null) {
      self.getPlaceholders($('ActiveUrl').value, setContent, setContent);
    }
  };

  this.getPlaceholders = function(url, success, failure) {
    if (url != null && url != '') {
      new Ajax.Request(url, {
        method: 'get',
        asynchronous: false,
        onSuccess: function(transport) {
          success(transport.responseText);
        },
        onFailure: function() {
          failure(null);
        }
      });
    } else {
      failure(null);
    }
  };

  this.highlightPlaceholder = function(element, evt, id) {
    var placeholder = $(id);

    if (placeholder != null) {
      if (evt.type == "mouseover") {
        placeholder.show();
      } else {
        placeholder.hide();
      }
    }

    showTooltip(element, evt);
  };

  this.onPlaceholderClick = function(element, evt, placeholder) {

    var e = $("ph_" + placeholder.replace(/[^a-zA-Z_0-9]/gi, "_"));

    $A($(e).up().childElements()).each(function(e) {
      e.className = "scPalettePlaceholder";
    });

    e.className = "scPalettePlaceholderSelected";

    $('__LISTACTIVE').value = e.id;
    $('__LISTVALUE').value = placeholder;

    Event.stop(evt);
  };

  function showTooltip(element, evt) {
    var tooltip = $(element.lastChild);
    var x = Event.pointerX(evt);
    var y = Event.pointerY(evt);

    if (evt.type == "mouseover") {
      if (tooltip.style.display == "none") {
        clearTimeout(this.tooltipTimer);

        this.tooltipTimer = setTimeout(function() {
          var html = tooltip.innerHTML;

          if (html == "") {
            return;
          }

          var t = $("scCurrentTooltip");
          if (t == null) {
            t = new Element("div", { "id": "scCurrentTooltip", "class": "scPalettePlaceholderTooltip", "style": "display:none" });
            document.body.appendChild(t);
          }

          t.innerHTML = html;

          t.style.left = x + "px";
          t.style.top = y + "px";
          t.style.display = "";
        }, 450);
      }
    } else {
      clearTimeout(this.tooltipTimer);
      var t = $("scCurrentTooltip");
      if (t != null) {
        t.style.display = "none";
      }
    }
  }

  function setContent(list) {
    if (list == 'indefined' || list == null || list.length == 0) {
      $('__LIST').innerHTML = "<div class='scEmptyContentListText'>" + $('__NO_PLACEHOLDERS_MESSAGE').value + "</div>";
    } else {
      $('__LIST').innerHTML = list;
    }

    var value = $('__LISTACTIVE').value;
    if (value != null && value != '') {
      value = value.replace(/emptyValue/, '');

      if ($(value) != null) {
        $('__LISTACTIVE').value = value;
      } else {
        $('__LISTACTIVE').value = 'emptyValue' + value;
      }
    }
  }

  Sitecore.Dhtml.attachEvent(window, "onload", self.load);
};