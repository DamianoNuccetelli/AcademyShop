import React, { useReducer } from 'react'; 
import './SignUp.css'; 

import logo from '../../img/Proconsul-Services.png'; 

import ErrorModal from './ErrorModal/ErrorModal'; 
import RegisteredModal from './RegisteredModal/RegisteredModal'; 

// Stato iniziale del form di registrazione
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
    isModalOpen: false, // Stato per il modal di errore
    isRegisteredModalOpen: false, // Stato per il modal di registrazione completata
};

// Funzione per aggiornare lo stato in base all'azione ricevuta
const reducer = (state, action) => {
    switch (action.type) {
        case 'SET_FIELD':
            return { ...state, [action.field]: action.value }; // Aggiorna il campo specificato con il nuovo valore
        case 'OPEN_MODAL':
            return { ...state, isModalOpen: true }; // Apre il modal di errore
        case 'CLOSE_MODAL':
            return { ...state, isModalOpen: false }; // Chiude il modal di errore
        case 'OPEN_REGISTERED_MODAL':
            return { ...state, isRegisteredModalOpen: true }; // Apre il modal di registrazione completata
        case 'CLOSE_REGISTERED_MODAL':
            return { ...state, isRegisteredModalOpen: false }; // Chiude il modal di registrazione completata
        default:
            return state; // Restituisce lo stato non modificato per azioni non riconosciute
    }
};

const SignUp = () => {
    const [state, dispatch] = useReducer(reducer, initialState); // Utilizza useReducer per gestire lo stato del componente
    const UrlApiRoot = 'https://localhost:7031/users'; // URL dell'API per la registrazione

    // Funzione per aggiornare i campi del form
    const handleChange = (field, value) => {
        dispatch({ type: 'SET_FIELD', field, value }); // Dispatch dell'azione per aggiornare il campo specificato
    };

    // Funzione per gestire l'invio del form
    const handleSubmit = async (event) => {
        event.preventDefault(); // Previene il comportamento predefinito del form

        // Validazione dei campi
        const { nome, cognome, cittaNascita, dataNascita, provinciaNascita, sesso, email, password, codiceFiscale } = state;
        if (!nome || !cognome || !cittaNascita || !dataNascita || !provinciaNascita || !sesso || !email || !password || !codiceFiscale) {
            dispatch({ type: 'OPEN_MODAL' }); // Apre il modal di errore se ci sono campi mancanti
            return;
        }

        try {
            const userData = { nome, cognome, cittaNascita, dataNascita, provinciaNascita, sesso, email, password, codiceFiscale };
            const response = await fetch(UrlApiRoot, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(userData), // Invia i dati dell'utente come JSON
            });

            if (!response.ok) {
                throw new Error('Errore di rete'); // Lancia un errore se la risposta non è OK
            }

            dispatch({ type: 'OPEN_REGISTERED_MODAL' }); // Apre il modal di registrazione completata se la richiesta è andata a buon fine
        } catch (error) {
            console.error('Si è verificato un errore durante la registrazione:', error);
            dispatch({ type: 'OPEN_MODAL' }); // Apre il modal di errore in caso di eccezione
        }
    };

    // Funzione per chiudere il modal di registrazione completata e ricaricare la pagina
    const closeModal = () => {
        dispatch({ type: 'CLOSE_REGISTERED_MODAL' }); 
        window.location.reload(); // Ricarica la pagina
    };

    // Funzione per chiudere il modal di errore
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

            {/* Modal per segnalare un errore */}
            <ErrorModal isOpen={state.isModalOpen} onClose={clodeModalError} />

            {/* Modal per confermare la registrazione */}
            <RegisteredModal isOpen={state.isRegisteredModalOpen} onClose={closeModal} />
        </div>
    );
};

export default SignUp;
