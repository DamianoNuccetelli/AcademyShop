import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBox, faPenToSquare, faMultiply } from '@fortawesome/free-solid-svg-icons'; // Esempio di un'icona casuale, puoi sostituirla con l'icona che desideri
import './ProductCard.css';
import Modal from 'react-modal';

const ProductCard = ({ name, price }) => {

    const [modalIsOpenEditProduct, setModalIsOpenEditProduct] = useState(false);

    const openModalEditProduct = () => {
        setModalIsOpenEditProduct(true);
    };

    const closeModalEditProduct = () => {
        setModalIsOpenEditProduct(false);
    };

    return (
        <div className='card_container'>
            <div className="product_icon_box">
                <FontAwesomeIcon icon={faBox} className='product_icon' />
                <h3>{name}</h3>
            </div>
            <div className='description'>
                <p className='mb20'><strong>Descrizione:</strong> Lorem Ipsum è un testo segnaposto utilizzato nel settore della tipografia e della stampa.</p>
                <div className='crud_button'>
                    <p><strong>Quantità</strong>: 2</p>
                    <div className='buttons'>
                        <FontAwesomeIcon icon={faPenToSquare} className='icon' onClick={openModalEditProduct} />
                        <FontAwesomeIcon icon={faMultiply} className='icon icon-red' />
                    </div>

                    <Modal
                        isOpen={modalIsOpenEditProduct}
                        onRequestClose={closeModalEditProduct}
                        className="modal"
                        overlayClassName="overlay"
                        ariaHideApp={false}
                    >
                        <div className="popup-content">
                            <h2>Welcome to our Popup</h2>
                            <p>This is a modern popup design example.</p>
                            <button onClick={closeModalEditProduct} className="close-button">Close</button>
                        </div>
                    </Modal>

                </div>
            </div>

        </div>
    );
};

export default ProductCard;
