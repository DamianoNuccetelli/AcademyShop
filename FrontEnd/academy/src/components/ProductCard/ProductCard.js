import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBox, faPenToSquare, faMultiply } from '@fortawesome/free-solid-svg-icons';
import './ProductCard.css';
import Modal from 'react-modal';

const ProductCard = ({ id, name, Descrizione,Quantità , onDelete, onEdit }) => {
    const [modalIsOpenEditProduct, setModalIsOpenEditProduct] = useState(false);
    const [modalIsOpenDeleteProduct, setModalIsOpenDeleteProduct] = useState(false);
    const [newName, setNewName] = useState(name);
    const [newDescrizione, setNewDescrizione] = useState(Descrizione);
    const [newQuantità, setNewQuantità] = useState(Quantità);

    const openModalEditProduct = () => {
        setNewName(name);
        setNewDescrizione(Descrizione);
        setNewQuantità(Quantità);
        setModalIsOpenEditProduct(true);
    };

    const closeModalEditProduct = () => {
        setModalIsOpenEditProduct(false);
    };

    const openModalDeleteProduct = () => {
        setModalIsOpenDeleteProduct(true);
    };

    const closeModalDeleteProduct = () => {
        setModalIsOpenDeleteProduct(false);
    };

    const handleDeleteProduct = async () => {
        try {
            const response = await fetch(`https://localhost:7031/products/${id}`, {
                method: 'DELETE',
            });
            if (response.ok) {
                onDelete(id);
                closeModalDeleteProduct();
            } else {
                console.error('Errore durante l\'eliminazione del prodotto:', response.status);
            }
        } catch (error) {
            console.error('Error:', error);
        }
    };

    const handleEditProduct = async () => {
        try {
            const response = await fetch(`https://localhost:7031/products/${id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ name: newName, Descrizione: newDescrizione, Quantita: newQuantità }),
            });
            if (response.ok) {
                onEdit(id, newName, newDescrizione, newQuantità);
                closeModalEditProduct();
            } else {
                console.error('Errore duante la modifica del prodotto:', response.status);
            }
        } catch (error) {
            console.error('Error:', error);
        }
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
                        <FontAwesomeIcon icon={faMultiply} className='icon icon-red' onClick={openModalDeleteProduct} />
                    </div>

                    <Modal
                        isOpen={modalIsOpenEditProduct}
                        onRequestClose={closeModalEditProduct}
                        className="modal"
                        overlayClassName="overlay"
                        ariaHideApp={false}
                    >
                        <div className="popup-content">
                            <h2>Modifica Prodotto</h2>
                            <form onSubmit={(e) => {
                                e.preventDefault();
                                handleEditProduct();
                            }}>
                                <div>
                                    <label>Nome:</label>
                                    <input
                                        type="text"
                                        value={newName}
                                        onChange={(e) => setNewName(e.target.value)}
                                    />
                                </div>
                                <div>
                                    <label>Descizione:</label>
                                    <input
                                        type="text"
                                        value={newDescrizione}
                                        onChange={(e) => setNewDescrizione(e.target.value)}
                                    />
                                </div>
                                <div>
                                    <label>Quantità:</label>
                                    <input
                                        type="number"
                                        value={newQuantità}
                                        onChange={(e) => setNewQuantità(e.target.value)}
                                    />
                                </div>
                                <button type="submit">Salva</button>
                                <button onClick={closeModalEditProduct} type="button">Annulla</button>
                            </form>
                        </div>
                    </Modal>

                    <Modal
                        isOpen={modalIsOpenDeleteProduct}
                        onRequestClose={closeModalDeleteProduct}
                        className="modal"
                        overlayClassName="overlay"
                        ariaHideApp={false}
                    >
                        <div className="popup-content">
                            <h2>Conferma Eliminazione</h2>
                            <p>Sei sicuro di voler eliminare questo prodotto?</p>
                            <button onClick={handleDeleteProduct} className="delete-button">Elimina</button>
                            <button onClick={closeModalDeleteProduct} className="close-button">Annulla</button>
                        </div>
                    </Modal>
                </div>
            </div>
        </div>
    );
};

export default ProductCard;
