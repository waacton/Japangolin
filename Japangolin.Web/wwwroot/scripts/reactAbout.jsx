var PassFailChart = React.createClass({
    render: function () {
        return (
            <div>
                <canvas id="countsChart" width="200" height="200"></canvas>
            </div>
        );
    },
    componentDidMount: function() {
        this.drawChart();
    },
    componentDidUpdate: function() {
        this.drawChart();
    },
    drawChart: function() {
        var data = [
            {
                value: this.props.passes,
                color: "#5CB85C",
                highlight: "#449D44",
                label: "Passes"
            },
            {
                value: this.props.fails,
                color: "#F0AD4E",
                highlight: "#EC971F",
                label: "Fails"
            }
        ];

        var canvas = $("#countsChart").get(0);
        var ctx = (canvas).getContext("2d");
        var chart = new Chart(ctx).Pie(data);
    }
});

var About = React.createClass({
    getInitialState: function () {
        return {
            passes: Number(localStorage.getItem("passes")), // Number(null) -> 0
            fails: Number(localStorage.getItem("fails"))
        };
    },
    componentDidMount: function () {
        document.title = "Japangolin [React] | About";
    },
    render: function () {
        // bootstrap's "container" provides default padding and margin
        return (
            <div className="container"> 
                <div style={{ marginTop: 20 }}>
                    <h3 style={{display: "inline"}}>Japangolin</h3>
                    <h6 style={{display: "inline", marginLeft: 5}}>by Wacton</h6>
                </div>

                <div style={{ marginTop: 5, whiteSpace: "nowrap" }}>
                    <span className="glyphicon glyphicon-star" aria-hidden="true"></span>
                    <a href="https://github.com/waacton/Japangolin" style={{marginLeft: 5}}>github.com/waacton/Japangolin</a>
                </div>

                <hr />
                <PassFailChart passes={this.state.passes} fails={this.state.fails}/>

                <hr />
                <footer>
                    <p>&copy; 2016 - Wacton</p>
                </footer>
            </div>
        );
    }
});

ReactDOM.render(
  <About url="react/about" />,
  document.getElementById("content")
);