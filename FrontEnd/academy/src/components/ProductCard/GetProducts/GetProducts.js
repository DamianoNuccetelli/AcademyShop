import React, { useEffect, useState } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faChevronLeft, faChevronRight } from '@fortawesome/free-solid-svg-icons';
import ProductCard from '../ProductCard'
import './GetProducts.css';

const GetProducts = ({ productsData, setProductsData }) => {
    const [currentPage, setCurrentPage] = useState(1);
    const productsPerPage = 6;

    useEffect(() => {
        fetchProducts();
    }, []);

    const fetchProducts = async () => {
        try {
            const response = await fetch('https://localhost:7031/products');
            if (response.ok) {
                const data = await response.json();
                setProductsData(data);
            } else {
                throw new Error('Errore nel recupero dei dati dei prodotti');
            }
        } catch (error) {
            console.error(error);
        }
    };

    const handleNextPage = () => {
        const totalPages = Math.ceil(productsData.length / productsPerPage);
        if (currentPage < totalPages) {
            setCurrentPage(currentPage + 1);
        }
    };

    const handlePrevPage = () => {
        if (currentPage > 1) {
            setCurrentPage(currentPage - 1);
        }
    };

    const indexOfLastProduct = currentPage * productsPerPage;
    const indexOfFirstProduct = indexOfLastProduct - productsPerPage;
    const currentProducts = productsData.slice(indexOfFirstProduct, indexOfLastProduct);
    const totalPages = Math.ceil(productsData.length / productsPerPage);

    return (
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
            <div className='products_card'>
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
    );
};

export default GetProducts;
