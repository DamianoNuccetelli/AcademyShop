import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus, faChevronLeft, faChevronRight } from '@fortawesome/free-solid-svg-icons';
import Modal from 'react-modal';

import ProductCard from '../ProductCard/ProductCard';
import './Header.css';


import banner from '../../img/banner.png';

const userId = localStorage.getItem('userId');

const Header = () => {
    const [modalIsOpenAddProduct, setModalIsOpenAddProduct] = useState(false);
    const [currentPage, setCurrentPage] = useState(1);
    const productsPerPage = 6;

    const productsData = [
        { id: 1, name: 'Product 1', quantity: 10 },
        { id: 2, name: 'Product 2', quantity: 20 },
        { id: 3, name: 'Product 3', quantity: 30 },
        { id: 4, name: 'Product 4', quantity: 40 },
        { id: 5, name: 'Product 5', quantity: 50 },
        { id: 6, name: 'Product 6', quantity: 60 },
        { id: 7, name: 'Product 7', quantity: 70 },
        { id: 8, name: 'Product 8', quantity: 80 },
        { id: 9, name: 'Product 9', quantity: 90 },
        { id: 10, name: 'Product 10', quantity: 100 },
        { id: 11, name: 'Product 11', quantity: 110 },
        { id: 12, name: 'Product 12', quantity: 120 },
        { id: 13, name: 'Product 13', quantity: 130 },
        { id: 14, name: 'Product 14', quantity: 140 },
        { id: 15, name: 'Product 15', quantity: 150 },
        { id: 16, name: 'Product 16', quantity: 160 },
        { id: 17, name: 'Product 17', quantity: 170 },
        { id: 18, name: 'Product 18', quantity: 180 },
    ];

    const openModalAddProduct = () => {
        setModalIsOpenAddProduct(true);
    };

    const closeModalAddProduct = () => {
        setModalIsOpenAddProduct(false);
    };

    const indexOfLastProduct = currentPage * productsPerPage;
    const indexOfFirstProduct = indexOfLastProduct - productsPerPage;
    const currentProducts = productsData.slice(indexOfFirstProduct, indexOfLastProduct);

    const totalPages = Math.ceil(productsData.length / productsPerPage);

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
        <div className="header">
            <div className='title_container'>
                <div className='title_text'>
                    <h1>Dashboard</h1>
                    <h2>Benvenuto, Mario {userId}</h2>
                </div>
            </div>
            <div className='welcome_container'>
                <div className='add_container'>
                    <FontAwesomeIcon icon={faPlus} className="plus-icon" onClick={openModalAddProduct} />
                    <Modal
                        isOpen={modalIsOpenAddProduct}
                        onRequestClose={closeModalAddProduct}
                        className="modal"
                        overlayClassName="overlay"
                        ariaHideApp={false}
                    >
                        <div className="popup-content">
                            <h2>Popup per AGGIUNGERE un prodotto</h2>
                            <p>Qui va inserito il form</p>
                            <button onClick={closeModalAddProduct} className="close-button">Close</button>
                        </div>
                    </Modal>
                </div>
                <div className='banner_container'>
                    <img src={banner} alt="Banner" />
                </div>
            </div>
            <div className='products_container'>
                <div className='products_header'>
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
                <div className='products'>
                    {currentProducts.map(product => (
                        <ProductCard
                            key={product.id}
                            name={product.name}
                            quantity={product.quantity}
                        />
                    ))}
                </div>
            </div>
        </div>
    );
};

export default Header;
