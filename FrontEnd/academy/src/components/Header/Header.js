import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import Modal from 'react-modal';

import ProductCard from '../ProductCard/ProductCard';
import './Header.css';

import banner from '../../img/banner.png';

const Header = () => {
    
    const [modalIsOpen, setModalIsOpen] = useState(false);

    const openModal = () => {
        setModalIsOpen(true);
    };
        
    const closeModal = () => {
        setModalIsOpen(false);
    };
    
    const productsData = [
        { id: 1, name: 'Product 1', price: 10 },
        { id: 2, name: 'Product 2', price: 20 },
        { id: 3, name: 'Product 3', price: 30 },
    ];
    
    return (
        <div className="header">
            <div className='title_container'>
                <div className='title_text'>
                    <h1>Dashboard</h1>
                    <h2>Benvenuto, Mario</h2>
                </div>
            </div>
            <div className='welcome_container'>
                <div className='add_container'>
                    <FontAwesomeIcon icon={faPlus} className="plus-icon" onClick={openModal} />
                </div>
                <div className='banner_container'>
                    <img src={banner} alt="Banner" />
                </div>
            </div>
            <div className='products_container'>
                <div className='products_header'>
                    <h2>Tutti i prodotti</h2>
                    <div className='buttons'>
                        <button className='button'>1/5</button>
                        <button className='button'>Filtri</button>
                    </div>
                </div>
                <div className='products'>
                    {productsData.map(product => (
                        <ProductCard
                            key={product.id}
                            name={product.name}
                            price={product.price}
                        />
                    ))}
                </div>
                <div className='products'>
                    {productsData.map(product => (
                        <ProductCard
                            key={product.id}
                            name={product.name}
                            price={product.price}
                        />
                    ))}
                </div>
            </div>

            <Modal
                isOpen={modalIsOpen}
                onRequestClose={closeModal}
                className="modal"
                overlayClassName="overlay"
            >
                <div className="popup-content">
                    <h2>Welcome to our Popup</h2>
                    <p>This is a modern popup design example.</p>
                    <button onClick={closeModal} className="close-button">Close</button>
                </div>
            </Modal>
        </div>
    );
};

export default Header;
