import React from 'react';
import logo from './logo.svg';
import './App.css';
import { WebSocket } from './WebSocket';
import ReactAccelerometer from 'react-accelerometer'

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
        <WebSocket />
        <ReactAccelerometer>
    {(position, rotation) => (
      <ul>
        <li>x: {position.x}</li>
        <li>y: {position.y}</li>
        <li>z: {position.z}</li>
        <li>rotation alpha: {rotation.alpha}</li>
        <li>rotation beta: {rotation.beta}</li>
        <li>rotation gamma: {rotation.gamma}</li>
      </ul>
    )}
  </ReactAccelerometer>
      </header>
    </div>
  );
}

export default App;
