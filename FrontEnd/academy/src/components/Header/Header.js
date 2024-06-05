import React, { useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus, faChevronLeft, faChevronRight } from '@fortawesome/free-solid-svg-icons';
import Modal from 'react-modal';

import ProductCard from '../ProductCard/ProductCard';
import './Header.css';

import banner from '../../img/banner.png';

const Header = () => {
    const [modalIsOpenAddProduct, setModalIsOpenAddProduct] = useState(false);
    const [currentPage, setCurrentPage] = useState(1);
    const productsPerPage = 6;

    const productsData = [
        { id: 1, name: 'Product 1', price: 10 },
        { id: 2, name: 'Product 2', price: 20 },
        { id: 3, name: 'Product 3', price: 30 },
        { id: 4, name: 'Product 4', price: 40 },
        { id: 5, name: 'Product 5', price: 50 },
        { id: 6, name: 'Product 6', price: 60 },
        { id: 7, name: 'Product 7', price: 70 },
        { id: 8, name: 'Product 8', price: 80 },
        { id: 9, name: 'Product 9', price: 90 },
        { id: 10, name: 'Product 10', price: 100 },
        { id: 11, name: 'Product 11', price: 110 },
        { id: 12, name: 'Product 12', price: 120 },
        { id: 13, name: 'Product 13', price: 130 },
        { id: 14, name: 'Product 14', price: 140 },
        { id: 15, name: 'Product 15', price: 150 },
        { id: 16, name: 'Product 16', price: 160 },
        { id: 17, name: 'Product 17', price: 170 },
        { id: 18, name: 'Product 18', price: 180 },
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
                    <h2>Benvenuto, Mario</h2>
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
                            price={product.price}
                        />
                    ))}
                </div>
            </div>
        </div>
    );
};

export default Header;
