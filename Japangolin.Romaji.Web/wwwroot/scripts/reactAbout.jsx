var PassFailChart = React.createClass({
    render: function () {
        var chart = null;
        if (this.hasChartData()) {
            chart = <canvas ref="countsChart" width="200" height="200" style={{marginBottom: 10}}></canvas>;
        }

        var total = this.props.passes + this.props.fails;
        var percentage = total === 0 ? 0 : Math.round((this.props.passes / total) * 100);

        var passString = `${this.props.passes} ${this.props.passes === 1 ? "pass" : "passes"}`;
        var failString = `${this.props.fails} ${this.props.fails === 1 ? "fail" : "fails"}`;
        var summaryString = `${passString}, ${failString} (${percentage}%)`;

        return (
            <div>
                {chart}
                <p>{summaryString}</p>
            </div>
        );
    },
    componentDidMount: function() {
        this.drawChart();
    },
    componentDidUpdate: function() {
        this.drawChart();
    },
    hasChartData: function () {
        return this.props.passes > 0 || this.props.fails > 0;
    },
    drawChart: function () {
        if (!this.hasChartData()) {
            return;
        }

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

        var canvas = this.refs.countsChart;
        var ctx = (canvas).getContext("2d");
        var chart = new Chart(ctx).Pie(data);
    }
});

var ClearButton = React.createClass({
    onClearClick: function(event) {
        this.props.handleClick();
        return;
    },
    render: function() {
        return (
            <div className="row">
                <div className="col-sm-2">
                    {/* uses 2 of 12 columns for sm, md, lg, xl window sizes; defaults to 12 columns for anything smaller (xs) */}
                    <input type="button" value="Clear" className="btn btn-primary btn-block" onClick={this.onClearClick}/>
                </div>
            </div>
        );
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
    clearLocalStorage: function () {
        console.log("Clearing local storage...");
        localStorage.clear();
        this.setState({
            passes: 0,
            fails: 0
        });
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
                    <a href="https://gitlab.com/Wacton/Japangolin" style={{marginLeft: 5}}>https://gitlab.com/Wacton/Japangolin</a>
                </div>

                <hr />
                <PassFailChart passes={this.state.passes} fails={this.state.fails} />

                <hr />
                <ClearButton handleClick={this.clearLocalStorage } />

                <hr />
                <footer>
                    <p>&copy; 2017 - Wacton</p>
                </footer>
            </div>
        );
    }
});

ReactDOM.render(
  <About url="react/about" />,
  document.getElementById("content")
);