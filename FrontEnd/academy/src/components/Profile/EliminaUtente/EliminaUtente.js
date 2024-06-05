import React from 'react';
import './EliminaUtente.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import sessoMaschile from '../../../img/sesso maschile.png';
import { useNavigate } from 'react-router-dom';
import { useState, useEffect } from 'react';

const userId = localStorage.getItem('userId');
const API_URL = 'https://localhost:7031/users/';


const EliminaUtente = () => {

    const navigate = useNavigate();
    
    // const handleDelete = () => {
    //     fetch(`https://localhost:7031/users/${userId}`, {
    //         method: 'DELETE',
    //         headers: {
    //             'Content-Type': 'application/json',
    //         },
    //     })
    //     .then(response => {
    //         if (!response.ok) {
    //             throw new Error('Network response was not ok ' + response.statusText);
    //         }
    //         console.log('Account eliminato con successo:', response.statusText);
    //         navigate('/Login');
    //         window.location.reload();

    //     })
    //     .catch(error => {
    //         console.error('Errore durante la cancellazione dell\'account:', error);
    //     });
    // };
    const handleDelete = async () => {
        try {
          const response = await fetch(`https://localhost:7031/users/${userId}`, {
            method: 'DELETE',
            headers: {
              'Content-Type': 'application/json',
            },
          });
      
          if (!response.ok) {
            throw new Error('Network response was not ok ' + response.statusText);
          }
      
          console.log('Account eliminato con successo:', response.statusText);
          navigate('/Login');
          window.location.reload();
        } catch (error) {
          console.error('Errore durante la cancellazione dell\'account:', error);
        }
      };
      

    return (
        <div className='form_container'>
            <h2>Questa operazione è irreversibile e tutti i tuoi dati andranno persi.</h2>
            <img src={sessoMaschile} alt="Icona maschio" className='imgMaschio' />
            <button className='delete-button' onClick={handleDelete}>Elimina</button>
            {/* <FontAwesomeIcon icon={fa}>Elimina Il Tuo account</FontAwesomeIcon> */}
            {/* Aggiungi il contenuto specifico per la rimozione del profilo */}
        </div>
    );
};

export default EliminaUtente;
