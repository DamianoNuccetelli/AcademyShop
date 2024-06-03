import React from 'react';
import './SignUp.css';

import logo from '../../img/Proconsul-Services.png';

const SignUp = () => {
    return (
        <div className="form-container sign-up">
            <form>
                <img src={logo} alt="Logo" />
                <h1>Crea un Account</h1>

                <span>o usa la tua email per registrarti</span>
                <div className="input-row">
                    <input type="text" placeholder="Nome" />
                    <input type="text" placeholder="Cognome" />
                </div>
                <div className="input-row">
                    <input type="email" placeholder="Città di nascita" />
                    <input type="text" placeholder="Provincia di nascita" />
                </div>
                <div className="input-row">
                    <input type="date" placeholder="Data di nascita" />
                    <select name="cars" id="cars">
                        <option value="choose">Seleziona il sesso</option>
                        <option value="M">Maschio</option>
                        <option value="F">Femmina</option>
                    </select>
                </div>
                <div className="input-row">
                    <input type="email" placeholder="Email" />
                    <input type="password" placeholder="Password" />
                </div>
                <div className="input-single-row">
                    <input type="text" placeholder="Codice fiscale" />
                </div>
                <button>Registrati</button>
            </form>
        </div>
    );
};

export default SignUp;
