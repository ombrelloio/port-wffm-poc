(function() {
  window.Sitecore = window.Sitecore || {};
  window.Sitecore.Wfm = window.Sitecore.Wfm || {};

  if (window.Sitecore.Wfm.ControlledChecklist) {
    return;
  }

  window.Sitecore.Wfm.ControlledChecklist = {
    changeStateInAllSibling: function(ctrl, state) {
      var nodes = getAllCheckboxes(ctrl);
      nodes.each(function(element, index) {
        if (index !== 0) {
          element.checked = state;
        }
      });
    },

    copyStateToAllSibling: function(ctrl) {
      this.changeStateInAllSibling(ctrl, ctrl.checked);
    },

    updateState: function(sender, elementIndex) {
      var nodes = getAllCheckboxes(sender);
      var state = true;

      if (!sender.checked) {
        state = false;
      } else {
        nodes.each(function(element, index) {
          if (elementIndex !== index && !element.checked) {
            state = false;
          }
        });
      }

      nodes.first().checked = state;
    }
  };

  function getAllCheckboxes(el) {
    var parent = $(el).up().up().up().up();
    return parent.select('input[type="checkbox"]');
  }
})();