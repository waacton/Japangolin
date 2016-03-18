var PhraseUpdater = (function () {
    function PhraseUpdater(window, document) {
        this.window = window;
        this.document = document;
        this.loadState();
        if (this.currentPhrase == null || this.nextPhrase == null) {
            this.initialisePhrases();
        }
    }
    PhraseUpdater.prototype.initialisePhrases = function () {
        var _this = this;
        $("#kana").html("Loading...");
        // this is an up-front cost to get the first phrase
        // future phrases will be retrieved in the background
        $.getJSON("api/random", function (result) {
            _this.nextPhrase = result;
            _this.updatePhrases();
        });
    };
    PhraseUpdater.prototype.updatePhrases = function () {
        var _this = this;
        this.isCurrentPassed = false;
        this.isCurrentFailed = false;
        this.currentPhrase = this.nextPhrase;
        this.updateHtml();
        // this will collect the next phrase in the background
        $.getJSON("api/random", function (result) {
            _this.nextPhrase = result;
        });
    };
    PhraseUpdater.prototype.updateHtml = function () {
        this.document.title = "Japangolin | " + this.currentPhrase.Kana;
        $("#kana").html(this.currentPhrase.Kana);
        $("#userText").val("");
        $("#kanji").val("");
        $("#meanings").val("");
        $("#kanji").hide();
        $("#meanings").hide();
        $("#skipRow").hide();
        $("#proceedRow").hide();
    };
    PhraseUpdater.prototype.validate = function () {
        if (this.isCurrentPassed) {
            this.updatePhrases();
        }
        else {
            var userInput = $("#userText").val();
            if (userInput.toLowerCase() === this.currentPhrase.Romaji.toLowerCase()) {
                this.isCurrentPassed = true;
                this.passes++;
                this.showPass();
            }
            else {
                this.isCurrentFailed = true;
                this.fails++;
                this.showFail();
            }
        }
    };
    PhraseUpdater.prototype.showPass = function () {
        if (this.currentPhrase.Kanji.length > 0) {
            var kanjiHtml = "<hr/>";
            this.currentPhrase.Kanji.forEach(function (item) {
                kanjiHtml += "<p>" + item + "</p>";
            });
            $("#kanji").html(kanjiHtml);
            $("#kanji").show();
        }
        var meaningsHtml = "<hr/>";
        this.currentPhrase.Meaning.forEach(function (item) {
            meaningsHtml += "<p>" + item + "</p>";
        });
        $("#meanings").html(meaningsHtml);
        $("#meanings").show();
        $("#skipRow").hide();
        $("#proceedRow").show();
    };
    PhraseUpdater.prototype.showFail = function () {
        $("#skipRow").show();
    };
    PhraseUpdater.prototype.saveState = function () {
        this.window.localStorage.setItem("currentPhrase", JSON.stringify(this.currentPhrase));
        this.window.localStorage.setItem("nextPhrase", JSON.stringify(this.nextPhrase));
        this.window.localStorage.setItem("isCurrentPassed", this.isCurrentPassed ? "true" : "false");
        this.window.localStorage.setItem("isCurrentFailed", this.isCurrentFailed ? "true" : "false");
        this.window.localStorage.setItem("passes", String(this.passes));
        this.window.localStorage.setItem("fails", String(this.fails));
        this.window.localStorage.setItem("userText", $("#userText").val());
    };
    PhraseUpdater.prototype.loadState = function () {
        this.currentPhrase = JSON.parse(this.window.localStorage.getItem("currentPhrase"));
        this.nextPhrase = JSON.parse(this.window.localStorage.getItem("nextPhrase"));
        this.isCurrentPassed = (this.window.localStorage.getItem("isCurrentPassed") == "true");
        this.isCurrentFailed = (this.window.localStorage.getItem("isCurrentFailed") == "true");
        this.passes = Number(this.window.localStorage.getItem("passes")); // Number(null) -> 0
        this.fails = Number(this.window.localStorage.getItem("fails"));
        this.updateHtml();
        $("#userText").val(this.window.localStorage.getItem("userText"));
        if (this.isCurrentPassed) {
            this.showPass();
        }
        else if (this.isCurrentFailed) {
            this.showFail();
        }
    };
    return PhraseUpdater;
})();
