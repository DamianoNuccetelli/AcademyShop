import React from 'react';
import Modal from 'react-modal';

import done from '../../../img/SfondoModalAcademyShop.png';
import errore from '../../../img/SfondoErrorAcademyShop.png';

const ErrorModal = ({ isOpen, onClose }) => {
    return (
        <Modal
            isOpen={isOpen}
            onRequestClose={onClose}
            contentLabel="Campi mancanti"
            ariaHideApp={false}
            className="modal-login text-bold-black"
            overlayClassName="modal-overlay"
        >
            <img src={errore} alt="Error" className='modal_img'/> 
            <h2>Oops!</h2>
            <p>Per favore, ricontrolla tutti i campi prima di inviare il form.</p>
            <button onClick={onClose}>Chiudi</button>
        </Modal>
    );
};

export default ErrorModal;
