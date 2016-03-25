var Navigation = React.createClass({
    render: function() {
        return (
            <div className="navbar navbar-default navbar-fixed-top">
                <div className="container">
                    <div className="navbar-header">
                        <button type="button" className="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                            <span className="sr-only">Toggle navigation</span>
                            <span className="icon-bar"></span>
                            <span className="icon-bar"></span>
                            <span className="icon-bar"></span>
                        </button>
                        <a className="navbar-brand">Japangolin</a>
                    </div>
                    <div className="navbar-collapse collapse">
                        <ul className="nav navbar-nav">
                            <li><a>About</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        );
    }
});

var Kana = React.createClass({
    render: function() {
        return (
            <h1>
                {this.props.kana}
            </h1>
        );
    }
});

var UserRomaji = React.createClass({
    render: function() {
        return (
            <div className="form-group">
                <input id="userText" type="text" placeholder="Romaji..." className="form-control form-control-lg" />
            </div>
        );
    }
});

var SkipButton = React.createClass({
    render: function() {
        return (
            <div className="row" id="skipRow">
                <div className="col-sm-2">
                    {/* uses 2 of 12 columns for sm, md, lg, xl window sizes; defaults to 12 columns for anything smaller (xs) */}
                    <input id="skipButton" type="button" value="Skip" className="btn btn-warning btn-block" />
                </div>
            </div>
        );
    }
});

var ProceedButton = React.createClass({
    render: function() {
        return (
            <div className="row" id="proceedRow">
                <div className="col-sm-2">
                    {/* uses 2 of 12 columns for sm, md, lg, xl window sizes; defaults to 12 columns for anything smaller (xs) */}
                    <input id="proceedButton" type="button" value="Proceed" className="btn btn-success btn-block" />
                </div>
            </div>
        );
    }
});

var ListItemWrapper = React.createClass({
    render: function() {
        return <li>{this.props.text}</li>;
    }
});

var Kanji = React.createClass({
    render: function () {
        if (!this.props.kanji) {
            return null;
        }
        return (
            <ul>
                {this.props.kanji.map(function(kanjiItem) {
                    return <ListItemWrapper text={kanjiItem }/>;
                })}
            </ul>
        );
    }
});

var Meaning = React.createClass({
    render: function () {
        if (!this.props.meaning) {
            return null;
        }
        return (
            <ul>
                {this.props.meaning.map(function(meaningItem) {
                    return <ListItemWrapper text={meaningItem}/>;
                })}
            </ul>
        );
    }
});

var Japangolin = React.createClass({
    updatePhraseFromServer: function() {
        $.ajax({
            url: this.props.url,
            dataType: "json",
            cache: false,
            success: function(data) {
                this.setState({ currentPhrase: data });
                this.render();
            }.bind(this),
            error: function(xhr, status, err) {
                console.error(this.props.url, status, err.toString());
            }.bind(this)
        });
    },
    getInitialState: function () {
        return {
            currentPhrase: JSON.parse(window.localStorage.getItem("currentPhrase")),
            nextPhrase: JSON.parse(window.localStorage.getItem("nextPhrase")),
            isCurrentPassed: (window.localStorage.getItem("isCurrentPassed") == "true"),
            isCurrentFailed: (window.localStorage.getItem("isCurrentFailed") == "true"),
            passes: Number(window.localStorage.getItem("passes")), // Number(null) -> 0
            fails: Number(window.localStorage.getItem("fails"))
        };
    },
    componentDidMount: function () {
        if (this.state.currentPhrase == null) {
            this.updatePhraseFromServer();
        }
    },
    render: function () {
        var userButton = this.state.isCurrentPassed ? <ProceedButton /> : this.state.isCurrentFailed ? <SkipButton /> : null;

        // bootstrap's container provides default padding and margin
        return (
            <div className="container"> 
                <Navigation />
                <Kana kana={this.state.currentPhrase.Kana} />
                <UserRomaji />
                {userButton}
                <hr />
                <Kanji kanji={this.state.currentPhrase.Kanji} />
                <hr />
                <Meaning meaning={this.state.currentPhrase.Meaning} />
                <hr />
                <footer>
                    <p>&copy; 2016 - Wacton</p>
                </footer>
            </div>
        );
    }
});

ReactDOM.render(
  <Japangolin url="api/random" />,
  document.getElementById("content")
);