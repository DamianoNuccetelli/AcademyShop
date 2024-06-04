import Modal from 'react-modal';
import React, { useState } from 'react'

const Popup = () => {

    return (
        <div>
            <h1>Popup Tutorial</h1>
            <button onClick={openModal}>Open Popup</button>
            <Modal
                isOpen={modalIsOpen}
                onRequestClose={closeModal}
                className="modal"
                overlayClassName="overlay"
            >
            <div className="popup-content">
                <h2>Welcome to our Popup</h2>
                <p>This is a modern popup design example.</p>
                <button onClick={closeModal} className="close-button">Close</button>
            </div>
            </Modal>
        </div>
    );
}

export default Popup;