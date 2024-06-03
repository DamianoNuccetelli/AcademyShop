import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';

import ProductCard from '../ProductCard/ProductCard';
import './Header.css';


const Header = () => {

    const productsData = [
        { id: 1, name: 'Product 1', price: 10 },
        { id: 2, name: 'Product 2', price: 20 },
        { id: 3, name: 'Product 3', price: 30 },
    ];
    
    return (
        <div className="header">
            <div className='title_container'>
                <h1>Dashboard</h1>
                <h2>Benvenuto, Mario</h2>
            </div>
            <div className='welcome_container'>
                <div className='add_container'>
                    <h2>Add new product</h2>
                    <FontAwesomeIcon icon={faPlus} className="plus-icon" />
                </div>
                <div className='banner_container'>
                    <div className='boxTemporaneo'></div>
                </div>
            </div>
            <div className='products_container'>
                <h2>All product</h2>
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
        </div>
    );
};

export default Header;


