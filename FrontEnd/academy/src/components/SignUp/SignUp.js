import React, { useReducer } from 'react';
import { useNavigate } from 'react-router-dom';
import './SignUp.css';

import logo from '../../img/Proconsul-Services.png';

import Modal from 'react-modal';
import ErrorModal from './ErrorModal/ErrorModal';
import RegisteredModal from './RegisteredModal/RegisteredModal';


const initialState = {
    nome: '',
    cognome: '',
    cittaNascita: '',
    dataNascita: '',
    provinciaNascita: '',
    sesso: '',
    email: '',
    password: '',
    codiceFiscale: '',
    isModalOpen: false,
    isRegisteredModalOpen: false,
    errors: {},
};

const reducer = (state, action) => {
    switch (action.type) {
        case 'SET_FIELD':
            return { ...state, [action.field]: action.value };
        case 'OPEN_MODAL':
            return { ...state, isModalOpen: true };
        case 'CLOSE_MODAL':
            return { ...state, isModalOpen: false };
        case 'OPEN_REGISTERED_MODAL':
            return { ...state, isRegisteredModalOpen: true };
        case 'CLOSE_REGISTERED_MODAL':
            return { ...state, isRegisteredModalOpen: false };
        case 'SET_ERRORS': 
            return { ...state, errors: action.errors };
        default:
            return state;
    }
};

const SignUp = () => {
    const [state, dispatch] = useReducer(reducer, initialState);
    const navigate = useNavigate();
    const UrlApiRoot='https://localhost:7031/users';

    const handleChange = (field, value) => {
        dispatch({ type: 'SET_FIELD', field, value });
    };

    const handleSubmit = async (event) => {
        event.preventDefault();

        // Validazione dei campi
        const { nome, cognome, cittaNascita, dataNascita, provinciaNascita, sesso, email, password, codiceFiscale } = state;
        if (!nome || !cognome || !cittaNascita || !dataNascita || !provinciaNascita || !sesso || !email || !password || !codiceFiscale) {
            dispatch({ type: 'OPEN_MODAL' });
            return;
        }

        try {
            const userData = { ...state };
            const response = await fetch(UrlApiRoot, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(userData),
            });

            if (!response.ok) {
                throw new Error('Errore di rete');
            }

            dispatch({ type: 'OPEN_REGISTERED_MODAL' });
        } catch (error) {
            console.error('Si è verificato un errore durante la registrazione:', error);
            dispatch({ type: 'OPEN_MODAL' });
        }
    };

    const closeModal = () => {
        dispatch({ type: 'CLOSE_REGISTERED_MODAL' }); 
        window.location.reload();
    };

    const clodeModalError = () => {
        dispatch({ type: 'CLOSE_MODAL' });
    };

    return (
        <div className="form-container sign-up">
            <form onSubmit={handleSubmit}>
                <img src={logo} alt="Logo" />
                <h1>Crea un Account</h1>
                <span>o usa la tua email per registrarti</span>
                <div className="input-row">
                    <input type="text" placeholder="Nome" value={state.nome} onChange={(e) => handleChange('nome', e.target.value)} />
                    <input type="text" placeholder="Cognome" value={state.cognome} onChange={(e) => handleChange('cognome', e.target.value)} />
                </div>
                <div className="input-row">
                    <input type="text" placeholder="Città di nascita" value={state.cittaNascita} onChange={(e) => handleChange('cittaNascita', e.target.value)} />
                    <input type="text" placeholder="Provincia di nascita" value={state.provinciaNascita} onChange={(e) => handleChange('provinciaNascita', e.target.value)} />
                </div>
                <div className="input-row">
                    <input type="date" value={state.dataNascita} onChange={(e) => handleChange('dataNascita', e.target.value)} />
                    <select value={state.sesso} onChange={(e) => handleChange('sesso', e.target.value)}>
                        <option value="">Seleziona il sesso</option>
                        <option value="M">Maschio</option>
                        <option value="F">Femmina</option>
                    </select>
                </div>
                <div className="input-row">
                    <input type="email" placeholder="Email" value={state.email} onChange={(e) => handleChange('email', e.target.value)} />
                    <input type="password" placeholder="Password" value={state.password} onChange={(e) => handleChange('password', e.target.value)} />
                </div>
                <div className="input-single-row">
                    <input type="text" placeholder="Codice fiscale" value={state.codiceFiscale} onChange={(e) => handleChange('codiceFiscale', e.target.value)} />
                </div>
                <button type="submit">Registrati</button>
            </form>

            <ErrorModal isOpen={state.isModalOpen} onClose={clodeModalError} />

            {/* Modal per confermare la registrazione */}
            <RegisteredModal isOpen={state.isRegisteredModalOpen} onClose={closeModal} />
        </div>
    );
};

export default SignUp;
