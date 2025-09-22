import React, { useState } from "react";

export default function ParentComponent() {
  const [count, setCount] = useState(0);
  
  return (
    <div>
      <SiblingA count={count} setCount={setCount} />
      <SiblingB count={count} />
      <SiblingC count={count} setCount={setCount} />
    </div>
  );
}

function SiblingA({ count, setCount }) {
  return (
    <div>
      <h2>Sibling A</h2>
      <p>Current count: {count}</p>
      <button onClick={() => setCount(count + 1)}>
        Increment
      </button>
    </div>
  );
}

function SiblingB({ count }) {
  return (
    <div>
      <h2>Sibling B</h2>
      <p>Count squared: {count * count}</p>
      <p>Count is {count % 2 === 0 ? 'even' : 'odd'}</p>
    </div>
  );
}

function SiblingC({ count, setCount }) {
  return (
    <div>
      <h2>Sibling C</h2>
      <button onClick={() => setCount(count - 1)}>
        Decrement
      </button>
      <button onClick={() => setCount(0)}>
        Reset
      </button>
    </div>
  );
}