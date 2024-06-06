import React from 'react';
import Modal from 'react-modal';
import done from '../../../img/SfondoModalAcademyShop.png';

const RegisteredModal = ({ isOpen, onClose }) => {
    return (
        <Modal
            isOpen={isOpen}
            onRequestClose={onClose}
            contentLabel="Registrazione Completata"
            ariaHideApp={false}
            className="modal-login text-bold-black"
            overlayClassName="modal-overlay"
        >
            <img src={done} alt="Done" className='modal_img'/>
            <h2>Grazie per esserti iscritto!</h2>
            <p>Esegui il login per verificare la registrazione.</p>
            <button onClick={onClose}>Chiudi</button>
        </Modal>
    );
};

export default RegisteredModal;
