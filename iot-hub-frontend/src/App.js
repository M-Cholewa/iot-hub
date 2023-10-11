import axios from "axios";
import React, { useState, useEffect } from "react";
import { serverAddress } from "./core/config/server.js";

export const App = () => {
  const [weatherData, setweatherData] = useState("");
  useEffect(() => {
    axios
      .get(`${serverAddress}/WeatherForecast`)
      .then((res) => setweatherData(res))
      .catch((err) => console.log(err));
  }, []);

  return (
    <div className="App">
      <header className="App-header">
        <h1>{JSON.stringify(weatherData)}</h1>
      </header>
    </div>
  );
};
