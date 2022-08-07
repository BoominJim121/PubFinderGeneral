import './App.css';
import React from 'react';
import {createRoot } from 'react-dom/client';
import Main from './Components/Main.tsx';

function App() {
  return (
     <Main /> 
  );
}
export default App;

const mainNode= document.getElementById('root');
const root = createRoot(mainNode);
root.render(<App />);
