var PhraseUpdater = (function () {
    function PhraseUpdater(window, document) {
        this.window = window;
        this.document = document;
        this.phrase = JSON.parse(this.window.localStorage.getItem("phrase"));
        if (this.phrase == null) {
            this.updatePhrase();
        }
        else {
            this.updateHtml();
        }
        this.isReviewing = Boolean(this.window.localStorage.getItem("isReviewing"));
        if (this.isReviewing) {
            this.updatePhrase();
        }
        // TODO: should instead be done when trying to read/write, both here and about page?
        if (this.window.localStorage.getItem("passes") == null) {
            this.window.localStorage.setItem("passes", "0");
        }
        if (this.window.localStorage.getItem("fails") == null) {
            this.window.localStorage.setItem("fails", "0");
        }
    }
    PhraseUpdater.prototype.updatePhrase = function () {
        var _this = this;
        this.isReviewing = false;
        this.window.localStorage.setItem("isReviewing", "false");
        $.getJSON("api/random", function (result) {
            _this.phrase = result;
            _this.window.localStorage.setItem("phrase", JSON.stringify(_this.phrase));
            _this.updateHtml();
        });
    };
    PhraseUpdater.prototype.updateHtml = function () {
        this.document.title = "Japangolin | " + this.phrase.Kana;
        $("#kana").html(this.phrase.Kana);
        $("#userText").val("");
        $("#kanji").val("");
        $("#meanings").val("");
        $("#kanji").hide();
        $("#meanings").hide();
        $("#skipRow").hide();
        $("#proceedRow").hide();
    };
    PhraseUpdater.prototype.validate = function () {
        if (this.isReviewing) {
            this.updatePhrase();
        }
        else {
            var userInput = $("#userText").val();
            if (userInput === this.phrase.Romaji) {
                var passes = Number(this.window.localStorage.getItem("passes")) + 1;
                this.window.localStorage.setItem("passes", String(passes));
                this.showReview();
            }
            else {
                var fails = Number(this.window.localStorage.getItem("fails")) + 1;
                this.window.localStorage.setItem("fails", String(fails));
                $("#skipRow").show();
            }
        }
    };
    PhraseUpdater.prototype.showReview = function () {
        this.isReviewing = true;
        this.window.localStorage.setItem("isReviewing", "true");
        if (this.phrase.Kanji.length > 0) {
            var kanjiHtml = "<hr/>";
            this.phrase.Kanji.forEach(function (item) {
                kanjiHtml += "<p>" + item + "</p>";
            });
            $("#kanji").html(kanjiHtml);
            $("#kanji").show();
        }
        var meaningsHtml = "<hr/>";
        this.phrase.Meaning.forEach(function (item) {
            meaningsHtml += "<p>" + item + "</p>";
        });
        $("#meanings").html(meaningsHtml);
        $("#meanings").show();
        $("#skipRow").hide();
        $("#proceedRow").show();
    };
    return PhraseUpdater;
})();
