class PhraseUpdater {
    window: Window;
    document: Document;
    phrase: any;
    isReviewing: boolean;

    constructor(window: Window, document: Document) {
        this.window = window;
        this.document = document;
    }

    update() {
        this.isReviewing = false;

        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = () => {
            if (xhttp.readyState == 4 && xhttp.status == 200) {
                this.phrase = JSON.parse(xhttp.responseText);

                this.document.title = `Japangolin | ${this.phrase.Kana}`;
                this.document.getElementById("kana").innerHTML = this.phrase.Kana;
                (<HTMLInputElement>this.document.getElementById("userText")).value = "";

                this.document.getElementById("kanji").style.display = "none";
                this.document.getElementById("meanings").style.display = "none";
            }
        };

        xhttp.open("GET", "api/random", true);
        xhttp.send();
    }

    validate(event: KeyboardEvent) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == 13) { // enter key
            var userInput = (<HTMLInputElement>this.document.getElementById("userText")).value;
            if (userInput === this.phrase.Romaji) {
                if (this.isReviewing) {
                    this.update();
                } else {
                    this.showReview();
                }
            } 
        }
    }

    showReview() {
        this.isReviewing = true;

        if (this.phrase.Kanji.length > 0) {
            var kanjiHtml = "<hr/>";
            this.phrase.Kanji.forEach(item => {
                kanjiHtml += `<p>${item}</p>`;
            });
            this.document.getElementById("kanji").innerHTML = kanjiHtml;
            this.document.getElementById("kanji").style.display = "inline";
        }
        
        var meaningsHtml = "<hr/>";
        this.phrase.Meaning.forEach(item => {
            meaningsHtml += `<p>${item}</p>`;
        });
        this.document.getElementById("meanings").innerHTML = meaningsHtml;
        this.document.getElementById("meanings").style.display = "inline";
    }
}