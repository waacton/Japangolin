import moment = require("moment");
import * as React from "react";
import BigCalendar from "react-big-calendar";
import { connect } from "react-redux";
import { Link, RouteComponentProps } from "react-router-dom";

import AddEventButton from "./addeventbutton";
import { ApplicationState } from "../store";
import * as CalendarState from '../store/calendar'
import exampleData from '../exampledata/ExampleCalendarEvents'

BigCalendar.momentLocalizer(moment);

type CalendarProps =
CalendarState.EventState                                    // ... state we've requested from the Redux store
& typeof CalendarState.actionCreators                                                          // ... plus action creators we've requested
& RouteComponentProps<{}>;

class Calendar extends React.Component<CalendarProps, {}> {
    public render() {
        return <div >
            <BigCalendar
                events={this.props.eventList}
            />

            <AddEventButton whenClicked={() => this.props.addMadeUpEvent()} name="Add Random Local Event" />
            <AddEventButton whenClicked={this.alertTest.bind(this)} name="Say Hello!" />
            <AddEventButton whenClicked={() => this.props.clearLocalEvents()} name="Clear Events" />
            <AddEventButton whenClicked={() => this.props.requestCalendarEvents()} name="Get Events" />
        </div>;
    }
    
    public alertTest() {
        alert("Hello!");
    }
}

// Wire up the React component to the Redux store
export default connect(
   (state: ApplicationState) => state.calendarState,    // Selects which state properties are merged into the component's props
   CalendarState.actionCreators                         // Selects which action creators are merged into the component's props
)(Calendar) as typeof Calendar;
