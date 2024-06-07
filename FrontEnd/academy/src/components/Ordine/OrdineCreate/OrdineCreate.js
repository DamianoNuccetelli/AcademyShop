import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import Modal from 'react-modal';
import './OrdineCreate.css';
import done from '../../../img/SfondoModalAcademyShop.png';

const Ordine = ({onEndCreate}) => {
  const userId = localStorage.getItem('userId');
  const [orders, setOrders] = useState([]);
  const [prodotti, setProdotti] = useState([]);
  const [quantità, setQuantità] = useState(0);
  const [selectedProduct, setSelectedProduct] = useState('');
  const [searchTerm, setSearchTerm] = useState('');
  const [filteredProducts, setFilteredProducts] = useState([]);
  const [activeIndex, setActiveIndex] = useState(-1);
  const [showDropdown, setShowDropdown] = useState(false);
  const [modalIsOpen, setModalIsOpen] = useState(false);

  const openModal = () => {
    setModalIsOpen(true);
  };

  const closeModal = () => {
    setModalIsOpen(false);
  };

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

  useEffect(() => {
    fetchOrders();
  }, []);

  useEffect(() => {
    fetchOrders();
  }, [userId]);

  const addOrdine = async (idUtente, idProdotto, quantitàProdotto) => {
    closeModal();
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
        result(true);
      } else {
        setSearchTerm('');
        console.error('Errore durante l\'aggiunta dell\'ordine:', response.status);
      }
    } catch (error) {
      console.error('Errore:', error);
    }
  };

  const getProducts = async () => {
    const API_URL = `https://localhost:7031/products?userId=${userId}`;
    try {
      const response = await fetch(API_URL);
      if (response.ok) {
        const data = await response.json();
        setQuantità(0);
        setSearchTerm('');
        setProdotti(data);
        setFilteredProducts(data);
        openModal();
      } else {
        console.error('Error fetching products:', response.status);
      }
    } catch (error) {
      console.error('Error:', error);
    }
  };

  const handleSearchChange = (event) => {
    setSearchTerm(event.target.value);
    const filtered = prodotti.filter((product) =>
      product.nome.toLowerCase().includes(event.target.value.toLowerCase())
    );
    setFilteredProducts(filtered);
    setShowDropdown(true); // Show the dropdown when search term changes
    setActiveIndex(-1); // Reset active index
  };

  const handleSearchFocus = () => {
    if (searchTerm === '') {
      setFilteredProducts(prodotti); // Show all products if searchTerm is empty
      setShowDropdown(true); // Show the dropdown when search box is focused and empty
    }
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
const result = (flag) => {
  onEndCreate(flag);
  return flag;
};

  return (
    <>
      {/* Modal Create */}
      <Modal
        isOpen={modalIsOpen}
        ariaHideApp={false}
        onRequestClose={closeModal}
        className="modal"
        overlayClassName="overlay"
      >
        <div className="popup-content">
          <img src={done} alt="Done" className='modal_img'/>
          <h2 className='title_create'>Inserisci un nuovo ordine</h2>
          <div>
            <div className="search-bar-dropdown">
            <label className='label_create'>Seleziona un prodotto: </label>
              <input
                type="text"
                value={searchTerm}
                onChange={handleSearchChange}
                onFocus={handleSearchFocus}
                onKeyDown={handleKeyDown}
                placeholder="Search..."
              />
              {showDropdown && filteredProducts.length > 0 && (
                <ul className="dropdown-list">
                  {filteredProducts.map((product, id) => (
                    <li
                      key={id}
                      className={id === activeIndex ? 'active' : ''}
                      onClick={() => handleClick(product)}
                    >
                      {product.nome}
                    </li>
                  ))}
                </ul>
              )}
            </div>
            <div>
            <label className='label_create'>Seleziona una quantità: </label>
            <div className='input_container_create'>
            <input
              type="number"
              min="0"
              placeholder={"Quantità:"} 
              value={quantità}
              onChange={handleQuantityChange}
              className='number-input'
            />
            </div>
          </div>
          <div className='button_beetween'>
          <button
            onClick={() => addOrdine(userId, selectedProduct, quantità)}
            className="close-button mb-20"
            disabled={!selectedProduct || !quantità}
          >
            Submit
          </button>
          <button onClick={closeModal} className="close-button">
            Close
          </button>
          </div>
          </div>
        </div>
      </Modal>
      <div className="add_container_ordine">
        <FontAwesomeIcon
          icon={faPlus}
          className="plus-icon"
          onClick={() => getProducts(userId)}
        />
      </div>
    </>
  );
};

export default Ordine;
