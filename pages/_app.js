import React from "react";
import { BrowserRouter, Route, Switch, NavLink } from 'react-router-dom';

const MyApp = ({ Component, pageProps }) => {
  return <Component {...pageProps} />;
};

export default MyApp;
