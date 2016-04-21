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
    handleKeyUp: function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == 13) { // enter key
            this.props.handleInputEntered(event.target.value);
        }
    },
    handleChange: function (event) {
        this.props.handleInputChanged(event.target.value);
    },
    render: function () {
        return (
            <div className="form-group">
                <input type="text" placeholder="Romaji..." className="form-control form-control-lg" 
                       value={this.props.text} onKeyUp={this.handleKeyUp} onChange={this.handleChange} disabled={this.props.isDisabled}/>
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
            <div className="row">
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
            <div className="row">
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

var ReactJapangolin = React.createClass({
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
            this.getNextPhraseFromServer(true);
        } else {
            document.title = "Japangolin [React] | " + this.state.currentPhrase.Kana;
        }
    },
    getNextPhraseFromServer: function (updateCurrentPhrase) {
        $.getJSON(this.props.url, function(data) {
            console.log("Setting next phrase: " + JSON.stringify(data));
            this.setState({ nextPhrase: data }, () => {
                if (updateCurrentPhrase) {
                    console.log("Setting next phrase callback: pushing to current phrase");
                    this.updateCurrentPhrase();
                } else {
                    console.log("Setting next phrase callback: saving state");
                    this.saveState();
                }
            });
            return (data);
        }.bind(this));
    },
    updateCurrentPhrase: function() {
        console.log("Setting current phrase: " + JSON.stringify(this.state.nextPhrase));
        this.setState({
            isCurrentPassed: false,
            isCurrentFailed: false,
            currentPhrase: this.state.nextPhrase,
            userText: ""
        }, () => {
            document.title = "Japangolin [React] | " + this.state.currentPhrase.Kana;
            console.log("Setting current phrase callback: saving state");
            this.saveState();
            
        });
        
        this.getNextPhraseFromServer(false);
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
    saveUserRomaji: function (userInput) {
        this.setState({ userText: userInput }, () => this.saveState());
    },
    renderButton: function() {
        var button = null;
        if (this.state.isCurrentPassed) {
            button = <ProceedButton handleClick={this.updateCurrentPhrase } />;
        }  else if (this.state.isCurrentFailed) {
            button = <SkipButton handleClick={this.updateCurrentPhrase } />;
        }

        return button;
    },
    renderDetails: function () {
        var details = null;
        if (this.state.isCurrentPassed) {
            var kanjiDetails = null;
            if (this.state.currentPhrase.Kanji.length > 0) {
                kanjiDetails = (
                    <div>
                        <hr />
                        <Kanji kanji={this.state.currentPhrase.Kanji } />
                    </div>
                );
            }

            var meaningDetails = null;
            if (this.state.currentPhrase.Meaning.length > 0) {
                meaningDetails = (
                    <div>
                        <hr />
                        <Meaning meaning={this.state.currentPhrase.Meaning } />
                    </div>
                );
            }

            details = (
                <div>
                    {kanjiDetails}
                    {meaningDetails}
                </div>
            );
        }

        return details;
    },
    render: function () {
        var button = this.renderButton();
        var details = this.renderDetails();

        // bootstrap's "container" provides default padding and margin
        return (
            <div className="container"> 
                <Kana kana={this.state.currentPhrase == null ? "Loading..." : this.state.currentPhrase.Kana} />
                <UserRomaji text={this.state.userText} isDisabled={this.state.currentPhrase == null || this.state.nextPhrase == null} handleInputEntered={this.validateUserRomaji} handleInputChanged={this.saveUserRomaji} />
                {button}
                {details}
                <hr />
                <footer>
                    <p>&copy; 2016 - Wacton</p>
                </footer>
            </div>
        );
    }
});

ReactDOM.render(
  <ReactJapangolin url="/api/random" />,
  document.getElementById("content")
);