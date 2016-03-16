$(document).ready(() => {
    var phraseUpdater = new PhraseUpdater(window, window.document);

    $("#skipButton").click(() => {
        phraseUpdater.updatePhrase();
    });

    $("#proceedButton").click(() => {
        phraseUpdater.updatePhrase();
    });

    $("#userText").keyup((event) => {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == 13) { // enter key
            phraseUpdater.validate();
        }
    });
});



/* old non-jQuery code, for curiosity! */
//window.onload = () => {
//    var phraseUpdater = new PhraseUpdater(window, window.document);

//    document.getElementById("skipButton").onclick = () => { phraseUpdater.updatePhrase(); }
//    document.getElementById("proceedButton").onclick = () => { phraseUpdater.updatePhrase(); }
//    document.getElementById("userText").onkeyup = (event) => {
//        var keycode = (event.keyCode ? event.keyCode : event.which);
//        if (keycode == 13) { // enter key
//            phraseUpdater.validate();
//        }
//    }

//    phraseUpdater.updatePhrase();
//};