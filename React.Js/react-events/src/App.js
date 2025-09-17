import React from 'react';
import ButtonComponent from './Components/button_Components';
import MouseEvents from './Components/MouseEvents';
import FormEventsComponents from './Components/FormEventsComponents'; 

function App() {
  const handleClick = () => {
    console.log("Hello World!!");
  };

  return (
    <>
      <ButtonComponent onClick={handleClick} label="Click Me" />
      <MouseEvents /> {/* Include the MouseEvents component here */}

      <FormEventsComponents/>
    </>
  );
}

export default App;
