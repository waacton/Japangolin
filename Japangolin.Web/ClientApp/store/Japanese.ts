import { addTask, fetch } from "domain-task";
import { Action, Reducer } from "redux";
import { AppThunkAction } from "./";

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface JapaneseState {
    index: number;
    phrase: JapanesePhrase;
}

export interface JapanesePhrase {
    kanji: string;
    kana: string;
    english: string;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.
// Use @typeName and isActionType for type detection that works even after serialization/deserialization.

interface DefaultJapaneseAction { type: "DEFAULT_JAPANESE" }
interface RequestJapaneseAction { type: "REQUEST_JAPANESE" }
interface ReceiveJapaneseAction { type: "RECEIVE_JAPANESE"; receivedJapanese: JapanesePhrase; newIndex: number }

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = DefaultJapaneseAction | RequestJapaneseAction | ReceiveJapaneseAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    defaultJapanese: () => <DefaultJapaneseAction>{ type: "DEFAULT_JAPANESE" },
    requestJapanese: (index: number): AppThunkAction<KnownAction> => (dispatch, getState) => {
        const fetchTask = fetch(`/api/Japanese/Get?id=${ index }`)
            .then(response => response.json() as Promise<JapanesePhrase>)
            .then(data => {
                dispatch({ type: "RECEIVE_JAPANESE", receivedJapanese: data, newIndex: index });
            });

        addTask(fetchTask); // Ensure server-side prerendering waits for this to complete
        dispatch({ type: "REQUEST_JAPANESE" });
        }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: JapaneseState = { index: -1, phrase: { english: "-", kana: "-", kanji: "-" } };

export const reducer: Reducer<JapaneseState> = (state: JapaneseState, action: KnownAction) => {
    switch (action.type) {
        case "DEFAULT_JAPANESE":
            return unloadedState;
        case "REQUEST_JAPANESE":
            return { index: state.index, phrase: state.phrase };
        case "RECEIVE_JAPANESE":
            return {
                index: action.newIndex,
                phrase: action.receivedJapanese
            };
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            const exhaustiveCheck: never = action;
    }

    // For unrecognized actions (or in cases where actions have no effect), must return the existing state
    //  (or default initial state if none was supplied)
    return state || unloadedState;
};
