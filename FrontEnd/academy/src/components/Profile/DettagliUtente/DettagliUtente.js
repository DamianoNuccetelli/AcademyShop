import React from 'react';
import './DettagliUtente.css';

import sessoMaschile from '../../../img/sesso maschile.png';

const DettagliUtente = () => {
    return (
        <div className='form_container'>
            <h2>Modifica il tuo profilo</h2>
            <img src={sessoMaschile} alt="Icona maschio" className='imgMaschio' />
            <div className='flex_row'>
                <input type="text" placeholder="Nome" />
                <input type="text" placeholder="Cognome" />
            </div>
            <div className='flex_row'>
                <input type="text" placeholder="Città di nascita" />
                <input type="text" placeholder="Provincia di nascita" />
            </div>
            <div className='flex_row'>
                <input type="date" className="input_data" />
                <select name="sesso" className="select_sesso">
                    <option value="" disabled selected hidden>Sesso</option> 
                    <option value="maschio">Maschio</option>
                    <option value="femmina">Femmina</option>
                </select>
            </div>
            <div className='flex_row'>
                <input type="text" placeholder="Email" />
                <input type="password" placeholder="Password" />
            </div>
            <div className='flex_row'>
                <input type="text" placeholder="Codice fiscale" />
                <input type="button" value="Salva" />
            </div>
        </div>
    );
};

export default DettagliUtente;
