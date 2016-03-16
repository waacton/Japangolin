var _this = this;
$(document).ready(function () {
    _this.updateCounts();
    $("#clearStorageButton").click(function () {
        _this.window.localStorage.clear();
        _this.window.localStorage.setItem("passes", "0");
        _this.window.localStorage.setItem("fails", "0");
        _this.updateCounts();
    });
});
function updateCounts() {
    var passHtml = "<p>Passes: " + this.window.localStorage.getItem("passes") + "</p>";
    var failHtml = "<p>Fails: " + this.window.localStorage.getItem("fails") + "</p>";
    var countsHtml = passHtml + failHtml;
    $("#counts").html(countsHtml);
}
