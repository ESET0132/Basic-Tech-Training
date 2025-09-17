import React, { useState } from 'react';

const dummyJSON = {
  firstName: "myFirstName",
  lastName: "lastname",
  jsonkey: {}
};

export default function FormEventsComponents() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState(""); // New state for password

  const onChangehandler = (event) => {
    setUsername(event.target.value);
    console.log(username);
  };

  const onPasswordChangeHandler = (event) => {
    setPassword(event.target.value);
    console.log(password); // Log the current password
  };

  return (
    <div>
      <input type='text' onChange={onChangehandler} placeholder="Username" />
      <input type='password' onChange={onPasswordChangeHandler} placeholder="Password" /> {/* Added password field */}
    </div>
  );
}
