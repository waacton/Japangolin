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
                this.setState({ data: data });
                this.render();
            }.bind(this),
            error: function(xhr, status, err) {
                console.error(this.props.url, status, err.toString());
            }.bind(this)
        });
    },
    getInitialState: function () {
        return { data: { Kana: "Loading...", Kanji: [], Meaning: [] } };
    },
    componentDidMount: function () {
        this.updatePhraseFromServer();
    },
    render: function () {
        // bootstrap's container provides default padding and margin
        return (
            <div className="container"> 
                <Navigation />
                <Kana kana={this.state.data.Kana} />
                <UserRomaji />
                <hr />
                <Kanji kanji={this.state.data.Kanji} />
                <hr />
                <Meaning meaning={this.state.data.Meaning} />
                <hr />
            </div>
        );
    }
});

ReactDOM.render(
  <Japangolin url="api/random" />,
  document.getElementById("content")
);