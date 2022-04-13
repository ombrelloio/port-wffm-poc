window.Sitecore.Wfm = window.Sitecore.Wfm || {};

window.Sitecore.Wfm.EmailEditor = new function () {
  var self = this;

  var rad = null;
  var scEditor = null;

  this.dictionary = [];

  Sitecore.Dhtml.attachEvent(window, "onload", load);

  function createFieldMenu(dropDowns) {
    $A(dropDowns).each(function (element) {
      if (element.className === "reDropdown") {
        var spanElements = element.getElementsByTagName("span");
        if (spanElements && spanElements.length > 0 && spanElements[0].className == "Insert Field") {
          element.id = "Insert Field";
          var translation = self.dictionary['Insert Field'];
          element.title = translation;
          var html = spanElements[0].innerHTML.toString().replace(/Insert Field/g, translation);
          spanElements[0].innerHTML = html;
        }

        Sitecore.Dhtml.attachEvent(element, "onclick", showPopup);
      }
    });
  }

  function insertSitecoreLink(commandName, editor) {
    var language = "";
    var database;

    var html = editor.getSelectionHtml();

    var id = null;

    // internal link in form of <a href="~/link.aspx?_id=110D559FDEA542EA9C1C8A5DF7E70EF9">...</a>
    if (html) {
      id = getMediaId(html);
    }

    // link to media in form of <a href="~/media/CC2393E7CA004EADB4A155BE4761086B.ashx">...</a>
    if (!id) {
      var regex = /~\/media\/([\w\d]+)\.ashx/;
      var match = regex.exec(html);
      if (match && match.length >= 1 && match[1]) {
        id = match[1];
      }
    }

    var urlVars;
    if (!id) {
      urlVars = getUrlVars();
      id = urlVars["id"];
      language = urlVars["la"];
      database = urlVars["da"];
    }

    scEditor = editor;

    editor.showExternalDialog(
      "/sitecore/shell/default.aspx?xmlcontrol=RichText.InsertLink&la=" + language + "&fo=" + id + (database ? "&databasename=" + database : ""),
      null, //argument
      570,
      455,
      scInsertSitecoreLink, //callback
      null, // callback args
      "Insert Link",
      true, //modal
      1 + 4 + 32, /*Telerik.Web.UI.WindowBehaviors.Resize +  Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move*/ // behaviors
      false, //showStatusBar
      false //showTitleBar
    );
  }

  function getMediaId(html) {
    var id = null;

    //todo: remove workaround for #6437 ( "2.It works only if no text has been selected.") 
    ///////////////////////////////////////////////
    var prefixes = "|~/media/|/sitecore_files/";
    ///////////////////////////////////////////////

    var list = prefixes.split('|');

    if (!list) {
      id = getIdByMediaPrefix('~\\/media\\/([\\w\\d]+)\\.ashx', html);
    } else {
      for (var i = 0; i < list.length; i++) {
        if (list[i] != '') {
          id = getIdByMediaPrefix(list[i] + '([\\w\\d]+)\\.ashx', html);
          if (id) {
            break;
          }
        }
      }
    }

    return id;
  }

  function getIdByMediaPrefix(pattern, html) {
    var regex = new RegExp(pattern, 'm');
    var match = regex.exec(html);
    if (match && match.length >= 1 && match[1]) {
      return match[1];
    }

    return null;
  }

  function insertField(element) {
    if (!element) {
      return;
    }

    var editor = rad.scGetEditor();
    var renderfield = element.getAttribute("renderfield");
    if (renderfield && renderfield.length > 0)
      editor.pasteHtml("[<label id='" + element.id + "' renderfield='" + renderfield + "'>" + element.innerHTML + "</label>]");
    else
      editor.pasteHtml("[<label id='" + element.id + "'>" + element.innerHTML + "</label>]");
    hidePopupMenu();
  }

  function createPopupMenu() {
    var form = document.forms[0];
    if (!form) {
       return;
    }

    var div = document.createElement("div");
    div.className = "reDropDownBody";
    div.setAttribute("id", "InsertFieldDiv");
    div.style.visibility = "hidden";
    div.style.position = "absolute";
    div.style.width = "205px";
    div.style.height = Sitecore.Wfm.Utils.IsFF() ? "360px" : "340px";
    div.style.left = "12px";
    div.style.top = Sitecore.Wfm.Utils.IsFF() ? "162px" : "175px";
    div.style.zIndex = "100000";
    div.style.borderStyle = "Solid";
    div.style.borderWidth = "1px";
    div.style.borderColor = "#828282";
    div.style.background = "white";
    div.setAttribute("unselectable", "on");
    div.style.overflow = "auto";

    var table = document.createElement("table");
    table.setAttribute("cellpadding", "0");
    table.setAttribute("border", "0");
    table.setAttribute("unselectable", "on");
    table.style.cursor = "default";
    table.style.width = "100%";
    table.style.height = "100%";
    var i = 0;
    var fields = $("__Field").value.split('&');

    fields.each(function(field) {
      var pair = field.split('=');
      if (pair[1] != null) {
        var tr = table.insertRow(i);
        var td = tr.insertCell(0);
        td.style.width = "100%";
        td.style.fontSize = "15px";
        td.style.fontFamily = "arial";
        Sitecore.Dhtml.attachEvent(td, "onclick", function () { return insertField(td); });
        Sitecore.Dhtml.attachEvent(td, "onmouseover", function () { td.style.background = '#DFDFDF'; });
        Sitecore.Dhtml.attachEvent(td, "onmouseout", function () { td.style.background = 'white'; });

        td.setAttribute("id", pair[0]);
        if (pair[2] && pair[2].length > 0)
          td.setAttribute("renderfield", pair[2]);
        td.innerHTML = pair[1];
        i++;
      }
    });

    div.appendChild(table);
    form.appendChild(div);
  }

  function load() {
    var editor = $("Editor");
    if (!editor) {
      return;
    }

    rad = editor.contentWindow;

    refreshEditor(rad);

    var doc = editor.contentDocument || editor.contentWindow.document;

    createFieldMenu(doc.getElementsByTagName("a"));

    createPopupMenu();

    rad.Telerik.Web.UI.Editor.CommandList.InsertSitecoreLinkWFFM = insertSitecoreLink;
  }

  function refreshEditor(editor) {
    var lastChild = $(editor).document.lastChild;
    if (lastChild) {
      lastChild.lastChild.style.height = "";
    }
  }

  function getUrlVars() {
    var vars = {};
    window.location.href.replace(/[?&]+([^=&]+)=([^&]*)/gi, function (m, key, value) {
      vars[key] = value;
    });

    return vars;
  }

  function scInsertSitecoreLink(sender, returnValue) {
    if (!returnValue) {
      return;
    }

    var d = scEditor.getSelection().getParentElement();

    if (Sitecore.Wfm.Utils.IsFF() && d.tagName == "A") {
      d.parentNode.removeChild(d);
    } else {
      scEditor.fire("Unlink");
    }

    var text = scEditor.getSelectionHtml();

    if (Sitecore.Wfm.Utils.IsIE()) {
      text = scIeFixRteTextRange(scEditor);
    }

    if (text == "" || text == null || ((text.length === 15) && (text.substring(2, 15).toLowerCase() === "<p>&nbsp;</p>"))) {
      text = returnValue.text;
    } else {
      // if selected string is a full paragraph, we want to insert the link inside the paragraph, and not the other way around.
      var regex = /^[\s]*<p>(.+)<\/p>[\s]*$/i;
      var match = regex.exec(text);
      if (match && match.length >= 2) {
        scEditor.pasteHtml("<p><a href=\"" + returnValue.url + "\">" + match[1] + "</a></p>", "DocumentManager");
        return;
      }
    }

    scEditor.pasteHtml("<a href=\"" + returnValue.url + "\">" + text + "</a>", "DocumentManager");
  }

  function scIeFixRteTextRange(scEditor) {
    var text = scEditor.getSelectionHtml();
    var regex = /^([\s]*<p.*?>).+(<\/p>[\s]*)$/i;
    var match = regex.exec(text);

    if (match && match.length === 3) {
      var elem = scEditor.getSelectedElement();
      if (elem.parentElement.lastChild != elem) {
        var range = scEditor.getSelection().getRange();
        range.moveEnd('character', -1);
        scEditor.getSelection().selectRange(range);

        text = scEditor.getSelectionHtml();
      }
    }

    return text;
  }

  function showPopup(event) {
    var div = document.getElementById("InsertFieldDiv");
    if (div) {
      div.style.visibility = (div.style.visibility == "visible" ? "hidden" : "visible");
    }

    if (event) {
      Event.stop(event);
    }
  }

  function hidePopupMenu() {
    var element = document.getElementById("InsertFieldDiv");
    element.style.visibility = "hidden";
  }
};