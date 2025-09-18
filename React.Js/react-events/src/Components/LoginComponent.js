import React, { useState } from 'react';

const LoginComponent = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [isLoggedIn, setIsLoggedIn] = useState(false); // State to track login status

  const handleSubmit = (event) => {
    event.preventDefault();
    console.log('Username:', username);
    console.log('Password:', password);
    
    // Here you can add your login logic
    // Assuming login is successful:
    setIsLoggedIn(true);
    resetForm(); // Reset the form fields after login
  };

  const resetForm = () => {
    setUsername('');
    setPassword('');
  };

  return (
    <div>
      <form onSubmit={handleSubmit}>
        <div>
          <label>
            Username:
            <input 
              type="text" 
              value={username} 
              onChange={(e) => setUsername(e.target.value)} 
              required 
            />
          </label>
        </div>
        <div>
          <label>
            Password:
            <input 
              type="password" 
              value={password} 
              onChange={(e) => setPassword(e.target.value)} 
              required 
            />
          </label>
        </div>
        <button type="submit">Login</button>
      </form>

      {isLoggedIn && <p>Logged In!</p>} {/* Display message if logged in */}
    </div>
  );
};

export default LoginComponent;
