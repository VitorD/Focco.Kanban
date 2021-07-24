import React from "react";
import { Route, Redirect, Switch } from "react-router";
import Layout from "./components/Layout";
import Home from "./components/Home";
import Login from "./components/Login";
import "./App.css";

export default function App() {
   return (
      <Layout>
         <Switch>
            <Route exact path="/" component={Home} />
            <Route exact path="/login" component={Login} />
            <Redirect to="/" />
         </Switch>
      </Layout>
   );
}
