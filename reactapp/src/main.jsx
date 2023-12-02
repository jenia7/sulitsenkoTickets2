import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import { RouterProvider } from "react-router-dom";
import { Provider } from "react-redux";
import store from "./app/store";
import router from "./AppRoutes";
import { fetchConcerts } from "./features/concerts/concertsSlice";

let initialized = false;
if (!initialized) {
  ymaps.ready(init);
  function init() {
    var map = new ymaps.Map("map", {
      center: [53.9, 27.56],
      zoom: 11,
    });
    map.geoObjects
      .add(new ymaps.Placemark([53.9, 27.5], { iconCaption: "Info about concert1" }))
      .add(new ymaps.Placemark([53.9, 27.6], { iconCaption: "Info about concert2" }));
  }
  store.dispatch(fetchConcerts({ filters: [], searchPattern: "" }));
}

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <Provider store={store}>
      <RouterProvider router={router} />
    </Provider>
  </React.StrictMode>
);
