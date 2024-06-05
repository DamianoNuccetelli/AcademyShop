import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './SignUp.css';
import logo from '../../img/Proconsul-Services.png';
import Modal from 'react-modal'

const SignUp = () => {
    // Stato per ogni campo di input
    const [nome, setNome] = useState('');
    const [cognome, setCognome] = useState('');
    const [cittaNascita, setCittaDiNascita] = useState('');
    const [dataNascita, setAnnoNascita] = useState('');
    const [provinciaNascita, setProvincia] = useState('');
    const [sesso, setSesso] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [codiceFiscale, setCodiceFiscale] = useState('');

    const [isModalOpen, setIsModalOpen] = useState(false);
    const navigate = useNavigate();
    const UrlApiRoot='https://localhost:7031/users';

  
    // Funzione per gestire l'invio del form
    const handleSubmit = async (event) => {
        
        event.preventDefault(); 
        try {
            const userData = {
                nome, 
                cognome,
                cittaNascita,
                dataNascita,
                provinciaNascita,
                sesso,
                email,
                password,
                codiceFiscale
 
            };
            
            console.log("Dati inviati:", userData); // Debugging

            const response = await fetch(UrlApiRoot, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(userData),
            });
            console.log(response)

            if (!response.ok) {
                throw new Error('Errore di rete');
            }

            const data = await response.json();
            console.log("Risposta del server:", data); 
                       
            setIsModalOpen(true);
            
        } catch (error) {
            console.error('Si è verificato un errore durante la registrazione:', error);
        }
    };

    const closeModal = () => {
        setIsModalOpen(false);
        window.location.reload();
    };

    return (
        <div className="form-container sign-up">
            <form onSubmit={handleSubmit}>
                <img src={logo} alt="Logo" />
                <h1>Crea un Account</h1>

                <span>o usa la tua email per registrarti</span>
                <div className="input-row">
                    <input type="text" placeholder="Nome" value={nome} onChange={(e) => setNome(e.target.value)} />
                    <input type="text" placeholder="Cognome" value={cognome} onChange={(e) => setCognome(e.target.value)} />
                </div>
                <div className="input-row">
                    <input type="text" placeholder="Città di nascita" value={cittaNascita} onChange={(e) => setCittaDiNascita(e.target.value)} />
                    <input type="text" placeholder="Provincia di nascita" value={provinciaNascita} onChange={(e) => setProvincia(e.target.value)} />
                </div>
                <div className="input-row">
                    <input type="date" value={dataNascita} onChange={(e) => setAnnoNascita(e.target.value)} />
                    <select value={sesso} onChange={(e) => setSesso(e.target.value)}>
                        <option value="">Seleziona il sesso</option>
                        <option value="M">Maschio</option>
                        <option value="F">Femmina</option>
                    </select>
                </div>
                <div className="input-row">
                    <input type="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} />
                    <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} />
                </div>
                <div className="input-single-row">
                    <input type="text" placeholder="Codice fiscale" value={codiceFiscale} onChange={(e) => setCodiceFiscale(e.target.value)} />
                </div>
                <button type="submit" onClick={handleSubmit}>Registrati</button>
            </form>
            <Modal
                isOpen={isModalOpen}
                onRequestClose={closeModal}
                contentLabel="Registrazione Completata"
                ariaHideApp={false}
                className="modal-login" // Assicurati di definire gli stili CSS per il modal
                overlayClassName="modal-overlay" // Assicurati di definire gli stili CSS per l'overlay del modal

            >
                <h2>Grazie per esserti iscritto!</h2>
                <p>Esegui il login per verificare la registrazione.</p>
                <button onClick={closeModal}>Chiudi</button>
            </Modal>


        </div>
    );
};

export default SignUp;