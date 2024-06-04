import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';

import ProductCard from '../ProductCard/ProductCard';
import './Header.css';

import logo from '../../img/Proconsul-Services.png';
import banner from '../../img/banner.png';

const Header = () => {

    const productsData = [
        { id: 1, name: 'Product 1', price: 10 },
        { id: 2, name: 'Product 2', price: 20 },
        { id: 3, name: 'Product 3', price: 30 },
    ];
    
    return (
        <div className="header">
            <div className='title_container'>
                <div>
                    <h1>Dashboard</h1>
                    <h2>Benvenuto, Mario</h2>
                </div>
                <div>
                    {/* <img src={logo} alt="Logo" /> */}
                </div>
            </div>
            <div className='welcome_container'>
                <div className='add_container'>
                    {/* <h2>Nuovo prodotto</h2> */}
                    <FontAwesomeIcon icon={faPlus} className="plus-icon" />
                </div>
                <div className='banner_container'>
                    <img src={banner} alt="Logo" />
                </div>
            </div>
            <div className='products_container'>
                <div className='products_header'>
                    <h2>Tutti i prodotti</h2>
                    <div className='buttons'>
                        <button className='button'>Button 1</button>
                        <button className='button'>Button 2</button>
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
        </div>
    );
};

export default Header;
