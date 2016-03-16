$(document).ready(() => {
    this.updateCounts();

    $("#clearStorageButton").click(() => {
        this.window.localStorage.clear();
        this.window.localStorage.setItem("passes", "0");
        this.window.localStorage.setItem("fails", "0");
        this.updateCounts();
    });
});

function updateCounts() {
    var passHtml = `<p>Passes: ${this.window.localStorage.getItem("passes")}</p>`;
    var failHtml = `<p>Fails: ${this.window.localStorage.getItem("fails")}</p>`;
    var countsHtml = passHtml + failHtml;
    $("#counts").html(countsHtml);
}