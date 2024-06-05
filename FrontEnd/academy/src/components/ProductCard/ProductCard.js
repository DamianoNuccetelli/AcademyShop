import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBox, faPenToSquare, faMultiply } from '@fortawesome/free-solid-svg-icons';
import './ProductCard.css';
import Modal from 'react-modal';

const ProductCard = ({ id, nome, descrizione, quantità, productsData, setProductsData }) => {
    const [modalIsOpenEditProduct, setModalIsOpenEditProduct] = useState(false);
    const [modalIsOpenDeleteProduct, setModalIsOpenDeleteProduct] = useState(false);
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

    const openModalDeleteProduct = () => {
        setModalIsOpenDeleteProduct(true);
    };

    const closeModalDeleteProduct = () => {
        setModalIsOpenDeleteProduct(false);
    };

    const handleDeleteProduct = async () => {
        try {
            const response = await fetch(`https://localhost:7031/products/${id}`);
            const data = await response.json();
            
            if ( data.dettaglioOrdines != null) {
                const deleteResponse = await fetch(`https://localhost:7031/products/${id}`, {
                    method: 'DELETE',
                });
    
                if (deleteResponse.ok) {
                    const updatedProducts = productsData.filter(product => product.id !== id);
                    setProductsData(updatedProducts);
                    closeModalDeleteProduct();
                } else {
                    console.error('Errore durante l\'eliminazione del prodotto:', deleteResponse.status);
                }
            } else if (data.dettaglioOrdines) {
                alert("Impossibile eliminare il prodotto. È associato a un ordine. Eliminare prima l'ordine corrispondente.");
            } else {
                alert("Impossibile eliminare il prodotto. La quantità non è zero.");
            }
        } catch (error) {
            console.error('Error:', error);
        }
    };
    

    const handleEditProduct = async (e) => {
        e.preventDefault();
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
        <div className='card_container'>
            <div className='box_card_container'>
            <div className="product_icon_box">
                <FontAwesomeIcon icon={faBox} className='product_icon' />
                <h3>{nome}</h3>
            </div>
            <div className='description'>
                <p className='mb20'><strong>Descrizione:</strong> {descrizione} </p>
            </div>
            <div className='quantity'>
                <div className='crud_button'>
                    <p><strong>Quantità:</strong> {quantità}</p>
                    <div className='ml_20'>
                        <FontAwesomeIcon icon={faPenToSquare} onClick={openModalEditProduct} className='edit-icon icon' />
                        <FontAwesomeIcon icon={faMultiply} onClick={openModalDeleteProduct} className='delete-icon icon-red icon' />
                    </div>
            </div>
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
                    <button onClick={handleDeleteProduct}>Elimina</button>
                    <button onClick={closeModalDeleteProduct} className="close-button">Annulla</button>
                </div>
            </Modal>
            </div>
        </div>
    );
};

export default ProductCard;