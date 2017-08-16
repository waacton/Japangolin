import { addTask, fetch } from "domain-task";
import { EventState } from './Calendar';
import { StaticRouter } from 'react-router-dom';
import { Action, Reducer } from "redux";
import exampleEvents from "../exampledata/ExampleCalendarEvents";
import { AppThunkAction } from "./";

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface EventState {
    isLoading: boolean;
    eventList: Event[];
}

export interface Event {
    title: string;
    start: Date;
    end: Date;
    allDay: boolean;
    desc: string;
}

interface EventAddAction { type: "ADD_EVENT"; event: Event; }
interface EventDeleteAction { type: "DELETE_EVENT"; title: string; }
interface EventAddMadeUpAction { type: "ADD_MADE_UP_EVENT"; }
interface EventRequestCalendar { type: "REQUEST_EVENTS"; }
interface EventReceiveCalendar { type: "RECEIVE_EVENTS"; receivedEvents: Event[] }
interface EventClearAll { type: "CLEAR_LOCAL_EVENTS" }

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = EventAddAction | EventDeleteAction | EventAddMadeUpAction | EventRequestCalendar | EventReceiveCalendar | EventClearAll;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    addEvent: (title: string, start: Date, end: Date, desc: string, allDay: boolean) => <EventAddAction>{ type: "ADD_EVENT", event: {title, start, end, desc, allDay} },
    deleteEvent: (eventTitle: string) => <EventDeleteAction>{ type: "DELETE_EVENT", title: eventTitle },
    addMadeUpEvent: () => <EventAddMadeUpAction>{ type: "ADD_MADE_UP_EVENT" },
    clearLocalEvents: () => <EventClearAll>{ type: "CLEAR_LOCAL_EVENTS" },
    requestCalendarEvents: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const fetchTask = fetch(`/api/Calendar/Events`)
            .then(response => response.json() as Promise<Event[]>)
            .then(data => {
                dispatch({ type: "RECEIVE_EVENTS", receivedEvents: data });
            });

        addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
        dispatch({ type: "REQUEST_EVENTS" });
    },
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: EventState = {eventList: exampleEvents, isLoading: false};

export const reducer: Reducer<EventState> = (state: EventState = unloadedState, action: KnownAction) => {
    switch (action.type) {
        case 'ADD_EVENT':
            return {
                    eventList: 
                    [
                        ...state.eventList,
                        action.event
                    ],
                    isLoading: state.isLoading
                };
        case 'DELETE_EVENT':
            const eventIndex = state.eventList.findIndex((event) => event.title === action.title);
            return {
                eventList: 
                [
                    ...state.eventList.slice(0, eventIndex),
                    ...state.eventList.slice(eventIndex+1),
                ],
                isLoading: state.isLoading
            };
        case "ADD_MADE_UP_EVENT":
            return {
                eventList:
                [
                    ...state.eventList,
                    {
                        start: new Date(Date.now()),
                        end: new Date(Date.now() + 60*60*1000),
                        allDay: false,
                        title: "Made Up Event!",
                        desc: "Really, we just made this up!"
                    }                    
                ],
                isLoading: state.isLoading
            };
        case "REQUEST_EVENTS":
            return {
                eventList:
                [
                    ...state.eventList
                ],
                isLoading: true,
            };
        case "RECEIVE_EVENTS":
            return {
                eventList: action.receivedEvents,
                isLoading: false,
            };
        case "CLEAR_LOCAL_EVENTS":
            return {
                eventList: [],
                isLoading: state.isLoading,
            }
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            const exhaustiveCheck: never = action;
    }

    return state || unloadedState;
};
