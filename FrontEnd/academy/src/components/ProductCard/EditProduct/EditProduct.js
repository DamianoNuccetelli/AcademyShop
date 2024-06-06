import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPenToSquare } from '@fortawesome/free-solid-svg-icons';
import Modal from 'react-modal';

const EditProduct = ({ id, nome, descrizione, quantità, productsData, setProductsData }) => {
    const [modalIsOpenEditProduct, setModalIsOpenEditProduct] = useState(false);
    const [newName, setNewName] = useState(nome);
    const [newDescrizione, setNewDescrizione] = useState(descrizione);
    const [newQuantità, setNewQuantità] = useState(quantità);

    const openModalEditProduct = () => {
        setNewName(nome);
        setNewDescrizione(descrizione);
        setNewQuantità(quantità);
        setModalIsOpenEditProduct(true);
    };

    const closeModalEditProduct = () => {
        setModalIsOpenEditProduct(false);
    };

    const handleEditProduct = async (e) => {
        e.preventDefault();
        if (newName.length > 50) {
            alert("Il nome non può superare i 50 caratteri.");
            return;
        }

        if (newDescrizione.length > 50) {
            alert("La descrizione non può superare i 50 caratteri.");
            return;
        }
        
        try {
            const response = await fetch(`https://localhost:7031/products/${id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ nome: newName, descrizione: newDescrizione, quantità: newQuantità }),
            });
            if (response.ok) {
                const updatedProducts = productsData.map(product => {
                    if (product.id === id) {
                        return { ...product, nome: newName, descrizione: newDescrizione, quantità: newQuantità };
                    }
                    return product;
                });
                setProductsData(updatedProducts);
                closeModalEditProduct();
            } else {
                console.error('Errore durante la modifica del prodotto:', response.status);
            }
        } catch (error) {
            console.error('Error:', error);
        }
    };

    return (
        <div>
            <FontAwesomeIcon icon={faPenToSquare} onClick={openModalEditProduct} className='edit-icon icon' />
            <Modal
                isOpen={modalIsOpenEditProduct}
                onRequestClose={closeModalEditProduct}
                className="modal"
                overlayClassName="overlay"
                ariaHideApp={false}
            >
                <div className="popup-content">
                    <h2>Modifica Prodotto</h2>
                    <form onSubmit={handleEditProduct}>
                        <label>
                            Nome:
                            <input
                                type="text"
                                value={newName}
                                onChange={(e) => setNewName(e.target.value)}
                            />
                        </label>
                        <label>
                            Descrizione:
                            <textarea
                                value={newDescrizione}
                                onChange={(e) => setNewDescrizione(e.target.value)}
                            />
                        </label>
                        <label>
                            Quantità:
                            <input
                                type="number"
                                value={newQuantità}
                                onChange={(e) => setNewQuantità(e.target.value)}
                            />
                        </label>
                        <button type="submit">Salva</button>
                        <button onClick={closeModalEditProduct} className="close-button">Annulla</button>
                    </form>
                </div>
            </Modal>
        </div>
    );
};

export default EditProduct;
