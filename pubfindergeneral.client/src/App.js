import './App.css';
import React, { useEffect, useState } from 'react';
import {createRoot } from 'react-dom/client';
import PubCardList from './Components/PubCardList.tsx';


function App() {
  let [result, setResult] = useState([])
  useEffect(() => {
    fetch(`http://localhost:3001/pubs`,{
      headers: {
        "Content-Type":"application/json"
      }
    })
    .then(res => {
        return res.json();
      })
      .then((data) => {
        setResult(data);
      },
      (err) => {
        return console.error(err)
      })
  }, [])
  
  return (
    <div className="App">
      <h1>Pub Finder General</h1>
      <div>
        { <PubCardList PubJsonList={result}/> }
      </div>
    </div>
  );
}
export default App;

const mainNode= document.getElementById('root');
const root = createRoot(mainNode);
root.render(<App />);
