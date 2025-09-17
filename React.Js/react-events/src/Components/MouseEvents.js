function MouseEvents() {
    return (
        <>
            <div style={{ 
                background: "black", 
                color: "white", 
                padding: "10px" }}
                
                
                onMouseEnter = { ()=> {console.log(" Mouse entered div")}}
                > Welcome to mouse events</div>
            
        </>
    );
}

export default MouseEvents;
