$(document).ready(function () {
    var phraseUpdater = new PhraseUpdater(window, window.document);
    $("#skipButton").click(function () {
        phraseUpdater.updatePhrases();
    });
    $("#proceedButton").click(function () {
        phraseUpdater.updatePhrases();
    });
    $("#userText").keyup(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == 13) {
            phraseUpdater.validate();
        }
    });
    $(window).on("beforeunload", function () {
        phraseUpdater.saveState();
    });
});
/* old non-jQuery code, for curiosity! */
//window.onload = () => {
//    var phraseUpdater = new PhraseUpdater(window, window.document);
//    document.getElementById("skipButton").onclick = () => { phraseUpdater.updatePhrases(); }
//    document.getElementById("proceedButton").onclick = () => { phraseUpdater.updatePhrases(); }
//    document.getElementById("userText").onkeyup = (event) => {
//        var keycode = (event.keyCode ? event.keyCode : event.which);
//        if (keycode == 13) { // enter key
//            phraseUpdater.validate();
//        }
//    }
//    phraseUpdater.updatePhrases();
//}; 
