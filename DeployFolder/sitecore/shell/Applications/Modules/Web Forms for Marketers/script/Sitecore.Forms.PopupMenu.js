(function(scForm, scBrowser, $$) {
  window.Sitecore = window.Sitecore || {};
  window.Sitecore.Wfm = window.Sitecore.Wfm || {};

  window.Sitecore.Wfm.PopupMenu = new function() {
    var self = this;

    this.activePopup = null;
    this.args = null;

    this.show = function(event, controlid, height, width, args) {
      window.Sitecore.Wfm.PopupMenu.args = args;
		
      var evt = (scForm.lastEvent != null ? scForm.lastEvent : event);

      this.args = args;

      var doc = document;

      if (document.popup != null && evt != null && evt.srcElement != null) {
        doc = evt.srcElement.ownerDocument;
      }

      var node = {
        controlid: controlid,
        id: null,
        where: null,
        value: null
      };

      var element = null;

      if (node.controlid != null) {
        var ctl = scForm.browser.getControl(node.controlid, doc);

        if (ctl == null) {
          alert("Control \"" + node.controlid + "\" not found.");
          return;
        }

        node.value = ctl;
      }

      if (typeof (node.value) == "string") {
        element = doc.createElement("span");
        element.innerHTML = node.value;
      } else if (doc != node.value.ownerDocument) {
        element = doc.createElement("span");
        element.innerHTML = node.value.outerHTML;
        element.firstChild.style.display = "";
      } else {
        element = node.value.cloneNode(true);
        element.style.display = "";
        element.style.position = "";
      }

      var div = doc.createElement("div");
      div.setAttribute("style", "position:absolute;left:99999;top:99999;width:" + doc.body.offsetWidth + "px;height:" + doc.body.offsetHeight + "px");
      doc.body.appendChild(div);

      var span = doc.createElement("span");
      div.appendChild(span);

      span.appendChild(element);

      node.width = span.offsetWidth;
      node.height = span.offsetHeight;

      if (width != null) {
        node.width = width;
      }

      if (height != null) {
        node.height = height;
      }

      if (node.height + 300 > $$("body")[0].clientHeight) {
        node.height = Math.max(100, $$("body")[0].clientHeight - 350);
        node.width += 17;
      }

      scForm.browser.removeChild(div);

      showPopupExt(evt, node, evt.clientX - 180, evt.clientY);
    };

    function showPopupExt(event, data, clientX, clientY) {
      if (self.activePopup != null && self.activePopup.parentNode != null) {
        $(self.activePopup).remove();
      }

      var id = data.id;
      var evt = (scForm.lastEvent != null ? scForm.lastEvent : event);

      scBrowser.prototype.clearEvent(evt, true, false);

      var doc = document;
      if (scForm.lastEvent != null && scForm.lastEvent.target != null) {
        doc = scForm.lastEvent.target.ownerDocument;
      }

      var popup = document.createElement("div");

      popup.id = "Popup" + (scBrowser.prototype.popups != null ? scBrowser.prototype.popups.length + 1 : 0);
      popup.className = "scPopup";
      popup.style.position = "absolute";
      popup.style.left = clientX + "px";
      popup.style.top = clientY + "px";
      popup.onBlur = "scForm.browser.removeChild(scBrowser.prototype.parentNode)";

      var html = "";

      if (typeof (data.value) == "string") {
        html = data.value;
      } else {
        html = scBrowser.prototype.getOuterHtml(data.value);

        var p = html.indexOf(">");
        if (p > 0) {
          html = html.substring(0, p).replace(/display[\s]*\:[\s]*none/gi, "") + html.substr(p);
          html = html.substring(0, p).replace(/position[\s]*\:[\s]*absolute/gi, "") + html.substr(p);
        }
      }

      popup.innerHTML = html;

      document.body.appendChild(popup);
      var width = popup.offsetWidth;
      if (popup.baseURI.indexOf("ListItemsEditor") !== -1) {
		var height = 520;
	  } else {
		var height = popup.offsetHeight;
	  }

      var ctl = null;
      var x = evt.clientX != null ? evt.clientX : 0;
      var y = evt.clientY != null ? evt.clientY : 0;

      if (id != null && id != "") {
        ctl = scForm.browser.getControl(id, doc);

        if (ctl != null && ctl.offsetWidth > 0) {
          var dimensions = $(ctl).getDimensions();

          switch (data.where) {
            case "contextmenu":
              x = evt.pageX;
              y = evt.pageY;
              break;

            case "left":
              x = -width;
              y = 0;
              break;

            case "right":
              x = dimensions.width - 3;
              y = 0;
              break;

            case "above":
              x = 0;
              y = -height + 1;
              break;

            case "below-right":
              x = dimensions.width - width;
              y = dimensions.height;
              break;

            case "dropdown":
              x = 0;
              y = dimensions.height;
              width = dimensions.width;
              break;

            default:
              x = 0;
              y = dimensions.height;
          }

          var vp = $(ctl).viewportOffset();
          x += vp.left;
          y += vp.top;
        }
      }

      var viewport = document.body;
      if (viewport.clientHeight == 0) {
        var form = $$("form")[0];
        if (form && form.clientHeight > 0) {
          viewport = form;
        }
      }

      if (x + width > viewport.clientWidth) {
        x = document.body.clientWidth - width;
      }

      if (y + height > viewport.clientHeight) {
        y = viewport.clientHeight - height;
      }

      if (x < 0) {
        x = 0;
      }

      if (y < 0) {
        y = 0;
      }

      popup.style.width = "" + width + "px";
      popup.style.height = "" + height + "px";
      popup.style.top = "" + y + "px";
      popup.style.left = "" + x + "px";
      popup.style.zIndex = (scBrowser.prototype.popups == null ? 1000 : 1000 + scBrowser.prototype.popups.length);

      if (scBrowser.prototype.popups != null) {
        scBrowser.prototype.popups.push(popup);
      } else {
        scBrowser.prototype.popups = new Array(popup);
      }

      if (self.activePopup != null && self.activePopup.parentNode != null) {
        scBrowser.prototype.closePopups("show popup", new Array(popup));
      }

      self.activePopup = popup;
      scForm.focus(popup);
    }

    window.addEventListener("load", function() {
      Event.observe($$('form')[0], 'click', function() {
        scForm.browser.closePopups();
        if (self.activePopup != null && self.activePopup.parentNode != null) {
          $(self.activePopup).remove();
        }
      });
    });
  };
})(window.scForm, window.scBrowser, window.$$);