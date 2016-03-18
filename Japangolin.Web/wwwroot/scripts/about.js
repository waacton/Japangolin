var _this = this;
$(document).ready(function () {
    _this.updateCounts();
    _this.createChart();
    $("#clearStorageButton").click(function () {
        _this.window.localStorage.clear();
        _this.updateCounts();
        _this.createChart();
    });
});
function updateCounts() {
    var passes = Number(this.window.localStorage.getItem("passes"));
    var fails = Number(this.window.localStorage.getItem("fails"));
    var total = passes + fails;
    var percentage = total == 0 ? 0 : Math.round((passes / total) * 100);
    var passString = passes + " " + (passes == 1 ? "pass" : "passes");
    var failString = fails + " " + (fails == 1 ? "fail" : "fails");
    var countsHtml = passString + ", " + failString + " (" + percentage + "%)";
    $("#counts").html(countsHtml);
}
function createChart() {
    var passes = Number(this.window.localStorage.getItem("passes"));
    var fails = Number(this.window.localStorage.getItem("fails"));
    if (passes == 0 && fails == 0) {
        $("#countsChart").hide();
        return;
    }
    $("#countsChart").show();
    var data = [
        {
            value: passes,
            color: "#5CB85C",
            highlight: "#449D44",
            label: "Passes"
        },
        {
            value: fails,
            color: "#F0AD4E",
            highlight: "#EC971F",
            label: "Fails"
        }
    ];
    var canvas = $("#countsChart").get(0);
    var ctx = canvas.getContext("2d");
    var chart = new Chart(ctx).Pie(data);
}
