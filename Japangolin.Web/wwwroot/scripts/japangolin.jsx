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
    getInitialState: function() {
        return { value: this.props.initialText };
    },
    handleKeyUp: function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == 13) { // enter key
            console.log("Enter key detected -> " + event.target.value);
            this.props.handleInputEntered(event.target.value);
        }
    },
    handleChange: function (event) {
        this.setState({ value: event.target.value });
        this.props.handleInputChanged(event.target.value);
    },
    render: function() {
        return (
            <div className="form-group">
                <input type="text" placeholder="Romaji..." className="form-control form-control-lg" 
                       value={this.state.value} onKeyUp={this.handleKeyUp} onChange={this.handleChange} disabled={this.props.isDisabled}/>
            </div>
        );
    }
});

var SkipButton = React.createClass({
    onSkipClick: function(event) {
        this.props.handleClick();
        return;
    },
    render: function() {
        return (
            <div className="row" id="skipRow">
                <div className="col-sm-2">
                    {/* uses 2 of 12 columns for sm, md, lg, xl window sizes; defaults to 12 columns for anything smaller (xs) */}
                    <input type="button" value="Skip" className="btn btn-warning btn-block" onClick={this.onSkipClick}/>
                </div>
            </div>
        );
    }
});

var ProceedButton = React.createClass({
    onProceedClick: function(event) {
        this.props.handleClick();
        return;
    },
    render: function() {
        return (
            <div className="row" id="proceedRow">
                <div className="col-sm-2">
                    {/* uses 2 of 12 columns for sm, md, lg, xl window sizes; defaults to 12 columns for anything smaller (xs) */}
                    <input type="button" value="Proceed" className="btn btn-success btn-block" onClick={this.onProceedClick}/>
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
    getInitialState: function () {
        return {
            currentPhrase: JSON.parse(localStorage.getItem("currentPhrase")),
            nextPhrase: JSON.parse(localStorage.getItem("nextPhrase")),
            isCurrentPassed: (localStorage.getItem("isCurrentPassed") == "true"),
            isCurrentFailed: (localStorage.getItem("isCurrentFailed") == "true"),
            passes: Number(localStorage.getItem("passes")), // Number(null) -> 0
            fails: Number(localStorage.getItem("fails")),
            userText: localStorage.getItem("userText")
        };
    },
    componentDidMount: function () {
        if (this.state.currentPhrase == null || this.state.nextPhrase == null) {
            this.getNextPhraseFromServer();
            this.updateCurrentPhrase();
        }
    },
    getNextPhraseFromServer: function () {
        $.ajax({
            url: this.props.url,
            dataType: "json",
            cache: false,
            success: function (data) {
                this.setState({ nextPhrase: data });
                this.saveState();
                this.render();
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(this.props.url, status, err.toString());
            }.bind(this)
        });
    },
    updateCurrentPhrase: function () {
        this.setState({
            isCurrentPassed: false,
            isCurrentFailed: false,
            currentPhrase: this.state.nextPhrase
        });

        this.getNextPhraseFromServer();
        this.saveState();
    },
    saveState: function() {
        localStorage.setItem("currentPhrase", JSON.stringify(this.state.currentPhrase));
        localStorage.setItem("nextPhrase", JSON.stringify(this.state.nextPhrase));
        localStorage.setItem("passes", String(this.state.passes));
        localStorage.setItem("fails", String(this.state.fails));
        localStorage.setItem("isCurrentPassed", this.state.isCurrentPassed ? "true" : "false");
        localStorage.setItem("isCurrentFailed", this.state.isCurrentFailed ? "true" : "false");
        localStorage.setItem("userText", this.state.userText);
    },
    validateUserRomaji: function (userInput) {
        if (this.state.isCurrentPassed) {
            this.updateCurrentPhrase();
            return;
        }

        if (userInput.toLowerCase() === this.state.currentPhrase.Romaji.toLowerCase()) {
            this.setState({ isCurrentPassed: true, passes: this.state.passes + 1, userText: userInput.toString() }, () => this.saveState());
        } else {
            this.setState({ isCurrentFailed: true, fails: this.state.fails + 1, userText: userInput.toString() }, () => this.saveState());
        }
    },
    saveUserRomaji: function(userInput) {
        this.setState({ userText: userInput }, () => this.saveState());
    },
    render: function () {
        var userButton;
        if (this.state.isCurrentPassed) {
            userButton = <ProceedButton handleClick={this.updateCurrentPhrase } />;
        }  else if (this.state.isCurrentFailed) {
            userButton = <SkipButton handleClick={this.updateCurrentPhrase } />;
        } else {
            userButton = null;
        }

        // bootstrap's "container" provides default padding and margin
        return (
            <div className="container"> 
                <Navigation />
                <Kana kana={this.state.currentPhrase.Kana} />
                <UserRomaji initialText={this.state.userText} isDisabled={this.state.currentPhrase == null || this.state.nextPhrase == null} handleInputEntered={this.validateUserRomaji} handleInputChanged={this.saveUserRomaji} />
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