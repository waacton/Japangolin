window.onload = function () {
    var phraseUpdater = new PhraseUpdater(window, window.document);
    document.getElementById("skipButton").onclick = function () { phraseUpdater.update(); };
    document.getElementById("proceedButton").onclick = function () { phraseUpdater.update(); };
    document.getElementById("userText").onkeyup = function (event) { phraseUpdater.validate(event); };
    phraseUpdater.update();
};
