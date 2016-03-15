$(document).ready(function () {
    var phraseUpdater = new PhraseUpdater(window, window.document);
    $("#skipButton").click(function () {
        phraseUpdater.update();
    });
    $("#proceedButton").click(function () {
        phraseUpdater.update();
    });
    $("#userText").keyup(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == 13) {
            phraseUpdater.validate();
        }
    });
    phraseUpdater.update();
});
/* old non-jQuery code, for curiosity! */
//window.onload = () => {
//    var phraseUpdater = new PhraseUpdater(window, window.document);
//    document.getElementById("skipButton").onclick = () => { phraseUpdater.update(); }
//    document.getElementById("proceedButton").onclick = () => { phraseUpdater.update(); }
//    document.getElementById("userText").onkeyup = (event) => {
//        var keycode = (event.keyCode ? event.keyCode : event.which);
//        if (keycode == 13) { // enter key
//            phraseUpdater.validate();
//        }
//    }
//    phraseUpdater.update();
//}; 
