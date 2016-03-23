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
                <input id="userText" type="text" placeholder="Romaji..." className="form-control form-control-lg" style={{maxWidth : 280}} />
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
    getPhraseFromServer: function() {
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
        this.getPhraseFromServer();
    },
    render: function() {
        return (
            <div>
                <h1>Japangolin!</h1>
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