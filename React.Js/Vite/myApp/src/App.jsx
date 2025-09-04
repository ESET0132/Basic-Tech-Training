import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'


function counter() {
var a = 5;
var b = 10;
  const add = (a,b) => a+b; 
  console.log(add(a,b));
  console.log("Hello World");
  console.log(count);   //hoisting , not getting error even if count is used before declaration
  var count = 0;
  console.log(count);
  count  = 5;
  console.log(count);

  const user = { name: "shivansh", age: 22 };
  console.log(user);
  console.log(user.name);     //destructuring

  // let a = 10;
  // let b = 20;
  [a, b] = [b, a];   //swapping
  console.log(a);
  console.log(b);


const original = [1, 2, 3];
// const copied = [...original]; //spread operator, creates new memory reference
const copied = original   //shallow copy, locates same memory reference

console.log(copied); 
console.log(original === copied); 


const name = ["shivansh", "kumar", "gupta"];
const moreNames = ["rahul", "singh", ...name]; //spread operator
console.log(moreNames);


  return(
    <div>
      <h1 id = 'a'>0</h1>
      <button onClick={() =>{
        const element = document.getElementById('a');
        var curr = parseInt(element.textContent);
        element.textContent = curr + 1;}}>Increment</button>
        <button onClick={() =>{
        const element = document.getElementById('a');
        var curr = parseInt(element.textContent);
        element.textContent = curr - 1;}}>Decrement</button>
        </div>
          
      
  );
}

// function App() {
//   const [count, setCount] = useState(0)

//   return (
//     <>
//       <div>
//         <a href="https://vite.dev" target="_blank">
//           <img src={viteLogo} className="logo" alt="Vite logo" />
//         </a>
//         <a href="https://react.dev" target="_blank">
//           <img src={reactLogo} className="logo react" alt="React logo" />
//         </a>
//       </div>
//       <h1>Vite + React</h1>
//       <div className="card">
//         <button onClick={() => setCount((count) => count + 1)}>
//           count is {count}
//         </button>
//         <p>
//           Edit <code>src/App.jsx</code> and save to test HMR
//         </p>
//       </div>
//       <p className="read-the-docs">
//         Click on the Vite and React logos to learn more
//       </p>
//     </>
//   )
// }

export default counter
