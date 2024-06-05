import React from 'react';
import './DettagliUtente.css';

import sessoMaschile from '../../../img/sesso maschile.png';

const DettagliUtente = () => {


    return (
        <div className='form_container'>
            <h2>Modifica il tuo profilo</h2>
            <img src={sessoMaschile} alt="Icona maschio" className='imgMaschio' />
        </div>
    );
};

export default DettagliUtente;