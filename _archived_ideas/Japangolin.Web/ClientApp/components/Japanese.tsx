import * as React from "react";
import { connect } from "react-redux";
import { Link, RouteComponentProps } from "react-router-dom";
import { ApplicationState } from "../store";
import * as JapaneseStore from "../store/Japanese";

type JapaneseProps =
    JapaneseStore.JapaneseState
    & typeof JapaneseStore.actionCreators
    & RouteComponentProps<{}>;

class Japanese extends React.Component<JapaneseProps, {}> {
    componentWillMount() {
        // This method runs when the component is first added to the page
        this.props.defaultJapanese();
    }

    public render() {
        return <div>
            <h1>Random Japanese:</h1>

            <p>{ this.props.phrase.kanji }</p>
            <p>{ this.props.phrase.kana }</p>
            <p>{ this.props.phrase.english }</p>

            <button className="btn btn-default pull-left" onClick={ () => { this.props.requestJapanese(this.props.index - 1) } } disabled={this.props.index <= 0}>Previous</button>
            <button className="btn btn-default pull-right" onClick={ () => { this.props.requestJapanese(this.props.index + 1) } }>Next</button>
        </div>;
    }
}

// Wire up the React component to the Redux store
export default connect(
    (state: ApplicationState) => state.japaneseState, // Selects which state properties are merged into the component's props
    JapaneseStore.actionCreators                 // Selects which action creators are merged into the component's props
)(Japanese) as typeof Japanese;
