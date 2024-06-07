import React from 'react';
import Modal from 'react-modal';

const ConfirmationModal = ({ isOpen, onClose, onConfirm }) => {
  return (
    <Modal
      isOpen={isOpen}
      onRequestClose={onClose}
      className="modal"
      overlayClassName="overlay"
      ariaHideApp={false}
    >
      <div className="popup-content">
        <h2>Conferma Eliminazione Account</h2>
        <p>Sei sicuro di voler eliminare il tuo account? Questa operazione è irreversibile.</p>
        <div className="buttons">
          <button onClick={onConfirm}>Conferma Eliminazione</button>
          <button onClick={onClose}>Annulla</button>
        </div>
      </div>
    </Modal>
  );
};

export default ConfirmationModal;
