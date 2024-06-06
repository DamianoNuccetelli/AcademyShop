import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import Modal from 'react-modal';
import './AddProduct.css';

const AddProduct = ({ fetchProducts }) => {
    const [modalIsOpenAddProduct, setModalIsOpenAddProduct] = useState(false);

    const openModalAddProduct = () => {
        setModalIsOpenAddProduct(true);
    };

    const closeModalAddProduct = () => {
        setModalIsOpenAddProduct(false);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const nomeProdotto = e.target.elements.nomeProdotto.value;
        const descrizioneProdotto = e.target.elements.descrizioneProdotto.value;
        const quantitàProdotto = e.target.elements.quantitaProdotto.value;

        if (nomeProdotto.length > 50) {
            alert("Il nome non può superare i 50 caratteri.");
            return;
        }

        if (descrizioneProdotto.length > 50) {
            alert("La descrizione non può superare i 50 caratteri.");
            return;
        }

        const data = {
            nome: nomeProdotto,
            descrizione: descrizioneProdotto,
            quantità: quantitàProdotto
        };

        try {
            const response = await fetch('https://localhost:7031/products', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data),
            });

            if (response.ok) {
                fetchProducts();
                closeModalAddProduct();
            } else {
                console.error('Errore durante l\'aggiunta del prodotto:', response.status);
            }
        } catch (error) {
            console.error('Errore:', error);
        }
    };

    return (
        <div className='add_container'>
            <FontAwesomeIcon icon={faPlus} className="plus-icon" onClick={openModalAddProduct} />
            <Modal
                isOpen={modalIsOpenAddProduct}
                onRequestClose={closeModalAddProduct}
                className="modal"
                overlayClassName="overlay"
                ariaHideApp={false}
            >
                <div className="popup-content">
                    <h2>Aggiungi Prodotto</h2>
                    <form onSubmit={handleSubmit}>
                        <div>
                            <input
                                type="text"
                                name="nomeProdotto"
                                placeholder="Nome Prodotto"
                                required
                            />
                        </div>
                        <div>
                            <textarea
                                name="descrizioneProdotto"
                                placeholder="Descrizione Prodotto"
                                required
                            />
                        </div>
                        <div>
                            <input
                                type="number"
                                name="quantitaProdotto"
                                placeholder="Quantità Prodotto"
                                required
                            />
                        </div>
                        <button type="submit">Aggiungi</button>
                    </form>
                    <button onClick={closeModalAddProduct} className="close-button">Chiudi</button>
                </div>
            </Modal>
        </div>
    );
};

export default AddProduct;
