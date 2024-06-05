import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus, faChevronLeft, faChevronRight } from '@fortawesome/free-solid-svg-icons';
import Modal from 'react-modal';

import ProductCard from '../ProductCard/ProductCard';
import './Header.css';


import banner from '../../img/banner.png';

const userId = localStorage.getItem('userId');

const Header = () => {
    const [modalIsOpenAddProduct, setModalIsOpenAddProduct] = useState(false);
    const [productsData, setProductsData] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const productsPerPage = 6;

    useEffect(() => {
        fetch('https://localhost:7031/products')
            .then(response => {
                if (!response.ok) {
                    throw new Error('Errore nel recupero dei dati dei prodotti');
                }
                return response.json();
            })
            .then(data => {
                setProductsData(data);
            })
            .catch(error => {
                console.error(error);
            });
    }, []);

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
                            id={product.id}
                            nome={product.nome}
                            descrizione={product.descrizione}
                            quantità={product.quantità}
                            productsData={productsData}
                            setProductsData={setProductsData}
                        />
                    ))}
                </div>
            </div>
        </div>
    );
};

export default Header;
