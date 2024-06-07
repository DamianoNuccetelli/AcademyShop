import React from 'react';
import Modal from 'react-modal';

import './ConfirmationModal.css'; 
import error from '../../../../img/SfondoErrorAcademyShop.png'; 

const ConfirmationModal = ({ isOpen, onClose, onConfirm }) => {
  return (
    <Modal
      isOpen={isOpen}
      onRequestClose={onClose}
      className="modal"
      overlayClassName="overlay"
      ariaHideApp={false}
    >
      <div className="popup-content_confirmDelete">
        <img src={error} alt="Logo" /> 
        <h2>Sicuro di voler cancellare il tuo account?</h2>
        <p>Una volta cancellato tutti i tuoi dati saranno eliminati definitivamente.</p>
        <div className="confirmDelete_buttons">
          <button onClick={onConfirm} className='confirmDelete_button'>Conferma</button>
          <button onClick={onClose} className='confirmDelete_button'>Annulla</button>
        </div>
      </div>
    </Modal>
  );
};

export default ConfirmationModal;
