window.onload = function () {
    var phraseUpdater = new PhraseUpdater(window, window.document);
    document.getElementById("updateButton").onclick = function () { phraseUpdater.update(); };
    document.getElementById("userText").onkeyup = function (event) { phraseUpdater.validate(event); };
    phraseUpdater.update();
};
