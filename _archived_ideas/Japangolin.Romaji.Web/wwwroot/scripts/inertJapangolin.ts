$(document).ready(() => {
    var phraseUpdater = new InertPhraseUpdater(window, window.document);

    $("#skipButton").click(() => {
        phraseUpdater.updatePhrases();
    });

    $("#proceedButton").click(() => {
        phraseUpdater.updatePhrases();
    });

    $("#userText").keyup((event) => {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == 13) { // enter key
            phraseUpdater.validate();
        }
    });

    $(window).on("beforeunload", () => {
        phraseUpdater.saveState();
    });
});

/* old non-jQuery code, for curiosity! */
//window.onload = () => {
//    var phraseUpdater = new inertPhraseUpdater(window, window.document);

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