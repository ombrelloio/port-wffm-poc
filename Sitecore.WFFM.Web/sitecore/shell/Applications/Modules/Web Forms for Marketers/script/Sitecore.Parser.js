(function($j) {
  window.Sitecore.Wfm = window.Sitecore.Wfm || {};

  window.Sitecore.Wfm.Parser = new function() {
    var self = this;

    this.xmlDoc = null;

    this.parseXMLByTag = function(xml, tagValue, tagText) {
      var list = [];

      var array = parseXml(xml);
      $j(array).each(function() {
        if (this != null && this.tagName != null) {
          if (this.tagName.toLowerCase() == tagValue) {
            list[list.size()] = { value: this, text: this };
          } else if (this.tagName.toLowerCase() == tagText) {
            list[list.size() - 1].text = this;
          }
        } else {
          list[list.size()] = { value: this, text: this };
        }
      });

      return list;
    };

    this.parseXMLToArray = function(xml) {
      var rawXml = xml;
      if (xml != null && xml != "") {
        var list = [];
        var array = getDomParser(xml.replace(/&amp;quot;/g, '&quot;'));
        if (array != null) {
          $j(array).each(function() {
            var regex = new RegExp("<" + this.tagName + ">", "gi");
            var matches = rawXml.match(regex);
            var tag = matches[0].replace('<', '').replace('>', '');

            if (typeof this.innerHTML === 'undefined') {

              if (typeof (this.childNodes) !== 'undefined') {
                if (this.childNodes.length > 0) {
                  list[tag] = this.xml.substring(2 + tag.length, this.xml.length - tag.length - 3);
                } else {
                  list[tag] = this.text;
                }
              } else {

                var parent = $j("<div></div>");
                if (typeof this.xml === 'undefined') {
                  $j(this).children().each(function() {
                    parent.append(this);
                  });
                } else {
                  parent.append((this.firstChild || { xml: '' }).xml || '');
                }
                var regex = new RegExp("<" + this.tagName + ">", "gi");
                var matches = rawXml.match(regex);

                var value = parent.html();
                if (value == '') {
                  value = this.textContent;
                }

                list[tag] = value;
              }
            } else {
              list[tag] = this.innerHTML;
            }
          });
        } else {
          if (xml != null && xml != '') {
            var parse = xml;
            while (parse != '') {
              var regex = new RegExp("<\\??.+?>", "i");
              var matches = parse.match(regex);
              if (matches && matches.length > 0) {
                var tag = matches[0].replace('<', '').replace('>', '');

                var pos = parse.indexOf("</" + tag + ">");
                if (pos > -1) {
                  var value = parse.substring(tag.length + 2, pos);
                  list[tag] = value;
                  parse = parse.substring(pos + 3 + tag.length);
                } else {
                  regex = new RegExp(">.*?</", "i");
                  var matches = parse.match(regex);
                  if (matches && matches.length > 0) {
                    list[tag] = matches[0].substring(1, matches[0].length - 2);
                  }
                  parse = '';
                }
              }
            }
          }
        }

        return list;
      }

      return [];
    };

    this.toXml = function(array) {
      var value = "";

      for (var element in array) {
        if (array[element] == null || array[element].wrap == null) {
          value += '<' + element + '>' + (array[element] || '') + '</' + element + '>';
        }
      }

      return value;
    };

    function getDomParser(xml) {
      var xmlContent = "<div>" + xml + "</div>";

      try {
        self.xmlDoc = self.xmlDoc || new ActiveXObject("Microsoft.XMLDOM");
        self.xmlDoc.async = "false";
        self.xmlDoc.loadXML(xmlContent);
        if (self.xmlDoc.parseError.errorCode != 0) {
          return null;
        }

        return $j(self.xmlDoc.childNodes[0].childNodes);
      } catch (e) {
        try {
          return $j(xmlContent).children();
        } catch (e) {
          alert(e.message);
        }
      }

      return null;
    }

    function parseXml(xml) {
      return getDomParser(xml.replace(/&/g, '&amp;').replace(/&amp;quot;/g, '&quot;')) || [];
    }
  };
})(jQuery);