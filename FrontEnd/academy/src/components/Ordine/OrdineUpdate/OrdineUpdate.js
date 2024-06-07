import React, { useState } from 'react';
import Modal from 'react-modal';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit } from '@fortawesome/free-solid-svg-icons';
import './OrdineUpdate.css';

const OrdineUpdate = ({ order, orders, setOrders, fetchOrders }) => {
  const userId = localStorage.getItem('userId');
  const [newQuantity, setNewQuantity] = useState(1); 
  const [modalEdit, setModalEdit] = useState(false);
  const [errorMessage, setErrorMessage] = useState('');

  const openModalEdit = () => {
    setModalEdit(true);
    setNewQuantity(1); 
    setErrorMessage('');
  };

  const closeModalEdit = () => {
    setModalEdit(false);
  };

  const handleUpdateOrder = async () => {
    const API_URL = `https://localhost:7031/orders/${order.idDettaglioOrdine}?idUtente=${userId}&quantita=${newQuantity}`;
    try {
      const response = await fetch(API_URL, {
        method: 'PUT',
        headers: {
          'Accept': '*/*',
          'Content-Type': 'application/json'
        }
      });
      if (response.ok) {
        const updatedOrder = await response.json();
        setOrders(orders.map(ord =>
          ord.idDettaglioOrdine === order.idDettaglioOrdine
            ? { ...ord, ...updatedOrder }
            : ord
        ));
        closeModalEdit();
        fetchOrders();
      } else {
        const errorData = await response.json();
        setErrorMessage(errorData.message || 'Errore nell\'aggiornamento dell\'ordine');
        console.error("Errore nell'aggiornamento dell'ordine:", response.status);
      }
    } catch (error) {
      setErrorMessage('Quantità di prodotto disponibile non sufficiente');
      console.error("Errore:", error);
    }
  };

  return (
    <>
      <button className='edit-button' onClick={openModalEdit}>
        <FontAwesomeIcon icon={faEdit}/>
      </button>
      <Modal
        isOpen={modalEdit}
        ariaHideApp={true}
        onRequestClose={closeModalEdit}
        contentLabel="Edit Order"
        overlayClassName="overlay"
        className="modal"
      >
        <div className="popup-content">
          <h2>Modifica dell'Ordine</h2>
          <label>
            Quantità:
            <input
              type="number"
              value={newQuantity}
              onChange={(e) => setNewQuantity(Number(e.target.value))}
            />
          </label>
          {errorMessage && <p className="error-message">{errorMessage}</p>}
          <div className="margin-button"><button onClick={handleUpdateOrder} className="save-button">Save</button></div>
          <div className="margin-button"><button onClick={closeModalEdit} className="cancel-button">Cancel</button></div>
        </div>
      </Modal>
    </>
  );
};

export default OrdineUpdate;
