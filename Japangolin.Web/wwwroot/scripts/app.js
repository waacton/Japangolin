//window.onload = () => {
//    var phraseUpdater = new PhraseUpdater(window, window.document);
//    document.getElementById("skipButton").onclick = () => { phraseUpdater.update(); }
//    document.getElementById("proceedButton").onclick = () => { phraseUpdater.update(); }
//    document.getElementById("userText").onkeyup = (event) => { phraseUpdater.validate(event); }
//    phraseUpdater.update();
//};
$(document).ready(function () {
    var phraseUpdater = new PhraseUpdater(window, window.document);
    $("#skipButton").click(function () {
        phraseUpdater.update();
    });
    document.getElementById("proceedButton").onclick = function () { phraseUpdater.update(); };
    document.getElementById("userText").onkeyup = function (event) { phraseUpdater.validate(event); };
    phraseUpdater.update();
});
