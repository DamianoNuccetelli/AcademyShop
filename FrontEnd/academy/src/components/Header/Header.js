import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus, faChevronLeft, faChevronRight } from '@fortawesome/free-solid-svg-icons';
import Modal from 'react-modal';

import ProductCard from '../ProductCard/ProductCard';
import './Header.css';
import banner from '../../img/banner.png';

const Header = () => {
    const [productsData, setProductsData] = useState([]);
    const userId = localStorage.getItem('userId');

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
                            <h2>Aggiungi Prodotto</h2>
                            <form onSubmit={handleSubmit}>
                                <div>
                                    <input
                                        type="text"
                                        name="nomeProdotto"
                                        placeholder="Nome Prodotto"
                                        required
                                    />
                                </div>
                                <div>
                                    <textarea
                                        name="descrizioneProdotto"
                                        placeholder="Descrizione Prodotto"
                                        required
                                    />
                                </div>
                                <div>
                                    <input
                                        type="number"
                                        name="quantitaProdotto"
                                        placeholder="Quantità Prodotto"
                                        required
                                    />
                                </div>
                                <button type="submit">Aggiungi</button>
                            </form>
                            <button onClick={closeModalAddProduct} className="close-button">Chiudi</button>
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
