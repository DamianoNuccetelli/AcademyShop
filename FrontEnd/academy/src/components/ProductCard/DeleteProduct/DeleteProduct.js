import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faMultiply } from '@fortawesome/free-solid-svg-icons';
import Modal from 'react-modal';

const DeleteProduct = ({ id, productsData, setProductsData }) => {
    const [modalIsOpenDeleteProduct, setModalIsOpenDeleteProduct] = useState(false);

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
            } else if (data.dettaglioOrdines = null) {
                alert("Impossibile eliminare il prodotto. È associato a un ordine. Eliminare prima l'ordine corrispondente.");
            } else {
                alert("Impossibile eliminare il prodotto. La quantità non è zero.");
            }
        } catch (error) {
            console.error('Error:', error);
        }
    };
    


    return (
        <div>
            <FontAwesomeIcon icon={faMultiply} onClick={openModalDeleteProduct} className='delete-icon icon-red icon' />
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
    );
};

export default DeleteProduct;
