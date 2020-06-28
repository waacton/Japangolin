import * as Counter from "./Counter";
import * as WeatherForecasts from "./WeatherForecasts";
import * as Calendar from "./calendar";
import * as Japanese from "./Japanese";

// The top-level state object
export interface ApplicationState {
    counter: Counter.CounterState,
    weatherForecasts: WeatherForecasts.WeatherForecastsState,
    calendarState: Calendar.EventState,
    japaneseState: Japanese.JapaneseState
}

// Whenever an action is dispatched, Redux will update each top-level application state property using
// the reducer with the matching name. It's important that the names match exactly, and that the reducer
// acts on the corresponding ApplicationState property type.
export const reducers = {
    counter: Counter.reducer,
    weatherForecasts: WeatherForecasts.reducer,
    calendarState: Calendar.reducer,
    japaneseState: Japanese.reducer
};

// This type can be used as a hint on action creators so that its 'dispatch' and 'getState' params are
// correctly typed to match your store.
export interface AppThunkAction<TAction> {
    (dispatch: (action: TAction) => void, getState: () => ApplicationState): void;
}
