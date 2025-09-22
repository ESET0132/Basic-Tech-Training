import React, { useState } from 'react';

const ThemeSwitcher = () => {
  const [isDark, setIsDark] = useState(false);

  const toggleTheme = () => {
    setIsDark(!isDark);
  };

  const containerStyle = {
    backgroundColor: isDark ? '#1a1a1a' : 'white',
    color: isDark ? 'white' : 'black',
    minHeight: '100vh',
    padding: '20px',
    transition: 'all 0.3s ease'
  };

  const buttonStyle = {
    padding: '10px 20px',
    backgroundColor: isDark ? '#333' : '#007bff',
    color: 'white',
    border: 'none',
    borderRadius: '5px',
    cursor: 'pointer',
    fontSize: '16px'
  };

  return (
    <div style={containerStyle}>
      <h1>Hello Students!</h1>
      <p>Click the button to switch themes</p>
      
      <button 
        onClick={toggleTheme}
        style={buttonStyle}
      >
        {isDark ? 'Switch to Light' : 'Switch to Dark'}
      </button>
      
      <p>Current theme: {isDark ? 'Dark' : 'Light'}</p>
    </div>
  );
};

export default ThemeSwitcher;