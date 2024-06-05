import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import '../../Header/Header.css';
import banner from '../../../img/banner.png';
import Modal from 'react-modal';

const ContentOrdine = () => {
  const [prodotti, setProdotti] = useState([]);
  const [quantità, setQuantità] = useState(1);

  const [modalIsOpen, setModalIsOpen] = useState(false);
  const [modalIsOpen2, setModalIsOpen2] = useState(false);

  const openModal = () => {
    setModalIsOpen(true);
  };

  const openModal2 = () => {
    setModalIsOpen2(true);
  };

  const closeModal = () => {
    setModalIsOpen(false);
  };

  const closeModal2 = () => {
    setModalIsOpen2(false);
  };

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
        console.log('Ordine aggiunto con successo', response.status);
      } else {
        console.error('Errore durante l\'aggiunta dell\'ordine:', response.status);
      }
    } catch (error) {
      console.error('Errore:', error);
    }
  };

  const [selectedProduct, setSelectedProduct] = useState('');
  const [searchTerm, setSearchTerm] = useState('');
  const [filteredProducts, setFilteredProducts] = useState([]);
  const [activeIndex, setActiveIndex] = useState(-1);
  const [showDropdown, setShowDropdown] = useState(false);
  const [orders, setOrders] = useState([]);
  const userId = localStorage.getItem('userId');

  useEffect(() => {
    const fetchOrders = async () => {
      const API_URL = `https://localhost:7031/orders?userId=${userId}`;
      try {
        const response = await fetch(API_URL);
        if (response.ok) {
          const data = await response.json();
          setOrders(data);
        } else {
          console.error("Error fetching orders:", response.status);
        }
      } catch (error) {
        console.error("Error:", error);
      }
    };

    fetchOrders();
  }, [addOrdine]);

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

  const getProducts = async () => {
    const API_URL = `https://localhost:7031/products?userId=${userId}`;
    try {
      const response = await fetch(API_URL);
      if (response.ok) {
        const data = await response.json();
        setProdotti(data);
        openModal(); // Call openModal after fetching products
      } else {
        console.error('Error fetching products:', response.status);
      }
    } catch (error) {
      console.error('Error:', error);
    }
  };

  const handleProductChange = (event) => {
    setSelectedProduct(event.target.value);
  };

  const handleSearchChange = (event) => {
    setSearchTerm(event.target.value);
  };

  const handleQuantityChange = (event) => {
    setQuantità(parseInt(event.target.value) || 1); // Ensure quantity is a positive integer
  };

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
  };

  const handleClick = (product) => {
    setSearchTerm(product.nome);
    setSelectedProduct(product.id);
    setShowDropdown(false);
  };

  return (
    <div className="header">
      <div className='title_container_ordine'>
        <div className='title_text'>
          <h1>Ordini</h1>
          <h2>Ordini di Mario</h2>
        </div>
      </div>
      <div className='welcome_container_ordine'>
        <div className='add_container_ordine'>
          <FontAwesomeIcon icon={faPlus} className="plus-icon" onClick={() => getProducts(userId)} />
        </div>
        <div className='banner_container'>
          <img src={banner} alt="Logo" />
        </div>
      </div>

      <Modal
        isOpen={modalIsOpen}
        ariaHideApp={false}
        onRequestClose={closeModal}
        className="modal"
        overlayClassName="overlay"
      >
        <div className="popup-content">
          <h2>Welcome to our Popup</h2>
          <div>
            <div className="search-bar-dropdown">
              <input
                type="text"
                value={searchTerm}
                onChange={handleSearchChange}
                onKeyDown={handleKeyDown}
                placeholder="Search..."
              />
              {showDropdown && filteredProducts.length > 0 && (
                <ul className="dropdown-list">
                  {filteredProducts.map((product, index) => (
                    <li
                      key={index}
                      className={index === activeIndex ? 'active' : ''}
                      onClick={() => handleClick(product)}
                    >
                      {product.nome}
                    </li>
                  ))}
                </ul>
              )}
            </div>
            <div>
              <input
                type="number"
                min="1"
                placeholder="Quantità"
                value={quantità}
                onChange={handleQuantityChange}
              />
            </div>
          </div>
          <button onClick={closeModal} className="close-button">Close</button>
          <button onClick={() => addOrdine(userId, selectedProduct, quantità)} className="close-button">Submit</button>
        </div>
      </Modal>


      <div className='products_container_ordine'>
        <div className='all_products_div'>
          <h2>Tutti i prodotti</h2>
        </div>
        <table>
          <thead>
            <tr>
              <th>Nome Prodotto</th>
              <th>Descrizione Prodotto</th>
              <th>Stato Ordine</th>
              <th>Quantità</th>
              <th>Id Prodotto</th>
              <th>Data Registrazione</th>
              <th>Data Aggiornamento</th>
              <th>Azioni</th>
            </tr>
          </thead>
          <tbody>
            {orders.map((order, index) => (
              <tr key={index}>
                <td>{order.prodottoNome}</td>
                <td>{order.prodottoDescrizione}</td>
                <td>{order.statoOrdineDescrizione}</td>
                <td>{order.quantita}</td>
                <td>{order.prodottoId}</td>
                <td>{new Date(order.dataRegistrazione).toLocaleDateString()}</td>
                <td>{new Date(order.dataAggiornamento).toLocaleDateString()}</td>
                <td>
                  <button>Delete</button>
                  <button onClick={openModal2}>Show</button>
                  <Modal
                    isOpen={modalIsOpen2}
                    ariaHideApp={false}
                    onRequestClose={closeModal2}
                    overlayClassName="overlay"
                    className="modal"
                  >
                    <div className="popup-content">
                      <h2>Dettagli Ordine</h2>
                      <p>Nome Prodotto: {order.prodottoNome}</p>
                      <p>Descrizione Prodotto: {order.prodottoDescrizione}</p>
                      <p>Stato Ordine: {order.statoOrdineDescrizione}</p>
                      <p>Quantità: {order.quantita}</p>
                      <p>Id Prodotto: {order.prodottoId}</p>
                      <p>Data Registrazione: {new Date(order.dataRegistrazione).toLocaleDateString()}</p>
                      <p>Data Aggiornamento: {new Date(order.dataAggiornamento).toLocaleDateString()}</p>
                      <button onClick={closeModal2}>Close</button>
                    </div>
                  </Modal>
                  <button>Edit</button>
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
