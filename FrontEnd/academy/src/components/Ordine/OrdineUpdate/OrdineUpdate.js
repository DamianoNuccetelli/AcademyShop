import React, { useState } from 'react';
import Modal from 'react-modal';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faEdit } from '@fortawesome/free-solid-svg-icons';

const OrdineUpdate = ({ order, orders, setOrders, fetchOrders }) => {
  const userId = localStorage.getItem('userId');
  const [newQuantity, setNewQuantity] = useState(order.quantita);
  const [modalEdit, setModalEdit] = useState(false);

  const openModalEdit = () => {
    setModalEdit(true);
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
        console.error("Errore nell'aggiornamento dell'ordine:", response.status);
      }
    } catch (error) {
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
          <h2>Edit Order</h2>
          <label>
            Quantità:
            <input
              type="number"
              value={newQuantity}
              onChange={(e) => setNewQuantity(Number(e.target.value))}
            />
          </label>
          <button onClick={handleUpdateOrder} className="close-button">Save</button>
          <button onClick={closeModalEdit} className="close-button">Cancel</button>
        </div>
      </Modal>
    </>
  );
};

export default OrdineUpdate;
