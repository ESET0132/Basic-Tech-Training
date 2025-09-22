import React, { useRef, useState } from 'react';

const ColorBoxHighlighter = () => {
  const boxRefs = useRef([]);
  const [currentHighlight, setCurrentHighlight] = useState(-1);

  const colors = ['red', 'green', 'blue'];
  const highlightStyle = {
    border: '4px solid yellow',
    boxShadow: '0 0 10px rgba(255, 255, 0, 0.5)'
  };

  const handleHighlightNext = () => {
    // Remove highlight from current box
    if (currentHighlight >= 0 && boxRefs.current[currentHighlight]) {
      boxRefs.current[currentHighlight].style.border = '2px solid #ccc';
      boxRefs.current[currentHighlight].style.boxShadow = 'none';
    }

    // Calculate next index (cycle back to 0 after last box)
    const nextIndex = (currentHighlight + 1) % colors.length;
    
    // Apply highlight to next box
    if (boxRefs.current[nextIndex]) {
      boxRefs.current[nextIndex].style.border = highlightStyle.border;
      boxRefs.current[nextIndex].style.boxShadow = highlightStyle.boxShadow;
    }

    setCurrentHighlight(nextIndex);
  };

  const resetHighlights = () => {
    // Remove all highlights
    boxRefs.current.forEach(box => {
      if (box) {
        box.style.border = '2px solid #ccc';
        box.style.boxShadow = 'none';
      }
    });
    setCurrentHighlight(-1);
  };

  return (
    <div style={{ padding: '20px', fontFamily: 'Arial, sans-serif' }}>
      <h2>Color Box Highlighter</h2>
      
      <div style={{ 
        display: 'flex', 
        gap: '20px', 
        marginBottom: '20px',
        justifyContent: 'center'
      }}>
        {colors.map((color, index) => (
          <div
            key={color}
            ref={el => boxRefs.current[index] = el}
            style={{
              width: '100px',
              height: '100px',
              backgroundColor: color,
              border: '2px solid #ccc',
              borderRadius: '8px',
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'center',
              color: 'white',
              fontWeight: 'bold',
              textShadow: '1px 1px 2px rgba(0,0,0,0.5)',
              transition: 'all 0.3s ease'
            }}
          >
            {color.charAt(0).toUpperCase() + color.slice(1)}
          </div>
        ))}
      </div>

      <div style={{ display: 'flex', gap: '10px', justifyContent: 'center' }}>
        <button
          onClick={handleHighlightNext}
          style={{
            padding: '10px 20px',
            backgroundColor: '#007bff',
            color: 'white',
            border: 'none',
            borderRadius: '5px',
            cursor: 'pointer',
            fontSize: '16px'
          }}
        >
          Highlight Next Box
        </button>
        
        <button
          onClick={resetHighlights}
          style={{
            padding: '10px 20px',
            backgroundColor: '#dc3545',
            color: 'white',
            border: 'none',
            borderRadius: '5px',
            cursor: 'pointer',
            fontSize: '16px'
          }}
        >
          Reset
        </button>
      </div>

      <div style={{ 
        marginTop: '20px', 
        textAlign: 'center',
        color: '#666'
      }}>
        <p>Current Highlight: {currentHighlight >= 0 ? colors[currentHighlight] : 'None'}</p>
      </div>
    </div>
  );
};

export default ColorBoxHighlighter;