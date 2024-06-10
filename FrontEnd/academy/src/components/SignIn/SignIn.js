import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './SignIn.css';
import logo from '../../img/Proconsul-Services.png';


const SignIn = () => {
    const navigate = useNavigate();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
   // let [userId, setUserId] = useState('');
//   const userSessionId =  sessionStorage.setItem('userIdSession', userSessionId);
//   const userId = sessionStorage.getItem('userId');

    const UrlApiRoot = "https://localhost:7031/";


    const handleSignIn = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch(UrlApiRoot + `managed-users?email=${email}&password=${password}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            if (!response.ok) {
                throw new Error('Login failed');
            }

            const data = await response.json();
            const userId = data.id;

            // Save userId in sessionStorage and localStorage
            sessionStorage.setItem('userId', userId);
            localStorage.setItem('userId', userId);

            // Navigate to another page on successful login
            navigate('/Dashboard');

        } catch (error) {
            console.error('Error:', error);
            setError('Login failed. Please check your email and password.');
        }
    };

    return (
        <div className="form-container sign-in">
            <form onSubmit={handleSignIn}>
                <img src={logo} alt="Logo" />
                <h1>Accedi</h1>
                <span>o usa la tua email e password</span>
                <div>
                    <input 
                        type="email" 
                        placeholder="Email" 
                        value={email} 
                        onChange={(e) => setEmail(e.target.value)} 
                        required 
                    />
                    <input 
                        type="password" 
                        placeholder="Password" 
                        value={password} 
                        onChange={(e) => setPassword(e.target.value)} 
                        required 
                    />
                </div>
                {error && <p className="error">{error}</p>}
                <a href="#">Hai dimenticato la password?</a>
                <button type="submit">Accedi</button>
            </form>
        </div>
    );
};

export default SignIn;
