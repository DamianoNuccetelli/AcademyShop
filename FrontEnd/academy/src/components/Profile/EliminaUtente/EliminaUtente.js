import React from 'react';
import './EliminaUtente.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import sessoMaschile from '../../../img/sesso maschile.png';


const userId = localStorage.getItem('userId');

const EliminaUtente = () => {




    return (
        <div className='form_container'>
            <h2>Questa operazione è irreversibile e tutti i tuoi dati andranno persi.</h2>
            <img src={sessoMaschile} alt="Icona maschio" className='imgMaschio' />
            <button className='delete-button'>Elimina</button>
            {/* <FontAwesomeIcon icon={fa}>Elimina Il Tuo account</FontAwesomeIcon> */}
            {/* Aggiungi il contenuto specifico per la rimozione del profilo */}
        </div>
    );
};

export default EliminaUtente;
