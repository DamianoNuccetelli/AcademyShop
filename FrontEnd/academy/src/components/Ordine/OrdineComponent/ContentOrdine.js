import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus, faChevronLeft, faChevronRight, faEdit, faTrashCan, faEye } from '@fortawesome/free-solid-svg-icons';
import '../../Header/Header.css';
import banner from '../../../img/banner.png';
import Modal from 'react-modal';
import OrdineUpdate from '../OrdineUpdate/OrdineUpdate';
import OrdineCreate from '../OrdineCreate/OrdineCreate';
import OrdineDetails from '../OrdineDetails/OrdineDetails';
import './ContentOrdine.css';


// import SfondoModalAcademyShop from '../../../img/SfondoModalAcademyShop.png';

const ContentOrdine = () => {

  const userId = localStorage.getItem('userId');
  const [orders, setOrders] = useState([]);
  const [orderDetail, setOrderDetail] = useState(null);
  
  const [OrdineDettaglioArray, setOrdineDettaglioArray] = useState([]);
  const [prodotti, setProdotti] = useState([]);
  const [quantità, setQuantità] = useState(0);
  const [deleteId, setdeleteId] = useState(0);

  const [selectedProduct, setSelectedProduct] = useState('');
  const [searchTerm, setSearchTerm] = useState('');
  const [filteredProducts, setFilteredProducts] = useState([]);
  const [activeIndex, setActiveIndex] = useState(-1);
  const [showDropdown, setShowDropdown] = useState(false);

  const [currentPage, setCurrentPage] = useState(1);
  const ordersPerPage = 5; 
  const indexOfLastProduct = currentPage * ordersPerPage;
  const indexOfFirstProduct = indexOfLastProduct - ordersPerPage;
  const order = orders.slice(indexOfFirstProduct, indexOfLastProduct);
  const totalPages = Math.ceil(orders.length / ordersPerPage);

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


  const [modalIsOpen, setModalIsOpen] = useState(false);
  const [modalIsOpen2, setModalIsOpen2] = useState(false);
  const [modalDelete, setModalDelete] = useState(false);
  const [modalEdit, setModalEdit] = useState(false);

  const openModal = () => {
    setModalIsOpen(true);
  };

  const openModalDelete = () => {
    setModalDelete(true);
  };
  

  const closeModal = () => {
    setModalIsOpen(false);
  };
  
  const closeModalDelete= () => {
    setModalDelete(false);
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

 

  const addOrdine = async (idUtente, idProdotto, quantitàProdotto) => {
    closeModal();
    console.log("addOrdine: ", idUtente, idProdotto, quantitàProdotto);
    const API_URL = `https://localhost:7031/orders?idUtente=${idUtente}&idProdotto=${idProdotto}&quantit%C3%A0=${quantitàProdotto}`;
    try {
      const response = await fetch(API_URL, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          idUtente: idUtente,
          idProdotto: idProdotto,
          quantità: quantitàProdotto,
        }),
      });

      if (response.ok) {
        fetchOrders();
        setCurrentPage(totalPages);
       
        console.log('Ordine aggiunto con successo', response.status);
      } else {
        setSearchTerm('');
        console.error('Errore durante l\'aggiunta dell\'ordine:', response.status);
      }
    } catch (error) {
      console.error('Errore:', error);
    }
  };

  
    useEffect(() => {
      const fetchOrderDetail = async () => {
        const API_URL = `https://localhost:7031/orders/${OrdineDettaglioArray}?userId=${userId}`;
        try {
          const response = await fetch(API_URL);
          if (response.ok) {
            
            const data = await response.json();
            setOrderDetail(data);
            console.log("Order detail: ", data);

          } else {
            console.error("Error fetching order detail:", response.status);
          }
        } catch (error) {
          console.error("Error:", error);
        }
      };
  
      fetchOrderDetail();
    }, []);


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

 /* const getProducts = async () => {
    const API_URL = `https://localhost:7031/products?userId=${userId}`;
    try {
      const response = await fetch(API_URL);
      if (response.ok) {
        const data = await response.json();
        setQuantità(0);
        setSearchTerm('');
        setProdotti(data);
        openModal(); // Call openModal after fetching products
      } else {
        console.error('Error fetching products:', response.status);
      }
    } catch (error) {
      console.error('Error:', error);
    }
  };

  const handleSearchChange = (event) => {
    setSearchTerm(event.target.value);
  };

  const handleQuantityChange = (event) => {
    setQuantità(parseInt(event.target.value) || 1); // Ensure quantity is a positive integer
  };*/
/*
  const handleKeyDown = (e) => {
    if (e.key === 'ArrowDown') {
      setActiveIndex((prevIndex) =>
        prevIndex < filteredProducts.length - 1 ? prevIndex + 1 : prevIndex
      );
    } else if (e.key === 'ArrowUp') {
      setActiveIndex((prevIndex) =>
        prevIndex > 0 ? prevIndex - 1 : 0
      );
    } else if (e.key === 'Enter') {
      if (activeIndex >= 0) {
        setSearchTerm(filteredProducts[activeIndex].nome);
        setSelectedProduct(filteredProducts[activeIndex].id);
        setShowDropdown(false);
        setActiveIndex(-1);
      }
    }
  };*/
/*
  const handleClick = (product) => {
    setSearchTerm(product.nome);
    setSelectedProduct(product.id);
    setShowDropdown(false);
  };*/
  const handleOnEndCreate = (flag) => {
    if (flag) {
      fetchOrders();
      setCurrentPage(totalPages);
    }
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
        const text = await response.text();
  
        if (text) {
          const data = JSON.parse(text);
          console.log(data);
        } else {
          console.log('Empty response');
        }
        setdeleteId(0);
        fetchOrders();
        setCurrentPage(1);
      } else {
        console.error('Error deleting order:', response.status);
      }
    } catch (error) {
      console.error('Error:', error);
    }
  };
  

  return (
    <div className="header">
      <div className="title_container_ordine">
        <div className="title_text">
          <h1>Ordini</h1>
          <h2>Ordini di Mario</h2>
        </div>
      </div>
      <div className="welcome_container_ordine">
        <OrdineCreate onEndCreate= {handleOnEndCreate}/>
        <div className="banner_container">
          <img src={banner} alt="Logo" />
        </div>
      </div>
    
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

    

            {/* Modal Edit */}
            {/* {modalEdit && selectedOrder && (
            <Modal
                isOpen={modalEdit}
                ariaHideApp={true}
                onRequestClose={closeModalEdit}
                contentLabel="Edit Order"
                overlayClassName="overlay"
                className="modal"
            >
                <OrdineUpdate
                    orders={orders}
                    setOrders={setOrders}
                    fetchOrders={fetchOrders}
                    selectedOrder={selectedOrder}
                    closeModalEdit={closeModalEdit}
                />
            </Modal>
        )} */}

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
                  <button onClick={() => deletePopUp(order.idDettaglioOrdine)} className="trash-button">
                    <FontAwesomeIcon icon={faTrashCan} />
                  </button>
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
