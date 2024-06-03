// App.js
import React, { useState } from 'react';
import SignIn from './components/SignIn/SignIn';
import SignUp from './components/SignUp/SignUp';
import TogglePanel from './components/TogglePanel/TogglePanel';
import './App.css';

const App = () => {
    const [active, setActive] = useState(false);

    const handleToggle = () => {
        setActive(!active);
    };

    return (
        <div className={`container ${active ? 'active' : ''}`} id="container">
            {active ? <SignUp /> : <SignIn />}
            <TogglePanel onClick={handleToggle} />
        </div>
    );
};

export default App;
