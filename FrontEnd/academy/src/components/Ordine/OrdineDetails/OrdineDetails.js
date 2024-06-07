import React, { useState} from 'react';
import {faEye } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import './OrdineDetails.css';
import Modal from 'react-modal';
import occhio from '../../../img/occhio.png';

const OrdineDetails = ({order}) => {
    const userId = localStorage.getItem('userId');
    const [modalIsOpen2, setModalIsOpen2] = useState(false);
    const [detailedOrders, setDetailedOrders] = useState([]);

    const openModal2 = () => {
        setModalIsOpen2(true);
      };
    const closeModal2 = () => {
        setModalIsOpen2(false);
      };
    const fetchDetailedOrder = async (idDettaglioOrdine) => {
        const API_URL = `https://localhost:7031/orders/${idDettaglioOrdine}?userId=${userId}`;
        try {
          const response = await fetch(API_URL);
          if (response.ok) {
            const data = await response.json();
            setDetailedOrders(data);
            openModal2();
          } else {
            console.error(`Error fetching order detail for idDettaglioOrdine ${idDettaglioOrdine}:`, response.status);
          }
        } catch (error) {
          console.error(`Error fetching order detail for idDettaglioOrdine ${idDettaglioOrdine}:`, error);
        }
      };


    return (
        <>
          {/* Modal Detail Gabriele */}
          <Modal
          isOpen={modalIsOpen2}
          ariaHideApp={false}
          onRequestClose={closeModal2}
          overlayClassName="modal-overlay"
          className="modal-detailsOrder text-bold-black"
        >
          <div className="popup-content">
          <img src={occhio} alt="Occhio" className='modal_img'/>
            <h2>Dettagli Ordine</h2>
          <div className="detail_content">
            <p>Prodotto: {detailedOrders.prodottoNome}</p>
            <p>Descrizione: {detailedOrders.prodottoDescrizione}</p>
            <p>Stato Ordine: {detailedOrders.statoOrdineDescrizione}</p>
            <p>Quantità: {detailedOrders.quantita}</p>
            <p>
              Data Registrazione:{" "}
              {new Date(detailedOrders.dataRegistrazione).toLocaleDateString()}
            </p>
            <p>
              Data Aggiornamento:{" "}
                    {order.dataAggiornamento == null ? (
                    <span>Non aggiornato</span>
                    ) : (
                     <p>  {new Date(order.dataAggiornamento).toLocaleDateString()}</p>
                     )}
            </p>
          </div>
            <div>     
            <button onClick={closeModal2} className="close-button">Chiudi</button>
            </div>
          </div>
        </Modal>
        <button className='show-button' onClick={() => fetchDetailedOrder(order.idDettaglioOrdine)}>
        <FontAwesomeIcon icon={faEye} />
        </button>
        </>
    );
};

export default OrdineDetails;


