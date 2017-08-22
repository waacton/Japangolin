import * as React from "react";
import { Route } from "react-router-dom";
import Calendar from "./components/Calendar";
import Counter from "./components/Counter";
import FetchData from "./components/FetchData";
import Home from "./components/Home";
import Japanese from "./components/Japanese";
import { Layout } from "./components/Layout";

export const routes = <Layout>
    <Route exact path="/" component={ Home } />
    <Route path="/counter" component={ Counter } />
    <Route path="/fetchdata/:startDateIndex?" component={ FetchData } />
    <Route path="/calendar" component={ Calendar } />
    <Route path="/japanese/:id?" component={ Japanese } />
</Layout>;
