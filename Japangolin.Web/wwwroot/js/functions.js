function updatePhrase(window, document) {
    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            var japanesePhrase = JSON.parse(xhttp.responseText);
            document.title = "Japangolin | " + japanesePhrase.Kana;
            document.getElementById("kana").innerHTML = japanesePhrase.Kana;

            var meaningsHtml = "";
            japanesePhrase.Meaning.forEach(function(item) {
                meaningsHtml += "<p>" + item + "</p>";
            });
            document.getElementById("meanings").innerHTML = meaningsHtml;
        }
    };

    xhttp.open("GET", "api/random?t=" + Math.random(), false); // unique ID to avoid caching (IE...)
    xhttp.send();
}