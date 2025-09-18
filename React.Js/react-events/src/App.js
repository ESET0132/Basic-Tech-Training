import React from 'react';
import ButtonComponent from './Components/button_Components';
import MouseEvents from './Components/MouseEvents';
import FormEventsComponents from './Components/FormEventsComponents'; 
import LoginComponent from './Components/LoginComponent'; // Import the LoginComponent
import UseEffectComponent from './Components/UseEffectComponent';

function App() {
  const handleClick = () => {
    console.log("Hello World!!");
  };

  return (
    <UseEffectComponent>
      <ButtonComponent onClick={handleClick} label="Click Me" />
      <MouseEvents />
      <FormEventsComponents />
      <LoginComponent /> 
      </UseEffectComponent>
    
  );
}

export default App;
