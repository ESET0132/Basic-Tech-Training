import './App.css'


function Button({ label, onClick }) {
  return <button onClick={onClick}>{label}</button>
}

export default App
   function App() {
  // Function to increment
  const increment = () => {
    console.log("Increment clicked");
    const element = document.getElementById('counter');
    let current = parseInt(element.textContent);
    element.textContent = current + 1;
  };

  // Function to decrement
  const decrement = () => {
    console.log("Decrement clicked");
    const element = document.getElementById('counter');
    let current = parseInt(element.textContent);
    element.textContent = current - 1;
  };

  return (
    <div >
      <h1 id="counter">0</h1>
        <Button label="Increment" onClick={increment} />
        <Button label="Decrement" onClick={decrement} />
    </div>
  );
}
