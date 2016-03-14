window.onload = () => {
    var phraseUpdater = new PhraseUpdater(window, window.document);

    document.getElementById("skipButton").onclick = () => { phraseUpdater.update(); }
    document.getElementById("proceedButton").onclick = () => { phraseUpdater.update(); }
    document.getElementById("userText").onkeyup = (event) => { phraseUpdater.validate(event); }

    phraseUpdater.update();
};