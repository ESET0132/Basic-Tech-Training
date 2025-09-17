import React from 'react';

const ButtonComponent = ({ onClick, label }) => {
  return (
    <button onClick={onClick}>
      {label}
    </button>
  );
};

export default ButtonComponent;
