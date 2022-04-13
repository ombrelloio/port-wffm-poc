window.Sitecore = window.Sitecore || {};
window.Sitecore.Wfm = window.Sitecore.Wfm || {};

window.Sitecore.Wfm.Utils = new function() {
  this.IsFF = function() {
    var agt = navigator.userAgent.toLowerCase();
    return (agt.indexOf("firefox") != -1);
  };

  this.IsIE = function() {
    var agt = navigator.userAgent.toLowerCase();
    return ((agt.indexOf("msie") != -1) && (agt.indexOf("opera") == -1));
  };

  this.zoom = function(ctr) {
    var x = (document.body.scrollWidth - 40) / ctr.clientWidth;

    ctr.style.zoom = x + "";

    $(ctr).select('input').each(function(element) {
      element.disabled = true;
    });
  };

  this.select = function(obj, id) {
    if (event.srcElement != null && event.srcElement.tagName == "A") {
      var ctr = $(id);
      if (ctr.checked) {
        ctr.checked = false;
      } else {
        ctr.checked = true;
      }

      event.cancelBubble = true;
    }

    return true;
  };

  this.updateDisabled = function(checkboxControl, area, forbid) {
    if (!forbid) {
      $(area).disabled = !(checkboxControl.checked);

      $$('#' + area + ' input').each(function(element) {
        element.disabled = $(area).disabled;
      });
    }
  };

  this.updateChecked = function(checkboxControl, value) {
    if (!value) {
      checkboxControl.checked = value;
    }
  };
};