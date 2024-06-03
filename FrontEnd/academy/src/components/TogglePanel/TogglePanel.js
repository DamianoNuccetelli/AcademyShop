import React from 'react';
import './TogglePanel.css';

const TogglePanel = ({ onClick }) => {
    return (
        <div className="toggle-container">
            <div className="toggle">
                <div className="toggle-panel toggle-left">
                    <h1>Hai già un account?</h1>
                    <p>Inserisci i tuoi dati personali per utilizzare tutte le funzionalità del sito</p>
                    <button className="hidden" id="login" onClick={onClick}>Accedi</button>
                </div>
                <div className="toggle-panel toggle-right">
                    <h1>Sei nuovo?</h1>
                    <p>Registrati con i tuoi dati personali per utilizzare tutte le funzionalità del sito</p>
                    <button className="hidden" id="register" onClick={onClick}>Registrati</button>
                </div>
            </div>
        </div>
    );
};

export default TogglePanel;
