import React from 'react';
import './SignIn.css';

import logo from '../../img/Proconsul-Services.png';


const SignIn = () => {
    return (
        <div className="form-container sign-in">
            <form>
            <img src={logo} alt="Logo" />
                <h1>Accedi</h1>
                
                <span>o usa la tua email e password</span>
                <input type="email" placeholder="Email" />
                <input type="password" placeholder="Password" />
                <a href="#">Hai dimenticato la password?</a>
                <button>Accedi</button>
            </form>
        </div>
    );
};

export default SignIn;
