import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus, faChevronLeft, faChevronRight, faEdit, faTrashCan, faEye } from '@fortawesome/free-solid-svg-icons';
import '../../Header/Header.css';
import banner from '../../../img/banner.png';
import OrdineUpdate from '../OrdineUpdate/OrdineUpdate';
import OrdineCreate from '../OrdineCreate/OrdineCreate';
import OrdineDetails from '../OrdineDetails/OrdineDetails';
import OrdineDelete from '../OrdineDelete/OrdineDelete';
import './ContentOrdine.css';

const ContentOrdine = () => {

  const userId = localStorage.getItem('userId');
  const [orders, setOrders] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const ordersPerPage = 5; 
  const indexOfLastProduct = currentPage * ordersPerPage;
  const indexOfFirstProduct = indexOfLastProduct - ordersPerPage;
  const order = orders.slice(indexOfFirstProduct, indexOfLastProduct);
  let totalPages = Math.ceil(orders.length / ordersPerPage);
  const nome = sessionStorage.getItem('nome');
  const cognome = sessionStorage.getItem('cognome');
 
  const fetchOrders = async () => {
    const API_URL = `https://localhost:7031/orders?userId=${userId}`;
    try {
      const response = await fetch(API_URL);
      if (response.ok) {
        const data = await response.json();
        setOrders(data);
      } else {
        console.error(response.statusText, response.status);
      }
    } catch (error) {
      console.error("Error:", error);
    }
  };

  useEffect(() => {
    fetchOrders(); 
  }, []);

const handleNextPage = () => {
  if (currentPage < totalPages) {
      setCurrentPage(currentPage + 1);
  }
};

const handlePrevPage = () => {
    if (currentPage > 1) {
        setCurrentPage(currentPage - 1);
    }
};


const handleOnEndCreate = (flag) => {
    if (flag) {
      fetchOrders();
      if((order.length-1) % ordersPerPage ){
        totalPages = totalPages + 1;
      }
      setCurrentPage(totalPages);
    }
  };

  const handleOnEndDelete = (flag) => {
    if (flag) {
      fetchOrders();
      setCurrentPage(1);
    }
  };

  return (
    <div className="header">
      <div className="title_container_ordine">
        <div className="title_text">
          <h1>Ordini</h1>
          <h2>{nome} {cognome}</h2>
        </div>
      </div>
      <div className="welcome_container_ordine">
        <OrdineCreate onEndCreate= {handleOnEndCreate}/>
        <div className="banner_container">
          <img src={banner} alt="Logo" />
        </div>
      </div>
    
      <div className="products_container_ordine">
        <div className="all_products_div">
          <h2>Tutti i prodotti</h2>
          <div className='pagination'>
                  <button onClick={handlePrevPage} disabled={currentPage === 1}>
                      <FontAwesomeIcon icon={faChevronLeft} />
                  </button>
                  <span className='pagination_number'>{currentPage}/{totalPages}</span>
                  <button onClick={handleNextPage} disabled={currentPage === totalPages}>
                      <FontAwesomeIcon icon={faChevronRight} />
                  </button>
            </div>
        </div>
        <table>
          <thead>
            <tr>
              <th>Nome Prodotto</th>
              <th>Descrizione Prodotto</th>
              <th>Stato Ordine</th>
              <th>Quantità</th>
              <th>Data Registrazione</th>
              <th>Data Aggiornamento</th>
              <th>Azioni</th>
            </tr>
          </thead>
          <tbody>
            {order.map((order, index) => (
              <tr key={index}>
                <td>{order.prodottoNome}</td>
                <td>{order.prodottoDescrizione}</td>
                <td>{order.statoOrdineDescrizione}</td>
                <td>{order.quantita}</td>
                <td>
                  {new Date(order.dataRegistrazione).toLocaleDateString()}
                </td>
                <td>{order.dataAggiornamento == null ? (
                    <span>Non aggiornato</span>
                    ) : (
                     <span>  {new Date(order.dataAggiornamento).toLocaleDateString()}</span>
                     )}
                </td>
                <td>
                  <div className='icons-container'>
                  <OrdineDetails order={order}/>
                  <OrdineUpdate setOrders={setOrders} fetchOrders={fetchOrders} order={order} orders={orders}/>
                  <OrdineDelete ordine={order} onEndDelete= {handleOnEndDelete}/>
                  </div>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default ContentOrdine;
