import React, { useState, useEffect } from 'react';
import './UtenteDetails.css';

import sessoMaschile from '../../../img/sesso maschile.png';

const DettagliUtente = () => {
    const [userData, setUserData] = useState({
        nome: '',
        cognome: '',
        cittaNascita: '',
        provinciaNascita: '',
        dataNascita: '',
        sesso: '',
        email: '',
        codiceFiscale: ''
    });

    useEffect(() => {
        fetch('https://localhost:7031/users/3')
            .then(response => response.json())
            .then(data => {
                setUserData({
                    nome: data.nome,
                    cognome: data.cognome,
                    cittaNascita: data.cittaNascita,
                    provinciaNascita: data.provinciaNascita,
                    dataNascita: data.dataNascita,
                    sesso: data.sesso === 'M' ? 'maschio' : 'femmina',
                    email: data.email,
                    codiceFiscale: data.codiceFiscale
                });
            })
            .catch(error => console.error('Errore nel recupero dei dati:', error));
    }, []);

    return (
        <div className='form_container'>
            <h2>Modifica il tuo profilo</h2>
            <img src={sessoMaschile} alt="Icona maschio" className='imgMaschio' />
            <div className='flex_row'>
                <input type="text" placeholder="Nome" value={userData.nome} disabled />
                <input type="text" placeholder="Cognome" value={userData.cognome} disabled />
            </div>
            <div className='flex_row'>
                <input type="text" placeholder="Città di nascita" value={userData.cittaNascita} disabled />
                <input type="text" placeholder="Provincia di nascita" value={userData.provinciaNascita} disabled />
            </div>
            <div className='flex_row'>
                <input type="date" className="input_data" value={userData.dataNascita} disabled />
                <select name="sesso" className="select_sesso" value={userData.sesso} disabled>
                    <option value="" disabled hidden>Sesso</option> 
                    <option value="maschio">Maschio</option>
                    <option value="femmina">Femmina</option>
                </select>
            </div>
            <div className='flex_row'>
                <input type="text" placeholder="Email" value={userData.email} disabled />
                {/* <input type="password" placeholder="Password" value="********" disabled /> */}
                <input type="text" placeholder="Codice fiscale" value={userData.codiceFiscale} disabled />
            </div>
            <div className='flex_row'>
                
                <input type="button" value="Salva" />
            </div>
        </div>
    );
};

export default DettagliUtente;
