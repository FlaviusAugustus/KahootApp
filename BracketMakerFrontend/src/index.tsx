import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import * as Router from "react-router-dom";
import GamemodeComponent from "./Components/GamemodeComponent";
import PlayerComponent from "./Components/PlayerComponent";
import HostComponent from "./Components/HostComponent";
import GameComponent from "./Components/GameComponent";

const router = Router.createBrowserRouter([
    {
        path: "/",
        element: <GamemodeComponent/>
    },
    {
        path: "/join",
        element: <PlayerComponent/>,
    },
    {
        path: "/host",
        element: <HostComponent/>
    },
    {
        path: "/game/:id",
        element: <GameComponent/>
    }
]);

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(

  <React.StrictMode>
      <Router.RouterProvider router={router}/>
  </React.StrictMode>
);