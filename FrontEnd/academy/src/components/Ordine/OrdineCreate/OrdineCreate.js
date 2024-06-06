import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Navbar from '../Navbar/Navbar';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import ContentOrdine from './OrdineComponent/ContentOrdine';
//import 'bootstrap/dist/css/bootstrap.css';
import './Ordine.css';
import './OrdineCreate.css'
const Ordine = () => {

  const userId = localStorage.getItem('userId');
  const [orders, setOrders] = useState([]);
  const [prodotti, setProdotti] = useState([]);
  const [quantità, setQuantità] = useState(0);
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
        console.log("Ordini: ", data);
                   
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
        //check this function
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

  const getProducts = async () => {
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

//----- check this functions
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
        <button onClick={closeModal} className="close-button">
          Close
        </button>
        <button
          onClick={() => addOrdine(userId, selectedProduct, quantità)}
          className="close-button"
          disabled={!selectedProduct || !quantità}
        >
          Submit
        </button>
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


