// App.js
import React, { useState } from 'react';

import './Login.css';
import SignIn from '../SignIn/SignIn';
import SignUp from '../SignUp/SignUp';
import TogglePanel from '../TogglePanel/TogglePanel';
import { useNavigate } from 'react-router-dom';

const Login = () => {
    const [active, setActive] = useState(false);
    const navigate = useNavigate();
    const [email, setEmail] = useState('');
    

    const handleToggle = () => {
        setActive(!active);
    };

    return (
        <div className='flex_container'>
        <div className={`container ${active ? 'active' : ''}`} id="container">
                {active ? <SignUp /> : <SignIn />}
                <TogglePanel onClick={handleToggle} />
        </div>
        </div>
    );
};

export default Login;
