import React, { useState } from 'react';
import GetProducts from '../ProductCard/GetProducts/GetProducts';
import AddProduct from '../ProductCard/AddProduct/AddProduct';
import banner from '../../img/banner.png';
import './Header.css';

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
                <AddProduct fetchProducts={() => setProductsData([])} />
                <div className='banner_container'>
                    <img src={banner} alt="Banner" />
                </div>
            </div>
            <GetProducts productsData={productsData} setProductsData={setProductsData} />
        </div>
    );
};

export default Header;
