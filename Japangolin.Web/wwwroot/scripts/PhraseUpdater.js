var PhraseUpdater = (function () {
    function PhraseUpdater(window, document) {
        this.window = window;
        this.document = document;
    }
    PhraseUpdater.prototype.update = function () {
        var _this = this;
        this.isReviewing = false;
        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (xhttp.readyState == 4 && xhttp.status == 200) {
                _this.phrase = JSON.parse(xhttp.responseText);
                _this.document.title = "Japangolin | " + _this.phrase.Kana;
                _this.document.getElementById("kana").innerHTML = _this.phrase.Kana;
                _this.document.getElementById("userText").value = "";
                _this.document.getElementById("kanji").style.display = "none";
                _this.document.getElementById("meanings").style.display = "none";
                _this.document.getElementById("skipRow").style.display = "none";
                _this.document.getElementById("proceedRow").style.display = "none";
            }
        };
        xhttp.open("GET", "api/random", true);
        xhttp.send();
    };
    PhraseUpdater.prototype.validate = function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == 13) {
            if (this.isReviewing) {
                this.update();
            }
            else {
                var userInput = this.document.getElementById("userText").value;
                if (userInput === this.phrase.Romaji) {
                    this.showReview();
                }
                else {
                    this.document.getElementById("skipRow").style.display = "block";
                }
            }
        }
    };
    PhraseUpdater.prototype.showReview = function () {
        this.isReviewing = true;
        if (this.phrase.Kanji.length > 0) {
            var kanjiHtml = "<hr/>";
            this.phrase.Kanji.forEach(function (item) {
                kanjiHtml += "<p>" + item + "</p>";
            });
            this.document.getElementById("kanji").innerHTML = kanjiHtml;
            this.document.getElementById("kanji").style.display = "inline";
        }
        var meaningsHtml = "<hr/>";
        this.phrase.Meaning.forEach(function (item) {
            meaningsHtml += "<p>" + item + "</p>";
        });
        this.document.getElementById("meanings").innerHTML = meaningsHtml;
        this.document.getElementById("meanings").style.display = "inline";
        this.document.getElementById("skipRow").style.display = "none";
        this.document.getElementById("proceedRow").style.display = "block";
    };
    return PhraseUpdater;
})();
