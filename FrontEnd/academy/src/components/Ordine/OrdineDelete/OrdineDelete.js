import React, { useState} from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Modal from 'react-modal';
import './OrdineDelete.css';
import { faTrashCan } from '@fortawesome/free-solid-svg-icons';

const Ordine = ({ ordine, onEndDelete }) => {
    const userId = localStorage.getItem('userId');
    const [modalDelete, setModalDelete] = useState(false);
    const [deleteId, setdeleteId] = useState(0);

    const openModalDelete = () => {
        setModalDelete(true);
      };
      
    const closeModalDelete= () => {
        setModalDelete(false);
      };
    
     
    const deletePopUp = async (idDettaglioOrdine) => {
        setdeleteId(idDettaglioOrdine);
        openModalDelete();
      }
      const deleteOrdine = async () => {
        
        closeModalDelete();
        const API_URL = `https://localhost:7031/orders/${deleteId}?idUtente=${userId}`;
        try {
          const response = await fetch(API_URL, {
            method: 'DELETE',
          });
      
          if (response.ok) {
            setdeleteId(0);
            result(true);
          } else {
            console.error("Errore durante l'eliminazione dell'ordine:", response.status);
          }
        } catch (error) {
          console.error('Errore Generico:', error);
        }
      };
      const result = (flag) => {
        onEndDelete(flag);
        return flag;
      };
    return (
        <>

            {/* Modal Delete */}
            <Modal
                isOpen={modalDelete}
                ariaHideApp={false}
                onRequestClose={closeModalDelete}
                className="modal"
                overlayClassName="overlay"
            >
                <div className="popup-content">
                    <h2>Vuoi Eliminare?</h2>
                    <button onClick={closeModalDelete} className="close-button">
                        Close
                    </button>
                    <button onClick={() => deleteOrdine()} className="close-button">
                        Delete
                    </button>
                </div>
            </Modal>
            <div className='icons-container'>
            <button onClick={() => deletePopUp(ordine.idDettaglioOrdine)} className="trash-button">
                   <FontAwesomeIcon icon={faTrashCan} /> 
            </button>
            </div>
        </>
    );
};

export default Ordine;
