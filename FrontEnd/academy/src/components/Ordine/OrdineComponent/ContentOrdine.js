import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus, faChevronLeft, faChevronRight, faEdit, faTrashCan, faEye } from '@fortawesome/free-solid-svg-icons';
import '../../Header/Header.css';
import banner from '../../../img/banner.png';
import Modal from 'react-modal';
import OrdineUpdate from '../OrdineUpdate/OrdineUpdate';
import OrdineCreate from '../OrdineCreate/OrdineCreate';
import OrdineDetails from '../OrdineDetails/OrdineDetails';
import OrdineDelete from '../OrdineDelete/OrdineDelete';
import './ContentOrdine.css';

const ContentOrdine = () => {

  const userId = localStorage.getItem('userId');
  const [orders, setOrders] = useState([]);
  const [orderDetail, setOrderDetail] = useState(null);
  
  const [OrdineDettaglioArray, setOrdineDettaglioArray] = useState([]);
  const [prodotti, setProdotti] = useState([]);
  const [searchTerm, setSearchTerm] = useState('');
  const [filteredProducts, setFilteredProducts] = useState([]);
  const [showDropdown, setShowDropdown] = useState(false);

  const [currentPage, setCurrentPage] = useState(1);
  const ordersPerPage = 5; 
  const indexOfLastProduct = currentPage * ordersPerPage;
  const indexOfFirstProduct = indexOfLastProduct - ordersPerPage;
  const order = orders.slice(indexOfFirstProduct, indexOfLastProduct);
  const totalPages = Math.ceil(orders.length / ordersPerPage);
  const nome = sessionStorage.getItem('nome');
  const cognome = sessionStorage.getItem('cognome');
 
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


  const fetchOrders = async () => {
    const API_URL = `https://localhost:7031/orders?userId=${userId}`;
    try {
      const response = await fetch(API_URL);
      if (response.ok) {
        const data = await response.json();
        setOrders(data);
        console.log("Ordini: ", data);
        const OrdineDettaglioArray = data.map(order => order.idDettaglioOrdine);
        setOrdineDettaglioArray(OrdineDettaglioArray);
        
        console.log("OrdineDettaglio array: ", OrdineDettaglioArray);                
      } else {
        console.error("Error fetching orders:", response.status);
      }
    } catch (error) {
      console.error("Error:", error);
    }
  };

  useEffect(() => {
    fetchOrders(); 
  }, []);

  useEffect(() => {
    fetchOrders(); 
  }, [userId]);
//  useEffect(() => {
//       const fetchOrderDetail = async () => {
//         const API_URL = `https://localhost:7031/orders/${OrdineDettaglioArray}?userId=${userId}`;
//         try {
//           const response = await fetch(API_URL);
//           if (response.ok) {
            
//             const data = await response.json();
//             setOrderDetail(data);
//             console.log("Order detail: ", data);

//           } else {
//             console.error("Error fetching order detail:", response.status);
//           }
//         } catch (error) {
//           console.error("Error:", error);
//         }
//       };
  
//       fetchOrderDetail();
//     }, []);


  useEffect(() => {
    if (searchTerm === '') {
      setFilteredProducts([]);
    } else {
      const filtered = prodotti.filter(product =>
        product.nome.toLowerCase().includes(searchTerm.toLowerCase())
      );
      setFilteredProducts(filtered);
      setShowDropdown(true);
    }
  }, [searchTerm, prodotti]);

  const handleOnEndCreate = (flag) => {
    if (flag) {
      fetchOrders();
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
                     <p>  {new Date(order.dataAggiornamento).toLocaleDateString()}</p>
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
