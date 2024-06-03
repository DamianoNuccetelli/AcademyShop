// App.js
import React, { useState } from 'react';

import './Login.css';
import SignIn from '../SignIn/SignIn';
import SignUp from '../SignUp/SignUp';
import TogglePanel from '../TogglePanel/TogglePanel';

const Login = () => {
    const [active, setActive] = useState(false);

    const handleToggle = () => {
        setActive(!active);
    };

    return (
        <div className={`container ${active ? 'active' : ''}`} id="container">
            <div className='prova'>
                {active ? <SignUp /> : <SignIn />}
                <TogglePanel onClick={handleToggle} />
            </div>
        </div>
    );
};

export default Login;
