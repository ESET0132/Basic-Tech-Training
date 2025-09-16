import './App.css';
import { useState } from 'react';

function App(props) {
  const [counter, setCounter] = useState(0); // INITIAL STATE

  const buttonHandler = () => {
    setCounter(counter + 1);
  };
  const buttonHandler2 = () => {
    setCounter(counter - 1);
  };

  const buttonStyle = {
    padding: '10px 20px',
    fontSize: '16px',
    margin: '5px',
    cursor: 'pointer',
    alignItems: 'center',
  };

  return (
    <div className='container'>
      <p>{props.name ? props.name : "Default Name"}</p>
      <p>{props.tagline}</p>

      <div className='counter'>
        <p style={{ fontSize: '100px', flexDirection: 'column', display: 'flex', alignItems: 'center' }}>
          {counter}
        </p>
      </div>
      <div className='button-container' style={buttonStyle}>
        <button onClick={buttonHandler} className='button'>
          Increment
        </button>
        <button onClick={buttonHandler2} className='button'>
          Decrement
        </button>
      </div>
    </div>
  );
}

export default App;
