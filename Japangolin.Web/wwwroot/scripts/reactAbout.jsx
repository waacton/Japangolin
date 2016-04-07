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