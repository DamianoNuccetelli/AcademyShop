import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Navbar from '../Navbar/Navbar';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import ContentOrdine from './OrdineComponent/ContentOrdine';
//import 'bootstrap/dist/css/bootstrap.css';
import './Ordine.css';

const Ordine = () => {
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


