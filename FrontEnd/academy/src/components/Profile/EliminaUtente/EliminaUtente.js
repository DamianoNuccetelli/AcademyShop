import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import ConfirmationModal from './ConfirmationModal/ConfirmationModal';
import sessoMaschile from '../../../img/sesso maschile.png';
import './EliminaUtente.css'; 

const EliminaUtente = () => {
  const navigate = useNavigate();
  const [modalIsOpen, setModalIsOpen] = useState(false);

  const handleDelete = async () => {

    const userId = sessionStorage.getItem('userId');

    try {
      const response = await fetch(`https://localhost:7031/users/${userId}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
        },
        cache: "no-cache",
      });

      if (!response.ok) {
        throw new Error("Failed to delete user. " + response.statusText);
      }

      sessionStorage.removeItem('userId');
      navigate("/Login");
      window.location.reload();
    } catch (error) {
      console.error('Error deleting user:', error);
    }
  };

  return (
    <div className="form_container">
      <h2>Cancella il tuo account</h2>
      <h4>Questa operazione è irreversibile e tutti i tuoi dati andranno persi.</h4>
      <img src={sessoMaschile} alt="Icona maschio" className="imgMaschio" />
      <button className="deleteUtente-button" onClick={() => setModalIsOpen(true)}>
        Elimina
      </button>
      <ConfirmationModal
        isOpen={modalIsOpen}
        onClose={() => setModalIsOpen(false)}
        onConfirm={handleDelete}
      />
    </div>
  );
};

export default EliminaUtente;
